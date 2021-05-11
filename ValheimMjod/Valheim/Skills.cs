using System;
using System.Collections.Generic;
using UnityEngine;

namespace Valheim
{
    public class Skills
    {
        //public float m_totalSkillCap = 600f;
        public List<Skills.SkillDef> m_skills = new List<Skills.SkillDef>();
        //public float m_DeathLowerFactor = 0.25f;
        public Dictionary<Skills.SkillType, Skills.Skill> SkillData = new Dictionary<Skills.SkillType, Skills.Skill>();
        //private const int dataVersion = 2;
        //private const float randomSkillRange = 0.15f;
        //private const float randomSkillMin = 0.4f;
        //public const float m_maxSkillLevel = 100f;
        //public const float m_skillCurve = 2f;
        //public bool m_useSkillCap;
        //private Player m_player;

        public void Save(ZPackage pkg)
        {
            pkg.Write(2);
            pkg.Write(this.SkillData.Count);
            foreach (KeyValuePair<Skills.SkillType, Skills.Skill> keyValuePair in this.SkillData)
            {
                pkg.Write((int)keyValuePair.Key);
                pkg.Write(keyValuePair.Value.m_level);
                pkg.Write(keyValuePair.Value.m_accumulator);
            }
        }

        public void Load(ZPackage pkg)
        {
            int num1 = pkg.ReadInt();
            this.SkillData.Clear();
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
            return type!= SkillType.All&& type!= SkillType.None&& Enum.IsDefined(typeof(Skills.SkillType), (object)type);
        }

        private Skills.Skill GetSkill(Skills.SkillType skillType)
        {
            Skills.Skill skill1;
            if (this.SkillData.TryGetValue(skillType, out skill1))
                return skill1;
            Skills.Skill skill2 = new Skills.Skill(this.GetSkillDef(skillType));
            this.SkillData.Add(skillType, skill2);
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
            //public Sprite m_icon;
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
}