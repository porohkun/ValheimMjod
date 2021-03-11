using System.Collections.ObjectModel;
using System.Linq;
using Valheim;

namespace ValheimMjod
{
    public class CharacterViewModelDummy : CharacterViewModel
    {
        public new string Name { get; } = "MyCharacter";

        public CharacterViewModelDummy() : base(null, null)
        {
            MainProps = new ObservableCollection<Prop>()
            {
                new Prop("Change name", "StringProp", () => "MyCharacter", null),
                new PropWithSelection("Gender", "SwitcherProp", () => 0, null, ("Male", 0), ("Female", 1))
            };
        }
    }

    public class CharacterViewModel : BindingBase
    {
        private Player _player;
        private PlayerProfile _profile;

        public string Name
        {
            get => _profile.GetName();
        }
        public ObservableCollection<Prop> MainProps { get; protected set; }
        public ObservableCollection<Prop> SkillsProps { get; protected set; }

        public CharacterViewModel(Player player, PlayerProfile profile)
        {
            _player = player;
            _profile = profile;

            MainProps = new ObservableCollection<Prop>()
            {
                new Prop("Character name", "StringProp", () => _profile?.GetName(), v => _profile.SetName(v as string)),
                new PropWithSelection("Gender", "SwitcherProp", () => _player?.ModelIndex, v => _player.ModelIndex = (int)v, ("Male", 0), ("Female", 1))
            };
            SkillsProps = new ObservableCollection<Prop>(_player.Skills.SkillData.Select(s => new Prop(s.Key.ToString(), "SkillProp",
                () => s.Value.m_level,
                v => s.Value.m_level = (float)v)));
        }
    }
}
