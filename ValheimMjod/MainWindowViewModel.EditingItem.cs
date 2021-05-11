using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ValheimMjod
{
    public partial class MainWindowViewModel
    {
        public static Tuple<string, string[]>[] _itemsWithCategories { get; } = new Tuple<string, string[]>[]
        {
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
                "draugr_arrow",
                "FishingBait",
            }),
            new Tuple<string, string[]>("Bow", new string[]
            {
                "Bow",
                "BowDraugrFang",
                "BowFineWood",
                "BowHuntsman",
                "draugr_bow",
                "skeleton_bow",
            }),
            new Tuple<string, string[]>("Chest", new string[]
            {
                "ArmorBronzeChest",
                "ArmorIronChest",
                "ArmorLeatherChest",
                "ArmorPaddedCuirass",
                "ArmorRagsChest",
                "ArmorTrollLeatherChest",
                "ArmorWolfChest",
                "GoblinArmband",
                "GoblinBrute_ArmGuard",
                "GoblinHelmet",
                "GoblinLegband",
                "GoblinShoulders",
                "StoneGolem_clubs",
                "StoneGolem_spikes",
            }),
            new Tuple<string, string[]>("Consumable", new string[]
            {
                "BarleyWine",
                "BloodPudding",
                "Blueberries",
                "Bread",
                "Carrot",
                "CarrotSoup",
                "Cloudberry",
                "CookedLoxMeat",
                "CookedMeat",
                "FishCooked",
                "FishWraps",
                "HealthUpgrade_Bonemass",
                "HealthUpgrade_GDKing",
                "Honey",
                "LoxPie",
                "MeadFrostResist",
                "MeadHealthMedium",
                "MeadHealthMinor",
                "MeadPoisonResist",
                "MeadStaminaMedium",
                "MeadStaminaMinor",
                "MeadTasty",
                "Mushroom",
                "MushroomBlue",
                "MushroomYellow",
                "NeckTailGrilled",
                "QueensJam",
                "Raspberry",
                "Sausages",
                "SerpentMeatCooked",
                "SerpentStew",
                "StaminaUpgrade_Greydwarf",
                "StaminaUpgrade_Troll",
                "StaminaUpgrade_Wraith",
                "TurnipStew",
            }),
            new Tuple<string, string[]>("Helmet", new string[]
            {
                "GoblinBrute_Backbones",
                "GoblinBrute_ExecutionerCap",
                "GoblinShaman_Headdress_antlers",
                "GoblinShaman_Headdress_feathers",
                "HelmetBronze",
                "HelmetDrake",
                "HelmetDverger",
                "HelmetIron",
                "HelmetLeather",
                "HelmetOdin",
                "HelmetPadded",
                "HelmetTrollLeather",
                "HelmetYule",
                "StoneGolem_hat",
            }),
            new Tuple<string, string[]>("Legs", new string[]
            {
                "ArmorBronzeLegs",
                "ArmorIronLegs",
                "ArmorLeatherLegs",
                "ArmorPaddedGreaves",
                "ArmorRagsLegs",
                "ArmorTrollLeatherLegs",
                "ArmorWolfLegs",
                "GoblinBrute_HipCloth",
                "GoblinLoin",
            }),
            new Tuple<string, string[]>("Material", new string[]
            {
                "Amber",
                "AmberPearl",
                "AncientSeed",
                "Barley",
                "BarleyFlour",
                "BarleyWineBase",
                "BeechSeeds",
                "BlackMetal",
                "BlackMetalScrap",
                "Bloodbag",
                "BoneFragments",
                "Bronze",
                "BronzeNails",
                "CarrotSeeds",
                "Chain",
                "Chitin",
                "Coal",
                "Coins",
                "Copper",
                "CopperOre",
                "Crystal",
                "Dandelion",
                "DeerHide",
                "DragonTear",
                "ElderBark",
                "Entrails",
                "Feathers",
                "FineWood",
                "FirCone",
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
                "MeadBaseFrostResist",
                "MeadBaseHealthMedium",
                "MeadBaseHealthMinor",
                "MeadBasePoisonResist",
                "MeadBaseStaminaMedium",
                "MeadBaseStaminaMinor",
                "MeadBaseTasty",
                "NeckTail",
                "Needle",
                "Obsidian",
                "Ooze",
                "PineCone",
                "QueenBee",
                "RawMeat",
                "Resin",
                "RoundLog",
                "Ruby",
                "SerpentMeat",
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
                "Turnip",
                "TurnipSeeds",
                "VegvisirShard_Bonemass",
                "WitheredBone",
                "WolfFang",
                "WolfPelt",
                "Wood",
                "YagluthDrop",
                "YmirRemains",
            }),
            new Tuple<string, string[]>("Misc", new string[]
            {
                "CryptKey",
                "DragonEgg",
                "GoblinTotem",
            }),
            new Tuple<string, string[]>("One-Handed Weapon", new string[]
            {
                "AxeBlackMetal",
                "AxeBronze",
                "AxeFlint",
                "AxeIron",
                "AxeStone",
                "BombOoze",
                "Club",
                "Deathsquito_sting",
                "draugr_axe",
                "draugr_sword",
                "GoblinClub",
                "GoblinSpear",
                "GoblinSword",
                "GoblinTorch",
                "KnifeBlackMetal",
                "KnifeChitin",
                "KnifeCopper",
                "KnifeFlint",
                "MaceBronze",
                "MaceIron",
                "MaceNeedle",
                "MaceSilver",
                "skeleton_mace",
                "skeleton_sword",
                "SpearBronze",
                "SpearChitin",
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
            }),
            new Tuple<string, string[]>("Shield", new string[]
            {
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
            }),
            new Tuple<string, string[]>("Shoulder", new string[]
            {
                "CapeDeerHide",
                "CapeLinen",
                "CapeLox",
                "CapeOdin",
                "CapeTest",
                "CapeTrollHide",
                "CapeWolf",
                "GoblinBrute_ShoulderGuard",
            }),
            new Tuple<string, string[]>("Tool", new string[]
            {
                "Cultivator",
                "Hammer",
                "Hoe",
            }),
            new Tuple<string, string[]>("Torch", new string[]
            {
                "Torch",
            }),
            new Tuple<string, string[]>("Trophy", new string[]
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
            new Tuple<string, string[]>("Two-Handed Weapon", new string[]
            {
                "AtgeirBlackmetal",
                "AtgeirBronze",
                "AtgeirIron",
                "Battleaxe",
                "FishingRod",
                "PickaxeAntler",
                "PickaxeBronze",
                "PickaxeIron",
                "PickaxeStone",
                "SledgeIron",
                "SledgeStagbreaker",
            }),
            new Tuple<string, string[]>("Utility", new string[]
            {
                "BeltStrength",
                "GoblinBrute_LegBones",
                "GoblinShaman_Staff_Bones",
                "GoblinShaman_Staff_Feathers",
                "Wishbone",
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
