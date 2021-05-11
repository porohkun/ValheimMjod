using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Data;
using UnityEngine;
using Valheim;

namespace ValheimMjod
{
    public class CharacterViewModelDummy : CharacterViewModel
    {
        public new string Name { get; } = "MyCharacter";

        public CharacterViewModelDummy() : base(new Player()
        {
            m_inventory = new Inventory("", 8, 4)
            {
                m_inventory = new List<ItemData>
                {
                    new ItemData() { m_gridPos = new Vector2i(0, 0), m_name = "Item1" },
                    new ItemData() { m_gridPos = new Vector2i(1, 0), m_name = "" },
                    new ItemData() { m_gridPos = new Vector2i(4, 1), m_name = "Item2" },
                    new ItemData() { m_gridPos = new Vector2i(5, 1), m_name = "Item3" },
                    new ItemData() { m_gridPos = new Vector2i(1, 2), m_name = "Item4" },
                    new ItemData() { m_gridPos = new Vector2i(7, 3), m_name = "Item5" }
                }
            }
        }, new PlayerProfile())
        {
            MainProps = new ObservableCollection<Prop>()
            {
                new Prop<string>("Change name", "StringProp", () => "MyCharacter", null),
                new PropWithSelection<int>("Gender", "SwitcherProp", () => 0, null, ("Male", 0), ("Female", 1))
            };
            SkillsProps = new ObservableCollection<Prop>()
            {
                new Prop<int>("Skill1", "SkillProp", () => 11, null),
                new Prop<int>("Skill2", "SkillProp", () => 11, null),
                new Prop<int>("Skill3", "SkillProp", () => 11, null),
            };
        }
    }

    public class CharacterViewModel : BindableBase
    {
        static (string, string)[] Hairs =
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

        static (string, string)[] Beards =
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
        public ObservableCollection<Prop> ItemsProps { get; protected set; }

        public CharacterViewModel(Player player, PlayerProfile profile)
        {
            Player = player;
            Profile = profile;

            MainProps = new ObservableCollection<Prop>()
            {
                new Prop<string>("Character name", "StringProp", () => Profile.GetName(), v => Profile.SetName(v as string)),
                new PropWithSelection<int>("Gender", "SwitcherProp", () => Player.ModelIndex, v => Player.ModelIndex = v, ("Male", 0), ("Female", 1))
            };
            SkillsProps = new ObservableCollection<Prop>(Player.Skills.SkillData.Select(s => new Prop<int>(s.Key.ToString(), "SkillProp",
                () => (int)s.Value.m_level,
                v => s.Value.m_level = v)));
            VisualProps = new ObservableCollection<Prop>()
            {
                new Prop<Vector3>("Skin color", "ColorProp", () => Player.m_skinColor, v => Player.m_skinColor = v),
                new Prop<double>("Skin glowing", "GlowProp", () => Player.m_skinColorGlow, v => Player.m_skinColorGlow = (float)v),
                new Prop<Vector3>("Hair color", "ColorProp", () => Player.m_hairColor, v => Player.m_hairColor = (Vector3)v),
                new Prop<double>("Hair glowing", "GlowProp", () => Player.m_hairColorGlow, v => Player.m_hairColorGlow = (float)v),
                new PropWithSelection<string>("Hair", "DropDownProp", () => Player.m_hairItem, v => Player.m_hairItem = (string)v, Hairs),
                new PropWithSelection<string>("Beard", "DropDownProp", () => Player.m_beardItem, v => Player.m_beardItem = (string)v, Beards)
            };
            ItemsProps = new ObservableCollection<Prop>(Player.m_inventory.m_inventory.Select(i => new InventoryProp($"{i.m_gridPos}", "InventoryProp",
                () =>
                {
                    var result = new Dictionary<string, Prop>();
                    result.Add("exists", new Prop<bool>("", "", () => !string.IsNullOrWhiteSpace(i.m_name)));
                    result.Add("name", new Prop<string>("", "", () => i.m_name, v => i.m_name = v));
                    result.Add("quality", new Prop<int>("Quality", "ItemProp", () => i.m_quality, v => i.m_quality = v));
                    result.Add("stack", new Prop<int>("Count", "ItemProp", () => i.m_stack, v => i.m_stack = v));
                    result.Add("durability", new Prop<int>("Durability", "ItemProp", () => (int)i.m_durability, v => i.m_durability = v));
                    result.Add("crafter_id", new Prop<string>("Crafter ID", "StringProp", () => i.m_crafterID.ToString(), v => i.m_crafterID = long.TryParse(v, out long r) ? r : i.m_crafterID));
                    result.Add("crafter_name", new Prop<string>("Crafter Name", "StringProp", () => i.m_crafterName, v => i.m_crafterName = v));
                    return result;
                },
                null,
                i.m_gridPos.x, i.m_gridPos.y)));
        }

        void RefreshItemsProps()
        {
            CollectionViewSource.GetDefaultView(ItemsProps).Refresh();
        }

        static int StringToInt(object value, int defaultValue)
        {
            if (value is int intValue)
                return intValue;
            if (!(value is string stringValue))
                return defaultValue;
            return int.TryParse(stringValue, out int result) ? result : defaultValue;
        }
    }
}
