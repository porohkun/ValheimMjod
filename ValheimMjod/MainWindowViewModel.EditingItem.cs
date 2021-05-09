﻿using System;
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
            new Tuple<string, string[]>("Enemies", new string[]
            {
                "Blob",
                "BlobElite",
                "Boar",
                "Boar_piggy",
                "Crow",
                "Deathsquito",
                "Deer",
                "Draugr",
                "Draugr_Elite",
                "Draugr_Ranged",
                "Fenring",
                "Ghost",
                "Goblin",
                "GoblinArcher",
                "GoblinBrute",
                "GoblinClub",
                "GoblinHelmet",
                "GoblinLegband",
                "GoblinLoin",
                "GoblinShaman",
                "GoblinShoulders",
                "GoblinSpear",
                "GoblinSword",
                "GoblinTorch",
                "GoblinTotem",
                "Greydwarf",
                "Greydwarf_Elite",
                "Greydwarf_Root",
                "Greydwarf_Shaman",
                "Greyling",
                "Leech",
                "Lox",
                "Neck",
                "Seagal",
                "Serpent",
                "Skeleton",
                "Skeleton_Poison",
                "StoneGolem",
                "Surtling",
                "Troll",
                "Valkyrie",
                "Wolf",
                "Wolf_cub",
                "Wraith",
            }),
            new Tuple<string, string[]>("Bosses", new string[]
            {
                "Eikthyr",
                "gd_king",
                "Bonemass",
                "Dragon",
                "GoblinKing",
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
            new Tuple<string, string[]>("Vehicle", new string[]
            {
                "Cart",
                "Raft",
                "Karve",
                "VikingShip",
                "Trailership",
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
            new Tuple<string, string[]>("Misc/Unsorted", new string[]
            {
                "GlowingMushroom",
                "ancientbarkspear_projectile",
                "aoe_nova",
                "barrell",
                "bed",
                "bee_aoe",
                "beech_log",
                "beech_log_half",
                "Beech_Sapling",
                "Beech_small1",
                "Beech_small2",
                "Beech_Stub",
                "Beech1",
                "Beehive",
                "Birch_log",
                "Birch_log_half",
                "Birch1",
                "Birch1_aut",
                "Birch2",
                "Birch2_aut",
                "BirchStub",
                "blastfurnace",
                "blob_aoe",
                "BlueberryBush",
                "boar_ragdoll",
                "BombOoze",
                "bonemass_aoe",
                "bonemass_throw_projectile",
                "bonfire",
                "BossStone_Bonemass",
                "BossStone_DragonQueen",
                "BossStone_Eikthyr",
                "BossStone_TheElder",
                "BossStone_Yagluth",
                "bow_projectile",
                "bow_projectile_fire",
                "bow_projectile_frost",
                "bow_projectile_needle",
                "bow_projectile_poison",
                "bronzespear_projectile",
                "bucket",
                "Bush01",
                "Bush01_heath",
                "Bush02_en",
                "CargoCrate",
                "CastleKit_braided_box01",
                "CastleKit_groundtorch",
                "CastleKit_groundtorch_green",
                "CastleKit_groundtorch_unlit",
                "CastleKit_pot03",
                "Chain",
                "charcoal_kiln",
                "Chest",
                "CloudberryBush",
                "cultivate",
                "dead_deer",
                "Deathsquito_sting",
                "deer_ragdoll",
                "DeerGodExplosion",
                "DG_Cave",
                "DG_ForestCrypt",
                "DG_GoblinCamp",
                "DG_MeadowsFarm",
                "DG_MeadowsVillage",
                "DG_SunkenCrypt",
                "digg",
                "digg_v2",
                "dragon_ice_projectile",
                "draugr_arrow",
                "draugr_bow_projectile",
                "Draugr_elite_ragdoll",
                "Draugr_ranged_ragdoll",
                "Draugr_ragdoll",
                "draugr_axe",
                "draugr_bow",
                "draugr_sword",
                "eikthyr_ragdoll",
                "dragoneggcup",
                "dungeon_forestcrypt_door",
                "dungeon_sunkencrypt_irongate",
                "eventzone_bonemass",
                "eventzone_eikthyr",
                "eventzone_gdking",
                "eventzone_goblinking",
                "eventzone_moder",
                "EvilHeart_Forest",
                "EvilHeart_Swamp",
                "Fenring_ragdoll",
                "fermenter",
                "fire_pit",
                "FireFlies",
                "FirTree",
                "FirTree_log",
                "FirTree_log_half",
                "FirTree_oldLog",
                "FirTree_Sapling",
                "FirTree_small",
                "FirTree_small_dead",
                "FirTree_Stub",
                "Fish1",
                "Fish2",
                "Fish3",
                "Fish4_cave",
                "FishingRodFloat",
                "FishingRodFloatProjectile",
                "Flies",
                "flintspear_projectile",
                "ForestTroll_ragdoll",
                "forge",
                "forge_ext1",
                "forge_ext2",
                "forge_ext3",
                "forge_ext4",
                "forge_ext5",
                "forge_ext6",
                "gdking_Ragdoll",
                "gdking_root_projectile",
                "goblin_banner",
                "goblin_bed",
                "Goblin_Dragdoll",
                "goblin_fence",
                "goblin_pole",
                "goblin_pole_small",
                "goblin_roof_45d",
                "goblin_roof_45d_corner",
                "goblin_roof_cap",
                "goblin_stairs",
                "goblin_stepladder",
                "goblin_totempole",
                "goblin_woodwall_1m",
                "goblin_woodwall_2m",
                "goblin_woodwall_2m_ribs",
                "GoblinArmband",
                "GoblinBrute_ArmGuard",
                "GoblinBrute_Attack",
                "GoblinBrute_Backbones",
                "GoblinBrute_ExecutionerCap",
                "GoblinBrute_HipCloth",
                "GoblinBrute_LegBones",
                "GoblinBrute_ragdoll",
                "GoblinBrute_RageAttack",
                "GoblinBrute_ShoulderGuard",
                "GoblinBrute_Taunt",
                "GoblinKing_ragdoll",
                "goblinking_totemholder",
                "GoblinShaman_attack_poke",
                "GoblinShaman_Headdress_antlers",
                "GoblinShaman_Headdress_feathers",
                "GoblinShaman_projectile_fireball",
                "GoblinShaman_protect_aoe",
                "GoblinShaman_ragdoll",
                "GoblinShaman_Staff_Bones",
                "GoblinShaman_Staff_Feathers",
                "GoblinSpear_projectile",
                "Greydwarf_elite_ragdoll",
                "Greydwarf_ragdoll",
                "Greydwarf_Shaman_ragdoll",
                "Greydwarf_throw_projectile",
                "Greyling_ragdoll",
                "guard_stone",
                "guard_stone_test",
                "GuckSack",
                "GuckSack_small",
                "Hatchling",
                "hatchling_cold_projectile",
                "Hatchling_ragdoll",
                "HealthUpgrade_Bonemass",
                "HealthUpgrade_GDKing",
                "hearth",
                "HeathRockPillar",
                "HeathRockPillar_frac",
                "highstone",
                "highstone_frac",
                "horizontal_web",
                "HugeRoot1",
                "ice_rock1",
                "ice_rock1_frac",
                "ice1",
                "IceBlocker",
                "Imp_fireball_projectile",
                "iron_grate",
                "itemstand",
                "itemstandh",
                "Leech_cave",
                "Leviathan",
                "LocationProxy",
                "loot_chest_stone",
                "loot_chest_wood",
                "LootSpawner_pineforest",
                "lox_ragdoll",
                "lox_stomp_aoe_OLD",
                "marker01",
                "marker02",
                "MineRock_Copper",
                "MineRock_Iron",
                "MineRock_Meteorite",
                "MineRock_Obsidian",
                "MineRock_Stone",
                "MineRock_Tin",
                "MountainGraveStone01",
                "mud_road",
                "mudpile",
                "mudpile_beacon",
                "mudpile_frac",
                "mudpile_old",
                "mudpile2",
                "mudpile2_frac",
                "Neck_Ragdoll",
                "Oak_log",
                "Oak_log_half",
                "Oak1",
                "OakStub",
                "odin",
                "OLD_wood_roof",
                "OLD_wood_roof_icorner",
                "OLD_wood_roof_ocorner",
                "OLD_wood_roof_top",
                "OLD_wood_wall_roof",
                "oozebomb_explosion",
                "oozebomb_projectile",
                "path",
                "paved_road",
                "Pickable_Barley",
                "Pickable_Barley_Wild",
                "Pickable_BogIronOre",
                "Pickable_Branch",
                "Pickable_Carrot",
                "Pickable_Dandelion",
                "Pickable_DolmenTreasure",
                "Pickable_DragonEgg",
                "Pickable_Flax",
                "Pickable_Flax_Wild",
                "Pickable_Flint",
                "Pickable_ForestCryptRandom",
                "Pickable_ForestCryptRemains01",
                "Pickable_ForestCryptRemains02",
                "Pickable_ForestCryptRemains03",
                "Pickable_ForestCryptRemains04",
                "Pickable_Item",
                "Pickable_Meteorite",
                "Pickable_MountainRemains01_buried",
                "Pickable_Mushroom",
                "Pickable_Mushroom_blue",
                "Pickable_Mushroom_yellow",
                "Pickable_Obsidian",
                "Pickable_RandomFood",
                "Pickable_SeedCarrot",
                "Pickable_SeedTurnip",
                "Pickable_Stone",
                "Pickable_SunkenCryptRandom",
                "Pickable_SurtlingCoreStand",
                "Pickable_Thistle",
                "Pickable_Tin",
                "Pickable_Turnip",
                "piece_artisanstation",
                "piece_banner01",
                "piece_banner02",
                "piece_banner03",
                "piece_banner04",
                "piece_banner05",
                "piece_bed02",
                "piece_beehive",
                "piece_bench01",
                "piece_brazierceiling01",
                "piece_cauldron",
                "piece_chair",
                "piece_chair02",
                "piece_chest",
                "piece_chest_private",
                "piece_chest_wood",
                "piece_cookingstation",
                "piece_gift1",
                "piece_gift2",
                "piece_gift3",
                "piece_groundtorch",
                "piece_groundtorch_green",
                "piece_groundtorch_wood",
                "piece_jackoturnip",
                "piece_maypole",
                "piece_sharpstakes",
                "piece_spinningwheel",
                "piece_stonecutter",
                "piece_table",
                "piece_throne01",
                "piece_walltorch",
                "piece_workbench",
                "piece_workbench_ext1",
                "piece_workbench_ext2",
                "piece_workbench_ext3",
                "piece_workbench_ext4",
                "piece_xmastree",
                "PineTree",
                "Pinetree_01",
                "Pinetree_01_Stub",
                "PineTree_log",
                "PineTree_log_half",
                "PineTree_log_halfOLD",
                "PineTree_logOLD",
                "PineTree_Sapling",
                "Player",
                "Player_ragdoll",
                "Player_ragdoll_old",
                "Player_tombstone",
                "PlayerUnarmed",
                "portal",
                "portal_wood",
                "projectile_beam",
                "projectile_chitinharpoon",
                "projectile_meteor",
                "projectile_wolffang",
                "raise",
                "RaspberryBush",
                "replant",
                "Rock_3",
                "Rock_3_frac",
                "Rock_4",
                "Rock_4_plains",
                "Rock_7",
                "Rock_destructible",
                "Rock_destructible_test",
                "rock1_mountain",
                "rock1_mountain_frac",
                "rock2_heath",
                "rock2_heath_frac",
                "rock2_mountain",
                "rock2_mountain_frac",
                "rock3_mountain",
                "rock3_mountain_frac",
                "rock3_silver",
                "rock3_silver_frac",
                "rock4_coast",
                "rock4_coast_frac",
                "rock4_copper",
                "rock4_copper_frac",
                "rock4_forest",
                "rock4_forest_frac",
                "rock4_heath",
                "rock4_heath_frac",
                "RockFinger",
                "RockFinger_frac",
                "RockFingerBroken",
                "RockFingerBroken_frac",
                "rockformation1",
                "RockThumb",
                "RockThumb_frac",
                "root07",
                "root08",
                "root11",
                "root12",
                "rug_deer",
                "rug_fur",
                "rug_wolf",
                "sapling_barley",
                "sapling_carrot",
                "sapling_flax",
                "sapling_seedcarrot",
                "sapling_seedturnip",
                "sapling_turnip",
                "shaman_attack_aoe",
                "shaman_heal_aoe",
                "ship_construction",
                "shipwreck_karve_bottomboards",
                "shipwreck_karve_bow",
                "shipwreck_karve_chest",
                "shipwreck_karve_dragonhead",
                "shipwreck_karve_stern",
                "shipwreck_karve_sternpost",
                "shrub_2",
                "shrub_2_heath",
                "sign",
                "sign_notext",
                "silvervein",
                "silvervein_frac",
                "skeleton_bow",
                "skeleton_mace",
                "Skeleton_NoArcher",
                "skeleton_sword",
                "Skull1",
                "Skull2",
                "sledge_aoe",
                "smelter",
                "stake_wall",
                "StaminaUpgrade_Greydwarf",
                "StaminaUpgrade_Troll",
                "StaminaUpgrade_Wraith",
                "StatueCorgi",
                "StatueDeer",
                "StatueEvil",
                "StatueHare",
                "StatueSeed",
                "stone_arch",
                "stone_floor",
                "stone_floor_2x2",
                "stone_pile",
                "stone_pillar",
                "stone_stair",
                "stone_wall_1x1",
                "stone_wall_2x1",
                "stone_wall_4x2",
                "stoneblock_fracture",
                "stonechest",
                "stonegolem_attack1_spike",
                "StoneGolem_clubs",
                "StoneGolem_hat",
                "Stonegolem_ragdoll",
                "StoneGolem_spikes",
                "stubbe",
                "stubbe_spawner",
                "sunken_crypt_gate",
                "SwampTree1",
                "SwampTree1_log",
                "SwampTree1_Stub",
                "SwampTree2",
                "SwampTree2_darkland",
                "SwampTree2_log",
                "TentaRoot",
                "tentaroot_attack",
                "tolroko_flyer",
                "TrainingDummy",
                "TreasureChest_blackforest",
                "TreasureChest_fCrypt",
                "TreasureChest_forestcrypt",
                "TreasureChest_heath",
                "TreasureChest_meadows",
                "TreasureChest_meadows_buried",
                "TreasureChest_mountains",
                "TreasureChest_plains_stone",
                "TreasureChest_sunkencrypt",
                "TreasureChest_swamp",
                "TreasureChest_trollcave",
                "troll_groundslam_aoe",
                "troll_log_swing_h",
                "troll_log_swing_v",
                "Troll_ragdoll",
                "troll_throw_projectile",
                "tunnel_web",
                "turf_roof",
                "turf_roof_top",
                "turf_roof_wall",
                "VegvisirShard_Bonemass",
                "vertical_web",
                "vines",
                "widestone",
                "widestone_frac",
                "windmill",
                "Wolf_Ragdoll",
                "wood_beam",
                "wood_beam_1",
                "wood_beam_26",
                "wood_beam_45",
                "wood_door",
                "wood_dragon1",
                "wood_fence",
                "wood_floor",
                "wood_floor_1x1",
                "wood_gate",
                "wood_ledge",
                "wood_pole",
                "wood_pole_log",
                "wood_pole_log_4",
                "wood_pole2",
                "wood_roof",
                "wood_roof_45",
                "wood_roof_icorner",
                "wood_roof_icorner_45",
                "wood_roof_ocorner",
                "wood_roof_ocorner_45",
                "wood_roof_top",
                "wood_roof_top_45",
                "wood_stack",
                "wood_stair",
                "wood_stepladder",
                "wood_wall_half",
                "wood_wall_log",
                "wood_wall_log_4x0.5",
                "wood_wall_roof",
                "wood_wall_roof_45",
                "wood_wall_roof_top",
                "wood_wall_roof_top_45",
                "woodiron_beam",
                "woodiron_pole",
                "woodwall",
                "wraith_melee",
            }),
        };

        private Prop _editingItem = null;
        public Prop EditingItem
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

        public string EditingItemName => (EditingItem?.Value as Dictionary<string, Prop>)?["name"].Value as string ?? null;


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
            var props = (Dictionary<string, Prop>)EditingItem.Value;
            var name = props["name"].Value as string;
            SelectedCategory = _itemsWithCategories.FirstOrDefault(c => c.Item2.Contains(name))?.Item1 ?? null;
            SelectedItem = name ?? "";
        }

        private void EndChangeItem()
        {
            var props = (Dictionary<string, Prop>)EditingItem.Value;
            props["name"].Value = SelectedItem;
            SelectedItem = null;
            if (EditingItemName != null)
            {
                props["quality"].Value = 1;
                props["stack"].Value = 1;
                props["durability"].Value = 1;
                props["crafter_name"].Value = "Cheater";
            }
            RaisePropertyChanged(nameof(EditingItemName));
            RaisePropertyChanged(nameof(OtherEditingItemProps));
        }

    }
}