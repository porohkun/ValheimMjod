// Decompiled with JetBrains decompiler
// Type: Heightmap
// Assembly: assembly_valheim, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F48D6A22-6962-45BF-8D82-0AAD6AFA4FDB
// Assembly location: D:\Games\Steam\steamapps\common\Valheim\valheim_Data\Managed\assembly_valheim.dll

using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Heightmap : MonoBehaviour
{
    private static float[] tempBiomeWeights = new float[513];
    private static List<Heightmap> tempHmaps = new List<Heightmap>();
    private static List<Heightmap> m_heightmaps = new List<Heightmap>();
    private static List<Vector3> m_tempVertises = new List<Vector3>();
    private static List<Vector2> m_tempUVs = new List<Vector2>();
    private static List<int> m_tempIndices = new List<int>();
    private static List<Color32> m_tempColors = new List<Color32>();
    public int m_width = 32;
    public float m_scale = 1f;
    private List<float> m_heights = new List<float>();
    private float[] m_oceanDepth = new float[4];
    private Heightmap.Biome[] m_cornerBiomes = new Heightmap.Biome[4]
    {
    Heightmap.Biome.Meadows,
    Heightmap.Biome.Meadows,
    Heightmap.Biome.Meadows,
    Heightmap.Biome.Meadows
    };
    public Material m_material;
    private const float m_levelMaxDelta = 8f;
    private const float m_smoothMaxDelta = 1f;
    public bool m_isDistantLod;
    public bool m_distantLodEditorHax;
    private HeightmapBuilder.HMBuildData m_buildData;
    private Texture2D m_clearedMask;
    private Material m_materialInstance;
    private MeshCollider m_collider;
    private Bounds m_bounds;
    private BoundingSphere m_boundingSphere;
    private Mesh m_collisionMesh;
    private Mesh m_renderMesh;
    private bool m_dirty;

    private void Awake()
    {
        if (!this.m_isDistantLod)
            Heightmap.m_heightmaps.Add(this);
        this.m_collider = this.GetComponent<MeshCollider>();
    }

    private void OnDestroy()
    {
        if (!this.m_isDistantLod)
            Heightmap.m_heightmaps.Remove(this);
        if (!(bool)(Object)this.m_materialInstance)
            return;
        Object.DestroyImmediate((Object)this.m_materialInstance);
    }

    private void OnEnable()
    {
        if (this.m_isDistantLod && Application.isPlaying && !this.m_distantLodEditorHax)
            return;
        this.Regenerate();
    }

    private void Update()
    {
        this.Render();
    }

    private void Render()
    {
        if (!this.IsVisible())
            return;
        if (this.m_dirty)
        {
            this.m_dirty = false;
            this.m_materialInstance.SetTexture("_ClearedMaskTex", (Texture)this.m_clearedMask);
            this.RebuildRenderMesh();
        }
        if (!(bool)(Object)this.m_renderMesh)
            return;
        Graphics.DrawMesh(this.m_renderMesh, Matrix4x4.TRS(this.transform.position, Quaternion.identity, Vector3.one), this.m_materialInstance, this.gameObject.layer);
    }

    private bool IsVisible()
    {
        return Utils.InsideMainCamera(this.m_boundingSphere) && Utils.InsideMainCamera(this.m_bounds);
    }

    public static void ForceGenerateAll()
    {
        foreach (Heightmap heightmap in Heightmap.m_heightmaps)
        {
            if (heightmap.HaveQueuedRebuild())
            {
                ZLog.Log((object)("Force generaeting hmap " + (object)heightmap.transform.position));
                heightmap.Regenerate();
            }
        }
    }

    public void Poke(bool delayed)
    {
        if (delayed)
        {
            if (this.HaveQueuedRebuild())
                this.CancelInvoke("Regenerate");
            this.InvokeRepeating("Regenerate", 0.1f, 0.0f);
        }
        else
            this.Regenerate();
    }

    public bool HaveQueuedRebuild()
    {
        return this.IsInvoking("Regenerate");
    }

    public void Regenerate()
    {
        if (this.HaveQueuedRebuild())
            this.CancelInvoke(nameof(Regenerate));
        this.Generate();
        this.RebuildCollisionMesh();
        this.UpdateCornerDepths();
        this.m_dirty = true;
    }

    private void UpdateCornerDepths()
    {
        float num = (bool)(Object)ZoneSystem.instance ? ZoneSystem.instance.m_waterLevel : 30f;
        this.m_oceanDepth[0] = this.GetHeight(0, this.m_width);
        this.m_oceanDepth[1] = this.GetHeight(this.m_width, this.m_width);
        this.m_oceanDepth[2] = this.GetHeight(this.m_width, 0);
        this.m_oceanDepth[3] = this.GetHeight(0, 0);
        this.m_oceanDepth[0] = Mathf.Max(0.0f, num - this.m_oceanDepth[0]);
        this.m_oceanDepth[1] = Mathf.Max(0.0f, num - this.m_oceanDepth[1]);
        this.m_oceanDepth[2] = Mathf.Max(0.0f, num - this.m_oceanDepth[2]);
        this.m_oceanDepth[3] = Mathf.Max(0.0f, num - this.m_oceanDepth[3]);
        this.m_materialInstance.SetFloatArray("_depth", this.m_oceanDepth);
    }

    public float[] GetOceanDepth()
    {
        return this.m_oceanDepth;
    }

    public static float GetOceanDepthAll(Vector3 worldPos)
    {
        Heightmap heightmap = Heightmap.FindHeightmap(worldPos);
        return (bool)(Object)heightmap ? heightmap.GetOceanDepth(worldPos) : 0.0f;
    }

    public float GetOceanDepth(Vector3 worldPos)
    {
        int x;
        int y;
        this.WorldToVertex(worldPos, out x, out y);
        float t1 = (float)x / (float)this.m_width;
        float t2 = (float)y / (float)this.m_width;
        return Mathf.Lerp(Mathf.Lerp(this.m_oceanDepth[3], this.m_oceanDepth[2], t1), Mathf.Lerp(this.m_oceanDepth[0], this.m_oceanDepth[1], t1), t2);
    }

    private void Initialize()
    {
        int num1 = this.m_width + 1;
        int num2 = num1 * num1;
        if (this.m_heights.Count == num2)
            return;
        this.m_heights.Clear();
        for (int index = 0; index < num2; ++index)
            this.m_heights.Add(0.0f);
        this.m_clearedMask = new Texture2D(this.m_width, this.m_width);
        this.m_clearedMask.wrapMode = TextureWrapMode.Clamp;
        this.m_materialInstance = new Material(this.m_material);
        this.m_materialInstance.SetTexture("_ClearedMaskTex", (Texture)this.m_clearedMask);
    }

    private void Generate()
    {
        this.Initialize();
        int num1 = this.m_width + 1;
        int num2 = num1 * num1;
        Vector3 position = this.transform.position;
        if (this.m_buildData == null || this.m_buildData.m_baseHeights.Count != num2 || (this.m_buildData.m_center != position || (double)this.m_buildData.m_scale != (double)this.m_scale) || this.m_buildData.m_worldGen != WorldGenerator.instance)
        {
            this.m_buildData = HeightmapBuilder.instance.RequestTerrainSync(position, this.m_width, this.m_scale, this.m_isDistantLod, WorldGenerator.instance);
            this.m_cornerBiomes = this.m_buildData.m_cornerBiomes;
        }
        for (int index = 0; index < num2; ++index)
            this.m_heights[index] = this.m_buildData.m_baseHeights[index];
        this.m_clearedMask.SetPixels(new Color[this.m_clearedMask.width * this.m_clearedMask.height]);
        this.ApplyModifiers();
    }

    private float Distance(float x, float y, float rx, float ry)
    {
        double num1 = (double)x - (double)rx;
        float num2 = y - ry;
        float num3 = 1.414f - Mathf.Sqrt((float)(num1 * num1 + (double)num2 * (double)num2));
        return num3 * num3 * num3;
    }

    public List<Heightmap.Biome> GetBiomes()
    {
        List<Heightmap.Biome> biomeList = new List<Heightmap.Biome>();
        foreach (Heightmap.Biome cornerBiome in this.m_cornerBiomes)
        {
            if (!biomeList.Contains(cornerBiome))
                biomeList.Add(cornerBiome);
        }
        return biomeList;
    }

    public bool HaveBiome(Heightmap.Biome biome)
    {
        return (this.m_cornerBiomes[0] & biome) != Heightmap.Biome.None || (this.m_cornerBiomes[1] & biome) != Heightmap.Biome.None || (this.m_cornerBiomes[2] & biome) != Heightmap.Biome.None || (uint)(this.m_cornerBiomes[3] & biome) > 0U;
    }

    public Heightmap.Biome GetBiome(Vector3 point)
    {
        if (this.m_isDistantLod)
            return WorldGenerator.instance.GetBiome(point.x, point.z);
        if (this.m_cornerBiomes[0] == this.m_cornerBiomes[1] && this.m_cornerBiomes[0] == this.m_cornerBiomes[2] && this.m_cornerBiomes[0] == this.m_cornerBiomes[3])
            return this.m_cornerBiomes[0];
        float x = point.x;
        float y = point.z;
        this.WorldToNormalizedHM(point, out x, out y);
        for (int index = 1; index < Heightmap.tempBiomeWeights.Length; ++index)
            Heightmap.tempBiomeWeights[index] = 0.0f;
        Heightmap.tempBiomeWeights[(int)this.m_cornerBiomes[0]] += this.Distance(x, y, 0.0f, 0.0f);
        Heightmap.tempBiomeWeights[(int)this.m_cornerBiomes[1]] += this.Distance(x, y, 1f, 0.0f);
        Heightmap.tempBiomeWeights[(int)this.m_cornerBiomes[2]] += this.Distance(x, y, 0.0f, 1f);
        Heightmap.tempBiomeWeights[(int)this.m_cornerBiomes[3]] += this.Distance(x, y, 1f, 1f);
        int num1 = 0;
        float num2 = -99999f;
        for (int index = 1; index < Heightmap.tempBiomeWeights.Length; ++index)
        {
            if ((double)Heightmap.tempBiomeWeights[index] > (double)num2)
            {
                num1 = index;
                num2 = Heightmap.tempBiomeWeights[index];
            }
        }
        return (Heightmap.Biome)num1;
    }

    public Heightmap.BiomeArea GetBiomeArea()
    {
        return this.IsBiomeEdge() ? Heightmap.BiomeArea.Edge : Heightmap.BiomeArea.Median;
    }

    public bool IsBiomeEdge()
    {
        return this.m_cornerBiomes[0] != this.m_cornerBiomes[1] || this.m_cornerBiomes[0] != this.m_cornerBiomes[2] || this.m_cornerBiomes[0] != this.m_cornerBiomes[3];
    }

    private void ApplyModifiers()
    {
        List<TerrainModifier> allInstances = TerrainModifier.GetAllInstances();
        float[] baseHeights = (float[])null;
        float[] levelOnly = (float[])null;
        foreach (TerrainModifier modifier in allInstances)
        {
            if (modifier.enabled && this.TerrainVSModifier(modifier))
            {
                if (modifier.m_playerModifiction && baseHeights == null)
                {
                    baseHeights = this.m_heights.ToArray();
                    levelOnly = this.m_heights.ToArray();
                }
                this.ApplyModifier(modifier, baseHeights, levelOnly);
            }
        }
        this.m_clearedMask.Apply();
    }

    private void ApplyModifier(TerrainModifier modifier, float[] baseHeights, float[] levelOnly)
    {
        if (modifier.m_level)
            this.LevelTerrain(modifier.transform.position + Vector3.up * modifier.m_levelOffset, modifier.m_levelRadius, modifier.m_square, baseHeights, levelOnly, modifier.m_playerModifiction);
        if (modifier.m_smooth)
            this.SmoothTerrain2(modifier.transform.position + Vector3.up * modifier.m_levelOffset, modifier.m_smoothRadius, modifier.m_square, levelOnly, modifier.m_smoothPower, modifier.m_playerModifiction);
        if (!modifier.m_paintCleared)
            return;
        this.PaintCleared(modifier.transform.position, modifier.m_paintRadius, modifier.m_paintType, modifier.m_paintHeightCheck, false);
    }

    public bool TerrainVSModifier(TerrainModifier modifier)
    {
        Vector3 position1 = modifier.transform.position;
        float num1 = modifier.GetRadius() + 4f;
        Vector3 position2 = this.transform.position;
        float num2 = (float)((double)this.m_width * (double)this.m_scale * 0.5);
        return (double)position1.x + (double)num1 >= (double)position2.x - (double)num2 && (double)position1.x - (double)num1 <= (double)position2.x + (double)num2 && ((double)position1.z + (double)num1 >= (double)position2.z - (double)num2 && (double)position1.z - (double)num1 <= (double)position2.z + (double)num2);
    }

    private Vector3 CalcNormal2(List<Vector3> vertises, int x, int y)
    {
        int num = this.m_width + 1;
        Vector3 vertise1 = vertises[y * num + x];
        Vector3 rhs;
        if (x == this.m_width)
        {
            Vector3 vertise2 = vertises[y * num + x - 1];
            rhs = vertise1 - vertise2;
        }
        else
            rhs = x != 0 ? vertises[y * num + x + 1] - vertises[y * num + x - 1] : vertises[y * num + x + 1] - vertise1;
        Vector3 lhs;
        if (y == this.m_width)
        {
            Vector3 vector3 = this.CalcVertex(x, y - 1);
            lhs = vertise1 - vector3;
        }
        else
            lhs = y != 0 ? vertises[(y + 1) * num + x] - vertises[(y - 1) * num + x] : this.CalcVertex(x, y + 1) - vertise1;
        Vector3 vector3_1 = Vector3.Cross(lhs, rhs);
        vector3_1.Normalize();
        return vector3_1;
    }

    private Vector3 CalcNormal(int x, int y)
    {
        Vector3 vector3_1 = this.CalcVertex(x, y);
        Vector3 rhs;
        if (x == this.m_width)
        {
            Vector3 vector3_2 = this.CalcVertex(x - 1, y);
            rhs = vector3_1 - vector3_2;
        }
        else
            rhs = this.CalcVertex(x + 1, y) - vector3_1;
        Vector3 lhs;
        if (y == this.m_width)
        {
            Vector3 vector3_2 = this.CalcVertex(x, y - 1);
            lhs = vector3_1 - vector3_2;
        }
        else
            lhs = this.CalcVertex(x, y + 1) - vector3_1;
        return Vector3.Cross(lhs, rhs).normalized;
    }

    private Vector3 CalcVertex(int x, int y)
    {
        int num = this.m_width + 1;
        Vector3 vector3_1 = new Vector3((float)((double)this.m_width * (double)this.m_scale * -0.5), 0.0f, (float)((double)this.m_width * (double)this.m_scale * -0.5));
        float height = this.m_heights[y * num + x];
        Vector3 vector3_2 = new Vector3((float)x * this.m_scale, height, (float)y * this.m_scale);
        return vector3_1 + vector3_2;
    }

    private Color GetBiomeColor(float ix, float iy)
    {
        if (this.m_cornerBiomes[0] == this.m_cornerBiomes[1] && this.m_cornerBiomes[0] == this.m_cornerBiomes[2] && this.m_cornerBiomes[0] == this.m_cornerBiomes[3])
            return (Color)Heightmap.GetBiomeColor(this.m_cornerBiomes[0]);
        Color32 biomeColor1 = Heightmap.GetBiomeColor(this.m_cornerBiomes[0]);
        Color32 biomeColor2 = Heightmap.GetBiomeColor(this.m_cornerBiomes[1]);
        Color32 biomeColor3 = Heightmap.GetBiomeColor(this.m_cornerBiomes[2]);
        Color32 biomeColor4 = Heightmap.GetBiomeColor(this.m_cornerBiomes[3]);
        Color32 b = biomeColor2;
        double num = (double)ix;
        return (Color)Color32.Lerp(Color32.Lerp(biomeColor1, b, (float)num), Color32.Lerp(biomeColor3, biomeColor4, ix), iy);
    }

    public static Color32 GetBiomeColor(Heightmap.Biome biome)
    {
        switch (biome)
        {
            case Heightmap.Biome.Swamp:
                return new Color32(byte.MaxValue, (byte)0, (byte)0, (byte)0);
            case Heightmap.Biome.Mountain:
                return new Color32((byte)0, byte.MaxValue, (byte)0, (byte)0);
            case Heightmap.Biome.BlackForest:
                return new Color32((byte)0, (byte)0, byte.MaxValue, (byte)0);
            case Heightmap.Biome.Plains:
                return new Color32((byte)0, (byte)0, (byte)0, byte.MaxValue);
            case Heightmap.Biome.AshLands:
                return new Color32(byte.MaxValue, (byte)0, (byte)0, byte.MaxValue);
            case Heightmap.Biome.DeepNorth:
                return new Color32((byte)0, byte.MaxValue, (byte)0, (byte)0);
            case Heightmap.Biome.Mistlands:
                return new Color32((byte)0, (byte)0, byte.MaxValue, byte.MaxValue);
            default:
                return new Color32((byte)0, (byte)0, (byte)0, (byte)0);
        }
    }

    private void RebuildCollisionMesh()
    {
        if ((Object)this.m_collisionMesh == (Object)null)
            this.m_collisionMesh = new Mesh();
        int num1 = this.m_width + 1;
        float y1 = -999999f;
        float y2 = 999999f;
        Heightmap.m_tempVertises.Clear();
        for (int y3 = 0; y3 < num1; ++y3)
        {
            for (int x = 0; x < num1; ++x)
            {
                Vector3 vector3 = this.CalcVertex(x, y3);
                Heightmap.m_tempVertises.Add(vector3);
                if ((double)vector3.y > (double)y1)
                    y1 = vector3.y;
                if ((double)vector3.y < (double)y2)
                    y2 = vector3.y;
            }
        }
        this.m_collisionMesh.SetVertices(Heightmap.m_tempVertises);
        if ((long)this.m_collisionMesh.GetIndexCount(0) != (long)((num1 - 1) * (num1 - 1) * 6))
        {
            Heightmap.m_tempIndices.Clear();
            for (int index1 = 0; index1 < num1 - 1; ++index1)
            {
                for (int index2 = 0; index2 < num1 - 1; ++index2)
                {
                    int num2 = index1 * num1 + index2;
                    int num3 = index1 * num1 + index2 + 1;
                    int num4 = (index1 + 1) * num1 + index2 + 1;
                    int num5 = (index1 + 1) * num1 + index2;
                    Heightmap.m_tempIndices.Add(num2);
                    Heightmap.m_tempIndices.Add(num5);
                    Heightmap.m_tempIndices.Add(num3);
                    Heightmap.m_tempIndices.Add(num3);
                    Heightmap.m_tempIndices.Add(num5);
                    Heightmap.m_tempIndices.Add(num4);
                }
            }
            this.m_collisionMesh.SetIndices(Heightmap.m_tempIndices.ToArray(), MeshTopology.Triangles, 0);
        }
        if ((bool)(Object)this.m_collider)
            this.m_collider.set_sharedMesh(this.m_collisionMesh);
        float num6 = (float)((double)this.m_width * (double)this.m_scale * 0.5);
        this.m_bounds.SetMinMax(this.transform.position + new Vector3(-num6, y2, -num6), this.transform.position + new Vector3(num6, y1, num6));
        this.m_boundingSphere.position = this.m_bounds.center;
        this.m_boundingSphere.radius = Vector3.Distance(this.m_boundingSphere.position, this.m_bounds.max);
    }

    private void RebuildRenderMesh()
    {
        if ((Object)this.m_renderMesh == (Object)null)
            this.m_renderMesh = new Mesh();
        WorldGenerator instance = WorldGenerator.instance;
        int num = this.m_width + 1;
        Vector3 vector3 = this.transform.position + new Vector3((float)((double)this.m_width * (double)this.m_scale * -0.5), 0.0f, (float)((double)this.m_width * (double)this.m_scale * -0.5));
        Heightmap.m_tempVertises.Clear();
        Heightmap.m_tempUVs.Clear();
        Heightmap.m_tempIndices.Clear();
        Heightmap.m_tempColors.Clear();
        for (int index1 = 0; index1 < num; ++index1)
        {
            float iy = Mathf.SmoothStep(0.0f, 1f, (float)index1 / (float)this.m_width);
            for (int index2 = 0; index2 < num; ++index2)
            {
                float ix = Mathf.SmoothStep(0.0f, 1f, (float)index2 / (float)this.m_width);
                Heightmap.m_tempUVs.Add(new Vector2((float)index2 / (float)this.m_width, (float)index1 / (float)this.m_width));
                if (this.m_isDistantLod)
                {
                    float wx = vector3.x + (float)index2 * this.m_scale;
                    float wy = vector3.z + (float)index1 * this.m_scale;
                    Heightmap.Biome biome = instance.GetBiome(wx, wy);
                    Heightmap.m_tempColors.Add(Heightmap.GetBiomeColor(biome));
                }
                else
                    Heightmap.m_tempColors.Add((Color32)this.GetBiomeColor(ix, iy));
            }
        }
        this.m_collisionMesh.GetVertices(Heightmap.m_tempVertises);
        this.m_collisionMesh.GetIndices(Heightmap.m_tempIndices, 0);
        this.m_renderMesh.Clear();
        this.m_renderMesh.SetVertices(Heightmap.m_tempVertises);
        this.m_renderMesh.SetColors(Heightmap.m_tempColors);
        this.m_renderMesh.SetUVs(0, Heightmap.m_tempUVs);
        this.m_renderMesh.SetIndices(Heightmap.m_tempIndices.ToArray(), MeshTopology.Triangles, 0, true);
        this.m_renderMesh.RecalculateNormals();
        this.m_renderMesh.RecalculateTangents();
    }

    private void SmoothTerrain2(
      Vector3 worldPos,
      float radius,
      bool square,
      float[] levelOnlyHeights,
      float power,
      bool playerModifiction)
    {
        int x1;
        int y1;
        this.WorldToVertex(worldPos, out x1, out y1);
        float num1 = worldPos.y - this.transform.position.y;
        float f1 = radius / this.m_scale;
        int num2 = Mathf.CeilToInt(f1);
        Vector2 a = new Vector2((float)x1, (float)y1);
        int num3 = this.m_width + 1;
        for (int y2 = y1 - num2; y2 <= y1 + num2; ++y2)
        {
            for (int x2 = x1 - num2; x2 <= x1 + num2; ++x2)
            {
                float num4 = Vector2.Distance(a, new Vector2((float)x2, (float)y2));
                if ((double)num4 <= (double)f1)
                {
                    float f2 = num4 / f1;
                    if (x2 >= 0 && y2 >= 0 && (x2 < num3 && y2 < num3))
                    {
                        float num5 = (double)power != 3.0 ? Mathf.Pow(f2, power) : f2 * f2 * f2;
                        double height = (double)this.GetHeight(x2, y2);
                        float num6 = 1f - num5;
                        double num7 = (double)num1;
                        double num8 = (double)num6;
                        float h = Mathf.Lerp((float)height, (float)num7, (float)num8);
                        if (playerModifiction)
                        {
                            float levelOnlyHeight = levelOnlyHeights[y2 * num3 + x2];
                            h = Mathf.Clamp(h, levelOnlyHeight - 1f, levelOnlyHeight + 1f);
                        }
                        this.SetHeight(x2, y2, h);
                    }
                }
            }
        }
    }

    private bool AtMaxWorldLevelDepth(Vector3 worldPos)
    {
        float height1;
        this.GetWorldHeight(worldPos, out height1);
        float height2;
        this.GetWorldBaseHeight(worldPos, out height2);
        return (double)Mathf.Max((float)-((double)height1 - (double)height2), 0.0f) >= 7.94999980926514;
    }

    private bool GetWorldBaseHeight(Vector3 worldPos, out float height)
    {
        int x;
        int y;
        this.WorldToVertex(worldPos, out x, out y);
        int num = this.m_width + 1;
        if (x < 0 || y < 0 || (x >= num || y >= num))
        {
            height = 0.0f;
            return false;
        }
        height = this.m_buildData.m_baseHeights[y * num + x] + this.transform.position.y;
        return true;
    }

    private bool GetWorldHeight(Vector3 worldPos, out float height)
    {
        int x;
        int y;
        this.WorldToVertex(worldPos, out x, out y);
        int num = this.m_width + 1;
        if (x < 0 || y < 0 || (x >= num || y >= num))
        {
            height = 0.0f;
            return false;
        }
        height = this.m_heights[y * num + x] + this.transform.position.y;
        return true;
    }

    private bool GetAverageWorldHeight(Vector3 worldPos, float radius, out float height)
    {
        int x1;
        int y1;
        this.WorldToVertex(worldPos, out x1, out y1);
        float f = radius / this.m_scale;
        int num1 = Mathf.CeilToInt(f);
        Vector2 a = new Vector2((float)x1, (float)y1);
        int num2 = this.m_width + 1;
        float num3 = 0.0f;
        int num4 = 0;
        for (int y2 = y1 - num1; y2 <= y1 + num1; ++y2)
        {
            for (int x2 = x1 - num1; x2 <= x1 + num1; ++x2)
            {
                if ((double)Vector2.Distance(a, new Vector2((float)x2, (float)y2)) <= (double)f && x2 >= 0 && (y2 >= 0 && x2 < num2) && y2 < num2)
                {
                    num3 += this.GetHeight(x2, y2);
                    ++num4;
                }
            }
        }
        if (num4 == 0)
        {
            height = 0.0f;
            return false;
        }
        height = num3 / (float)num4 + this.transform.position.y;
        return true;
    }

    private bool GetMinWorldHeight(Vector3 worldPos, float radius, out float height)
    {
        int x1;
        int y1;
        this.WorldToVertex(worldPos, out x1, out y1);
        float f = radius / this.m_scale;
        int num1 = Mathf.CeilToInt(f);
        Vector2 a = new Vector2((float)x1, (float)y1);
        int num2 = this.m_width + 1;
        height = 99999f;
        for (int y2 = y1 - num1; y2 <= y1 + num1; ++y2)
        {
            for (int x2 = x1 - num1; x2 <= x1 + num1; ++x2)
            {
                if ((double)Vector2.Distance(a, new Vector2((float)x2, (float)y2)) <= (double)f && x2 >= 0 && (y2 >= 0 && x2 < num2) && y2 < num2)
                {
                    float height1 = this.GetHeight(x2, y2);
                    if ((double)height1 < (double)height)
                        height = height1;
                }
            }
        }
        return (double)height != 99999.0;
    }

    private bool GetMaxWorldHeight(Vector3 worldPos, float radius, out float height)
    {
        int x1;
        int y1;
        this.WorldToVertex(worldPos, out x1, out y1);
        float f = radius / this.m_scale;
        int num1 = Mathf.CeilToInt(f);
        Vector2 a = new Vector2((float)x1, (float)y1);
        int num2 = this.m_width + 1;
        height = -99999f;
        for (int y2 = y1 - num1; y2 <= y1 + num1; ++y2)
        {
            for (int x2 = x1 - num1; x2 <= x1 + num1; ++x2)
            {
                if ((double)Vector2.Distance(a, new Vector2((float)x2, (float)y2)) <= (double)f && x2 >= 0 && (y2 >= 0 && x2 < num2) && y2 < num2)
                {
                    float height1 = this.GetHeight(x2, y2);
                    if ((double)height1 > (double)height)
                        height = height1;
                }
            }
        }
        return (double)height != -99999.0;
    }

    public static bool AtMaxLevelDepth(Vector3 worldPos)
    {
        Heightmap heightmap = Heightmap.FindHeightmap(worldPos);
        return (bool)(Object)heightmap && heightmap.AtMaxWorldLevelDepth(worldPos);
    }

    public static bool GetHeight(Vector3 worldPos, out float height)
    {
        Heightmap heightmap = Heightmap.FindHeightmap(worldPos);
        if ((bool)(Object)heightmap && heightmap.GetWorldHeight(worldPos, out height))
            return true;
        height = 0.0f;
        return false;
    }

    public static bool GetAverageHeight(Vector3 worldPos, float radius, out float height)
    {
        List<Heightmap> heightmaps = new List<Heightmap>();
        Heightmap.FindHeightmap(worldPos, radius, heightmaps);
        float num1 = 0.0f;
        int num2 = 0;
        foreach (Heightmap heightmap in heightmaps)
        {
            float height1;
            if (heightmap.GetAverageWorldHeight(worldPos, radius, out height1))
            {
                num1 += height1;
                ++num2;
            }
        }
        if (num2 > 0)
        {
            height = num1 / (float)num2;
            return true;
        }
        height = 0.0f;
        return false;
    }

    private void SmoothTerrain(Vector3 worldPos, float radius, bool square, float intensity)
    {
        int x;
        int y;
        this.WorldToVertex(worldPos, out x, out y);
        float f = radius / this.m_scale;
        int num = Mathf.CeilToInt(f);
        Vector2 a = new Vector2((float)x, (float)y);
        List<KeyValuePair<Vector2i, float>> keyValuePairList = new List<KeyValuePair<Vector2i, float>>();
        for (int index1 = y - num; index1 <= y + num; ++index1)
        {
            for (int index2 = x - num; index2 <= x + num; ++index2)
            {
                if ((square || (double)Vector2.Distance(a, new Vector2((float)index2, (float)index1)) <= (double)f) && (index2 != 0 && index1 != 0) && (index2 != this.m_width && index1 != this.m_width))
                    keyValuePairList.Add(new KeyValuePair<Vector2i, float>(new Vector2i(index2, index1), this.GetAvgHeight(index2, index1, 1)));
            }
        }
        foreach (KeyValuePair<Vector2i, float> keyValuePair in keyValuePairList)
        {
            float h = Mathf.Lerp(this.GetHeight(keyValuePair.Key.x, keyValuePair.Key.y), keyValuePair.Value, intensity);
            this.SetHeight(keyValuePair.Key.x, keyValuePair.Key.y, h);
        }
    }

    private float GetAvgHeight(int cx, int cy, int w)
    {
        int num1 = this.m_width + 1;
        float num2 = 0.0f;
        int num3 = 0;
        for (int y = cy - w; y <= cy + w; ++y)
        {
            for (int x = cx - w; x <= cx + w; ++x)
            {
                if (x >= 0 && y >= 0 && (x < num1 && y < num1))
                {
                    num2 += this.GetHeight(x, y);
                    ++num3;
                }
            }
        }
        return num3 == 0 ? 0.0f : num2 / (float)num3;
    }

    private float GroundHeight(Vector3 point)
    {
        RaycastHit raycastHit;
        return ((Collider)this.m_collider).Raycast(new Ray(point + Vector3.up * 100f, Vector3.down), ref raycastHit, 300f) ? ((RaycastHit)ref raycastHit).get_point().y : -10000f;
    }

    private void FindObjectsToMove(Vector3 worldPos, float area, List<Rigidbody> objects)
    {
        if ((Object)this.m_collider == (Object)null)
            return;
        foreach (Collider collider in Physics.OverlapBox(worldPos, new Vector3(area / 2f, 500f, area / 2f)))
        {
            if (!((Object)collider == (Object)this.m_collider) && (bool)(Object)collider.get_attachedRigidbody())
            {
                Rigidbody attachedRigidbody = collider.get_attachedRigidbody();
                ZNetView component = ((Component)attachedRigidbody).GetComponent<ZNetView>();
                if (!(bool)(Object)component || component.IsOwner())
                    objects.Add(attachedRigidbody);
            }
        }
    }

    private void PaintCleared(
      Vector3 worldPos,
      float radius,
      TerrainModifier.PaintType paintType,
      bool heightCheck,
      bool apply)
    {
        worldPos.x -= 0.5f;
        worldPos.z -= 0.5f;
        float num1 = worldPos.y - this.transform.position.y;
        int x1;
        int y1;
        this.WorldToVertex(worldPos, out x1, out y1);
        float f = radius / this.m_scale;
        int num2 = Mathf.CeilToInt(f);
        Vector2 a = new Vector2((float)x1, (float)y1);
        for (int y2 = y1 - num2; y2 <= y1 + num2; ++y2)
        {
            for (int x2 = x1 - num2; x2 <= x1 + num2; ++x2)
            {
                float num3 = Vector2.Distance(a, new Vector2((float)x2, (float)y2));
                if (x2 >= 0 && y2 >= 0 && (x2 < this.m_clearedMask.width && y2 < this.m_clearedMask.height) && (!heightCheck || (double)this.GetHeight(x2, y2) <= (double)num1))
                {
                    float t = Mathf.Pow(1f - Mathf.Clamp01(num3 / f), 0.1f);
                    Color color = this.m_clearedMask.GetPixel(x2, y2);
                    switch (paintType)
                    {
                        case TerrainModifier.PaintType.Dirt:
                            color = Color.Lerp(color, Color.red, t);
                            break;
                        case TerrainModifier.PaintType.Cultivate:
                            color = Color.Lerp(color, Color.green, t);
                            break;
                        case TerrainModifier.PaintType.Paved:
                            color = Color.Lerp(color, Color.blue, t);
                            break;
                        case TerrainModifier.PaintType.Reset:
                            color = Color.Lerp(color, Color.black, t);
                            break;
                    }
                    this.m_clearedMask.SetPixel(x2, y2, color);
                }
            }
        }
        if (!apply)
            return;
        this.m_clearedMask.Apply();
    }

    public bool IsCleared(Vector3 worldPos)
    {
        worldPos.x -= 0.5f;
        worldPos.z -= 0.5f;
        int x;
        int y;
        this.WorldToVertex(worldPos, out x, out y);
        Color pixel = this.m_clearedMask.GetPixel(x, y);
        return (double)pixel.r > 0.5 || (double)pixel.g > 0.5 || (double)pixel.b > 0.5;
    }

    public bool IsCultivated(Vector3 worldPos)
    {
        int x;
        int y;
        this.WorldToVertex(worldPos, out x, out y);
        return (double)this.m_clearedMask.GetPixel(x, y).g > 0.5;
    }

    private void WorldToVertex(Vector3 worldPos, out int x, out int y)
    {
        Vector3 vector3 = worldPos - this.transform.position;
        x = Mathf.FloorToInt((float)((double)vector3.x / (double)this.m_scale + 0.5)) + this.m_width / 2;
        y = Mathf.FloorToInt((float)((double)vector3.z / (double)this.m_scale + 0.5)) + this.m_width / 2;
    }

    private void WorldToNormalizedHM(Vector3 worldPos, out float x, out float y)
    {
        float num = (float)this.m_width * this.m_scale;
        Vector3 vector3 = worldPos - this.transform.position;
        x = (float)((double)vector3.x / (double)num + 0.5);
        y = (float)((double)vector3.z / (double)num + 0.5);
    }

    private void LevelTerrain(
      Vector3 worldPos,
      float radius,
      bool square,
      float[] baseHeights,
      float[] levelOnly,
      bool playerModifiction)
    {
        int x1;
        int y1;
        this.WorldToVertex(worldPos, out x1, out y1);
        Vector3 vector3 = worldPos - this.transform.position;
        float f = radius / this.m_scale;
        int num1 = Mathf.CeilToInt(f);
        int num2 = this.m_width + 1;
        Vector2 a = new Vector2((float)x1, (float)y1);
        for (int y2 = y1 - num1; y2 <= y1 + num1; ++y2)
        {
            for (int x2 = x1 - num1; x2 <= x1 + num1; ++x2)
            {
                if ((square || (double)Vector2.Distance(a, new Vector2((float)x2, (float)y2)) <= (double)f) && (x2 >= 0 && y2 >= 0) && (x2 < num2 && y2 < num2))
                {
                    float h = vector3.y;
                    if (playerModifiction)
                    {
                        float baseHeight = baseHeights[y2 * num2 + x2];
                        h = Mathf.Clamp(h, baseHeight - 8f, baseHeight + 8f);
                        levelOnly[y2 * num2 + x2] = h;
                    }
                    this.SetHeight(x2, y2, h);
                }
            }
        }
    }

    private float GetHeight(int x, int y)
    {
        int num = this.m_width + 1;
        return x < 0 || y < 0 || (x >= num || y >= num) ? 0.0f : this.m_heights[y * num + x];
    }

    private float GetBaseHeight(int x, int y)
    {
        int num = this.m_width + 1;
        return x < 0 || y < 0 || (x >= num || y >= num) ? 0.0f : this.m_buildData.m_baseHeights[y * num + x];
    }

    private void SetHeight(int x, int y, float h)
    {
        int num = this.m_width + 1;
        if (x < 0 || y < 0 || (x >= num || y >= num))
            return;
        this.m_heights[y * num + x] = h;
    }

    public bool IsPointInside(Vector3 point, float radius = 0.0f)
    {
        float num = (float)((double)this.m_width * (double)this.m_scale * 0.5);
        Vector3 position = this.transform.position;
        return (double)point.x + (double)radius >= (double)position.x - (double)num && (double)point.x - (double)radius <= (double)position.x + (double)num && ((double)point.z + (double)radius >= (double)position.z - (double)num && (double)point.z - (double)radius <= (double)position.z + (double)num);
    }

    public static List<Heightmap> GetAllHeightmaps()
    {
        return Heightmap.m_heightmaps;
    }

    public static Heightmap FindHeightmap(Vector3 point)
    {
        foreach (Heightmap heightmap in Heightmap.m_heightmaps)
        {
            if (heightmap.IsPointInside(point, 0.0f))
                return heightmap;
        }
        return (Heightmap)null;
    }

    public static void FindHeightmap(Vector3 point, float radius, List<Heightmap> heightmaps)
    {
        foreach (Heightmap heightmap in Heightmap.m_heightmaps)
        {
            if (heightmap.IsPointInside(point, radius))
                heightmaps.Add(heightmap);
        }
    }

    public static Heightmap.Biome FindBiome(Vector3 point)
    {
        Heightmap heightmap = Heightmap.FindHeightmap(point);
        return (bool)(Object)heightmap ? heightmap.GetBiome(point) : Heightmap.Biome.None;
    }

    public static bool HaveQueuedRebuild(Vector3 point, float radius)
    {
        Heightmap.tempHmaps.Clear();
        Heightmap.FindHeightmap(point, radius, Heightmap.tempHmaps);
        foreach (Heightmap tempHmap in Heightmap.tempHmaps)
        {
            if (tempHmap.HaveQueuedRebuild())
                return true;
        }
        return false;
    }

    public static Heightmap.Biome FindBiomeClutter(Vector3 point)
    {
        if ((bool)(Object)ZoneSystem.instance && !ZoneSystem.instance.IsZoneLoaded(point))
            return Heightmap.Biome.None;
        Heightmap heightmap = Heightmap.FindHeightmap(point);
        return (bool)(Object)heightmap ? heightmap.GetBiome(point) : Heightmap.Biome.None;
    }

    public void Clear()
    {
        this.m_heights.Clear();
        this.m_clearedMask = (Texture2D)null;
        this.m_materialInstance = (Material)null;
        this.m_buildData = (HeightmapBuilder.HMBuildData)null;
        if ((bool)(Object)this.m_collisionMesh)
            this.m_collisionMesh.Clear();
        if ((bool)(Object)this.m_renderMesh)
            this.m_renderMesh.Clear();
        if (!(bool)(Object)this.m_collider)
            return;
        this.m_collider.set_sharedMesh((Mesh)null);
    }

    public enum Biome
    {
        None = 0,
        Meadows = 1,
        Swamp = 2,
        Mountain = 4,
        BlackForest = 8,
        Plains = 16, // 0x00000010
        AshLands = 32, // 0x00000020
        DeepNorth = 64, // 0x00000040
        Ocean = 256, // 0x00000100
        Mistlands = 512, // 0x00000200
        BiomesMax = 513, // 0x00000201
    }

    public enum BiomeArea
    {
        Edge = 1,
        Median = 2,
        Everything = 3,
    }
}
