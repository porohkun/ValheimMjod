using System;

namespace Valheim
{
    public class ZDO /*: IEquatable<ZDO>*/
    {
        //public int m_tempRemoveEarmark = -1;
        //public int m_tempCreateEarmark = -1;
        //private Vector2i m_sector = Vector2i.zero;
        //private Vector3 m_position = Vector3.zero;
        //private Quaternion m_rotation = Quaternion.identity;
        //public ZDOID m_uid;
        //public bool m_persistent;
        //public bool m_distant;
        //public long m_owner;
        //public long m_timeCreated;
        //public uint m_ownerRevision;
        //public uint m_dataRevision;
        //public int m_pgwVersion;
        //public ZDO.ObjectType m_type;
        //public float m_tempSortValue;
        //public bool m_tempHaveRevision;
        //private int m_prefab;
        //private Dictionary<int, float> m_floats;
        //private Dictionary<int, Vector3> m_vec3;
        //private Dictionary<int, Quaternion> m_quats;
        //private Dictionary<int, int> m_ints;
        //private Dictionary<int, long> m_longs;
        //private Dictionary<int, string> m_strings;
        //private ZDOMan m_zdoMan;

        //public void Initialize(ZDOMan man, ZDOID id, Vector3 position)
        //{
        //    this.m_zdoMan = man;
        //    this.m_uid = id;
        //    this.m_position = position;
        //    this.m_sector = ZoneSystem.instance.GetZone(this.m_position);
        //    this.m_zdoMan.AddToSector(this, this.m_sector);
        //}

        //public void Initialize(ZDOMan man)
        //{
        //    this.m_zdoMan = man;
        //}

        //public bool IsValid()
        //{
        //    return this.m_zdoMan != null;
        //}

        //public void Reset()
        //{
        //    this.m_uid = ZDOID.None;
        //    this.m_persistent = false;
        //    this.m_owner = 0L;
        //    this.m_timeCreated = 0L;
        //    this.m_ownerRevision = 0U;
        //    this.m_dataRevision = 0U;
        //    this.m_pgwVersion = 0;
        //    this.m_distant = false;
        //    this.m_tempSortValue = 0.0f;
        //    this.m_tempHaveRevision = false;
        //    this.m_prefab = 0;
        //    this.m_sector = Vector2i.zero;
        //    this.m_position = Vector3.zero;
        //    this.m_rotation = Quaternion.identity;
        //    this.ReleaseFloats();
        //    this.ReleaseVec3();
        //    this.ReleaseQuats();
        //    this.ReleaseInts();
        //    this.ReleaseLongs();
        //    this.ReleaseStrings();
        //    this.m_zdoMan = (ZDOMan)null;
        //}

        //public ZDO Clone()
        //{
        //    ZDO zdo = this.MemberwiseClone() as ZDO;
        //    zdo.m_floats = (Dictionary<int, float>)null;
        //    zdo.m_vec3 = (Dictionary<int, Vector3>)null;
        //    zdo.m_quats = (Dictionary<int, Quaternion>)null;
        //    zdo.m_ints = (Dictionary<int, int>)null;
        //    zdo.m_longs = (Dictionary<int, long>)null;
        //    zdo.m_strings = (Dictionary<int, string>)null;
        //    if (this.m_floats != null && this.m_floats.Count > 0)
        //    {
        //        zdo.InitFloats();
        //        zdo.m_floats.Copy<int, float>(this.m_floats);
        //    }
        //    if (this.m_vec3 != null && this.m_vec3.Count > 0)
        //    {
        //        zdo.InitVec3();
        //        zdo.m_vec3.Copy<int, Vector3>(this.m_vec3);
        //    }
        //    if (this.m_quats != null && this.m_quats.Count > 0)
        //    {
        //        zdo.InitQuats();
        //        zdo.m_quats.Copy<int, Quaternion>(this.m_quats);
        //    }
        //    if (this.m_ints != null && this.m_ints.Count > 0)
        //    {
        //        zdo.InitInts();
        //        zdo.m_ints.Copy<int, int>(this.m_ints);
        //    }
        //    if (this.m_longs != null && this.m_longs.Count > 0)
        //    {
        //        zdo.InitLongs();
        //        zdo.m_longs.Copy<int, long>(this.m_longs);
        //    }
        //    if (this.m_strings != null && this.m_strings.Count > 0)
        //    {
        //        zdo.InitStrings();
        //        zdo.m_strings.Copy<int, string>(this.m_strings);
        //    }
        //    return zdo;
        //}

        //public bool Equals(ZDO other)
        //{
        //    return this == other;
        //}

        //public void Set(KeyValuePair<int, int> hashPair, ZDOID id)
        //{
        //    this.Set(hashPair.Key, id.userID);
        //    this.Set(hashPair.Value, (long)id.id);
        //}

        //public static KeyValuePair<int, int> GetHashZDOID(string name)
        //{
        //    return new KeyValuePair<int, int>((name + "_u").GetStableHashCode(), (name + "_i").GetStableHashCode());
        //}

        //public void Set(string name, ZDOID id)
        //{
        //    this.Set(ZDO.GetHashZDOID(name), id);
        //}

        //public ZDOID GetZDOID(KeyValuePair<int, int> hashPair)
        //{
        //    long userID = this.GetLong(hashPair.Key, 0L);
        //    uint id = (uint)this.GetLong(hashPair.Value, 0L);
        //    return userID == 0L || id == 0U ? ZDOID.None : new ZDOID(userID, id);
        //}

        //public ZDOID GetZDOID(string name)
        //{
        //    return this.GetZDOID(ZDO.GetHashZDOID(name));
        //}

        //public void Set(string name, float value)
        //{
        //    this.Set(name.GetStableHashCode(), value);
        //}

        //public void Set(int hash, float value)
        //{
        //    this.InitFloats();
        //    float num;
        //    if (this.m_floats.TryGetValue(hash, out num) && (double)num == (double)value)
        //        return;
        //    this.m_floats[hash] = value;
        //    this.IncreseDataRevision();
        //}

        //public void Set(string name, Vector3 value)
        //{
        //    this.Set(name.GetStableHashCode(), value);
        //}

        //public void Set(int hash, Vector3 value)
        //{
        //    this.InitVec3();
        //    Vector3 vector3;
        //    if (this.m_vec3.TryGetValue(hash, out vector3) && vector3 == value)
        //        return;
        //    this.m_vec3[hash] = value;
        //    this.IncreseDataRevision();
        //}

        //public void Set(string name, Quaternion value)
        //{
        //    this.Set(name.GetStableHashCode(), value);
        //}

        //public void Set(int hash, Quaternion value)
        //{
        //    this.InitQuats();
        //    Quaternion quaternion;
        //    if (this.m_quats.TryGetValue(hash, out quaternion) && quaternion == value)
        //        return;
        //    this.m_quats[hash] = value;
        //    this.IncreseDataRevision();
        //}

        //public void Set(string name, int value)
        //{
        //    this.Set(name.GetStableHashCode(), value);
        //}

        //public void Set(int hash, int value)
        //{
        //    this.InitInts();
        //    int num;
        //    if (this.m_ints.TryGetValue(hash, out num) && num == value)
        //        return;
        //    this.m_ints[hash] = value;
        //    this.IncreseDataRevision();
        //}

        //public void Set(string name, bool value)
        //{
        //    this.Set(name, value ? 1 : 0);
        //}

        //public void Set(int hash, bool value)
        //{
        //    this.Set(hash, value ? 1 : 0);
        //}

        //public void Set(string name, long value)
        //{
        //    this.Set(name.GetStableHashCode(), value);
        //}

        //public void Set(int hash, long value)
        //{
        //    this.InitLongs();
        //    long num;
        //    if (this.m_longs.TryGetValue(hash, out num) && num == value)
        //        return;
        //    this.m_longs[hash] = value;
        //    this.IncreseDataRevision();
        //}

        //public void Set(string name, byte[] bytes)
        //{
        //    string base64String = Convert.ToBase64String(bytes);
        //    this.Set(name, base64String);
        //}

        //public byte[] GetByteArray(string name)
        //{
        //    string s = this.GetString(name, "");
        //    return s.Length > 0 ? Convert.FromBase64String(s) : (byte[])null;
        //}

        //public void Set(string name, string value)
        //{
        //    this.InitStrings();
        //    int stableHashCode = name.GetStableHashCode();
        //    string str;
        //    if (this.m_strings.TryGetValue(stableHashCode, out str) && str == value)
        //        return;
        //    this.m_strings[stableHashCode] = value;
        //    this.IncreseDataRevision();
        //}

        //public void SetPosition(Vector3 pos)
        //{
        //    this.InternalSetPosition(pos);
        //}

        //public void InternalSetPosition(Vector3 pos)
        //{
        //    if (this.m_position == pos)
        //        return;
        //    this.m_position = pos;
        //    this.SetSector(ZoneSystem.instance.GetZone(this.m_position));
        //    if (!this.IsOwner())
        //        return;
        //    this.IncreseDataRevision();
        //}

        //private void SetSector(Vector2i sector)
        //{
        //    if (this.m_sector == sector)
        //        return;
        //    this.m_zdoMan.RemoveFromSector(this, this.m_sector);
        //    this.m_sector = sector;
        //    this.m_zdoMan.AddToSector(this, this.m_sector);
        //    this.m_zdoMan.ZDOSectorInvalidated(this);
        //}

        //public Vector2i GetSector()
        //{
        //    return this.m_sector;
        //}

        //public void SetRotation(Quaternion rot)
        //{
        //    if (this.m_rotation == rot)
        //        return;
        //    this.m_rotation = rot;
        //    this.IncreseDataRevision();
        //}

        //public void SetType(ZDO.ObjectType type)
        //{
        //    if (this.m_type == type)
        //        return;
        //    this.m_type = type;
        //    this.IncreseDataRevision();
        //}

        //public void SetDistant(bool distant)
        //{
        //    if (this.m_distant == distant)
        //        return;
        //    this.m_distant = distant;
        //    this.IncreseDataRevision();
        //}

        //public void SetPrefab(int prefab)
        //{
        //    if (this.m_prefab == prefab)
        //        return;
        //    this.m_prefab = prefab;
        //    this.IncreseDataRevision();
        //}

        //public int GetPrefab()
        //{
        //    return this.m_prefab;
        //}

        //public Vector3 GetPosition()
        //{
        //    return this.m_position;
        //}

        //public Quaternion GetRotation()
        //{
        //    return this.m_rotation;
        //}

        //private void IncreseDataRevision()
        //{
        //    ++this.m_dataRevision;
        //    if (ZNet.instance.IsServer())
        //        return;
        //    ZDOMan.instance.ClientChanged(this.m_uid);
        //}

        //private void IncreseOwnerRevision()
        //{
        //    ++this.m_ownerRevision;
        //    if (ZNet.instance.IsServer())
        //        return;
        //    ZDOMan.instance.ClientChanged(this.m_uid);
        //}

        //public float GetFloat(string name, float defaultValue = 0.0f)
        //{
        //    return this.GetFloat(name.GetStableHashCode(), defaultValue);
        //}

        //public float GetFloat(int hash, float defaultValue = 0.0f)
        //{
        //    float num;
        //    return this.m_floats == null || !this.m_floats.TryGetValue(hash, out num) ? defaultValue : num;
        //}

        //public Vector3 GetVec3(string name, Vector3 defaultValue)
        //{
        //    return this.GetVec3(name.GetStableHashCode(), defaultValue);
        //}

        //public Vector3 GetVec3(int hash, Vector3 defaultValue)
        //{
        //    Vector3 vector3;
        //    return this.m_vec3 == null || !this.m_vec3.TryGetValue(hash, out vector3) ? defaultValue : vector3;
        //}

        //public Quaternion GetQuaternion(string name, Quaternion defaultValue)
        //{
        //    return this.GetQuaternion(name.GetStableHashCode(), defaultValue);
        //}

        //public Quaternion GetQuaternion(int hash, Quaternion defaultValue)
        //{
        //    Quaternion quaternion;
        //    return this.m_quats == null || !this.m_quats.TryGetValue(hash, out quaternion) ? defaultValue : quaternion;
        //}

        //public int GetInt(string name, int defaultValue = 0)
        //{
        //    return this.GetInt(name.GetStableHashCode(), defaultValue);
        //}

        //public int GetInt(int hash, int defaultValue = 0)
        //{
        //    int num;
        //    return this.m_ints == null || !this.m_ints.TryGetValue(hash, out num) ? defaultValue : num;
        //}

        //public bool GetBool(string name, bool defaultValue = false)
        //{
        //    return this.GetBool(name.GetStableHashCode(), defaultValue);
        //}

        //public bool GetBool(int hash, bool defaultValue = false)
        //{
        //    int num;
        //    return this.m_ints == null || !this.m_ints.TryGetValue(hash, out num) ? defaultValue : (uint)num > 0U;
        //}

        //public long GetLong(string name, long defaultValue = 0)
        //{
        //    return this.GetLong(name.GetStableHashCode(), defaultValue);
        //}

        //public long GetLong(int hash, long defaultValue = 0)
        //{
        //    long num;
        //    return this.m_longs == null || !this.m_longs.TryGetValue(hash, out num) ? defaultValue : num;
        //}

        //public string GetString(string name, string defaultValue = "")
        //{
        //    string str;
        //    return this.m_strings == null || !this.m_strings.TryGetValue(name.GetStableHashCode(), out str) ? defaultValue : str;
        //}

        //public void Serialize(ZPackage pkg)
        //{
        //    pkg.Write(this.m_persistent);
        //    pkg.Write(this.m_distant);
        //    pkg.Write(this.m_timeCreated);
        //    pkg.Write(this.m_pgwVersion);
        //    pkg.Write((sbyte)this.m_type);
        //    pkg.Write(this.m_prefab);
        //    pkg.Write(this.m_rotation);
        //    int data = 0;
        //    if (this.m_floats != null && this.m_floats.Count > 0)
        //        data |= 1;
        //    if (this.m_vec3 != null && this.m_vec3.Count > 0)
        //        data |= 2;
        //    if (this.m_quats != null && this.m_quats.Count > 0)
        //        data |= 4;
        //    if (this.m_ints != null && this.m_ints.Count > 0)
        //        data |= 8;
        //    if (this.m_strings != null && this.m_strings.Count > 0)
        //        data |= 16;
        //    if (this.m_longs != null && this.m_longs.Count > 0)
        //        data |= 64;
        //    pkg.Write(data);
        //    if (this.m_floats != null && this.m_floats.Count > 0)
        //    {
        //        pkg.Write((byte)this.m_floats.Count);
        //        foreach (KeyValuePair<int, float> keyValuePair in this.m_floats)
        //        {
        //            pkg.Write(keyValuePair.Key);
        //            pkg.Write(keyValuePair.Value);
        //        }
        //    }
        //    if (this.m_vec3 != null && this.m_vec3.Count > 0)
        //    {
        //        pkg.Write((byte)this.m_vec3.Count);
        //        foreach (KeyValuePair<int, Vector3> keyValuePair in this.m_vec3)
        //        {
        //            pkg.Write(keyValuePair.Key);
        //            pkg.Write(keyValuePair.Value);
        //        }
        //    }
        //    if (this.m_quats != null && this.m_quats.Count > 0)
        //    {
        //        pkg.Write((byte)this.m_quats.Count);
        //        foreach (KeyValuePair<int, Quaternion> quat in this.m_quats)
        //        {
        //            pkg.Write(quat.Key);
        //            pkg.Write(quat.Value);
        //        }
        //    }
        //    if (this.m_ints != null && this.m_ints.Count > 0)
        //    {
        //        pkg.Write((byte)this.m_ints.Count);
        //        foreach (KeyValuePair<int, int> keyValuePair in this.m_ints)
        //        {
        //            pkg.Write(keyValuePair.Key);
        //            pkg.Write(keyValuePair.Value);
        //        }
        //    }
        //    if (this.m_longs != null && this.m_longs.Count > 0)
        //    {
        //        pkg.Write((byte)this.m_longs.Count);
        //        foreach (KeyValuePair<int, long> keyValuePair in this.m_longs)
        //        {
        //            pkg.Write(keyValuePair.Key);
        //            pkg.Write(keyValuePair.Value);
        //        }
        //    }
        //    if (this.m_strings == null || this.m_strings.Count <= 0)
        //        return;
        //    pkg.Write((byte)this.m_strings.Count);
        //    foreach (KeyValuePair<int, string> keyValuePair in this.m_strings)
        //    {
        //        pkg.Write(keyValuePair.Key);
        //        pkg.Write(keyValuePair.Value);
        //    }
        //}

        //public void Deserialize(ZPackage pkg)
        //{
        //    this.m_persistent = pkg.ReadBool();
        //    this.m_distant = pkg.ReadBool();
        //    this.m_timeCreated = pkg.ReadLong();
        //    this.m_pgwVersion = pkg.ReadInt();
        //    this.m_type = (ZDO.ObjectType)pkg.ReadSByte();
        //    this.m_prefab = pkg.ReadInt();
        //    this.m_rotation = pkg.ReadQuaternion();
        //    int num1 = pkg.ReadInt();
        //    if ((num1 & 1) != 0)
        //    {
        //        this.InitFloats();
        //        int num2 = (int)pkg.ReadByte();
        //        for (int index = 0; index < num2; ++index)
        //            this.m_floats[pkg.ReadInt()] = pkg.ReadSingle();
        //    }
        //    else
        //        this.ReleaseFloats();
        //    if ((num1 & 2) != 0)
        //    {
        //        this.InitVec3();
        //        int num2 = (int)pkg.ReadByte();
        //        for (int index = 0; index < num2; ++index)
        //            this.m_vec3[pkg.ReadInt()] = pkg.ReadVector3();
        //    }
        //    else
        //        this.ReleaseVec3();
        //    if ((num1 & 4) != 0)
        //    {
        //        this.InitQuats();
        //        int num2 = (int)pkg.ReadByte();
        //        for (int index = 0; index < num2; ++index)
        //            this.m_quats[pkg.ReadInt()] = pkg.ReadQuaternion();
        //    }
        //    else
        //        this.ReleaseQuats();
        //    if ((num1 & 8) != 0)
        //    {
        //        this.InitInts();
        //        int num2 = (int)pkg.ReadByte();
        //        for (int index = 0; index < num2; ++index)
        //            this.m_ints[pkg.ReadInt()] = pkg.ReadInt();
        //    }
        //    else
        //        this.ReleaseInts();
        //    if ((num1 & 64) != 0)
        //    {
        //        this.InitLongs();
        //        int num2 = (int)pkg.ReadByte();
        //        for (int index = 0; index < num2; ++index)
        //            this.m_longs[pkg.ReadInt()] = pkg.ReadLong();
        //    }
        //    else
        //        this.ReleaseLongs();
        //    if ((num1 & 16) != 0)
        //    {
        //        this.InitStrings();
        //        int num2 = (int)pkg.ReadByte();
        //        for (int index = 0; index < num2; ++index)
        //            this.m_strings[pkg.ReadInt()] = pkg.ReadString();
        //    }
        //    else
        //        this.ReleaseStrings();
        //}

        //public void Save(ZPackage pkg)
        //{
        //    pkg.Write(this.m_ownerRevision);
        //    pkg.Write(this.m_dataRevision);
        //    pkg.Write(this.m_persistent);
        //    pkg.Write(this.m_owner);
        //    pkg.Write(this.m_timeCreated);
        //    pkg.Write(this.m_pgwVersion);
        //    pkg.Write((sbyte)this.m_type);
        //    pkg.Write(this.m_distant);
        //    pkg.Write(this.m_prefab);
        //    pkg.Write(this.m_sector);
        //    pkg.Write(this.m_position);
        //    pkg.Write(this.m_rotation);
        //    if (this.m_floats != null)
        //    {
        //        pkg.Write((char)this.m_floats.Count);
        //        foreach (KeyValuePair<int, float> keyValuePair in this.m_floats)
        //        {
        //            pkg.Write(keyValuePair.Key);
        //            pkg.Write(keyValuePair.Value);
        //        }
        //    }
        //    else
        //        pkg.Write(char.MinValue);
        //    if (this.m_vec3 != null)
        //    {
        //        pkg.Write((char)this.m_vec3.Count);
        //        foreach (KeyValuePair<int, Vector3> keyValuePair in this.m_vec3)
        //        {
        //            pkg.Write(keyValuePair.Key);
        //            pkg.Write(keyValuePair.Value);
        //        }
        //    }
        //    else
        //        pkg.Write(char.MinValue);
        //    if (this.m_quats != null)
        //    {
        //        pkg.Write((char)this.m_quats.Count);
        //        foreach (KeyValuePair<int, Quaternion> quat in this.m_quats)
        //        {
        //            pkg.Write(quat.Key);
        //            pkg.Write(quat.Value);
        //        }
        //    }
        //    else
        //        pkg.Write(char.MinValue);
        //    if (this.m_ints != null)
        //    {
        //        pkg.Write((char)this.m_ints.Count);
        //        foreach (KeyValuePair<int, int> keyValuePair in this.m_ints)
        //        {
        //            pkg.Write(keyValuePair.Key);
        //            pkg.Write(keyValuePair.Value);
        //        }
        //    }
        //    else
        //        pkg.Write(char.MinValue);
        //    if (this.m_longs != null)
        //    {
        //        pkg.Write((char)this.m_longs.Count);
        //        foreach (KeyValuePair<int, long> keyValuePair in this.m_longs)
        //        {
        //            pkg.Write(keyValuePair.Key);
        //            pkg.Write(keyValuePair.Value);
        //        }
        //    }
        //    else
        //        pkg.Write(char.MinValue);
        //    if (this.m_strings != null)
        //    {
        //        pkg.Write((char)this.m_strings.Count);
        //        foreach (KeyValuePair<int, string> keyValuePair in this.m_strings)
        //        {
        //            pkg.Write(keyValuePair.Key);
        //            pkg.Write(keyValuePair.Value);
        //        }
        //    }
        //    else
        //        pkg.Write(char.MinValue);
        //}

        //public void Load(ZPackage pkg, int version)
        //{
        //    this.m_ownerRevision = pkg.ReadUInt();
        //    this.m_dataRevision = pkg.ReadUInt();
        //    this.m_persistent = pkg.ReadBool();
        //    this.m_owner = pkg.ReadLong();
        //    this.m_timeCreated = pkg.ReadLong();
        //    this.m_pgwVersion = pkg.ReadInt();
        //    if (version >= 16 && version < 24)
        //        pkg.ReadInt();
        //    if (version >= 23)
        //        this.m_type = (ZDO.ObjectType)pkg.ReadSByte();
        //    if (version >= 22)
        //        this.m_distant = pkg.ReadBool();
        //    if (version < 13)
        //    {
        //        int num1 = (int)pkg.ReadChar();
        //        int num2 = (int)pkg.ReadChar();
        //    }
        //    if (version >= 17)
        //        this.m_prefab = pkg.ReadInt();
        //    this.m_sector = pkg.ReadVector2i();
        //    this.m_position = pkg.ReadVector3();
        //    this.m_rotation = pkg.ReadQuaternion();
        //    int num3 = (int)pkg.ReadChar();
        //    if (num3 > 0)
        //    {
        //        this.InitFloats();
        //        for (int index = 0; index < num3; ++index)
        //            this.m_floats[pkg.ReadInt()] = pkg.ReadSingle();
        //    }
        //    else
        //        this.ReleaseFloats();
        //    int num4 = (int)pkg.ReadChar();
        //    if (num4 > 0)
        //    {
        //        this.InitVec3();
        //        for (int index = 0; index < num4; ++index)
        //            this.m_vec3[pkg.ReadInt()] = pkg.ReadVector3();
        //    }
        //    else
        //        this.ReleaseVec3();
        //    int num5 = (int)pkg.ReadChar();
        //    if (num5 > 0)
        //    {
        //        this.InitQuats();
        //        for (int index = 0; index < num5; ++index)
        //            this.m_quats[pkg.ReadInt()] = pkg.ReadQuaternion();
        //    }
        //    else
        //        this.ReleaseQuats();
        //    int num6 = (int)pkg.ReadChar();
        //    if (num6 > 0)
        //    {
        //        this.InitInts();
        //        for (int index = 0; index < num6; ++index)
        //            this.m_ints[pkg.ReadInt()] = pkg.ReadInt();
        //    }
        //    else
        //        this.ReleaseInts();
        //    int num7 = (int)pkg.ReadChar();
        //    if (num7 > 0)
        //    {
        //        this.InitLongs();
        //        for (int index = 0; index < num7; ++index)
        //            this.m_longs[pkg.ReadInt()] = pkg.ReadLong();
        //    }
        //    else
        //        this.ReleaseLongs();
        //    int num8 = (int)pkg.ReadChar();
        //    if (num8 > 0)
        //    {
        //        this.InitStrings();
        //        for (int index = 0; index < num8; ++index)
        //            this.m_strings[pkg.ReadInt()] = pkg.ReadString();
        //    }
        //    else
        //        this.ReleaseStrings();
        //    if (version >= 17)
        //        return;
        //    this.m_prefab = this.GetInt("prefab", 0);
        //}

        //public bool IsOwner()
        //{
        //    return this.m_owner == this.m_zdoMan.GetMyID();
        //}

        //public bool HasOwner()
        //{
        //    return (ulong)this.m_owner > 0UL;
        //}

        //public void Print()
        //{
        //    ZLog.Log((object)("UID:" + (object)this.m_uid));
        //    ZLog.Log((object)("Persistent:" + this.m_persistent.ToString()));
        //    ZLog.Log((object)("Owner:" + (object)this.m_owner));
        //    ZLog.Log((object)("Revision:" + (object)this.m_ownerRevision));
        //    foreach (KeyValuePair<int, float> keyValuePair in this.m_floats)
        //        ZLog.Log((object)("F:" + (object)keyValuePair.Key + " = " + (object)keyValuePair.Value));
        //}

        //public void SetOwner(long uid)
        //{
        //    if (this.m_owner == uid)
        //        return;
        //    this.m_owner = uid;
        //    this.IncreseOwnerRevision();
        //}

        //public void SetPGWVersion(int version)
        //{
        //    this.m_pgwVersion = version;
        //}

        //public int GetPGWVersion()
        //{
        //    return this.m_pgwVersion;
        //}

        //private void InitFloats()
        //{
        //    if (this.m_floats != null)
        //        return;
        //    this.m_floats = Pool<Dictionary<int, float>>.Create();
        //    this.m_floats.Clear();
        //}

        //private void InitVec3()
        //{
        //    if (this.m_vec3 != null)
        //        return;
        //    this.m_vec3 = Pool<Dictionary<int, Vector3>>.Create();
        //    this.m_vec3.Clear();
        //}

        //private void InitQuats()
        //{
        //    if (this.m_quats != null)
        //        return;
        //    this.m_quats = Pool<Dictionary<int, Quaternion>>.Create();
        //    this.m_quats.Clear();
        //}

        //private void InitInts()
        //{
        //    if (this.m_ints != null)
        //        return;
        //    this.m_ints = Pool<Dictionary<int, int>>.Create();
        //    this.m_ints.Clear();
        //}

        //private void InitLongs()
        //{
        //    if (this.m_longs != null)
        //        return;
        //    this.m_longs = Pool<Dictionary<int, long>>.Create();
        //    this.m_longs.Clear();
        //}

        //private void InitStrings()
        //{
        //    if (this.m_strings != null)
        //        return;
        //    this.m_strings = Pool<Dictionary<int, string>>.Create();
        //    this.m_strings.Clear();
        //}

        //private void ReleaseFloats()
        //{
        //    if (this.m_floats == null)
        //        return;
        //    Pool<Dictionary<int, float>>.Release(this.m_floats);
        //    this.m_floats = (Dictionary<int, float>)null;
        //}

        //private void ReleaseVec3()
        //{
        //    if (this.m_vec3 == null)
        //        return;
        //    Pool<Dictionary<int, Vector3>>.Release(this.m_vec3);
        //    this.m_vec3 = (Dictionary<int, Vector3>)null;
        //}

        //private void ReleaseQuats()
        //{
        //    if (this.m_quats == null)
        //        return;
        //    Pool<Dictionary<int, Quaternion>>.Release(this.m_quats);
        //    this.m_quats = (Dictionary<int, Quaternion>)null;
        //}

        //private void ReleaseInts()
        //{
        //    if (this.m_ints == null)
        //        return;
        //    Pool<Dictionary<int, int>>.Release(this.m_ints);
        //    this.m_ints = (Dictionary<int, int>)null;
        //}

        //private void ReleaseLongs()
        //{
        //    if (this.m_longs == null)
        //        return;
        //    Pool<Dictionary<int, long>>.Release(this.m_longs);
        //    this.m_longs = (Dictionary<int, long>)null;
        //}

        //private void ReleaseStrings()
        //{
        //    if (this.m_strings == null)
        //        return;
        //    Pool<Dictionary<int, string>>.Release(this.m_strings);
        //    this.m_strings = (Dictionary<int, string>)null;
        //}

        //public enum ObjectType
        //{
        //    Default,
        //    Prioritized,
        //    Solid,
        //}
    }

}
