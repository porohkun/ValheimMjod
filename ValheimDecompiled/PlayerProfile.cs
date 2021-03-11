// Decompiled with JetBrains decompiler
// Type: PlayerProfile
// Assembly: assembly_valheim, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F48D6A22-6962-45BF-8D82-0AAD6AFA4FDB
// Assembly location: D:\Games\Steam\steamapps\common\Valheim\valheim_Data\Managed\assembly_valheim.dll

using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PlayerProfile
{
    public static Vector3 m_originalSpawnPoint = new Vector3(-676f, 50f, 299f);
    private string m_filename = "";
    private string m_playerName = "";
    private string m_startSeed = "";
    private Dictionary<long, PlayerProfile.WorldPlayerData> m_worldData = new Dictionary<long, PlayerProfile.WorldPlayerData>();
    public PlayerProfile.PlayerStats m_playerStats = new PlayerProfile.PlayerStats();
    private long m_playerID;
    private byte[] m_playerData;

    public PlayerProfile(string filename = null)
    {
        this.m_filename = filename;
        this.m_playerName = "Stranger";
        this.m_playerID = Utils.GenerateUID();
    }

    public bool Load()
    {
        return this.m_filename != null && this.LoadPlayerFromDisk();
    }

    public bool Save()
    {
        return this.m_filename != null && this.SavePlayerToDisk();
    }

    public bool HaveIncompatiblPlayerData()
    {
        if (this.m_filename == null)
            return false;
        ZPackage zpackage = this.LoadPlayerDataFromDisk();
        if (zpackage == null || Version.IsPlayerVersionCompatible(zpackage.ReadInt()))
            return false;
        ZLog.Log((object)"Player data is not compatible, ignoring");
        return true;
    }

    public void SavePlayerData(Player player)
    {
        ZPackage pkg = new ZPackage();
        player.Save(pkg);
        this.m_playerData = pkg.GetArray();
    }

    public void LoadPlayerData(Player player)
    {
        player.SetPlayerID(this.m_playerID, this.m_playerName);
        if (this.m_playerData != null)
        {
            ZPackage pkg = new ZPackage(this.m_playerData);
            player.Load(pkg);
        }
        else
            player.GiveDefaultItems();
    }

    public void SaveLogoutPoint()
    {
        if (!(bool)(UnityEngine.Object)Player.m_localPlayer || Player.m_localPlayer.IsDead() || Player.m_localPlayer.InIntro())
            return;
        this.SetLogoutPoint(Player.m_localPlayer.transform.position);
    }

    private bool SavePlayerToDisk()
    {
        Directory.CreateDirectory(Utils.GetSaveDataPath() + "/characters");
        string str1 = Utils.GetSaveDataPath() + "/characters/" + this.m_filename + ".fch";
        string str2 = Utils.GetSaveDataPath() + "/characters/" + this.m_filename + ".fch.old";
        string str3 = Utils.GetSaveDataPath() + "/characters/" + this.m_filename + ".fch.new";
        ZPackage zpackage = new ZPackage();
        zpackage.Write(Version.m_playerVersion);
        zpackage.Write(this.m_playerStats.m_kills);
        zpackage.Write(this.m_playerStats.m_deaths);
        zpackage.Write(this.m_playerStats.m_crafts);
        zpackage.Write(this.m_playerStats.m_builds);
        zpackage.Write(this.m_worldData.Count);
        foreach (KeyValuePair<long, PlayerProfile.WorldPlayerData> keyValuePair in this.m_worldData)
        {
            zpackage.Write(keyValuePair.Key);
            zpackage.Write(keyValuePair.Value.m_haveCustomSpawnPoint);
            zpackage.Write(keyValuePair.Value.m_spawnPoint);
            zpackage.Write(keyValuePair.Value.m_haveLogoutPoint);
            zpackage.Write(keyValuePair.Value.m_logoutPoint);
            zpackage.Write(keyValuePair.Value.m_haveDeathPoint);
            zpackage.Write(keyValuePair.Value.m_deathPoint);
            zpackage.Write(keyValuePair.Value.m_homePoint);
            zpackage.Write(keyValuePair.Value.m_mapData != null);
            if (keyValuePair.Value.m_mapData != null)
                zpackage.Write(keyValuePair.Value.m_mapData);
        }
        zpackage.Write(this.m_playerName);
        zpackage.Write(this.m_playerID);
        zpackage.Write(this.m_startSeed);
        if (this.m_playerData != null)
        {
            zpackage.Write(true);
            zpackage.Write(this.m_playerData);
        }
        else
            zpackage.Write(false);
        byte[] hash = zpackage.GenerateHash();
        byte[] array = zpackage.GetArray();
        FileStream fileStream = File.Create(str3);
        BinaryWriter binaryWriter = new BinaryWriter((Stream)fileStream);
        binaryWriter.Write(array.Length);
        binaryWriter.Write(array);
        binaryWriter.Write(hash.Length);
        binaryWriter.Write(hash);
        binaryWriter.Flush();
        fileStream.Flush(true);
        fileStream.Close();
        fileStream.Dispose();
        if (File.Exists(str1))
        {
            if (File.Exists(str2))
                File.Delete(str2);
            File.Move(str1, str2);
        }
        File.Move(str3, str1);
        return true;
    }

    private bool LoadPlayerFromDisk()
    {
        try
        {
            ZPackage zpackage = this.LoadPlayerDataFromDisk();
            if (zpackage == null)
            {
                ZLog.LogWarning((object)"No player data");
                return false;
            }
            int version = zpackage.ReadInt();
            if (!Version.IsPlayerVersionCompatible(version))
            {
                ZLog.Log((object)"Player data is not compatible, ignoring");
                return false;
            }
            if (version >= 28)
            {
                this.m_playerStats.m_kills = zpackage.ReadInt();
                this.m_playerStats.m_deaths = zpackage.ReadInt();
                this.m_playerStats.m_crafts = zpackage.ReadInt();
                this.m_playerStats.m_builds = zpackage.ReadInt();
            }
            this.m_worldData.Clear();
            int num = zpackage.ReadInt();
            for (int index = 0; index < num; ++index)
            {
                long key = zpackage.ReadLong();
                PlayerProfile.WorldPlayerData worldPlayerData = new PlayerProfile.WorldPlayerData();
                worldPlayerData.m_haveCustomSpawnPoint = zpackage.ReadBool();
                worldPlayerData.m_spawnPoint = zpackage.ReadVector3();
                worldPlayerData.m_haveLogoutPoint = zpackage.ReadBool();
                worldPlayerData.m_logoutPoint = zpackage.ReadVector3();
                if (version >= 30)
                {
                    worldPlayerData.m_haveDeathPoint = zpackage.ReadBool();
                    worldPlayerData.m_deathPoint = zpackage.ReadVector3();
                }
                worldPlayerData.m_homePoint = zpackage.ReadVector3();
                if (version >= 29 && zpackage.ReadBool())
                    worldPlayerData.m_mapData = zpackage.ReadByteArray();
                this.m_worldData.Add(key, worldPlayerData);
            }
            this.m_playerName = zpackage.ReadString();
            this.m_playerID = zpackage.ReadLong();
            this.m_startSeed = zpackage.ReadString();
            this.m_playerData = !zpackage.ReadBool() ? (byte[])null : zpackage.ReadByteArray();
        }
        catch (Exception ex)
        {
            ZLog.LogWarning((object)("Exception while loading player profile:" + this.m_filename + " , " + ex.ToString()));
        }
        return true;
    }

    private ZPackage LoadPlayerDataFromDisk()
    {
        string path = Utils.GetSaveDataPath() + "/characters/" + this.m_filename + ".fch";
        FileStream fileStream;
        try
        {
            fileStream = File.OpenRead(path);
        }
        catch
        {
            ZLog.Log((object)("  failed to load " + path));
            return (ZPackage)null;
        }
        byte[] data;
        try
        {
            BinaryReader binaryReader = new BinaryReader((Stream)fileStream);
            data = binaryReader.ReadBytes(binaryReader.ReadInt32());
            binaryReader.ReadBytes(binaryReader.ReadInt32());
        }
        catch
        {
            ZLog.LogError((object)"  error loading player.dat");
            fileStream.Dispose();
            return (ZPackage)null;
        }
        fileStream.Dispose();
        return new ZPackage(data);
    }

    public void SetLogoutPoint(Vector3 point)
    {
        this.GetWorldData(ZNet.instance.GetWorldUID()).m_haveLogoutPoint = true;
        this.GetWorldData(ZNet.instance.GetWorldUID()).m_logoutPoint = point;
    }

    public void SetDeathPoint(Vector3 point)
    {
        this.GetWorldData(ZNet.instance.GetWorldUID()).m_haveDeathPoint = true;
        this.GetWorldData(ZNet.instance.GetWorldUID()).m_deathPoint = point;
    }

    public void SetMapData(byte[] data)
    {
        long worldUid = ZNet.instance.GetWorldUID();
        if (worldUid == 0L)
            return;
        this.GetWorldData(worldUid).m_mapData = data;
    }

    public byte[] GetMapData()
    {
        return this.GetWorldData(ZNet.instance.GetWorldUID()).m_mapData;
    }

    public void ClearLoguoutPoint()
    {
        this.GetWorldData(ZNet.instance.GetWorldUID()).m_haveLogoutPoint = false;
    }

    public bool HaveLogoutPoint()
    {
        return this.GetWorldData(ZNet.instance.GetWorldUID()).m_haveLogoutPoint;
    }

    public Vector3 GetLogoutPoint()
    {
        return this.GetWorldData(ZNet.instance.GetWorldUID()).m_logoutPoint;
    }

    public bool HaveDeathPoint()
    {
        return this.GetWorldData(ZNet.instance.GetWorldUID()).m_haveDeathPoint;
    }

    public Vector3 GetDeathPoint()
    {
        return this.GetWorldData(ZNet.instance.GetWorldUID()).m_deathPoint;
    }

    public void SetCustomSpawnPoint(Vector3 point)
    {
        this.GetWorldData(ZNet.instance.GetWorldUID()).m_haveCustomSpawnPoint = true;
        this.GetWorldData(ZNet.instance.GetWorldUID()).m_spawnPoint = point;
    }

    public Vector3 GetCustomSpawnPoint()
    {
        return this.GetWorldData(ZNet.instance.GetWorldUID()).m_spawnPoint;
    }

    public bool HaveCustomSpawnPoint()
    {
        return this.GetWorldData(ZNet.instance.GetWorldUID()).m_haveCustomSpawnPoint;
    }

    public void ClearCustomSpawnPoint()
    {
        this.GetWorldData(ZNet.instance.GetWorldUID()).m_haveCustomSpawnPoint = false;
    }

    public void SetHomePoint(Vector3 point)
    {
        this.GetWorldData(ZNet.instance.GetWorldUID()).m_homePoint = point;
    }

    public Vector3 GetHomePoint()
    {
        return this.GetWorldData(ZNet.instance.GetWorldUID()).m_homePoint;
    }

    public void SetName(string name)
    {
        this.m_playerName = name;
    }

    public string GetName()
    {
        return this.m_playerName;
    }

    public long GetPlayerID()
    {
        return this.m_playerID;
    }

    public static List<PlayerProfile> GetAllPlayerProfiles()
    {
        string[] strArray;
        try
        {
            strArray = Directory.GetFiles(Utils.GetSaveDataPath() + "/characters", "*.fch");
        }
        catch
        {
            strArray = new string[0];
        }
        List<PlayerProfile> playerProfileList = new List<PlayerProfile>();
        foreach (string path in strArray)
        {
            string withoutExtension = Path.GetFileNameWithoutExtension(path);
            PlayerProfile playerProfile = new PlayerProfile(withoutExtension);
            if (!playerProfile.Load())
                ZLog.Log((object)("Failed to load " + withoutExtension));
            else
                playerProfileList.Add(playerProfile);
        }
        return playerProfileList;
    }

    public static void RemoveProfile(string name)
    {
        try
        {
            File.Delete(Utils.GetSaveDataPath() + "/characters/" + name + ".fch");
        }
        catch
        {
        }
    }

    public static bool HaveProfile(string name)
    {
        return File.Exists(Utils.GetSaveDataPath() + "/characters/" + name + ".fch");
    }

    public string GetFilename()
    {
        return this.m_filename;
    }

    private PlayerProfile.WorldPlayerData GetWorldData(long worldUID)
    {
        PlayerProfile.WorldPlayerData worldPlayerData1;
        if (this.m_worldData.TryGetValue(worldUID, out worldPlayerData1))
            return worldPlayerData1;
        PlayerProfile.WorldPlayerData worldPlayerData2 = new PlayerProfile.WorldPlayerData();
        this.m_worldData.Add(worldUID, worldPlayerData2);
        return worldPlayerData2;
    }

    private class WorldPlayerData
    {
        public Vector3 m_spawnPoint = Vector3.zero;
        public Vector3 m_logoutPoint = Vector3.zero;
        public Vector3 m_deathPoint = Vector3.zero;
        public Vector3 m_homePoint = Vector3.zero;
        public bool m_haveCustomSpawnPoint;
        public bool m_haveLogoutPoint;
        public bool m_haveDeathPoint;
        public byte[] m_mapData;
    }

    public class PlayerStats
    {
        public int m_kills;
        public int m_deaths;
        public int m_crafts;
        public int m_builds;
    }
}
