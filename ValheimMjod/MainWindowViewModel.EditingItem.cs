using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ValheimMjod
{
    public partial class MainWindowViewModel
    {
        private Tuple<string, string[]>[] _itemsWithCategories { get; } = new Tuple<string, string[]>[]
        {
            new Tuple<string, string[]>("Items", new string[]
            {
                "Amber",
                "AmberPearl",
                "AncientSeed",
                "Barley",
                "BarleyFlour",
                "BarleyWine",
                "BarleyWineBase",
                "BeechSeeds",
                "BlackMetal",
                "BlackMetalScrap",
                "Bloodbag",
                "Bronze",
                "BronzeNails",
                "BoneFragments",
                "CarrotSeeds",
                "Chitin",
                "Coal",
                "Coins",
                "Copper",
                "CopperOre",
                "CryptKey",
                "Crystal",
                "Dandelion",
                "DeerHide",
                "DragonEgg",
                "DragonTear",
                "ElderBark",
                "Entrails",
                "Feathers",
                "FineWood",
                "FirCone",
                "FishingBait",
                "FishRaw",
                "Flametal",
                "FlametalOre",
                "Flax",
                "Flint",
                "FreezeGland",
                "GreydwarfEye",
                "Guck",
                "HardAntler",
                "Iron",
                "IronNails",
                "IronOre",
                "IronScrap",
                "LeatherScraps",
                "LinenThread",
                "LoxMeat",
                "LoxPelt",
                "Needle",
                "Obsidian",
                "Ooze",
                "PineCone",
                "QueenBee",
                "Ruby",
                "SerpentScale",
                "SharpeningStone",
                "Silver",
                "SilverNecklace",
                "SilverOre",
                "Stone",
                "SurtlingCore",
                "Thistle",
                "Tin",
                "TinOre",
                "TrollHide",
                "TurnipSeeds",
                "WitheredBone",
                "WolfFang",
                "WolfPelt",
                "Wood",
                "YmirRemains",
                "NeckTail",
                "RawMeat",
                "Resin",
                "RoundLog",
                "SerpentMeat",
                "YagluthDrop",
            }),
            new Tuple<string, string[]>("Armor", new string[]
            {
                "ArmorBronzeChest",
                "ArmorBronzeLegs",
                "ArmorIronChest",
                "ArmorIronLegs",
                "ArmorLeatherChest",
                "ArmorLeatherLegs",
                "ArmorPaddedCuirass",
                "ArmorPaddedGreaves",
                "ArmorRagsChest",
                "ArmorRagsLegs",
                "ArmorTrollLeatherChest",
                "ArmorTrollLeatherLegs",
                "ArmorWolfChest",
                "ArmorWolfLegs",
                "CapeDeerHide",
                "CapeLinen",
                "CapeLox",
                "CapeOdin",
                "CapeTest",
                "CapeTrollHide",
                "CapeWolf",
                "HelmetBronze",
                "HelmetDrake",
                "HelmetDverger",
                "HelmetIron",
                "HelmetLeather",
                "HelmetOdin",
                "HelmetPadded",
                "HelmetTrollLeather",
                "HelmetYule",
            }),
            new Tuple<string, string[]>("Ammo", new string[]
            {
                "ArrowBronze",
                "ArrowFire",
                "ArrowFlint",
                "ArrowFrost",
                "ArrowIron",
                "ArrowNeedle",
                "ArrowObsidian",
                "ArrowPoison",
                "ArrowSilver",
                "ArrowWood",
            }),
            new Tuple<string, string[]>("Weapons", new string[]
            {
                "AtgeirBlackmetal",
                "AtgeirBronze",
                "AtgeirIron",
                "Battleaxe",
                "Bow",
                "BowDraugrFang",
                "BowFineWood",
                "BowHuntsman",
                "KnifeBlackMetal",
                "KnifeChitin",
                "KnifeCopper",
                "KnifeFlint",
                "MaceBronze",
                "MaceIron",
                "MaceNeedle",
                "MaceSilver",
                "ShieldBanded",
                "ShieldBlackmetal",
                "ShieldBlackmetalTower",
                "ShieldBronzeBuckler",
                "ShieldIronSquare",
                "ShieldIronTower",
                "ShieldKnight",
                "ShieldSerpentscale",
                "ShieldSilver",
                "ShieldWood",
                "ShieldWoodTower",
                "SledgeIron",
                "SledgeStagbreaker",
                "SpearBronze",
                "SpearElderbark",
                "SpearFlint",
                "SpearWolfFang",
                "SwordBlackmetal",
                "SwordBronze",
                "SwordCheat",
                "SwordIron",
                "SwordIronFire",
                "SwordSilver",
                "Tankard",
                "TankardOdin",
                "Club",
                "Torch",
            }),
            new Tuple<string, string[]>("Tools", new string[]
            {
                "AxeBronze",
                "AxeFlint",
                "AxeIron",
                "AxeStone",
                "AxeBlackMetal",
                "PickaxeAntler",
                "PickaxeBronze",
                "PickaxeIron",
                "PickaxeStone",
                "Cultivator",
                "FishingRod",
                "Hammer",
                "Hoe",
                "SpearChitin",
            }),
            new Tuple<string, string[]>("Trinkets", new string[]
            {
                "BeltStrength",
                "Wishbone",
                "HelmetDverger",
            }),
            new Tuple<string, string[]>("Food", new string[]
            {
                "Bread",
                "BloodPudding",
                "Blueberries",
                "Carrot",
                "CarrotSoup",
                "Cloudberry",
                "CookedLoxMeat",
                "CookedMeat",
                "FishCooked",
                "FishWraps",
                "Honey",
                "LoxPie",
                "Mushroom",
                "MushroomBlue",
                "MushroomYellow",
                "NeckTailGrilled",
                "Raspberry",
                "QueensJam",
                "Sausages",
                "SerpentMeatCooked",
                "SerpentStew",
                "Turnip",
                "TurnipStew",
            }),
            new Tuple<string, string[]>("Mead", new string[]
            {
                "MeadBaseFrostResist",
                "MeadBaseHealthMedium",
                "MeadBaseHealthMinor",
                "MeadBasePoisonResist",
                "MeadBaseStaminaMedium",
                "MeadBaseStaminaMinor",
                "MeadBaseTasty",
                "MeadFrostResist",
                "MeadHealthMedium",
                "MeadHealthMinor",
                "MeadPoisonResist",
                "MeadStaminaMedium",
                "MeadStaminaMinor",
                "MeadTasty",
            }),
            new Tuple<string, string[]>("Trophies", new string[]
            {
                "TrophyBlob",
                "TrophyBoar",
                "TrophyBonemass",
                "TrophyDeathsquito",
                "TrophyDeer",
                "TrophyDragonQueen",
                "TrophyDraugr",
                "TrophyDraugrElite",
                "TrophyDraugrFem",
                "TrophyEikthyr",
                "TrophyFenring",
                "TrophyForestTroll",
                "TrophyFrostTroll",
                "TrophyGoblin",
                "TrophyGoblinBrute",
                "TrophyGoblinKing",
                "TrophyGoblinShaman",
                "TrophyGreydwarf",
                "TrophyGreydwarfBrute",
                "TrophyGreydwarfShaman",
                "TrophyHatchling",
                "TrophyLeech",
                "TrophyLox",
                "TrophyNeck",
                "TrophySerpent",
                "TrophySGolem",
                "TrophySkeleton",
                "TrophySkeletonPoison",
                "TrophySurtling",
                "TrophyTheElder",
                "TrophyWolf",
                "TrophyWraith",
            }),
        };

        private InventoryProp _editingItem = null;
        public InventoryProp EditingItem
        {
            get => _editingItem;
            set
            {
                if (SetProperty(ref _editingItem, value))
                {
                    RaisePropertyChanged(nameof(EditingItemName));
                    RaisePropertyChanged(nameof(OtherEditingItemProps));
                }
            }
        }

        public string EditingItemName => EditingItem?.Value["name"].GetValue<string>() ?? null;


        public Prop[] OtherEditingItemProps
        {
            get
            {
                if (EditingItem == null)
                    return new Prop[0];
                var props = (Dictionary<string, Prop>)EditingItem.Value;
                return new Prop[]
                {
                    props["quality"],
                    props["stack"],
                    props["durability"],
                    props["crafter_id"],
                    props["crafter_name"]
                };
            }
        }

        public IEnumerable<string> ItemCategories => _itemsWithCategories.Select(c => c.Item1);
        public ObservableCollection<string> ItemsInSelectedCategory { get; } = new ObservableCollection<string>();

        private string _selectedCategory;
        public string SelectedCategory
        {
            get => _selectedCategory;
            set
            {
                if (SetProperty(ref _selectedCategory, value))
                {
                    ItemsInSelectedCategory.Clear();
                    foreach (var item in _itemsWithCategories.FirstOrDefault(c => c.Item1 == value)?.Item2 ?? new string[0])
                        ItemsInSelectedCategory.Add(item);
                }
            }
        }

        private string _selectedItem = null;
        public string SelectedItem
        {
            get => _selectedItem;
            set => SetProperty(ref _selectedItem, value);
        }



        private void ChangeItem()
        {
            var name = EditingItem.Value["name"].GetValue<string>();
            SelectedCategory = _itemsWithCategories.FirstOrDefault(c => c.Item2.Contains(name))?.Item1 ?? null;
            SelectedItem = name ?? "";
        }

        private void EndChangeItem()
        {
            EditingItem.Value["name"].SetValue(SelectedItem);
            SelectedItem = null;
            if (EditingItemName != null)
            {
                EditingItem.Value["quality"].SetValue(1);
                EditingItem.Value["stack"].SetValue(1);
                EditingItem.Value["durability"].SetValue(1);
                EditingItem.Value["crafter_name"].SetValue("Cheater");
            }
            RaisePropertyChanged(nameof(EditingItemName));
            RaisePropertyChanged(nameof(OtherEditingItemProps));
        }

    }
}
