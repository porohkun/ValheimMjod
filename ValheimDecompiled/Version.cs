// Decompiled with JetBrains decompiler
// Type: Version
// Assembly: assembly_valheim, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F48D6A22-6962-45BF-8D82-0AAD6AFA4FDB
// Assembly location: D:\Games\Steam\steamapps\common\Valheim\valheim_Data\Managed\assembly_valheim.dll

using UnityEngine;

internal class Version
{
    public static int m_major = 0;
    public static int m_minor = 147;
    public static int m_patch = 3;
    public static int m_playerVersion = 33;
    public static int[] m_compatiblePlayerVersions = new int[6]
    {
    32,
    31,
    30,
    29,
    28,
    27
    };
    public static int m_worldVersion = 26;
    public static int[] m_compatibleWorldVersions = new int[16]
    {
    25,
    24,
    23,
    22,
    21,
    20,
    19,
    18,
    17,
    16,
    15,
    14,
    13,
    11,
    10,
    9
    };
    public static int m_worldGenVersion = 1;

    public static string GetVersionString()
    {
        return Version.CombineVersion(Version.m_major, Version.m_minor, Version.m_patch);
    }

    public static bool IsVersionNewer(int major, int minor, int patch)
    {
        if (major > Version.m_major || major == Version.m_major && minor > Version.m_minor)
            return true;
        if (major != Version.m_major || minor != Version.m_minor)
            return false;
        if (Version.m_patch >= 0)
            return patch > Version.m_patch;
        return patch >= 0 || patch < Version.m_patch;
    }

    public static string CombineVersion(int major, int minor, int patch)
    {
        if (patch == 0)
            return major.ToString() + "." + minor.ToString();
        return patch < 0 ? major.ToString() + "." + minor.ToString() + ".rc" + Mathf.Abs(patch).ToString() : major.ToString() + "." + minor.ToString() + "." + patch.ToString();
    }

    public static bool IsWorldVersionCompatible(int version)
    {
        if (version == Version.m_worldVersion)
            return true;
        foreach (int compatibleWorldVersion in Version.m_compatibleWorldVersions)
        {
            if (version == compatibleWorldVersion)
                return true;
        }
        return false;
    }

    public static bool IsPlayerVersionCompatible(int version)
    {
        if (version == Version.m_playerVersion)
            return true;
        foreach (int compatiblePlayerVersion in Version.m_compatiblePlayerVersions)
        {
            if (version == compatiblePlayerVersion)
                return true;
        }
        return false;
    }
}
