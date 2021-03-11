using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;
using Valheim;

namespace ValheimMjod
{
    public class CharacterViewModelDummy : CharacterViewModel
    {
        public new string Name { get; } = "MyCharacter";

        public CharacterViewModelDummy() : base(new Player(), new PlayerProfile())
        {
            MainProps = new ObservableCollection<Prop>()
            {
                new Prop("Change name", "StringProp", () => "MyCharacter", null),
                new PropWithSelection("Gender", "SwitcherProp", () => 0, null, ("Male", 0), ("Female", 1))
            };
            SkillsProps = new ObservableCollection<Prop>()
            {
                new Prop("Skill1", "SkillProp", () => 11, null),
                new Prop("Skill2", "SkillProp", () => 11, null),
                new Prop("Skill3", "SkillProp", () => 11, null),
            };
        }
    }

    public class CharacterViewModel : BindingBase
    {
        static (string, object)[] Hairs =
        {
            ("No hair", "HairNone"),
            ("Braided 1", "Hair3"),
            ("Braided 2", "Hair11"),
            ("Braided 3", "Hair12"),
            ("Braided 4", "Hair13"),
            ("Long 1", "Hair6"),
            ("Ponytail 1", "Hair1"),
            ("Ponytail 2", "Hair2"),
            ("Ponytail 3", "Hair4"),
            ("Ponytail 4", "Hair7"),
            ("Short 1", "Hair5"),
            ("Short 2", "Hair8"),
            ("Side Swept 1", "Hair9"),
            ("Side Swept 2", "Hair10"),
            ("Side Swept 3", "Hair14")
        };

        static (string, object)[] Beards =
        {
            ("No beard", "BeardNone"),
            ("Braided 1", "Beard5"),
            ("Braided 2", "Beard6"),
            ("Braided 3", "Beard9"),
            ("Braided 4", "Beard10"),
            ("Long 1", "Beard1"),
            ("Long 2", "Beard2"),
            ("Short 1", "Beard3"),
            ("Short 2", "Beard4"),
            ("Short 3", "Beard7"),
            ("Thick 1", "Beard8")
        };

        public Player Player { get; protected set; }
        public PlayerProfile Profile { get; protected set; }

        public string Name
        {
            get => Profile.GetName();
        }
        public ObservableCollection<Prop> MainProps { get; protected set; }
        public ObservableCollection<Prop> SkillsProps { get; protected set; }
        public ObservableCollection<Prop> VisualProps { get; protected set; }

        public CharacterViewModel(Player player, PlayerProfile profile)
        {
            Player = player;
            Profile = profile;

            MainProps = new ObservableCollection<Prop>()
            {
                new Prop("Character name", "StringProp", () => Profile.GetName(), v => Profile.SetName(v as string)),
                new PropWithSelection("Gender", "SwitcherProp", () => Player.ModelIndex, v => Player.ModelIndex = (int)v, ("Male", 0), ("Female", 1))
            };
            SkillsProps = new ObservableCollection<Prop>(Player.Skills.SkillData.Select(s => new Prop(s.Key.ToString(), "SkillProp",
                () => (int)s.Value.m_level,
                v => s.Value.m_level = (float)(int)v)));
            VisualProps = new ObservableCollection<Prop>()
            {
                new Prop("Skin color", "ColorProp", () => Player.m_skinColor, v => Player.m_skinColor = (Vector3)v),
                new Prop("Hair color", "ColorProp", () => Player.m_hairColor, v => Player.m_hairColor = (Vector3)v),
                new PropWithSelection("Hair", "DropDownProp", () => Player.m_hairItem, v => Player.m_hairItem = (string)v, Hairs),
                new PropWithSelection("Beard", "DropDownProp", () => Player.m_beardItem, v => Player.m_beardItem = (string)v, Beards)
            };
        }
    }
}
