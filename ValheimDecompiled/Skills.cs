// Decompiled with JetBrains decompiler
// Type: Skills
// Assembly: assembly_valheim, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F48D6A22-6962-45BF-8D82-0AAD6AFA4FDB
// Assembly location: D:\Games\Steam\steamapps\common\Valheim\valheim_Data\Managed\assembly_valheim.dll

using System;
using System.Collections.Generic;
using UnityEngine;

public class Skills : MonoBehaviour
{
    public float m_totalSkillCap = 600f;
    public List<Skills.SkillDef> m_skills = new List<Skills.SkillDef>();
    public float m_DeathLowerFactor = 0.25f;
    private Dictionary<Skills.SkillType, Skills.Skill> m_skillData = new Dictionary<Skills.SkillType, Skills.Skill>();
    private const int dataVersion = 2;
    private const float randomSkillRange = 0.15f;
    private const float randomSkillMin = 0.4f;
    public const float m_maxSkillLevel = 100f;
    public const float m_skillCurve = 2f;
    public bool m_useSkillCap;
    private Player m_player;

    public void Awake()
    {
        this.m_player = this.GetComponent<Player>();
    }

    public void Save(ZPackage pkg)
    {
        pkg.Write(2);
        pkg.Write(this.m_skillData.Count);
        foreach (KeyValuePair<Skills.SkillType, Skills.Skill> keyValuePair in this.m_skillData)
        {
            pkg.Write((int)keyValuePair.Value.m_info.m_skill);
            pkg.Write(keyValuePair.Value.m_level);
            pkg.Write(keyValuePair.Value.m_accumulator);
        }
    }

    public void Load(ZPackage pkg)
    {
        int num1 = pkg.ReadInt();
        this.m_skillData.Clear();
        int num2 = pkg.ReadInt();
        for (int index = 0; index < num2; ++index)
        {
            Skills.SkillType skillType = (Skills.SkillType)pkg.ReadInt();
            float num3 = pkg.ReadSingle();
            float num4 = num1 >= 2 ? pkg.ReadSingle() : 0.0f;
            if (this.IsSkillValid(skillType))
            {
                Skills.Skill skill = this.GetSkill(skillType);
                skill.m_level = num3;
                skill.m_accumulator = num4;
            }
        }
    }

    private bool IsSkillValid(Skills.SkillType type)
    {
        return Enum.IsDefined(typeof(Skills.SkillType), (object)type);
    }

    public float GetSkillFactor(Skills.SkillType skillType)
    {
        return skillType == Skills.SkillType.None ? 0.0f : this.GetSkill(skillType).m_level / 100f;
    }

    public void GetRandomSkillRange(out float min, out float max, Skills.SkillType skillType)
    {
        float num = Mathf.Lerp(0.4f, 1f, this.GetSkillFactor(skillType));
        min = Mathf.Clamp01(num - 0.15f);
        max = Mathf.Clamp01(num + 0.15f);
    }

    public float GetRandomSkillFactor(Skills.SkillType skillType)
    {
        double num = (double)Mathf.Lerp(0.4f, 1f, this.GetSkillFactor(skillType));
        return Mathf.Lerp(Mathf.Clamp01((float)(num - 0.150000005960464)), Mathf.Clamp01((float)(num + 0.150000005960464)), UnityEngine.Random.value);
    }

    public void CheatRaiseSkill(string name, float value)
    {
        foreach (Skills.SkillType skillType in Enum.GetValues(typeof(Skills.SkillType)))
        {
            if (skillType.ToString().ToLower() == name)
            {
                Skills.Skill skill = this.GetSkill(skillType);
                skill.m_level += value;
                skill.m_level = Mathf.Clamp(skill.m_level, 0.0f, 100f);
                if (this.m_useSkillCap)
                    this.RebalanceSkills(skillType);
                this.m_player.Message(MessageHud.MessageType.TopLeft, "Skill incresed " + skill.m_info.m_skill.ToString() + ": " + (object)(int)skill.m_level, 0, skill.m_info.m_icon);
                Console.instance.Print("Skill " + skillType.ToString() + " = " + skill.m_level.ToString());
                return;
            }
        }
        Console.instance.Print("Skill not found " + name);
    }

    public void CheatResetSkill(string name)
    {
        foreach (Skills.SkillType skillType in Enum.GetValues(typeof(Skills.SkillType)))
        {
            if (skillType.ToString().ToLower() == name)
            {
                this.ResetSkill(skillType);
                Console.instance.Print("Skill " + skillType.ToString() + " reset");
                return;
            }
        }
        Console.instance.Print("Skill not found " + name);
    }

    public void ResetSkill(Skills.SkillType skillType)
    {
        this.m_skillData.Remove(skillType);
    }

    public void RaiseSkill(Skills.SkillType skillType, float factor = 1f)
    {
        if (skillType == Skills.SkillType.None)
            return;
        Skills.Skill skill = this.GetSkill(skillType);
        float level = skill.m_level;
        if (!skill.Raise(factor))
            return;
        if (this.m_useSkillCap)
            this.RebalanceSkills(skillType);
        this.m_player.OnSkillLevelup(skillType, skill.m_level);
        this.m_player.Message((int)level == 0 ? MessageHud.MessageType.Center : MessageHud.MessageType.TopLeft, "$msg_skillup $skill_" + skill.m_info.m_skill.ToString().ToLower() + ": " + (object)(int)skill.m_level, 0, skill.m_info.m_icon);
        Gogan.LogEvent("Game", "Levelup", skillType.ToString(), (long)(int)skill.m_level);
    }

    private void RebalanceSkills(Skills.SkillType skillType)
    {
        if ((double)this.GetTotalSkill() < (double)this.m_totalSkillCap)
            return;
        float num1 = this.m_totalSkillCap - this.GetSkill(skillType).m_level;
        float num2 = 0.0f;
        foreach (KeyValuePair<Skills.SkillType, Skills.Skill> keyValuePair in this.m_skillData)
        {
            if (keyValuePair.Key != skillType)
                num2 += keyValuePair.Value.m_level;
        }
        foreach (KeyValuePair<Skills.SkillType, Skills.Skill> keyValuePair in this.m_skillData)
        {
            if (keyValuePair.Key != skillType)
                keyValuePair.Value.m_level = keyValuePair.Value.m_level / num2 * num1;
        }
    }

    public void Clear()
    {
        this.m_skillData.Clear();
    }

    public void OnDeath()
    {
        this.LowerAllSkills(this.m_DeathLowerFactor);
    }

    public void LowerAllSkills(float factor)
    {
        foreach (KeyValuePair<Skills.SkillType, Skills.Skill> keyValuePair in this.m_skillData)
        {
            float num = keyValuePair.Value.m_level * factor;
            keyValuePair.Value.m_level -= num;
            keyValuePair.Value.m_accumulator = 0.0f;
        }
        this.m_player.Message(MessageHud.MessageType.TopLeft, "$msg_skills_lowered", 0, (Sprite)null);
    }

    private Skills.Skill GetSkill(Skills.SkillType skillType)
    {
        Skills.Skill skill1;
        if (this.m_skillData.TryGetValue(skillType, out skill1))
            return skill1;
        Skills.Skill skill2 = new Skills.Skill(this.GetSkillDef(skillType));
        this.m_skillData.Add(skillType, skill2);
        return skill2;
    }

    private Skills.SkillDef GetSkillDef(Skills.SkillType type)
    {
        foreach (Skills.SkillDef skill in this.m_skills)
        {
            if (skill.m_skill == type)
                return skill;
        }
        return (Skills.SkillDef)null;
    }

    public List<Skills.Skill> GetSkillList()
    {
        List<Skills.Skill> skillList = new List<Skills.Skill>();
        foreach (KeyValuePair<Skills.SkillType, Skills.Skill> keyValuePair in this.m_skillData)
            skillList.Add(keyValuePair.Value);
        return skillList;
    }

    public float GetTotalSkill()
    {
        float num = 0.0f;
        foreach (KeyValuePair<Skills.SkillType, Skills.Skill> keyValuePair in this.m_skillData)
            num += keyValuePair.Value.m_level;
        return num;
    }

    public float GetTotalSkillCap()
    {
        return this.m_totalSkillCap;
    }

    public enum SkillType
    {
        None = 0,
        Swords = 1,
        Knives = 2,
        Clubs = 3,
        Polearms = 4,
        Spears = 5,
        Blocking = 6,
        Axes = 7,
        Bows = 8,
        FireMagic = 9,
        FrostMagic = 10, // 0x0000000A
        Unarmed = 11, // 0x0000000B
        Pickaxes = 12, // 0x0000000C
        WoodCutting = 13, // 0x0000000D
        Jump = 100, // 0x00000064
        Sneak = 101, // 0x00000065
        Run = 102, // 0x00000066
        Swim = 103, // 0x00000067
        All = 999, // 0x000003E7
    }

    [Serializable]
    public class SkillDef
    {
        public Skills.SkillType m_skill = Skills.SkillType.Swords;
        public string m_description = "";
        public float m_increseStep = 1f;
        public Sprite m_icon;
    }

    public class Skill
    {
        public Skills.SkillDef m_info;
        public float m_level;
        public float m_accumulator;

        public Skill(Skills.SkillDef info)
        {
            this.m_info = info;
        }

        public bool Raise(float factor)
        {
            if ((double)this.m_level >= 100.0)
                return false;
            this.m_accumulator += this.m_info.m_increseStep * factor;
            if ((double)this.m_accumulator < (double)this.GetNextLevelRequirement())
                return false;
            ++this.m_level;
            this.m_level = Mathf.Clamp(this.m_level, 0.0f, 100f);
            this.m_accumulator = 0.0f;
            return true;
        }

        private float GetNextLevelRequirement()
        {
            return (float)((double)Mathf.Pow(this.m_level + 1f, 1.5f) * 0.5 + 0.5);
        }

        public float GetLevelPercentage()
        {
            return (double)this.m_level >= 100.0 ? 0.0f : Mathf.Clamp01(this.m_accumulator / this.GetNextLevelRequirement());
        }
    }
}
