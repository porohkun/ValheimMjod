//using System;
//using System.Collections.Generic;
//using System.Linq;

//namespace Valheim
//{
//    public class Player : Humanoid
//    {

//        public void Save(ZPackage pkg)
//        {
//            pkg.Write(24);
//            pkg.Write(this.GetMaxHealth());
//            pkg.Write(this.GetHealth());
//            pkg.Write(this.GetMaxStamina());
//            pkg.Write(this.m_firstSpawn);
//            pkg.Write(this.m_timeSinceDeath);
//            pkg.Write(this.m_guardianPower);
//            pkg.Write(this.m_guardianPowerCooldown);
//            this.m_inventory.Save(pkg);
//            pkg.Write(this.m_knownRecipes.Count);
//            foreach (string knownRecipe in this.m_knownRecipes)
//                pkg.Write(knownRecipe);
//            pkg.Write(this.m_knownStations.Count);
//            foreach (KeyValuePair<string, int> knownStation in this.m_knownStations)
//            {
//                pkg.Write(knownStation.Key);
//                pkg.Write(knownStation.Value);
//            }
//            pkg.Write(this.m_knownMaterial.Count);
//            foreach (string data in this.m_knownMaterial)
//                pkg.Write(data);
//            pkg.Write(this.m_shownTutorials.Count);
//            foreach (string shownTutorial in this.m_shownTutorials)
//                pkg.Write(shownTutorial);
//            pkg.Write(this.m_uniques.Count);
//            foreach (string unique in this.m_uniques)
//                pkg.Write(unique);
//            pkg.Write(this.m_trophies.Count);
//            foreach (string trophy in this.m_trophies)
//                pkg.Write(trophy);
//            pkg.Write(this.m_knownBiome.Count);
//            foreach (Heightmap.Biome biome in this.m_knownBiome)
//                pkg.Write((int)biome);
//            pkg.Write(this.m_knownTexts.Count);
//            foreach (KeyValuePair<string, string> knownText in this.m_knownTexts)
//            {
//                pkg.Write(knownText.Key);
//                pkg.Write(knownText.Value);
//            }
//            pkg.Write(this.m_beardItem);
//            pkg.Write(this.m_hairItem);
//            pkg.Write(this.m_skinColor);
//            pkg.Write(this.m_hairColor);
//            pkg.Write(this.m_modelIndex);
//            pkg.Write(this.m_foods.Count);
//            foreach (Player.Food food in this.m_foods)
//            {
//                pkg.Write(food.m_name);
//                pkg.Write(food.m_health);
//                pkg.Write(food.m_stamina);
//            }
//            this.m_skills.Save(pkg);
//        }

//        public void Load(ZPackage pkg)
//        {
//            this.m_isLoading = true;
//            this.UnequipAllItems();
//            int num1 = pkg.ReadInt();
//            if (num1 >= 7)
//                this.SetMaxHealth(pkg.ReadSingle(), false);
//            float num2 = pkg.ReadSingle();
//            float maxHealth = this.GetMaxHealth();
//            if ((double)num2 <= 0.0 || (double)num2 > (double)maxHealth || float.IsNaN(num2))
//                num2 = maxHealth;
//            this.SetHealth(num2);
//            if (num1 >= 10)
//            {
//                float stamina = pkg.ReadSingle();
//                this.SetMaxStamina(stamina, false);
//                this.m_stamina = stamina;
//            }
//            if (num1 >= 8)
//                this.m_firstSpawn = pkg.ReadBool();
//            if (num1 >= 20)
//                this.m_timeSinceDeath = pkg.ReadSingle();
//            if (num1 >= 23)
//                this.SetGuardianPower(pkg.ReadString());
//            if (num1 >= 24)
//                this.m_guardianPowerCooldown = pkg.ReadSingle();
//            if (num1 == 2)
//                pkg.ReadZDOID();
//            this.m_inventory.Load(pkg);
//            int num3 = pkg.ReadInt();
//            for (int index = 0; index < num3; ++index)
//                this.m_knownRecipes.Add(pkg.ReadString());
//            if (num1 < 15)
//            {
//                int num4 = pkg.ReadInt();
//                for (int index = 0; index < num4; ++index)
//                    pkg.ReadString();
//            }
//            else
//            {
//                int num4 = pkg.ReadInt();
//                for (int index = 0; index < num4; ++index)
//                    this.m_knownStations.Add(pkg.ReadString(), pkg.ReadInt());
//            }
//            int num5 = pkg.ReadInt();
//            for (int index = 0; index < num5; ++index)
//                this.m_knownMaterial.Add(pkg.ReadString());
//            if (num1 < 19 || num1 >= 21)
//            {
//                int num4 = pkg.ReadInt();
//                for (int index = 0; index < num4; ++index)
//                    this.m_shownTutorials.Add(pkg.ReadString());
//            }
//            if (num1 >= 6)
//            {
//                int num4 = pkg.ReadInt();
//                for (int index = 0; index < num4; ++index)
//                    this.m_uniques.Add(pkg.ReadString());
//            }
//            if (num1 >= 9)
//            {
//                int num4 = pkg.ReadInt();
//                for (int index = 0; index < num4; ++index)
//                    this.m_trophies.Add(pkg.ReadString());
//            }
//            if (num1 >= 18)
//            {
//                int num4 = pkg.ReadInt();
//                for (int index = 0; index < num4; ++index)
//                    this.m_knownBiome.Add((Heightmap.Biome)pkg.ReadInt());
//            }
//            if (num1 >= 22)
//            {
//                int num4 = pkg.ReadInt();
//                for (int index = 0; index < num4; ++index)
//                    this.m_knownTexts.Add(pkg.ReadString(), pkg.ReadString());
//            }
//            //=========================================
//            if (num1 >= 4)
//            {
//                string name = pkg.ReadString();
//                string hair = pkg.ReadString();
//                this.SetBeard(name);
//                this.SetHair(hair);
//            }
//            if (num1 >= 5)
//            {
//                Vector3 color1 = pkg.ReadVector3();
//                Vector3 color2 = pkg.ReadVector3();
//                this.SetSkinColor(color1);
//                this.SetHairColor(color2);
//            }
//            if (num1 >= 11)
//                this.SetPlayerModel(pkg.ReadInt());
//            if (num1 >= 12)
//            {
//                this.m_foods.Clear();
//                int num4 = pkg.ReadInt();
//                for (int index = 0; index < num4; ++index)
//                {
//                    if (num1 >= 14)
//                    {
//                        Player.Food food = new Player.Food();
//                        food.m_name = pkg.ReadString();
//                        food.m_health = pkg.ReadSingle();
//                        if (num1 >= 16)
//                            food.m_stamina = pkg.ReadSingle();
//                        GameObject itemPrefab = ObjectDB.instance.GetItemPrefab(food.m_name);
//                        if ((UnityEngine.Object)itemPrefab == (UnityEngine.Object)null)
//                        {
//                            ZLog.LogWarning((object)("FAiled to find food item " + food.m_name));
//                        }
//                        else
//                        {
//                            food.m_item = itemPrefab.GetComponent<ItemDrop>().m_itemData;
//                            this.m_foods.Add(food);
//                        }
//                    }
//                    else
//                    {
//                        pkg.ReadString();
//                        double num6 = (double)pkg.ReadSingle();
//                        double num7 = (double)pkg.ReadSingle();
//                        double num8 = (double)pkg.ReadSingle();
//                        double num9 = (double)pkg.ReadSingle();
//                        double num10 = (double)pkg.ReadSingle();
//                        double num11 = (double)pkg.ReadSingle();
//                        if (num1 >= 13)
//                        {
//                            double num12 = (double)pkg.ReadSingle();
//                        }
//                    }
//                }
//            }
//            if (num1 >= 17)
//                this.m_skills.Load(pkg);
//            this.m_isLoading = false;
//            this.UpdateAvailablePiecesList();
//            this.EquipIventoryItems();
//        }


//        #region decompiled

//        public static Player m_localPlayer = (Player)null;
//        private static List<Player> m_players = new List<Player>();
//        public static bool m_debugMode = false;
//        private static int crouching = 0;
//        protected static int m_attackMask = 0;
//        protected static int m_animatorTagDodge = Animator.StringToHash("dodge");
//        protected static int m_animatorTagCutscene = Animator.StringToHash("cutscene");
//        protected static int m_animatorTagCrouch = Animator.StringToHash("crouch");
//        protected static int m_animatorTagMinorAction = Animator.StringToHash("minoraction");
//        protected static int m_animatorTagEmote = Animator.StringToHash("emote");
//        [Header("Player")]
//        public float m_maxPlaceDistance = 5f;
//        public float m_maxInteractDistance = 5f;
//        public float m_scrollSens = 4f;
//        public float m_staminaRegen = 5f;
//        public float m_staminaRegenTimeMultiplier = 1f;
//        public float m_staminaRegenDelay = 1f;
//        public float m_runStaminaDrain = 10f;
//        public float m_sneakStaminaDrain = 5f;
//        public float m_swimStaminaDrainMinSkill = 5f;
//        public float m_swimStaminaDrainMaxSkill = 2f;
//        public float m_dodgeStaminaUsage = 10f;
//        public float m_weightStaminaFactor = 0.1f;
//        public float m_autoPickupRange = 2f;
//        public float m_maxCarryWeight = 300f;
//        public float m_encumberedStaminaDrain = 10f;
//        public float m_hardDeathCooldown = 10f;
//        public float m_baseCameraShake = 4f;
//        public EffectList m_drownEffects = new EffectList();
//        public EffectList m_spawnEffects = new EffectList();
//        public EffectList m_removeEffects = new EffectList();
//        public EffectList m_dodgeEffects = new EffectList();
//        public EffectList m_autopickupEffects = new EffectList();
//        public EffectList m_skillLevelupEffects = new EffectList();
//        public EffectList m_equipStartEffects = new EffectList();
//        private HashSet<string> m_knownRecipes = new HashSet<string>();
//        private Dictionary<string, int> m_knownStations = new Dictionary<string, int>();
//        private HashSet<string> m_knownMaterial = new HashSet<string>();
//        private HashSet<string> m_shownTutorials = new HashSet<string>();
//        private HashSet<string> m_uniques = new HashSet<string>();
//        private HashSet<string> m_trophies = new HashSet<string>();
//        private HashSet<Heightmap.Biome> m_knownBiome = new HashSet<Heightmap.Biome>();
//        private Dictionary<string, string> m_knownTexts = new Dictionary<string, string>();
//        private List<Player.Food> m_foods = new List<Player.Food>();
//        private float m_stamina = 100f;
//        private float m_maxStamina = 100f;
//        private string m_guardianPower = "";
//        private Player.PlacementStatus m_placementStatus = Player.PlacementStatus.Invalid;
//        private List<Player.EquipQueueData> m_equipQueue = new List<Player.EquipQueueData>();
//        private bool m_underRoof = true;
//        private Vector3 m_queuedDodgeDir = Vector3.zero;
//        private string m_emoteState = "";
//        private bool m_firstSpawn = true;
//        private string m_attachAnimation = "";
//        private Vector3 m_detachOffset = Vector3.zero;
//        private Vector3 m_skinColor = Vector3.one;
//        private Vector3 m_hairColor = Vector3.one;
//        private Vector3 m_lastStealthPosition = Vector3.zero;
//        private float m_wakeupTimer = -1f;
//        private float m_timeSinceDeath = 999999f;
//        private List<PieceTable> m_tempOwnedPieceTables = new List<PieceTable>();
//        private List<Transform> m_tempSnapPoints1 = new List<Transform>();
//        private List<Transform> m_tempSnapPoints2 = new List<Transform>();
//        private List<Piece> m_tempPieces = new List<Piece>();
//        private float m_rotatePieceTimer;
//        private float m_baseValueUpdatetimer;
//        private const int dataVersion = 24;
//        private float m_equipQueuePause;
//        public GameObject m_placeMarker;
//        public GameObject m_tombstone;
//        public GameObject m_valkyrie;
//        public Sprite m_textIcon;
//        private Skills m_skills;
//        private PieceTable m_buildPieces;
//        private bool m_noPlacementCost;
//        private bool m_hideUnavailable;
//        private float m_stationDiscoverTimer;
//        private bool m_debugFly;
//        private bool m_godMode;
//        private bool m_ghostMode;
//        private float m_lookPitch;
//        private const float m_baseHP = 25f;
//        private const float m_baseStamina = 75f;
//        private const int m_maxFoods = 3;
//        private const float m_foodDrainPerSec = 0.1f;
//        private float m_foodUpdateTimer;
//        private float m_foodRegenTimer;
//        private float m_staminaRegenTimer;
//        private float m_guardianPowerCooldown;
//        private StatusEffect m_guardianSE;
//        private GameObject m_placementMarkerInstance;
//        private GameObject m_placementGhost;
//        private int m_placeRotation;
//        private int m_placeRayMask;
//        private int m_placeGroundRayMask;
//        private int m_placeWaterRayMask;
//        private int m_removeRayMask;
//        private int m_interactMask;
//        private int m_autoPickupMask;
//        private GameObject m_hovering;
//        private Character m_hoveringCreature;
//        private float m_lastHoverInteractTime;
//        private bool m_pvp;
//        private float m_updateCoverTimer;
//        private float m_coverPercentage;
//        private float m_nearFireTimer;
//        private bool m_isLoading;
//        private float m_queuedAttackTimer;
//        private float m_queuedSecondAttackTimer;
//        private float m_queuedDodgeTimer;
//        private bool m_inDodge;
//        private bool m_dodgeInvincible;
//        private CraftingStation m_currentStation;
//        private Ragdoll m_ragdoll;
//        private Piece m_hoveringPiece;
//        private int m_emoteID;
//        private bool m_intro;
//        private bool m_crouchToggled;
//        private bool m_autoRun;
//        private bool m_safeInHome;
//        private ShipControlls m_shipControl;
//        private bool m_attached;
//        private bool m_sleeping;
//        private Transform m_attachPoint;
//        private int m_modelIndex;
//        private bool m_teleporting;
//        private bool m_distantTeleport;
//        private float m_teleportTimer;
//        private float m_teleportCooldown;
//        private Vector3 m_teleportFromPos;
//        private Quaternion m_teleportFromRot;
//        private Vector3 m_teleportTargetPos;
//        private Quaternion m_teleportTargetRot;
//        private Heightmap.Biome m_currentBiome;
//        private float m_biomeTimer;
//        private int m_baseValue;
//        private int m_comfortLevel;
//        private float m_drownDamageTimer;
//        private float m_timeSinceTargeted;
//        private float m_timeSinceSensed;
//        private float m_stealthFactorUpdateTimer;
//        private float m_stealthFactor;
//        private float m_stealthFactorTarget;
//        private float m_runSkillImproveTimer;
//        private float m_swimSkillImproveTimer;
//        private float m_sneakSkillImproveTimer;
//        private float m_equipmentMovementModifier;

//        protected override void Awake()
//        {
//            base.Awake();
//            Player.m_players.Add(this);
//            this.m_skills = this.GetComponent<Skills>();
//            this.SetupAwake();
//            if (this.m_nview.GetZDO() == null)
//                return;
//            this.m_placeRayMask = LayerMask.GetMask("Default", "static_solid", "Default_small", "piece", "piece_nonsolid", "terrain", "vehicle");
//            this.m_placeWaterRayMask = LayerMask.GetMask("Default", "static_solid", "Default_small", "piece", "piece_nonsolid", "terrain", "Water", "vehicle");
//            this.m_removeRayMask = LayerMask.GetMask("Default", "static_solid", "Default_small", "piece", "piece_nonsolid", "terrain", "vehicle");
//            this.m_interactMask = LayerMask.GetMask("item", "piece", "piece_nonsolid", "Default", "static_solid", "Default_small", "character", "character_net", "terrain", "vehicle");
//            this.m_autoPickupMask = LayerMask.GetMask("item");
//            this.m_inventory.m_onChanged += new Action(this.OnInventoryChanged);
//            if (Player.m_attackMask == 0)
//                Player.m_attackMask = LayerMask.GetMask("Default", "static_solid", "Default_small", "piece", "piece_nonsolid", "terrain", "character", "character_net", "character_ghost", "hitbox", "character_noenv", "vehicle");
//            if (Player.crouching == 0)
//                Player.crouching = ZSyncAnimation.GetHash("crouching");
//            this.m_nview.Register("OnDeath", new Action<long>(this.RPC_OnDeath));
//            if (!this.m_nview.IsOwner())
//                return;
//            this.m_nview.Register<int, string, int>("Message", new Action<long, int, string, int>(this.RPC_Message));
//            this.m_nview.Register<bool, bool>("OnTargeted", new Action<long, bool, bool>(this.RPC_OnTargeted));
//            this.m_nview.Register<float>("UseStamina", new Action<long, float>(this.RPC_UseStamina));
//            if ((bool)(UnityEngine.Object)MusicMan.instance)
//                MusicMan.instance.TriggerMusic("Wakeup");
//            this.UpdateKnownRecipesList();
//            this.UpdateAvailablePiecesList();
//            this.SetupPlacementGhost();
//        }

//        public void SetLocalPlayer()
//        {
//            if ((UnityEngine.Object)Player.m_localPlayer == (UnityEngine.Object)this)
//                return;
//            Player.m_localPlayer = this;
//            ZNet.instance.SetReferencePosition(this.transform.position);
//            EnvMan.instance.SetForceEnvironment("");
//        }

//        public void SetPlayerID(long playerID, string name)
//        {
//            if (this.m_nview.GetZDO() == null || this.GetPlayerID() != 0L)
//                return;
//            this.m_nview.GetZDO().Set(nameof(playerID), playerID);
//            this.m_nview.GetZDO().Set("playerName", name);
//        }

//        public long GetPlayerID()
//        {
//            return this.m_nview.IsValid() ? this.m_nview.GetZDO().GetLong("playerID", 0L) : 0L;
//        }

//        public string GetPlayerName()
//        {
//            return this.m_nview.IsValid() ? this.m_nview.GetZDO().GetString("playerName", "...") : "";
//        }

//        public override string GetHoverText()
//        {
//            return "";
//        }

//        public override string GetHoverName()
//        {
//            return this.GetPlayerName();
//        }

//        protected override void Start()
//        {
//            base.Start();
//            this.m_nview.GetZDO();
//        }

//        public override void OnDestroy()
//        {
//            ZDO zdo = this.m_nview.GetZDO();
//            if (zdo != null && (UnityEngine.Object)ZNet.instance != (UnityEngine.Object)null)
//                ZLog.LogWarning((object)("Player destroyed sec:" + (object)zdo.GetSector() + "  pos:" + (object)this.transform.position + "  zdopos:" + (object)zdo.GetPosition() + "  ref " + (object)ZNet.instance.GetReferencePosition()));
//            if ((bool)(UnityEngine.Object)this.m_placementGhost)
//            {
//                UnityEngine.Object.Destroy((UnityEngine.Object)this.m_placementGhost);
//                this.m_placementGhost = (GameObject)null;
//            }
//            base.OnDestroy();
//            Player.m_players.Remove(this);
//            if (!((UnityEngine.Object)Player.m_localPlayer == (UnityEngine.Object)this))
//                return;
//            ZLog.LogWarning((object)"Local player destroyed");
//            Player.m_localPlayer = (Player)null;
//        }

//        protected override void FixedUpdate()
//        {
//            base.FixedUpdate();
//            float fixedDeltaTime = Time.fixedDeltaTime;
//            this.UpdateAwake(fixedDeltaTime);
//            if (this.m_nview.GetZDO() == null)
//                return;
//            this.UpdateTargeted(fixedDeltaTime);
//            if (!this.m_nview.IsOwner())
//                return;
//            if ((UnityEngine.Object)Player.m_localPlayer != (UnityEngine.Object)this)
//            {
//                ZLog.Log((object)"Destroying old local player");
//                ZNetScene.instance.Destroy(this.gameObject);
//            }
//            else
//            {
//                if (this.IsDead())
//                    return;
//                this.UpdateEquipQueue(fixedDeltaTime);
//                this.PlayerAttackInput(fixedDeltaTime);
//                this.UpdateAttach();
//                this.UpdateShipControl(fixedDeltaTime);
//                this.UpdateCrouch(fixedDeltaTime);
//                this.UpdateDodge(fixedDeltaTime);
//                this.UpdateCover(fixedDeltaTime);
//                this.UpdateStations(fixedDeltaTime);
//                this.UpdateGuardianPower(fixedDeltaTime);
//                this.UpdateBaseValue(fixedDeltaTime);
//                this.UpdateStats(fixedDeltaTime);
//                this.UpdateTeleport(fixedDeltaTime);
//                this.AutoPickup(fixedDeltaTime);
//                this.EdgeOfWorldKill(fixedDeltaTime);
//                this.UpdateBiome(fixedDeltaTime);
//                this.UpdateStealth(fixedDeltaTime);
//                if ((bool)(UnityEngine.Object)GameCamera.instance && (double)Vector3.Distance(GameCamera.instance.transform.position, this.transform.position) < 2.0)
//                    this.SetVisible(false);
//                AudioMan.instance.SetIndoor(this.InShelter());
//            }
//        }

//        private void Update()
//        {
//            if (!this.m_nview.IsValid() || !this.m_nview.IsOwner())
//                return;
//            bool input = this.TakeInput();
//            this.UpdateHover();
//            if (input)
//            {
//                if (Player.m_debugMode && Console.instance.IsCheatsEnabled())
//                {
//                    if (Input.GetKeyDown(KeyCode.Z))
//                    {
//                        this.m_debugFly = !this.m_debugFly;
//                        this.m_nview.GetZDO().Set("DebugFly", this.m_debugFly);
//                        this.Message(MessageHud.MessageType.TopLeft, "Debug fly:" + this.m_debugFly.ToString(), 0, (Sprite)null);
//                    }
//                    if (Input.GetKeyDown(KeyCode.B))
//                    {
//                        this.m_noPlacementCost = !this.m_noPlacementCost;
//                        this.Message(MessageHud.MessageType.TopLeft, "No placement cost:" + this.m_noPlacementCost.ToString(), 0, (Sprite)null);
//                        this.UpdateAvailablePiecesList();
//                    }
//                    if (Input.GetKeyDown(KeyCode.K))
//                    {
//                        int num = 0;
//                        foreach (Character allCharacter in Character.GetAllCharacters())
//                        {
//                            if (!allCharacter.IsPlayer())
//                            {
//                                allCharacter.Damage(new HitData()
//                                {
//                                    m_damage = {
//                  m_damage = 99999f
//                }
//                                });
//                                ++num;
//                            }
//                        }
//                        this.Message(MessageHud.MessageType.TopLeft, "Killing all the monsters:" + (object)num, 0, (Sprite)null);
//                    }
//                }
//                if (ZInput.GetButtonDown("Use") || ZInput.GetButtonDown("JoyUse"))
//                {
//                    if ((bool)(UnityEngine.Object)this.m_hovering)
//                        this.Interact(this.m_hovering, false);
//                    else if ((bool)(UnityEngine.Object)this.m_shipControl)
//                        this.StopShipControl();
//                }
//                else if ((ZInput.GetButton("Use") || ZInput.GetButton("JoyUse")) && (bool)(UnityEngine.Object)this.m_hovering)
//                    this.Interact(this.m_hovering, true);
//                if (ZInput.GetButtonDown("Hide") || ZInput.GetButtonDown("JoyHide"))
//                {
//                    if (this.GetRightItem() != null || this.GetLeftItem() != null)
//                    {
//                        if (!this.InAttack())
//                            this.HideHandItems();
//                    }
//                    else if (!this.IsSwiming() || this.IsOnGround())
//                        this.ShowHandItems();
//                }
//                if (ZInput.GetButtonDown("ToggleWalk"))
//                {
//                    this.SetWalk(!this.GetWalk());
//                    if (this.GetWalk())
//                        this.Message(MessageHud.MessageType.TopLeft, "$msg_walk 1", 0, (Sprite)null);
//                    else
//                        this.Message(MessageHud.MessageType.TopLeft, "$msg_walk 0", 0, (Sprite)null);
//                }
//                if (ZInput.GetButtonDown("Sit") || !this.InPlaceMode() && ZInput.GetButtonDown("JoySit"))
//                {
//                    if (this.InEmote() && this.IsSitting())
//                        this.StopEmote();
//                    else
//                        this.StartEmote("sit", false);
//                }
//                if (ZInput.GetButtonDown("GPower") || ZInput.GetButtonDown("JoyGPower"))
//                    this.StartGuardianPower();
//                if (Input.GetKeyDown(KeyCode.Alpha1))
//                    this.UseHotbarItem(1);
//                if (Input.GetKeyDown(KeyCode.Alpha2))
//                    this.UseHotbarItem(2);
//                if (Input.GetKeyDown(KeyCode.Alpha3))
//                    this.UseHotbarItem(3);
//                if (Input.GetKeyDown(KeyCode.Alpha4))
//                    this.UseHotbarItem(4);
//                if (Input.GetKeyDown(KeyCode.Alpha5))
//                    this.UseHotbarItem(5);
//                if (Input.GetKeyDown(KeyCode.Alpha6))
//                    this.UseHotbarItem(6);
//                if (Input.GetKeyDown(KeyCode.Alpha7))
//                    this.UseHotbarItem(7);
//                if (Input.GetKeyDown(KeyCode.Alpha8))
//                    this.UseHotbarItem(8);
//            }
//            this.UpdatePlacement(input, Time.deltaTime);
//        }

//        private void UpdatePlacement(bool takeInput, float dt)
//        {
//            this.UpdateWearNTearHover();
//            if (this.InPlaceMode())
//            {
//                if (!takeInput)
//                    return;
//                this.UpdateBuildGuiInput();
//                if (Hud.IsPieceSelectionVisible())
//                    return;
//                ItemData rightItem = this.GetRightItem();
//                if ((ZInput.GetButtonDown("Remove") || ZInput.GetButtonDown("JoyRemove")) && rightItem.m_shared.m_buildPieces.m_canRemovePieces)
//                {
//                    if (this.HaveStamina(rightItem.m_shared.m_attack.m_attackStamina))
//                    {
//                        if (this.RemovePiece())
//                        {
//                            this.AddNoise(50f);
//                            this.UseStamina(rightItem.m_shared.m_attack.m_attackStamina);
//                            if (rightItem.m_shared.m_useDurability)
//                                rightItem.m_durability -= rightItem.m_shared.m_useDurabilityDrain;
//                        }
//                    }
//                    else
//                        Hud.instance.StaminaBarNoStaminaFlash();
//                }
//                if (ZInput.GetButtonDown("Attack") || ZInput.GetButtonDown("JoyPlace"))
//                {
//                    Piece selectedPiece = this.m_buildPieces.GetSelectedPiece();
//                    if ((UnityEngine.Object)selectedPiece != (UnityEngine.Object)null)
//                    {
//                        if (this.HaveStamina(rightItem.m_shared.m_attack.m_attackStamina))
//                        {
//                            if (selectedPiece.m_repairPiece)
//                                this.Repair(rightItem, selectedPiece);
//                            else if ((UnityEngine.Object)this.m_placementGhost != (UnityEngine.Object)null)
//                            {
//                                if (this.m_noPlacementCost || this.HaveRequirements(selectedPiece, Player.RequirementMode.CanBuild))
//                                {
//                                    if (this.PlacePiece(selectedPiece))
//                                    {
//                                        this.ConsumeResources(selectedPiece.m_resources, 0);
//                                        this.UseStamina(rightItem.m_shared.m_attack.m_attackStamina);
//                                        if (rightItem.m_shared.m_useDurability)
//                                            rightItem.m_durability -= rightItem.m_shared.m_useDurabilityDrain;
//                                    }
//                                }
//                                else
//                                    this.Message(MessageHud.MessageType.Center, "$msg_missingrequirement", 0, (Sprite)null);
//                            }
//                        }
//                        else
//                            Hud.instance.StaminaBarNoStaminaFlash();
//                    }
//                }
//                if ((double)Input.GetAxis("Mouse ScrollWheel") < 0.0)
//                    --this.m_placeRotation;
//                if ((double)Input.GetAxis("Mouse ScrollWheel") > 0.0)
//                    ++this.m_placeRotation;
//                float joyRightStickX = ZInput.GetJoyRightStickX();
//                if (ZInput.GetButton("JoyRotate") && (double)Mathf.Abs(joyRightStickX) > 0.5)
//                {
//                    if ((double)this.m_rotatePieceTimer == 0.0)
//                    {
//                        if ((double)joyRightStickX < 0.0)
//                            ++this.m_placeRotation;
//                        else
//                            --this.m_placeRotation;
//                    }
//                    else if ((double)this.m_rotatePieceTimer > 0.25)
//                    {
//                        if ((double)joyRightStickX < 0.0)
//                            ++this.m_placeRotation;
//                        else
//                            --this.m_placeRotation;
//                        this.m_rotatePieceTimer = 0.17f;
//                    }
//                    this.m_rotatePieceTimer += dt;
//                }
//                else
//                    this.m_rotatePieceTimer = 0.0f;
//            }
//            else
//            {
//                if (!(bool)(UnityEngine.Object)this.m_placementGhost)
//                    return;
//                this.m_placementGhost.SetActive(false);
//            }
//        }

//        private void UpdateBuildGuiInput()
//        {
//            if (Hud.instance.IsQuickPieceSelectEnabled())
//            {
//                if (!Hud.IsPieceSelectionVisible() && ZInput.GetButtonDown("BuildMenu"))
//                    Hud.instance.TogglePieceSelection();
//            }
//            else if (ZInput.GetButtonDown("BuildMenu"))
//                Hud.instance.TogglePieceSelection();
//            if (ZInput.GetButtonDown("JoyUse"))
//                Hud.instance.TogglePieceSelection();
//            if (!Hud.IsPieceSelectionVisible())
//                return;
//            if (Input.GetKeyDown(KeyCode.Escape) || ZInput.GetButtonDown("JoyButtonB"))
//                Hud.HidePieceSelection();
//            if (ZInput.GetButtonDown("JoyTabLeft") || ZInput.GetButtonDown("BuildPrev") || (double)Input.GetAxis("Mouse ScrollWheel") > 0.0)
//            {
//                this.m_buildPieces.PrevCategory();
//                this.UpdateAvailablePiecesList();
//            }
//            if (ZInput.GetButtonDown("JoyTabRight") || ZInput.GetButtonDown("BuildNext") || (double)Input.GetAxis("Mouse ScrollWheel") < 0.0)
//            {
//                this.m_buildPieces.NextCategory();
//                this.UpdateAvailablePiecesList();
//            }
//            if (ZInput.GetButtonDown("JoyLStickLeft"))
//            {
//                this.m_buildPieces.LeftPiece();
//                this.SetupPlacementGhost();
//            }
//            if (ZInput.GetButtonDown("JoyLStickRight"))
//            {
//                this.m_buildPieces.RightPiece();
//                this.SetupPlacementGhost();
//            }
//            if (ZInput.GetButtonDown("JoyLStickUp"))
//            {
//                this.m_buildPieces.UpPiece();
//                this.SetupPlacementGhost();
//            }
//            if (!ZInput.GetButtonDown("JoyLStickDown"))
//                return;
//            this.m_buildPieces.DownPiece();
//            this.SetupPlacementGhost();
//        }

//        public void SetSelectedPiece(Vector2Int p)
//        {
//            if (!(bool)(UnityEngine.Object)this.m_buildPieces || !(this.m_buildPieces.GetSelectedIndex() != p))
//                return;
//            this.m_buildPieces.SetSelected(p);
//            this.SetupPlacementGhost();
//        }

//        public Piece GetPiece(Vector2Int p)
//        {
//            return (bool)(UnityEngine.Object)this.m_buildPieces ? this.m_buildPieces.GetPiece(p) : (Piece)null;
//        }

//        public bool IsPieceAvailable(Piece piece)
//        {
//            return (bool)(UnityEngine.Object)this.m_buildPieces && this.m_buildPieces.IsPieceAvailable(piece);
//        }

//        public Piece GetSelectedPiece()
//        {
//            return (bool)(UnityEngine.Object)this.m_buildPieces ? this.m_buildPieces.GetSelectedPiece() : (Piece)null;
//        }

//        private void LateUpdate()
//        {
//            if (!this.m_nview.IsValid())
//                return;
//            this.UpdateEmote();
//            if (!this.m_nview.IsOwner())
//                return;
//            ZNet.instance.SetReferencePosition(this.transform.position);
//            this.UpdatePlacementGhost(false);
//        }

//        private void SetupAwake()
//        {
//            if (this.m_nview.GetZDO() == null)
//            {
//                this.m_animator.SetBool("wakeup", false);
//            }
//            else
//            {
//                bool flag = this.m_nview.GetZDO().GetBool("wakeup", true);
//                this.m_animator.SetBool("wakeup", flag);
//                if (!flag)
//                    return;
//                this.m_wakeupTimer = 0.0f;
//            }
//        }

//        private void UpdateAwake(float dt)
//        {
//            if ((double)this.m_wakeupTimer < 0.0)
//                return;
//            this.m_wakeupTimer += dt;
//            if ((double)this.m_wakeupTimer <= 1.0)
//                return;
//            this.m_wakeupTimer = -1f;
//            this.m_animator.SetBool("wakeup", false);
//            if (!this.m_nview.IsOwner())
//                return;
//            this.m_nview.GetZDO().Set("wakeup", false);
//        }

//        private void EdgeOfWorldKill(float dt)
//        {
//            if (this.IsDead())
//                return;
//            float magnitude = this.transform.position.magnitude;
//            float l = 10420f;
//            if ((double)magnitude > (double)l && (this.IsSwiming() || (double)this.transform.position.y < (double)ZoneSystem.instance.m_waterLevel))
//                this.m_body.MovePosition(this.m_body.get_position() + Vector3.Normalize(this.transform.position) * (Utils.LerpStep(l, 10500f, magnitude) * 10f) * dt);
//            if ((double)magnitude <= (double)l || (double)this.transform.position.y >= (double)ZoneSystem.instance.m_waterLevel - 40.0)
//                return;
//            this.Damage(new HitData()
//            {
//                m_damage = {
//        m_damage = 99999f
//      }
//            });
//        }

//        private void AutoPickup(float dt)
//        {
//            if (this.IsTeleporting())
//                return;
//            Vector3 b = this.transform.position + Vector3.up;
//            foreach (Collider collider in Physics.OverlapSphere(b, this.m_autoPickupRange, this.m_autoPickupMask))
//            {
//                if ((bool)(UnityEngine.Object)collider.get_attachedRigidbody())
//                {
//                    ItemDrop component = ((Component)collider.get_attachedRigidbody()).GetComponent<ItemDrop>();
//                    if (!((UnityEngine.Object)component == (UnityEngine.Object)null) && component.m_autoPickup && (!this.HaveUniqueKey(component.m_itemData.m_shared.m_name) && component.GetComponent<ZNetView>().IsValid()))
//                    {
//                        if (!component.CanPickup())
//                            component.RequestOwn();
//                        else if (this.m_inventory.CanAddItem(component.m_itemData, -1) && (double)component.m_itemData.GetWeight() + (double)this.m_inventory.GetTotalWeight() <= (double)this.GetMaxCarryWeight())
//                        {
//                            float num1 = Vector3.Distance(component.transform.position, b);
//                            if ((double)num1 <= (double)this.m_autoPickupRange)
//                            {
//                                if ((double)num1 < 0.300000011920929)
//                                {
//                                    this.Pickup(component.gameObject);
//                                }
//                                else
//                                {
//                                    Vector3 vector3 = Vector3.Normalize(b - component.transform.position);
//                                    float num2 = 15f;
//                                    component.transform.position = component.transform.position + vector3 * num2 * dt;
//                                }
//                            }
//                        }
//                    }
//                }
//            }
//        }

//        private void PlayerAttackInput(float dt)
//        {
//            if (this.InPlaceMode())
//                return;
//            ItemData currentWeapon = this.GetCurrentWeapon();
//            if (currentWeapon != null && (double)currentWeapon.m_shared.m_holdDurationMin > 0.0)
//            {
//                if (this.m_blocking || this.InMinorAction())
//                {
//                    this.m_attackDrawTime = -1f;
//                    if (string.IsNullOrEmpty(currentWeapon.m_shared.m_holdAnimationState))
//                        return;
//                    this.m_zanim.SetBool(currentWeapon.m_shared.m_holdAnimationState, false);
//                }
//                else
//                {
//                    bool flag = (double)currentWeapon.m_shared.m_holdStaminaDrain <= 0.0 || this.HaveStamina(0.0f);
//                    if ((double)this.m_attackDrawTime < 0.0)
//                    {
//                        if (this.m_attackDraw)
//                            return;
//                        this.m_attackDrawTime = 0.0f;
//                    }
//                    else if (this.m_attackDraw & flag && (double)this.m_attackDrawTime >= 0.0)
//                    {
//                        if ((double)this.m_attackDrawTime == 0.0)
//                        {
//                            if (!currentWeapon.m_shared.m_attack.StartDraw((Humanoid)this, currentWeapon))
//                            {
//                                this.m_attackDrawTime = -1f;
//                                return;
//                            }
//                            currentWeapon.m_shared.m_holdStartEffect.Create(this.transform.position, Quaternion.identity, this.transform, 1f);
//                        }
//                        this.m_attackDrawTime += Time.fixedDeltaTime;
//                        if (!string.IsNullOrEmpty(currentWeapon.m_shared.m_holdAnimationState))
//                            this.m_zanim.SetBool(currentWeapon.m_shared.m_holdAnimationState, true);
//                        this.UseStamina(currentWeapon.m_shared.m_holdStaminaDrain * dt);
//                    }
//                    else
//                    {
//                        if ((double)this.m_attackDrawTime <= 0.0)
//                            return;
//                        if (flag)
//                            this.StartAttack((Character)null, false);
//                        if (!string.IsNullOrEmpty(currentWeapon.m_shared.m_holdAnimationState))
//                            this.m_zanim.SetBool(currentWeapon.m_shared.m_holdAnimationState, false);
//                        this.m_attackDrawTime = 0.0f;
//                    }
//                }
//            }
//            else
//            {
//                if (this.m_attack)
//                {
//                    this.m_queuedAttackTimer = 0.5f;
//                    this.m_queuedSecondAttackTimer = 0.0f;
//                }
//                if (this.m_secondaryAttack)
//                {
//                    this.m_queuedSecondAttackTimer = 0.5f;
//                    this.m_queuedAttackTimer = 0.0f;
//                }
//                this.m_queuedAttackTimer -= Time.fixedDeltaTime;
//                this.m_queuedSecondAttackTimer -= Time.fixedDeltaTime;
//                if ((double)this.m_queuedAttackTimer > 0.0 && this.StartAttack((Character)null, false))
//                    this.m_queuedAttackTimer = 0.0f;
//                if ((double)this.m_queuedSecondAttackTimer <= 0.0 || !this.StartAttack((Character)null, true))
//                    return;
//                this.m_queuedSecondAttackTimer = 0.0f;
//            }
//        }

//        protected override bool HaveQueuedChain()
//        {
//            return (double)this.m_queuedAttackTimer > 0.0 && this.GetCurrentWeapon() != null && this.m_currentAttack != null && this.m_currentAttack.CanStartChainAttack();
//        }

//        private void UpdateBaseValue(float dt)
//        {
//            this.m_baseValueUpdatetimer += dt;
//            if ((double)this.m_baseValueUpdatetimer <= 2.0)
//                return;
//            this.m_baseValueUpdatetimer = 0.0f;
//            this.m_baseValue = EffectArea.GetBaseValue(this.transform.position, 20f);
//            this.m_nview.GetZDO().Set("baseValue", this.m_baseValue);
//            this.m_comfortLevel = SE_Rested.CalculateComfortLevel(this);
//        }

//        public int GetComfortLevel()
//        {
//            return this.m_comfortLevel;
//        }

//        public int GetBaseValue()
//        {
//            if (!this.m_nview.IsValid())
//                return 0;
//            return this.m_nview.IsOwner() ? this.m_baseValue : this.m_nview.GetZDO().GetInt("baseValue", 0);
//        }

//        public bool IsSafeInHome()
//        {
//            return this.m_safeInHome;
//        }

//        private void UpdateBiome(float dt)
//        {
//            if (this.InIntro())
//                return;
//            this.m_biomeTimer += dt;
//            if ((double)this.m_biomeTimer <= 1.0)
//                return;
//            this.m_biomeTimer = 0.0f;
//            Heightmap.Biome biome = Heightmap.FindBiome(this.transform.position);
//            if (this.m_currentBiome == biome)
//                return;
//            this.m_currentBiome = biome;
//            this.AddKnownBiome(biome);
//        }

//        public Heightmap.Biome GetCurrentBiome()
//        {
//            return this.m_currentBiome;
//        }

//        public override void RaiseSkill(Skills.SkillType skill, float value = 1f)
//        {
//            float multiplier = 1f;
//            this.m_seman.ModifyRaiseSkill(skill, ref multiplier);
//            value *= multiplier;
//            this.m_skills.RaiseSkill(skill, value);
//        }

//        private void UpdateStats(float dt)
//        {
//            if (this.InIntro() || this.IsTeleporting())
//                return;
//            this.m_timeSinceDeath += dt;
//            this.UpdateMovementModifier();
//            this.UpdateFood(dt, false);
//            bool flag = this.IsEncumbered();
//            float maxStamina = this.GetMaxStamina();
//            float num1 = 1f;
//            if (this.IsBlocking())
//                num1 *= 0.8f;
//            if (((this.IsSwiming() && !this.IsOnGround() || (this.InAttack() || this.InDodge()) ? 1 : (this.m_wallRunning ? 1 : 0)) | (flag ? 1 : 0)) != 0)
//                num1 = 0.0f;
//            float num2 = (this.m_staminaRegen + (float)(1.0 - (double)this.m_stamina / (double)maxStamina) * this.m_staminaRegen * this.m_staminaRegenTimeMultiplier) * num1;
//            float staminaMultiplier = 1f;
//            this.m_seman.ModifyStaminaRegen(ref staminaMultiplier);
//            float num3 = num2 * staminaMultiplier;
//            this.m_staminaRegenTimer -= dt;
//            if ((double)this.m_stamina < (double)maxStamina && (double)this.m_staminaRegenTimer <= 0.0)
//                this.m_stamina = Mathf.Min(maxStamina, this.m_stamina + num3 * dt);
//            this.m_nview.GetZDO().Set("stamina", this.m_stamina);
//            if (flag)
//            {
//                if ((double)this.m_moveDir.magnitude > 0.100000001490116)
//                    this.UseStamina(this.m_encumberedStaminaDrain * dt);
//                this.m_seman.AddStatusEffect("Encumbered", false);
//                this.ShowTutorial("encumbered", false);
//            }
//            else
//                this.m_seman.RemoveStatusEffect("Encumbered", false);
//            if (!this.HardDeath())
//                this.m_seman.AddStatusEffect("SoftDeath", false);
//            else
//                this.m_seman.RemoveStatusEffect("SoftDeath", false);
//            this.UpdateEnvStatusEffects(dt);
//        }

//        private void UpdateEnvStatusEffects(float dt)
//        {
//            this.m_nearFireTimer += dt;
//            HitData.DamageModifiers damageModifiers = this.GetDamageModifiers();
//            bool flag1 = (double)this.m_nearFireTimer < 0.25;
//            bool flag2 = this.m_seman.HaveStatusEffect("Burning");
//            bool flag3 = this.InShelter();
//            HitData.DamageModifier modifier = damageModifiers.GetModifier(HitData.DamageType.Frost);
//            bool flag4 = EnvMan.instance.IsFreezing();
//            int num = EnvMan.instance.IsCold() ? 1 : 0;
//            bool flag5 = EnvMan.instance.IsWet();
//            bool flag6 = this.IsSensed();
//            bool flag7 = this.m_seman.HaveStatusEffect("Wet");
//            bool flag8 = this.IsSitting();
//            bool flag9 = flag4 && !flag1 && !flag3;
//            bool flag10 = num != 0 && !flag1 || flag4 & flag1 && !flag3 || ((!flag4 ? 0 : (!flag1 ? 1 : 0)) & (flag3 ? 1 : 0)) != 0;
//            if (modifier == HitData.DamageModifier.Resistant || modifier == HitData.DamageModifier.VeryResistant)
//            {
//                flag9 = false;
//                flag10 = false;
//            }
//            if (flag5 && !this.m_underRoof)
//                this.m_seman.AddStatusEffect("Wet", true);
//            if (flag3)
//                this.m_seman.AddStatusEffect("Shelter", false);
//            else
//                this.m_seman.RemoveStatusEffect("Shelter", false);
//            if (flag1)
//                this.m_seman.AddStatusEffect("CampFire", false);
//            else
//                this.m_seman.RemoveStatusEffect("CampFire", false);
//            bool flag11 = ((flag6 || !(flag8 | flag3) || (!(!flag10 & !flag9) || flag7) ? 0 : (!flag2 ? 1 : 0)) & (flag1 ? 1 : 0)) != 0;
//            if (flag11)
//                this.m_seman.AddStatusEffect("Resting", false);
//            else
//                this.m_seman.RemoveStatusEffect("Resting", false);
//            this.m_safeInHome = flag11 & flag3;
//            if (flag9)
//            {
//                if (this.m_seman.RemoveStatusEffect("Cold", true))
//                    return;
//                this.m_seman.AddStatusEffect("Freezing", false);
//            }
//            else if (flag10)
//            {
//                if (this.m_seman.RemoveStatusEffect("Freezing", true) || !(bool)(UnityEngine.Object)this.m_seman.AddStatusEffect("Cold", false))
//                    return;
//                this.ShowTutorial("cold", false);
//            }
//            else
//            {
//                this.m_seman.RemoveStatusEffect("Cold", false);
//                this.m_seman.RemoveStatusEffect("Freezing", false);
//            }
//        }

//        private bool CanEat(ItemData item, bool showMessages)
//        {
//            foreach (Player.Food food in this.m_foods)
//            {
//                if (food.m_item.m_shared.m_name == item.m_shared.m_name)
//                {
//                    if (food.CanEatAgain())
//                        return true;
//                    this.Message(MessageHud.MessageType.Center, Localization.instance.Localize("$msg_nomore", item.m_shared.m_name), 0, (Sprite)null);
//                    return false;
//                }
//            }
//            foreach (Player.Food food in this.m_foods)
//            {
//                if (food.CanEatAgain())
//                    return true;
//            }
//            if (this.m_foods.Count < 3)
//                return true;
//            this.Message(MessageHud.MessageType.Center, "$msg_isfull", 0, (Sprite)null);
//            return false;
//        }

//        private Player.Food GetMostDepletedFood()
//        {
//            Player.Food food1 = (Player.Food)null;
//            foreach (Player.Food food2 in this.m_foods)
//            {
//                if (food2.CanEatAgain() && (food1 == null || (double)food2.m_health < (double)food1.m_health))
//                    food1 = food2;
//            }
//            return food1;
//        }

//        public void ClearFood()
//        {
//            this.m_foods.Clear();
//        }

//        private bool EatFood(ItemData item)
//        {
//            if (!this.CanEat(item, false))
//                return false;
//            foreach (Player.Food food in this.m_foods)
//            {
//                if (food.m_item.m_shared.m_name == item.m_shared.m_name)
//                {
//                    if (!food.CanEatAgain())
//                        return false;
//                    food.m_health = item.m_shared.m_food;
//                    food.m_stamina = item.m_shared.m_foodStamina;
//                    this.UpdateFood(0.0f, true);
//                    return true;
//                }
//            }
//            if (this.m_foods.Count < 3)
//            {
//                this.m_foods.Add(new Player.Food()
//                {
//                    m_name = item.m_dropPrefab.name,
//                    m_item = item,
//                    m_health = item.m_shared.m_food,
//                    m_stamina = item.m_shared.m_foodStamina
//                });
//                this.UpdateFood(0.0f, true);
//                return true;
//            }
//            Player.Food mostDepletedFood = this.GetMostDepletedFood();
//            if (mostDepletedFood == null)
//                return false;
//            mostDepletedFood.m_name = item.m_dropPrefab.name;
//            mostDepletedFood.m_item = item;
//            mostDepletedFood.m_health = item.m_shared.m_food;
//            mostDepletedFood.m_stamina = item.m_shared.m_foodStamina;
//            return true;
//        }

//        private void UpdateFood(float dt, bool forceUpdate)
//        {
//            this.m_foodUpdateTimer += dt;
//            if ((double)this.m_foodUpdateTimer >= 1.0 | forceUpdate)
//            {
//                this.m_foodUpdateTimer = 0.0f;
//                foreach (Player.Food food in this.m_foods)
//                {
//                    food.m_health -= food.m_item.m_shared.m_food / food.m_item.m_shared.m_foodBurnTime;
//                    food.m_stamina -= food.m_item.m_shared.m_foodStamina / food.m_item.m_shared.m_foodBurnTime;
//                    if ((double)food.m_health < 0.0)
//                        food.m_health = 0.0f;
//                    if ((double)food.m_stamina < 0.0)
//                        food.m_stamina = 0.0f;
//                    if ((double)food.m_health <= 0.0)
//                    {
//                        this.Message(MessageHud.MessageType.Center, "$msg_food_done", 0, (Sprite)null);
//                        this.m_foods.Remove(food);
//                        break;
//                    }
//                }
//                float hp;
//                float stamina;
//                this.GetTotalFoodValue(out hp, out stamina);
//                this.SetMaxHealth(hp, true);
//                this.SetMaxStamina(stamina, true);
//            }
//            if (forceUpdate)
//                return;
//            this.m_foodRegenTimer += dt;
//            if ((double)this.m_foodRegenTimer < 10.0)
//                return;
//            this.m_foodRegenTimer = 0.0f;
//            float num = 0.0f;
//            foreach (Player.Food food in this.m_foods)
//                num += food.m_item.m_shared.m_foodRegen;
//            if ((double)num <= 0.0)
//                return;
//            float regenMultiplier = 1f;
//            this.m_seman.ModifyHealthRegen(ref regenMultiplier);
//            this.Heal(num * regenMultiplier, true);
//        }

//        private void GetTotalFoodValue(out float hp, out float stamina)
//        {
//            hp = 25f;
//            stamina = 75f;
//            foreach (Player.Food food in this.m_foods)
//            {
//                hp += food.m_health;
//                stamina += food.m_stamina;
//            }
//        }

//        public float GetBaseFoodHP()
//        {
//            return 25f;
//        }

//        public List<Player.Food> GetFoods()
//        {
//            return this.m_foods;
//        }

//        public void OnSpawned()
//        {
//            this.m_spawnEffects.Create(this.transform.position, Quaternion.identity, (Transform)null, 1f);
//            if (!this.m_firstSpawn)
//                return;
//            if ((UnityEngine.Object)this.m_valkyrie != (UnityEngine.Object)null)
//                UnityEngine.Object.Instantiate<GameObject>(this.m_valkyrie, this.transform.position, Quaternion.identity);
//            this.m_firstSpawn = false;
//        }

//        protected override bool CheckRun(Vector3 moveDir, float dt)
//        {
//            if (!base.CheckRun(moveDir, dt))
//                return false;
//            bool flag = this.HaveStamina(0.0f);
//            float drain = this.m_runStaminaDrain * Mathf.Lerp(1f, 0.5f, this.m_skills.GetSkillFactor(Skills.SkillType.Run));
//            this.m_seman.ModifyRunStaminaDrain(drain, ref drain);
//            this.UseStamina(dt * drain);
//            if (this.HaveStamina(0.0f))
//            {
//                this.m_runSkillImproveTimer += dt;
//                if ((double)this.m_runSkillImproveTimer > 1.0)
//                {
//                    this.m_runSkillImproveTimer = 0.0f;
//                    this.RaiseSkill(Skills.SkillType.Run, 1f);
//                }
//                this.AbortEquipQueue();
//                return true;
//            }
//            if (flag)
//                Hud.instance.StaminaBarNoStaminaFlash();
//            return false;
//        }

//        private void UpdateMovementModifier()
//        {
//            this.m_equipmentMovementModifier = 0.0f;
//            if (this.m_rightItem != null)
//                this.m_equipmentMovementModifier += this.m_rightItem.m_shared.m_movementModifier;
//            if (this.m_leftItem != null)
//                this.m_equipmentMovementModifier += this.m_leftItem.m_shared.m_movementModifier;
//            if (this.m_chestItem != null)
//                this.m_equipmentMovementModifier += this.m_chestItem.m_shared.m_movementModifier;
//            if (this.m_legItem != null)
//                this.m_equipmentMovementModifier += this.m_legItem.m_shared.m_movementModifier;
//            if (this.m_helmetItem != null)
//                this.m_equipmentMovementModifier += this.m_helmetItem.m_shared.m_movementModifier;
//            if (this.m_shoulderItem != null)
//                this.m_equipmentMovementModifier += this.m_shoulderItem.m_shared.m_movementModifier;
//            if (this.m_utilityItem == null)
//                return;
//            this.m_equipmentMovementModifier += this.m_utilityItem.m_shared.m_movementModifier;
//        }

//        public void OnSkillLevelup(Skills.SkillType skill, float level)
//        {
//            this.m_skillLevelupEffects.Create(this.m_head.position, this.m_head.rotation, this.m_head, 1f);
//        }

//        protected override void OnJump()
//        {
//            this.AbortEquipQueue();
//            float staminaUse = this.m_jumpStaminaUsage - this.m_jumpStaminaUsage * this.m_equipmentMovementModifier;
//            this.m_seman.ModifyJumpStaminaUsage(staminaUse, ref staminaUse);
//            this.UseStamina(staminaUse);
//        }

//        protected override void OnSwiming(Vector3 targetVel, float dt)
//        {
//            base.OnSwiming(targetVel, dt);
//            if ((double)targetVel.magnitude > 0.100000001490116)
//            {
//                float num = Mathf.Lerp(this.m_swimStaminaDrainMinSkill, this.m_swimStaminaDrainMaxSkill, this.m_skills.GetSkillFactor(Skills.SkillType.Swim));
//                this.UseStamina(dt * num);
//                this.m_swimSkillImproveTimer += dt;
//                if ((double)this.m_swimSkillImproveTimer > 1.0)
//                {
//                    this.m_swimSkillImproveTimer = 0.0f;
//                    this.RaiseSkill(Skills.SkillType.Swim, 1f);
//                }
//            }
//            if (this.HaveStamina(0.0f))
//                return;
//            this.m_drownDamageTimer += dt;
//            if ((double)this.m_drownDamageTimer <= 1.0)
//                return;
//            this.m_drownDamageTimer = 0.0f;
//            float num1 = Mathf.Ceil(this.GetMaxHealth() / 20f);
//            this.Damage(new HitData()
//            {
//                m_damage = {
//        m_damage = num1
//      },
//                m_point = this.GetCenterPoint(),
//                m_dir = Vector3.down,
//                m_pushForce = 10f
//            });
//            Vector3 position = this.transform.position;
//            position.y = this.m_waterLevel;
//            this.m_drownEffects.Create(position, this.transform.rotation, (Transform)null, 1f);
//        }

//        protected override bool TakeInput()
//        {
//            bool flag = (!(bool)(UnityEngine.Object)Chat.instance || !Chat.instance.HasFocus()) && (!Console.IsVisible() && !TextInput.IsVisible()) && (!StoreGui.IsVisible() && !InventoryGui.IsVisible() && !Menu.IsVisible() && ((!(bool)(UnityEngine.Object)TextViewer.instance || !TextViewer.instance.IsVisible()) && !Minimap.IsOpen()) && !GameCamera.InFreeFly());
//            if (this.IsDead() || this.InCutscene() || this.IsTeleporting())
//                flag = false;
//            return flag;
//        }

//        public void UseHotbarItem(int index)
//        {
//            ItemData itemAt = this.m_inventory.GetItemAt(index - 1, 0);
//            if (itemAt == null)
//                return;
//            this.UseItem((Inventory)null, itemAt, false);
//        }

//        public bool RequiredCraftingStation(Recipe recipe, int qualityLevel, bool checkLevel)
//        {
//            CraftingStation requiredStation = recipe.GetRequiredStation(qualityLevel);
//            if ((UnityEngine.Object)requiredStation != (UnityEngine.Object)null)
//            {
//                if ((UnityEngine.Object)this.m_currentStation == (UnityEngine.Object)null || requiredStation.m_name != this.m_currentStation.m_name || checkLevel && this.m_currentStation.GetLevel() < recipe.GetRequiredStationLevel(qualityLevel))
//                    return false;
//            }
//            else if ((UnityEngine.Object)this.m_currentStation != (UnityEngine.Object)null && !this.m_currentStation.m_showBasicRecipies)
//                return false;
//            return true;
//        }

//        public bool HaveRequirements(Recipe recipe, bool discover, int qualityLevel)
//        {
//            if (discover)
//            {
//                if ((bool)(UnityEngine.Object)recipe.m_craftingStation && !this.KnowStationLevel(recipe.m_craftingStation.m_name, recipe.m_minStationLevel))
//                    return false;
//            }
//            else if (!this.RequiredCraftingStation(recipe, qualityLevel, true))
//                return false;
//            return (recipe.m_item.m_itemData.m_shared.m_dlc.Length <= 0 || DLCMan.instance.IsDLCInstalled(recipe.m_item.m_itemData.m_shared.m_dlc)) && this.HaveRequirements(recipe.m_resources, discover, qualityLevel);
//        }

//        private bool HaveRequirements(Piece.Requirement[] resources, bool discover, int qualityLevel)
//        {
//            foreach (Piece.Requirement resource in resources)
//            {
//                if ((bool)(UnityEngine.Object)resource.m_resItem)
//                {
//                    if (discover)
//                    {
//                        if (resource.m_amount > 0 && !this.m_knownMaterial.Contains(resource.m_resItem.m_itemData.m_shared.m_name))
//                            return false;
//                    }
//                    else
//                    {
//                        int amount = resource.GetAmount(qualityLevel);
//                        if (this.m_inventory.CountItems(resource.m_resItem.m_itemData.m_shared.m_name) < amount)
//                            return false;
//                    }
//                }
//            }
//            return true;
//        }

//        public bool HaveRequirements(Piece piece, Player.RequirementMode mode)
//        {
//            if ((bool)(UnityEngine.Object)piece.m_craftingStation)
//            {
//                if (mode == Player.RequirementMode.IsKnown || mode == Player.RequirementMode.CanAlmostBuild)
//                {
//                    if (!this.m_knownStations.ContainsKey(piece.m_craftingStation.m_name))
//                        return false;
//                }
//                else if (!(bool)(UnityEngine.Object)CraftingStation.HaveBuildStationInRange(piece.m_craftingStation.m_name, this.transform.position))
//                    return false;
//            }
//            if (piece.m_dlc.Length > 0 && !DLCMan.instance.IsDLCInstalled(piece.m_dlc))
//                return false;
//            foreach (Piece.Requirement resource in piece.m_resources)
//            {
//                if ((bool)(UnityEngine.Object)resource.m_resItem && resource.m_amount > 0)
//                {
//                    switch (mode)
//                    {
//                        case Player.RequirementMode.CanBuild:
//                            if (this.m_inventory.CountItems(resource.m_resItem.m_itemData.m_shared.m_name) < resource.m_amount)
//                                return false;
//                            continue;
//                        case Player.RequirementMode.IsKnown:
//                            if (!this.m_knownMaterial.Contains(resource.m_resItem.m_itemData.m_shared.m_name))
//                                return false;
//                            continue;
//                        case Player.RequirementMode.CanAlmostBuild:
//                            if (!this.m_inventory.HaveItem(resource.m_resItem.m_itemData.m_shared.m_name))
//                                return false;
//                            continue;
//                        default:
//                            continue;
//                    }
//                }
//            }
//            return true;
//        }

//        public void SetCraftingStation(CraftingStation station)
//        {
//            if ((UnityEngine.Object)this.m_currentStation == (UnityEngine.Object)station)
//                return;
//            if ((bool)(UnityEngine.Object)station)
//            {
//                this.AddKnownStation(station);
//                station.PokeInUse();
//            }
//            this.m_currentStation = station;
//            this.HideHandItems();
//            this.m_zanim.SetInt("crafting", (bool)(UnityEngine.Object)this.m_currentStation ? this.m_currentStation.m_useAnimation : 0);
//        }

//        public CraftingStation GetCurrentCraftingStation()
//        {
//            return this.m_currentStation;
//        }

//        public void ConsumeResources(Piece.Requirement[] requirements, int qualityLevel)
//        {
//            foreach (Piece.Requirement requirement in requirements)
//            {
//                if ((bool)(UnityEngine.Object)requirement.m_resItem)
//                {
//                    int amount = requirement.GetAmount(qualityLevel);
//                    if (amount > 0)
//                        this.m_inventory.RemoveItem(requirement.m_resItem.m_itemData.m_shared.m_name, amount);
//                }
//            }
//        }

//        private void UpdateHover()
//        {
//            if (this.InPlaceMode() || this.IsDead() || (UnityEngine.Object)this.m_shipControl != (UnityEngine.Object)null)
//            {
//                this.m_hovering = (GameObject)null;
//                this.m_hoveringCreature = (Character)null;
//            }
//            else
//                this.FindHoverObject(out this.m_hovering, out this.m_hoveringCreature);
//        }

//        private bool CheckCanRemovePiece(Piece piece)
//        {
//            if (this.m_noPlacementCost || !((UnityEngine.Object)piece.m_craftingStation != (UnityEngine.Object)null) || (bool)(UnityEngine.Object)CraftingStation.HaveBuildStationInRange(piece.m_craftingStation.m_name, this.transform.position))
//                return true;
//            this.Message(MessageHud.MessageType.Center, "$msg_missingstation", 0, (Sprite)null);
//            return false;
//        }

//        private bool RemovePiece()
//        {
//            RaycastHit raycastHit;
//            if (Physics.Raycast(GameCamera.instance.transform.position, GameCamera.instance.transform.forward, ref raycastHit, 50f, this.m_removeRayMask) && (double)Vector3.Distance(((RaycastHit)ref raycastHit).get_point(), this.m_eye.position) < (double)this.m_maxPlaceDistance)
//            {
//                Piece piece = ((Component)((RaycastHit)ref raycastHit).get_collider()).GetComponentInParent<Piece>();
//                if ((UnityEngine.Object)piece == (UnityEngine.Object)null && (bool)(UnityEngine.Object)((Component)((RaycastHit)ref raycastHit).get_collider()).GetComponent<Heightmap>())
//                    piece = TerrainModifier.FindClosestModifierPieceInRange(((RaycastHit)ref raycastHit).get_point(), 2.5f);
//                if ((bool)(UnityEngine.Object)piece && piece.m_canBeRemoved)
//                {
//                    if (Location.IsInsideNoBuildLocation(piece.transform.position))
//                    {
//                        this.Message(MessageHud.MessageType.Center, "$msg_nobuildzone", 0, (Sprite)null);
//                        return false;
//                    }
//                    if (!PrivateArea.CheckAccess(piece.transform.position, 0.0f, true))
//                    {
//                        this.Message(MessageHud.MessageType.Center, "$msg_privatezone", 0, (Sprite)null);
//                        return false;
//                    }
//                    if (!this.CheckCanRemovePiece(piece))
//                        return false;
//                    ZNetView component1 = piece.GetComponent<ZNetView>();
//                    if ((UnityEngine.Object)component1 == (UnityEngine.Object)null)
//                        return false;
//                    if (!piece.CanBeRemoved())
//                    {
//                        this.Message(MessageHud.MessageType.Center, "$msg_cantremovenow", 0, (Sprite)null);
//                        return false;
//                    }
//                    WearNTear component2 = piece.GetComponent<WearNTear>();
//                    if ((bool)(UnityEngine.Object)component2)
//                    {
//                        component2.Remove();
//                    }
//                    else
//                    {
//                        ZLog.Log((object)("Removing non WNT object with hammer " + piece.name));
//                        component1.ClaimOwnership();
//                        piece.DropResources();
//                        piece.m_placeEffect.Create(piece.transform.position, piece.transform.rotation, piece.gameObject.transform, 1f);
//                        this.m_removeEffects.Create(piece.transform.position, Quaternion.identity, (Transform)null, 1f);
//                        ZNetScene.instance.Destroy(piece.gameObject);
//                    }
//                    ItemData rightItem = this.GetRightItem();
//                    if (rightItem != null)
//                    {
//                        this.FaceLookDirection();
//                        this.m_zanim.SetTrigger(rightItem.m_shared.m_attack.m_attackAnimation);
//                    }
//                    return true;
//                }
//            }
//            return false;
//        }

//        public void FaceLookDirection()
//        {
//            this.transform.rotation = this.GetLookYaw();
//        }

//        private bool PlacePiece(Piece piece)
//        {
//            this.UpdatePlacementGhost(true);
//            Vector3 position = this.m_placementGhost.transform.position;
//            Quaternion rotation = this.m_placementGhost.transform.rotation;
//            GameObject gameObject1 = piece.gameObject;
//            switch (this.m_placementStatus)
//            {
//                case Player.PlacementStatus.Invalid:
//                    this.Message(MessageHud.MessageType.Center, "$msg_invalidplacement", 0, (Sprite)null);
//                    return false;
//                case Player.PlacementStatus.BlockedbyPlayer:
//                    this.Message(MessageHud.MessageType.Center, "$msg_blocked", 0, (Sprite)null);
//                    return false;
//                case Player.PlacementStatus.NoBuildZone:
//                    this.Message(MessageHud.MessageType.Center, "$msg_nobuildzone", 0, (Sprite)null);
//                    return false;
//                case Player.PlacementStatus.PrivateZone:
//                    this.Message(MessageHud.MessageType.Center, "$msg_privatezone", 0, (Sprite)null);
//                    return false;
//                case Player.PlacementStatus.MoreSpace:
//                    this.Message(MessageHud.MessageType.Center, "$msg_needspace", 0, (Sprite)null);
//                    return false;
//                case Player.PlacementStatus.NoTeleportArea:
//                    this.Message(MessageHud.MessageType.Center, "$msg_noteleportarea", 0, (Sprite)null);
//                    return false;
//                case Player.PlacementStatus.ExtensionMissingStation:
//                    this.Message(MessageHud.MessageType.Center, "$msg_extensionmissingstation", 0, (Sprite)null);
//                    return false;
//                case Player.PlacementStatus.WrongBiome:
//                    this.Message(MessageHud.MessageType.Center, "$msg_wrongbiome", 0, (Sprite)null);
//                    return false;
//                case Player.PlacementStatus.NeedCultivated:
//                    this.Message(MessageHud.MessageType.Center, "$msg_needcultivated", 0, (Sprite)null);
//                    return false;
//                case Player.PlacementStatus.NotInDungeon:
//                    this.Message(MessageHud.MessageType.Center, "$msg_notindungeon", 0, (Sprite)null);
//                    return false;
//                default:
//                    TerrainModifier.SetTriggerOnPlaced(true);
//                    GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(gameObject1, position, rotation);
//                    TerrainModifier.SetTriggerOnPlaced(false);
//                    CraftingStation componentInChildren = gameObject2.GetComponentInChildren<CraftingStation>();
//                    if ((bool)(UnityEngine.Object)componentInChildren)
//                        this.AddKnownStation(componentInChildren);
//                    Piece component1 = gameObject2.GetComponent<Piece>();
//                    if ((bool)(UnityEngine.Object)component1)
//                        component1.SetCreator(this.GetPlayerID());
//                    PrivateArea component2 = gameObject2.GetComponent<PrivateArea>();
//                    if ((bool)(UnityEngine.Object)component2)
//                        component2.Setup(Game.instance.GetPlayerProfile().GetName());
//                    WearNTear component3 = gameObject2.GetComponent<WearNTear>();
//                    if ((bool)(UnityEngine.Object)component3)
//                        component3.OnPlaced();
//                    ItemData rightItem = this.GetRightItem();
//                    if (rightItem != null)
//                    {
//                        this.FaceLookDirection();
//                        this.m_zanim.SetTrigger(rightItem.m_shared.m_attack.m_attackAnimation);
//                    }
//                    piece.m_placeEffect.Create(position, rotation, gameObject2.transform, 1f);
//                    this.AddNoise(50f);
//                    ++Game.instance.GetPlayerProfile().m_playerStats.m_builds;
//                    ZLog.Log((object)("Placed " + gameObject1.name));
//                    Gogan.LogEvent("Game", "PlacedPiece", gameObject1.name, 0L);
//                    return true;
//            }
//        }

//        public override bool IsPlayer()
//        {
//            return true;
//        }

//        public void GetBuildSelection(
//          out Piece go,
//          out Vector2Int id,
//          out int total,
//          out Piece.PieceCategory category,
//          out bool useCategory)
//        {
//            category = this.m_buildPieces.m_selectedCategory;
//            useCategory = this.m_buildPieces.m_useCategories;
//            if (this.m_buildPieces.GetAvailablePiecesInSelectedCategory() == 0)
//            {
//                go = (Piece)null;
//                id = Vector2Int.zero;
//                total = 0;
//            }
//            else
//            {
//                GameObject selectedPrefab = this.m_buildPieces.GetSelectedPrefab();
//                go = (bool)(UnityEngine.Object)selectedPrefab ? selectedPrefab.GetComponent<Piece>() : (Piece)null;
//                id = this.m_buildPieces.GetSelectedIndex();
//                total = this.m_buildPieces.GetAvailablePiecesInSelectedCategory();
//            }
//        }

//        public List<Piece> GetBuildPieces()
//        {
//            return (bool)(UnityEngine.Object)this.m_buildPieces ? this.m_buildPieces.GetPiecesInSelectedCategory() : (List<Piece>)null;
//        }

//        public int GetAvailableBuildPiecesInCategory(Piece.PieceCategory cat)
//        {
//            return (bool)(UnityEngine.Object)this.m_buildPieces ? this.m_buildPieces.GetAvailablePiecesInCategory(cat) : 0;
//        }

//        private void RPC_OnDeath(long sender)
//        {
//            this.m_visual.SetActive(false);
//        }

//        private void CreateDeathEffects()
//        {
//            foreach (GameObject gameObject in this.m_deathEffects.Create(this.transform.position, this.transform.rotation, this.transform, 1f))
//            {
//                Ragdoll component = gameObject.GetComponent<Ragdoll>();
//                if ((bool)(UnityEngine.Object)component)
//                {
//                    Vector3 velocity = this.m_body.get_velocity();
//                    if ((double)this.m_pushForce.magnitude * 0.5 > (double)velocity.magnitude)
//                        velocity = this.m_pushForce * 0.5f;
//                    component.Setup(velocity, 0.0f, 0.0f, 0.0f, (CharacterDrop)null);
//                    this.OnRagdollCreated(component);
//                    this.m_ragdoll = component;
//                }
//            }
//        }

//        public void UnequipDeathDropItems()
//        {
//            if (this.m_rightItem != null)
//                this.UnequipItem(this.m_rightItem, false);
//            if (this.m_leftItem != null)
//                this.UnequipItem(this.m_leftItem, false);
//            if (this.m_ammoItem != null)
//                this.UnequipItem(this.m_ammoItem, false);
//            if (this.m_utilityItem == null)
//                return;
//            this.UnequipItem(this.m_utilityItem, false);
//        }

//        private void CreateTombStone()
//        {
//            if (this.m_inventory.NrOfItems() == 0)
//                return;
//            this.UnequipAllItems();
//            GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.m_tombstone, this.GetCenterPoint(), this.transform.rotation);
//            gameObject.GetComponent<Container>().GetInventory().MoveInventoryToGrave(this.m_inventory);
//            TombStone component = gameObject.GetComponent<TombStone>();
//            PlayerProfile playerProfile = Game.instance.GetPlayerProfile();
//            string name = playerProfile.GetName();
//            long playerId = playerProfile.GetPlayerID();
//            component.Setup(name, playerId);
//        }

//        private bool HardDeath()
//        {
//            return (double)this.m_timeSinceDeath > (double)this.m_hardDeathCooldown;
//        }

//        protected override void OnDeath()
//        {
//            int num = this.HardDeath() ? 1 : 0;
//            this.m_nview.GetZDO().Set("dead", true);
//            this.m_nview.InvokeRPC(ZNetView.Everybody, nameof(OnDeath), (object[])Array.Empty<object>());
//            ++Game.instance.GetPlayerProfile().m_playerStats.m_deaths;
//            Game.instance.GetPlayerProfile().SetDeathPoint(this.transform.position);
//            this.CreateDeathEffects();
//            this.CreateTombStone();
//            this.m_foods.Clear();
//            if (num != 0)
//                this.m_skills.OnDeath();
//            Game.instance.RequestRespawn(10f);
//            this.m_timeSinceDeath = 0.0f;
//            if (num == 0)
//                this.Message(MessageHud.MessageType.TopLeft, "$msg_softdeath", 0, (Sprite)null);
//            this.Message(MessageHud.MessageType.Center, "$msg_youdied", 0, (Sprite)null);
//            this.ShowTutorial("death", false);
//            Gogan.LogEvent("Game", "Death", "biome:" + this.GetCurrentBiome().ToString(), 0L);
//        }

//        public void OnRespawn()
//        {
//            this.m_nview.GetZDO().Set("dead", false);
//            this.SetHealth(this.GetMaxHealth());
//        }

//        private void SetupPlacementGhost()
//        {
//            if ((bool)(UnityEngine.Object)this.m_placementGhost)
//            {
//                UnityEngine.Object.Destroy((UnityEngine.Object)this.m_placementGhost);
//                this.m_placementGhost = (GameObject)null;
//            }
//            if ((UnityEngine.Object)this.m_buildPieces == (UnityEngine.Object)null)
//                return;
//            GameObject selectedPrefab = this.m_buildPieces.GetSelectedPrefab();
//            if ((UnityEngine.Object)selectedPrefab == (UnityEngine.Object)null || selectedPrefab.GetComponent<Piece>().m_repairPiece)
//                return;
//            bool flag = false;
//            TerrainModifier componentInChildren1 = selectedPrefab.GetComponentInChildren<TerrainModifier>();
//            if ((bool)(UnityEngine.Object)componentInChildren1)
//            {
//                flag = componentInChildren1.enabled;
//                componentInChildren1.enabled = false;
//            }
//            ZNetView.m_forceDisableInit = true;
//            this.m_placementGhost = UnityEngine.Object.Instantiate<GameObject>(selectedPrefab);
//            ZNetView.m_forceDisableInit = false;
//            this.m_placementGhost.name = selectedPrefab.name;
//            if ((bool)(UnityEngine.Object)componentInChildren1)
//                componentInChildren1.enabled = flag;
//            foreach (UnityEngine.Object componentsInChild in this.m_placementGhost.GetComponentsInChildren<Joint>())
//                UnityEngine.Object.Destroy(componentsInChild);
//            foreach (UnityEngine.Object componentsInChild in this.m_placementGhost.GetComponentsInChildren<Rigidbody>())
//                UnityEngine.Object.Destroy(componentsInChild);
//            foreach (Collider componentsInChild in this.m_placementGhost.GetComponentsInChildren<Collider>())
//            {
//                if ((1 << ((Component)componentsInChild).gameObject.layer & this.m_placeRayMask) == 0)
//                {
//                    ZLog.Log((object)("Disabling " + ((Component)componentsInChild).gameObject.name + "  " + LayerMask.LayerToName(((Component)componentsInChild).gameObject.layer)));
//                    componentsInChild.set_enabled(false);
//                }
//            }
//            Transform[] componentsInChildren = this.m_placementGhost.GetComponentsInChildren<Transform>();
//            int layer = LayerMask.NameToLayer("ghost");
//            foreach (Component component in componentsInChildren)
//                component.gameObject.layer = layer;
//            foreach (UnityEngine.Object componentsInChild in this.m_placementGhost.GetComponentsInChildren<TerrainModifier>())
//                UnityEngine.Object.Destroy(componentsInChild);
//            foreach (UnityEngine.Object componentsInChild in this.m_placementGhost.GetComponentsInChildren<GuidePoint>())
//                UnityEngine.Object.Destroy(componentsInChild);
//            foreach (UnityEngine.Object componentsInChild in this.m_placementGhost.GetComponentsInChildren<Light>())
//                UnityEngine.Object.Destroy(componentsInChild);
//            foreach (Behaviour componentsInChild in this.m_placementGhost.GetComponentsInChildren<AudioSource>())
//                componentsInChild.enabled = false;
//            foreach (Behaviour componentsInChild in this.m_placementGhost.GetComponentsInChildren<ZSFX>())
//                componentsInChild.enabled = false;
//            Windmill componentInChildren2 = this.m_placementGhost.GetComponentInChildren<Windmill>();
//            if ((bool)(UnityEngine.Object)componentInChildren2)
//                componentInChildren2.enabled = false;
//            foreach (Component componentsInChild in this.m_placementGhost.GetComponentsInChildren<ParticleSystem>())
//                componentsInChild.gameObject.SetActive(false);
//            Transform transform = this.m_placementGhost.transform.Find("_GhostOnly");
//            if ((bool)(UnityEngine.Object)transform)
//                transform.gameObject.SetActive(true);
//            this.m_placementGhost.transform.position = this.transform.position;
//            this.m_placementGhost.transform.localScale = selectedPrefab.transform.localScale;
//            foreach (MeshRenderer componentsInChild in this.m_placementGhost.GetComponentsInChildren<MeshRenderer>())
//            {
//                if (!((UnityEngine.Object)componentsInChild.sharedMaterial == (UnityEngine.Object)null))
//                {
//                    Material[] sharedMaterials = componentsInChild.sharedMaterials;
//                    for (int index = 0; index < sharedMaterials.Length; ++index)
//                    {
//                        Material material = new Material(sharedMaterials[index]);
//                        material.SetFloat("_RippleDistance", 0.0f);
//                        material.SetFloat("_ValueNoise", 0.0f);
//                        sharedMaterials[index] = material;
//                    }
//                    componentsInChild.sharedMaterials = sharedMaterials;
//                    componentsInChild.shadowCastingMode = ShadowCastingMode.Off;
//                }
//            }
//        }

//        private void SetPlacementGhostValid(bool valid)
//        {
//            this.m_placementGhost.GetComponent<Piece>().SetInvalidPlacementHeightlight(!valid);
//        }

//        protected override void SetPlaceMode(PieceTable buildPieces)
//        {
//            base.SetPlaceMode(buildPieces);
//            this.m_buildPieces = buildPieces;
//            this.UpdateAvailablePiecesList();
//        }

//        public void SetBuildCategory(int index)
//        {
//            if (!((UnityEngine.Object)this.m_buildPieces != (UnityEngine.Object)null))
//                return;
//            this.m_buildPieces.SetCategory(index);
//            this.UpdateAvailablePiecesList();
//        }

//        public override bool InPlaceMode()
//        {
//            return (UnityEngine.Object)this.m_buildPieces != (UnityEngine.Object)null;
//        }

//        private void Repair(ItemData toolItem, Piece repairPiece)
//        {
//            if (!this.InPlaceMode())
//                return;
//            Piece hoveringPiece = this.GetHoveringPiece();
//            if (!(bool)(UnityEngine.Object)hoveringPiece || !this.CheckCanRemovePiece(hoveringPiece) || !PrivateArea.CheckAccess(hoveringPiece.transform.position, 0.0f, true))
//                return;
//            bool flag = false;
//            WearNTear component = hoveringPiece.GetComponent<WearNTear>();
//            if ((bool)(UnityEngine.Object)component && component.Repair())
//                flag = true;
//            if (flag)
//            {
//                this.FaceLookDirection();
//                this.m_zanim.SetTrigger(toolItem.m_shared.m_attack.m_attackAnimation);
//                hoveringPiece.m_placeEffect.Create(hoveringPiece.transform.position, hoveringPiece.transform.rotation, (Transform)null, 1f);
//                this.Message(MessageHud.MessageType.TopLeft, Localization.instance.Localize("$msg_repaired", hoveringPiece.m_name), 0, (Sprite)null);
//                this.UseStamina(toolItem.m_shared.m_attack.m_attackStamina);
//                if (!toolItem.m_shared.m_useDurability)
//                    return;
//                toolItem.m_durability -= toolItem.m_shared.m_useDurabilityDrain;
//            }
//            else
//                this.Message(MessageHud.MessageType.TopLeft, hoveringPiece.m_name + " $msg_doesnotneedrepair", 0, (Sprite)null);
//        }

//        private void UpdateWearNTearHover()
//        {
//            if (!this.InPlaceMode())
//            {
//                this.m_hoveringPiece = (Piece)null;
//            }
//            else
//            {
//                this.m_hoveringPiece = (Piece)null;
//                RaycastHit raycastHit;
//                if (!Physics.Raycast(GameCamera.instance.transform.position, GameCamera.instance.transform.forward, ref raycastHit, 50f, this.m_removeRayMask) || (double)Vector3.Distance(this.m_eye.position, ((RaycastHit)ref raycastHit).get_point()) >= (double)this.m_maxPlaceDistance)
//                    return;
//                Piece componentInParent = ((Component)((RaycastHit)ref raycastHit).get_collider()).GetComponentInParent<Piece>();
//                this.m_hoveringPiece = componentInParent;
//                if (!(bool)(UnityEngine.Object)componentInParent)
//                    return;
//                WearNTear component = componentInParent.GetComponent<WearNTear>();
//                if (!(bool)(UnityEngine.Object)component)
//                    return;
//                component.Highlight();
//            }
//        }

//        public Piece GetHoveringPiece()
//        {
//            return this.InPlaceMode() ? this.m_hoveringPiece : (Piece)null;
//        }

//        private void UpdatePlacementGhost(bool flashGuardStone)
//        {
//            if ((UnityEngine.Object)this.m_placementGhost == (UnityEngine.Object)null)
//            {
//                if (!(bool)(UnityEngine.Object)this.m_placementMarkerInstance)
//                    return;
//                this.m_placementMarkerInstance.SetActive(false);
//            }
//            else
//            {
//                bool flag = ZInput.GetButton("AltPlace") || ZInput.GetButton("JoyAltPlace");
//                Piece component1 = this.m_placementGhost.GetComponent<Piece>();
//                bool water = component1.m_waterPiece || component1.m_noInWater;
//                Vector3 point;
//                Vector3 normal;
//                Piece piece;
//                Heightmap heightmap;
//                Collider waterSurface;
//                if (this.PieceRayTest(out point, out normal, out piece, out heightmap, out waterSurface, water))
//                {
//                    this.m_placementStatus = Player.PlacementStatus.Valid;
//                    if ((UnityEngine.Object)this.m_placementMarkerInstance == (UnityEngine.Object)null)
//                        this.m_placementMarkerInstance = UnityEngine.Object.Instantiate<GameObject>(this.m_placeMarker, point, Quaternion.identity);
//                    this.m_placementMarkerInstance.SetActive(true);
//                    this.m_placementMarkerInstance.transform.position = point;
//                    this.m_placementMarkerInstance.transform.rotation = Quaternion.LookRotation(normal);
//                    if (component1.m_groundOnly || component1.m_groundPiece || component1.m_cultivatedGroundOnly)
//                        this.m_placementMarkerInstance.SetActive(false);
//                    WearNTear wearNtear = (UnityEngine.Object)piece != (UnityEngine.Object)null ? piece.GetComponent<WearNTear>() : (WearNTear)null;
//                    StationExtension component2 = component1.GetComponent<StationExtension>();
//                    if ((UnityEngine.Object)component2 != (UnityEngine.Object)null)
//                    {
//                        CraftingStation closestStationInRange = component2.FindClosestStationInRange(point);
//                        if ((bool)(UnityEngine.Object)closestStationInRange)
//                        {
//                            component2.StartConnectionEffect(closestStationInRange);
//                        }
//                        else
//                        {
//                            component2.StopConnectionEffect();
//                            this.m_placementStatus = Player.PlacementStatus.ExtensionMissingStation;
//                        }
//                        if (component2.OtherExtensionInRange(component1.m_spaceRequirement))
//                            this.m_placementStatus = Player.PlacementStatus.MoreSpace;
//                    }
//                    if ((bool)(UnityEngine.Object)wearNtear && !wearNtear.m_supports)
//                        this.m_placementStatus = Player.PlacementStatus.Invalid;
//                    if (component1.m_waterPiece && (UnityEngine.Object)waterSurface == (UnityEngine.Object)null && !flag)
//                        this.m_placementStatus = Player.PlacementStatus.Invalid;
//                    if (component1.m_noInWater && (UnityEngine.Object)waterSurface != (UnityEngine.Object)null)
//                        this.m_placementStatus = Player.PlacementStatus.Invalid;
//                    if (component1.m_groundPiece && (UnityEngine.Object)heightmap == (UnityEngine.Object)null)
//                    {
//                        this.m_placementGhost.SetActive(false);
//                        this.m_placementStatus = Player.PlacementStatus.Invalid;
//                        return;
//                    }
//                    if (component1.m_groundOnly && (UnityEngine.Object)heightmap == (UnityEngine.Object)null)
//                        this.m_placementStatus = Player.PlacementStatus.Invalid;
//                    if (component1.m_cultivatedGroundOnly && ((UnityEngine.Object)heightmap == (UnityEngine.Object)null || !heightmap.IsCultivated(point)))
//                        this.m_placementStatus = Player.PlacementStatus.NeedCultivated;
//                    if (component1.m_notOnWood && (bool)(UnityEngine.Object)piece && (bool)(UnityEngine.Object)wearNtear && (wearNtear.m_materialType == WearNTear.MaterialType.Wood || wearNtear.m_materialType == WearNTear.MaterialType.HardWood))
//                        this.m_placementStatus = Player.PlacementStatus.Invalid;
//                    if (component1.m_notOnTiltingSurface && (double)normal.y < 0.800000011920929)
//                        this.m_placementStatus = Player.PlacementStatus.Invalid;
//                    if (component1.m_inCeilingOnly && (double)normal.y > -0.5)
//                        this.m_placementStatus = Player.PlacementStatus.Invalid;
//                    if (component1.m_notOnFloor && (double)normal.y > 0.100000001490116)
//                        this.m_placementStatus = Player.PlacementStatus.Invalid;
//                    if (component1.m_onlyInTeleportArea && !(bool)(UnityEngine.Object)EffectArea.IsPointInsideArea(point, EffectArea.Type.Teleport, 0.0f))
//                        this.m_placementStatus = Player.PlacementStatus.NoTeleportArea;
//                    if (!component1.m_allowedInDungeons && this.InInterior())
//                        this.m_placementStatus = Player.PlacementStatus.NotInDungeon;
//                    if ((bool)(UnityEngine.Object)heightmap)
//                        normal = Vector3.up;
//                    this.m_placementGhost.SetActive(true);
//                    Quaternion quaternion = Quaternion.Euler(0.0f, 22.5f * (float)this.m_placeRotation, 0.0f);
//                    if ((component1.m_groundPiece || component1.m_clipGround) && (bool)(UnityEngine.Object)heightmap || component1.m_clipEverything)
//                    {
//                        if ((bool)(UnityEngine.Object)this.m_buildPieces.GetSelectedPrefab().GetComponent<TerrainModifier>() && component1.m_allowAltGroundPlacement && (component1.m_groundPiece && !ZInput.GetButton("AltPlace")) && !ZInput.GetButton("JoyAltPlace"))
//                        {
//                            float groundHeight = ZoneSystem.instance.GetGroundHeight(this.transform.position);
//                            point.y = groundHeight;
//                        }
//                        this.m_placementGhost.transform.position = point;
//                        this.m_placementGhost.transform.rotation = quaternion;
//                    }
//                    else
//                    {
//                        Collider[] componentsInChildren = this.m_placementGhost.GetComponentsInChildren<Collider>();
//                        if (componentsInChildren.Length != 0)
//                        {
//                            this.m_placementGhost.transform.position = point + normal * 50f;
//                            this.m_placementGhost.transform.rotation = quaternion;
//                            Vector3 vector3_1 = Vector3.zero;
//                            float num1 = 999999f;
//                            foreach (Collider collider in componentsInChildren)
//                            {
//                                if (!collider.get_isTrigger() && collider.get_enabled())
//                                {
//                                    MeshCollider meshCollider = collider as MeshCollider;
//                                    if (!((UnityEngine.Object)meshCollider != (UnityEngine.Object)null) || meshCollider.get_convex())
//                                    {
//                                        Vector3 a = collider.ClosestPoint(point);
//                                        float num2 = Vector3.Distance(a, point);
//                                        if ((double)num2 < (double)num1)
//                                        {
//                                            vector3_1 = a;
//                                            num1 = num2;
//                                        }
//                                    }
//                                }
//                            }
//                            Vector3 vector3_2 = this.m_placementGhost.transform.position - vector3_1;
//                            if (component1.m_waterPiece)
//                                vector3_2.y = 3f;
//                            this.m_placementGhost.transform.position = point + vector3_2;
//                            this.m_placementGhost.transform.rotation = quaternion;
//                        }
//                    }
//                    if (!flag)
//                    {
//                        this.m_tempPieces.Clear();
//                        Transform a;
//                        Transform b;
//                        if (this.FindClosestSnapPoints(this.m_placementGhost.transform, 0.5f, out a, out b, this.m_tempPieces))
//                        {
//                            Vector3 position = b.parent.position;
//                            Vector3 p = b.position - (a.position - this.m_placementGhost.transform.position);
//                            if (!this.IsOverlapingOtherPiece(p, this.m_placementGhost.name, this.m_tempPieces))
//                                this.m_placementGhost.transform.position = p;
//                        }
//                    }
//                    if (Location.IsInsideNoBuildLocation(this.m_placementGhost.transform.position))
//                        this.m_placementStatus = Player.PlacementStatus.NoBuildZone;
//                    if (!PrivateArea.CheckAccess(this.m_placementGhost.transform.position, (bool)(UnityEngine.Object)component1.GetComponent<PrivateArea>() ? component1.GetComponent<PrivateArea>().m_radius : 0.0f, flashGuardStone))
//                        this.m_placementStatus = Player.PlacementStatus.PrivateZone;
//                    if (this.CheckPlacementGhostVSPlayers())
//                        this.m_placementStatus = Player.PlacementStatus.BlockedbyPlayer;
//                    if (component1.m_onlyInBiome != Heightmap.Biome.None && (Heightmap.FindBiome(this.m_placementGhost.transform.position) & component1.m_onlyInBiome) == Heightmap.Biome.None)
//                        this.m_placementStatus = Player.PlacementStatus.WrongBiome;
//                    if (component1.m_noClipping && this.TestGhostClipping(this.m_placementGhost, 0.2f))
//                        this.m_placementStatus = Player.PlacementStatus.Invalid;
//                }
//                else
//                {
//                    if ((bool)(UnityEngine.Object)this.m_placementMarkerInstance)
//                        this.m_placementMarkerInstance.SetActive(false);
//                    this.m_placementGhost.SetActive(false);
//                    this.m_placementStatus = Player.PlacementStatus.Invalid;
//                }
//                this.SetPlacementGhostValid(this.m_placementStatus == Player.PlacementStatus.Valid);
//            }
//        }

//        private bool IsOverlapingOtherPiece(Vector3 p, string pieceName, List<Piece> pieces)
//        {
//            foreach (Piece tempPiece in this.m_tempPieces)
//            {
//                if ((double)Vector3.Distance(p, tempPiece.transform.position) < 0.0500000007450581 && tempPiece.gameObject.name.StartsWith(pieceName))
//                    return true;
//            }
//            return false;
//        }

//        private bool FindClosestSnapPoints(
//          Transform ghost,
//          float maxSnapDistance,
//          out Transform a,
//          out Transform b,
//          List<Piece> pieces)
//        {
//            this.m_tempSnapPoints1.Clear();
//            ghost.GetComponent<Piece>().GetSnapPoints(this.m_tempSnapPoints1);
//            this.m_tempSnapPoints2.Clear();
//            this.m_tempPieces.Clear();
//            Piece.GetSnapPoints(ghost.transform.position, 10f, this.m_tempSnapPoints2, this.m_tempPieces);
//            float num = 9999999f;
//            a = (Transform)null;
//            b = (Transform)null;
//            foreach (Transform transform in this.m_tempSnapPoints1)
//            {
//                Transform closest;
//                float distance;
//                if (this.FindClosestSnappoint(transform.position, this.m_tempSnapPoints2, maxSnapDistance, out closest, out distance) && (double)distance < (double)num)
//                {
//                    num = distance;
//                    a = transform;
//                    b = closest;
//                }
//            }
//            return (UnityEngine.Object)a != (UnityEngine.Object)null;
//        }

//        private bool FindClosestSnappoint(
//          Vector3 p,
//          List<Transform> snapPoints,
//          float maxDistance,
//          out Transform closest,
//          out float distance)
//        {
//            closest = (Transform)null;
//            distance = 999999f;
//            foreach (Transform snapPoint in snapPoints)
//            {
//                float num = Vector3.Distance(snapPoint.position, p);
//                if ((double)num <= (double)maxDistance && (double)num < (double)distance)
//                {
//                    closest = snapPoint;
//                    distance = num;
//                }
//            }
//            return (UnityEngine.Object)closest != (UnityEngine.Object)null;
//        }

//        private bool TestGhostClipping(GameObject ghost, float maxPenetration)
//        {
//            Collider[] componentsInChildren = ghost.GetComponentsInChildren<Collider>();
//            Collider[] colliderArray = Physics.OverlapSphere(ghost.transform.position, 10f, this.m_placeRayMask);
//            foreach (Collider collider1 in componentsInChildren)
//            {
//                foreach (Collider collider2 in colliderArray)
//                {
//                    Vector3 vector3;
//                    float num;
//                    if (Physics.ComputePenetration(collider1, ((Component)collider1).transform.position, ((Component)collider1).transform.rotation, collider2, ((Component)collider2).transform.position, ((Component)collider2).transform.rotation, ref vector3, ref num) && (double)num > (double)maxPenetration)
//                        return true;
//                }
//            }
//            return false;
//        }

//        private bool CheckPlacementGhostVSPlayers()
//        {
//            if ((UnityEngine.Object)this.m_placementGhost == (UnityEngine.Object)null)
//                return false;
//            List<Character> characters = new List<Character>();
//            Character.GetCharactersInRange(this.transform.position, 30f, characters);
//            foreach (Collider componentsInChild in this.m_placementGhost.GetComponentsInChildren<Collider>())
//            {
//                if (!componentsInChild.get_isTrigger() && componentsInChild.get_enabled())
//                {
//                    MeshCollider meshCollider = componentsInChild as MeshCollider;
//                    if (!((UnityEngine.Object)meshCollider != (UnityEngine.Object)null) || meshCollider.get_convex())
//                    {
//                        foreach (Character character in characters)
//                        {
//                            CapsuleCollider collider = character.GetCollider();
//                            Vector3 vector3;
//                            float num;
//                            if (Physics.ComputePenetration(componentsInChild, ((Component)componentsInChild).transform.position, ((Component)componentsInChild).transform.rotation, (Collider)collider, ((Component)collider).transform.position, ((Component)collider).transform.rotation, ref vector3, ref num))
//                                return true;
//                        }
//                    }
//                }
//            }
//            return false;
//        }

//        private bool PieceRayTest(
//          out Vector3 point,
//          out Vector3 normal,
//          out Piece piece,
//          out Heightmap heightmap,
//          out Collider waterSurface,
//          bool water)
//        {
//            int num = this.m_placeRayMask;
//            if (water)
//                num = this.m_placeWaterRayMask;
//            RaycastHit raycastHit;
//            if (Physics.Raycast(GameCamera.instance.transform.position, GameCamera.instance.transform.forward, ref raycastHit, 50f, num) && (bool)(UnityEngine.Object)((RaycastHit)ref raycastHit).get_collider() && (!(bool)(UnityEngine.Object)((RaycastHit)ref raycastHit).get_collider().get_attachedRigidbody() && (double)Vector3.Distance(this.m_eye.position, ((RaycastHit)ref raycastHit).get_point()) < (double)this.m_maxPlaceDistance))
//            {
//                point = ((RaycastHit)ref raycastHit).get_point();
//                normal = ((RaycastHit)ref raycastHit).get_normal();
//                piece = ((Component)((RaycastHit)ref raycastHit).get_collider()).GetComponentInParent<Piece>();
//                heightmap = ((Component)((RaycastHit)ref raycastHit).get_collider()).GetComponent<Heightmap>();
//                waterSurface = ((Component)((RaycastHit)ref raycastHit).get_collider()).gameObject.layer != LayerMask.NameToLayer("Water") ? (Collider)null : ((RaycastHit)ref raycastHit).get_collider();
//                return true;
//            }
//            point = Vector3.zero;
//            normal = Vector3.zero;
//            piece = (Piece)null;
//            heightmap = (Heightmap)null;
//            waterSurface = (Collider)null;
//            return false;
//        }

//        private void FindHoverObject(out GameObject hover, out Character hoverCreature)
//        {
//            hover = (GameObject)null;
//            hoverCreature = (Character)null;
//            RaycastHit[] array = Physics.RaycastAll(GameCamera.instance.transform.position, GameCamera.instance.transform.forward, 50f, this.m_interactMask);
//            Array.Sort<RaycastHit>(array, (Comparison<RaycastHit>)((x, y) => ((RaycastHit)ref x).get_distance().CompareTo(((RaycastHit)ref y).get_distance())));
//            foreach (RaycastHit raycastHit in array)
//            {
//                if (!(bool)(UnityEngine.Object)((RaycastHit)ref raycastHit).get_collider().get_attachedRigidbody() || !((UnityEngine.Object)((Component)((RaycastHit)ref raycastHit).get_collider().get_attachedRigidbody()).gameObject == (UnityEngine.Object)this.gameObject))
//                {
//                    if ((UnityEngine.Object)hoverCreature == (UnityEngine.Object)null)
//                    {
//                        Character character = (bool)(UnityEngine.Object)((RaycastHit)ref raycastHit).get_collider().get_attachedRigidbody() ? ((Component)((RaycastHit)ref raycastHit).get_collider().get_attachedRigidbody()).GetComponent<Character>() : ((Component)((RaycastHit)ref raycastHit).get_collider()).GetComponent<Character>();
//                        if ((UnityEngine.Object)character != (UnityEngine.Object)null)
//                            hoverCreature = character;
//                    }
//                    if ((double)Vector3.Distance(this.m_eye.position, ((RaycastHit)ref raycastHit).get_point()) >= (double)this.m_maxInteractDistance)
//                        break;
//                    if (((Component)((RaycastHit)ref raycastHit).get_collider()).GetComponent<Hoverable>() != null)
//                    {
//                        hover = ((Component)((RaycastHit)ref raycastHit).get_collider()).gameObject;
//                        break;
//                    }
//                    if ((bool)(UnityEngine.Object)((RaycastHit)ref raycastHit).get_collider().get_attachedRigidbody())
//                    {
//                        hover = ((Component)((RaycastHit)ref raycastHit).get_collider().get_attachedRigidbody()).gameObject;
//                        break;
//                    }
//                    hover = ((Component)((RaycastHit)ref raycastHit).get_collider()).gameObject;
//                    break;
//                }
//            }
//        }

//        private void Interact(GameObject go, bool hold)
//        {
//            if (this.InAttack() || this.InDodge() || hold && (double)Time.time - (double)this.m_lastHoverInteractTime < 0.200000002980232)
//                return;
//            Interactable componentInParent = go.GetComponentInParent<Interactable>();
//            if (componentInParent == null)
//                return;
//            this.m_lastHoverInteractTime = Time.time;
//            if (!componentInParent.Interact((Humanoid)this, hold))
//                return;
//            Vector3 forward = go.transform.position - this.transform.position;
//            forward.y = 0.0f;
//            forward.Normalize();
//            this.transform.rotation = Quaternion.LookRotation(forward);
//            this.m_zanim.SetTrigger("interact");
//        }

//        private void UpdateStations(float dt)
//        {
//            this.m_stationDiscoverTimer += dt;
//            if ((double)this.m_stationDiscoverTimer > 1.0)
//            {
//                this.m_stationDiscoverTimer = 0.0f;
//                CraftingStation.UpdateKnownStationsInRange(this);
//            }
//            if (!((UnityEngine.Object)this.m_currentStation != (UnityEngine.Object)null))
//                return;
//            if (!this.m_currentStation.InUseDistance((Humanoid)this))
//            {
//                InventoryGui.instance.Hide();
//                this.SetCraftingStation((CraftingStation)null);
//            }
//            else if (!InventoryGui.IsVisible())
//            {
//                this.SetCraftingStation((CraftingStation)null);
//            }
//            else
//            {
//                this.m_currentStation.PokeInUse();
//                if (!(bool)(UnityEngine.Object)this.m_currentStation || this.AlwaysRotateCamera())
//                    return;
//                Vector3 normalized = (this.m_currentStation.transform.position - this.transform.position).normalized;
//                normalized.y = 0.0f;
//                normalized.Normalize();
//                this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, Quaternion.LookRotation(normalized), this.m_turnSpeed * dt);
//            }
//        }

//        private void UpdateCover(float dt)
//        {
//            this.m_updateCoverTimer += dt;
//            if ((double)this.m_updateCoverTimer <= 1.0)
//                return;
//            this.m_updateCoverTimer = 0.0f;
//            Cover.GetCoverForPoint(this.GetCenterPoint(), out this.m_coverPercentage, out this.m_underRoof);
//        }

//        public Character GetHoverCreature()
//        {
//            return this.m_hoveringCreature;
//        }

//        public override GameObject GetHoverObject()
//        {
//            return this.m_hovering;
//        }

//        public override void OnNearFire(Vector3 point)
//        {
//            this.m_nearFireTimer = 0.0f;
//        }

//        public bool InShelter()
//        {
//            return (double)this.m_coverPercentage >= 0.800000011920929 && this.m_underRoof;
//        }

//        public float GetStamina()
//        {
//            return this.m_stamina;
//        }

//        public override float GetMaxStamina()
//        {
//            return this.m_maxStamina;
//        }

//        public override float GetStaminaPercentage()
//        {
//            return this.m_stamina / this.m_maxStamina;
//        }

//        public void SetGodMode(bool godMode)
//        {
//            this.m_godMode = godMode;
//        }

//        public override bool InGodMode()
//        {
//            return this.m_godMode;
//        }

//        public void SetGhostMode(bool ghostmode)
//        {
//            this.m_ghostMode = ghostmode;
//        }

//        public override bool InGhostMode()
//        {
//            return this.m_ghostMode;
//        }

//        public override bool IsDebugFlying()
//        {
//            return this.m_nview.IsOwner() ? this.m_debugFly : this.m_nview.GetZDO().GetBool("DebugFly", false);
//        }

//        public override void AddStamina(float v)
//        {
//            this.m_stamina += v;
//            if ((double)this.m_stamina <= (double)this.m_maxStamina)
//                return;
//            this.m_stamina = this.m_maxStamina;
//        }

//        public override void UseStamina(float v)
//        {
//            if ((double)v == 0.0 || !this.m_nview.IsValid())
//                return;
//            if (this.m_nview.IsOwner())
//                this.RPC_UseStamina(0L, v);
//            else
//                this.m_nview.InvokeRPC(nameof(UseStamina), (object)v);
//        }

//        private void RPC_UseStamina(long sender, float v)
//        {
//            if ((double)v == 0.0)
//                return;
//            this.m_stamina -= v;
//            if ((double)this.m_stamina < 0.0)
//                this.m_stamina = 0.0f;
//            this.m_staminaRegenTimer = this.m_staminaRegenDelay;
//        }

//        public override bool HaveStamina(float amount = 0.0f)
//        {
//            return this.m_nview.IsValid() && !this.m_nview.IsOwner() ? (double)this.m_nview.GetZDO().GetFloat("stamina", this.m_maxStamina) > (double)amount : (double)this.m_stamina > (double)amount;
//        }

//        public void Save(ZPackage pkg)
//        {
//            pkg.Write(24);
//            pkg.Write(this.GetMaxHealth());
//            pkg.Write(this.GetHealth());
//            pkg.Write(this.GetMaxStamina());
//            pkg.Write(this.m_firstSpawn);
//            pkg.Write(this.m_timeSinceDeath);
//            pkg.Write(this.m_guardianPower);
//            pkg.Write(this.m_guardianPowerCooldown);
//            this.m_inventory.Save(pkg);
//            pkg.Write(this.m_knownRecipes.Count);
//            foreach (string knownRecipe in this.m_knownRecipes)
//                pkg.Write(knownRecipe);
//            pkg.Write(this.m_knownStations.Count);
//            foreach (KeyValuePair<string, int> knownStation in this.m_knownStations)
//            {
//                pkg.Write(knownStation.Key);
//                pkg.Write(knownStation.Value);
//            }
//            pkg.Write(this.m_knownMaterial.Count);
//            foreach (string data in this.m_knownMaterial)
//                pkg.Write(data);
//            pkg.Write(this.m_shownTutorials.Count);
//            foreach (string shownTutorial in this.m_shownTutorials)
//                pkg.Write(shownTutorial);
//            pkg.Write(this.m_uniques.Count);
//            foreach (string unique in this.m_uniques)
//                pkg.Write(unique);
//            pkg.Write(this.m_trophies.Count);
//            foreach (string trophy in this.m_trophies)
//                pkg.Write(trophy);
//            pkg.Write(this.m_knownBiome.Count);
//            foreach (Heightmap.Biome biome in this.m_knownBiome)
//                pkg.Write((int)biome);
//            pkg.Write(this.m_knownTexts.Count);
//            foreach (KeyValuePair<string, string> knownText in this.m_knownTexts)
//            {
//                pkg.Write(knownText.Key);
//                pkg.Write(knownText.Value);
//            }
//            pkg.Write(this.m_beardItem);
//            pkg.Write(this.m_hairItem);
//            pkg.Write(this.m_skinColor);
//            pkg.Write(this.m_hairColor);
//            pkg.Write(this.m_modelIndex);
//            pkg.Write(this.m_foods.Count);
//            foreach (Player.Food food in this.m_foods)
//            {
//                pkg.Write(food.m_name);
//                pkg.Write(food.m_health);
//                pkg.Write(food.m_stamina);
//            }
//            this.m_skills.Save(pkg);
//        }

//        public void Load(ZPackage pkg)
//        {
//            this.m_isLoading = true;
//            this.UnequipAllItems();
//            int num1 = pkg.ReadInt();
//            if (num1 >= 7)
//                this.SetMaxHealth(pkg.ReadSingle(), false);
//            float num2 = pkg.ReadSingle();
//            float maxHealth = this.GetMaxHealth();
//            if ((double)num2 <= 0.0 || (double)num2 > (double)maxHealth || float.IsNaN(num2))
//                num2 = maxHealth;
//            this.SetHealth(num2);
//            if (num1 >= 10)
//            {
//                float stamina = pkg.ReadSingle();
//                this.SetMaxStamina(stamina, false);
//                this.m_stamina = stamina;
//            }
//            if (num1 >= 8)
//                this.m_firstSpawn = pkg.ReadBool();
//            if (num1 >= 20)
//                this.m_timeSinceDeath = pkg.ReadSingle();
//            if (num1 >= 23)
//                this.SetGuardianPower(pkg.ReadString());
//            if (num1 >= 24)
//                this.m_guardianPowerCooldown = pkg.ReadSingle();
//            if (num1 == 2)
//                pkg.ReadZDOID();
//            this.m_inventory.Load(pkg);
//            int num3 = pkg.ReadInt();
//            for (int index = 0; index < num3; ++index)
//                this.m_knownRecipes.Add(pkg.ReadString());
//            if (num1 < 15)
//            {
//                int num4 = pkg.ReadInt();
//                for (int index = 0; index < num4; ++index)
//                    pkg.ReadString();
//            }
//            else
//            {
//                int num4 = pkg.ReadInt();
//                for (int index = 0; index < num4; ++index)
//                    this.m_knownStations.Add(pkg.ReadString(), pkg.ReadInt());
//            }
//            int num5 = pkg.ReadInt();
//            for (int index = 0; index < num5; ++index)
//                this.m_knownMaterial.Add(pkg.ReadString());
//            if (num1 < 19 || num1 >= 21)
//            {
//                int num4 = pkg.ReadInt();
//                for (int index = 0; index < num4; ++index)
//                    this.m_shownTutorials.Add(pkg.ReadString());
//            }
//            if (num1 >= 6)
//            {
//                int num4 = pkg.ReadInt();
//                for (int index = 0; index < num4; ++index)
//                    this.m_uniques.Add(pkg.ReadString());
//            }
//            if (num1 >= 9)
//            {
//                int num4 = pkg.ReadInt();
//                for (int index = 0; index < num4; ++index)
//                    this.m_trophies.Add(pkg.ReadString());
//            }
//            if (num1 >= 18)
//            {
//                int num4 = pkg.ReadInt();
//                for (int index = 0; index < num4; ++index)
//                    this.m_knownBiome.Add((Heightmap.Biome)pkg.ReadInt());
//            }
//            if (num1 >= 22)
//            {
//                int num4 = pkg.ReadInt();
//                for (int index = 0; index < num4; ++index)
//                    this.m_knownTexts.Add(pkg.ReadString(), pkg.ReadString());
//            }
//            if (num1 >= 4)
//            {
//                string name = pkg.ReadString();
//                string hair = pkg.ReadString();
//                this.SetBeard(name);
//                this.SetHair(hair);
//            }
//            if (num1 >= 5)
//            {
//                Vector3 color1 = pkg.ReadVector3();
//                Vector3 color2 = pkg.ReadVector3();
//                this.SetSkinColor(color1);
//                this.SetHairColor(color2);
//            }
//            if (num1 >= 11)
//                this.SetPlayerModel(pkg.ReadInt());
//            if (num1 >= 12)
//            {
//                this.m_foods.Clear();
//                int num4 = pkg.ReadInt();
//                for (int index = 0; index < num4; ++index)
//                {
//                    if (num1 >= 14)
//                    {
//                        Player.Food food = new Player.Food();
//                        food.m_name = pkg.ReadString();
//                        food.m_health = pkg.ReadSingle();
//                        if (num1 >= 16)
//                            food.m_stamina = pkg.ReadSingle();
//                        GameObject itemPrefab = ObjectDB.instance.GetItemPrefab(food.m_name);
//                        if ((UnityEngine.Object)itemPrefab == (UnityEngine.Object)null)
//                        {
//                            ZLog.LogWarning((object)("FAiled to find food item " + food.m_name));
//                        }
//                        else
//                        {
//                            food.m_item = itemPrefab.GetComponent<ItemDrop>().m_itemData;
//                            this.m_foods.Add(food);
//                        }
//                    }
//                    else
//                    {
//                        pkg.ReadString();
//                        double num6 = (double)pkg.ReadSingle();
//                        double num7 = (double)pkg.ReadSingle();
//                        double num8 = (double)pkg.ReadSingle();
//                        double num9 = (double)pkg.ReadSingle();
//                        double num10 = (double)pkg.ReadSingle();
//                        double num11 = (double)pkg.ReadSingle();
//                        if (num1 >= 13)
//                        {
//                            double num12 = (double)pkg.ReadSingle();
//                        }
//                    }
//                }
//            }
//            if (num1 >= 17)
//                this.m_skills.Load(pkg);
//            this.m_isLoading = false;
//            this.UpdateAvailablePiecesList();
//            this.EquipIventoryItems();
//        }

//        private void EquipIventoryItems()
//        {
//            foreach (ItemData equipedtem in this.m_inventory.GetEquipedtems())
//            {
//                if (!this.EquipItem(equipedtem, false))
//                    equipedtem.m_equiped = false;
//            }
//        }

//        public override bool CanMove()
//        {
//            return !this.m_teleporting && !this.InCutscene() && (!this.IsEncumbered() || this.HaveStamina(0.0f)) && base.CanMove();
//        }

//        public override bool IsEncumbered()
//        {
//            return (double)this.m_inventory.GetTotalWeight() > (double)this.GetMaxCarryWeight();
//        }

//        public float GetMaxCarryWeight()
//        {
//            float maxCarryWeight = this.m_maxCarryWeight;
//            this.m_seman.ModifyMaxCarryWeight(maxCarryWeight, ref maxCarryWeight);
//            return maxCarryWeight;
//        }

//        public override bool HaveUniqueKey(string name)
//        {
//            return this.m_uniques.Contains(name);
//        }

//        public override void AddUniqueKey(string name)
//        {
//            if (this.m_uniques.Contains(name))
//                return;
//            this.m_uniques.Add(name);
//        }

//        public bool IsBiomeKnown(Heightmap.Biome biome)
//        {
//            return this.m_knownBiome.Contains(biome);
//        }

//        public void AddKnownBiome(Heightmap.Biome biome)
//        {
//            if (this.m_knownBiome.Contains(biome))
//                return;
//            this.m_knownBiome.Add(biome);
//            if (biome != Heightmap.Biome.Meadows && biome != Heightmap.Biome.None)
//                MessageHud.instance.ShowBiomeFoundMsg("$biome_" + biome.ToString().ToLower(), true);
//            if (biome == Heightmap.Biome.BlackForest && !ZoneSystem.instance.GetGlobalKey("defeated_eikthyr"))
//                this.ShowTutorial("blackforest", false);
//            Gogan.LogEvent("Game", "BiomeFound", biome.ToString(), 0L);
//        }

//        public bool IsRecipeKnown(string name)
//        {
//            return this.m_knownRecipes.Contains(name);
//        }

//        public void AddKnownRecipe(Recipe recipe)
//        {
//            if (this.m_knownRecipes.Contains(recipe.m_item.m_itemData.m_shared.m_name))
//                return;
//            this.m_knownRecipes.Add(recipe.m_item.m_itemData.m_shared.m_name);
//            MessageHud.instance.QueueUnlockMsg(recipe.m_item.m_itemData.GetIcon(), "$msg_newrecipe", recipe.m_item.m_itemData.m_shared.m_name);
//            Gogan.LogEvent("Game", "RecipeFound", recipe.m_item.m_itemData.m_shared.m_name, 0L);
//        }

//        public void AddKnownPiece(Piece piece)
//        {
//            if (this.m_knownRecipes.Contains(piece.m_name))
//                return;
//            this.m_knownRecipes.Add(piece.m_name);
//            MessageHud.instance.QueueUnlockMsg(piece.m_icon, "$msg_newpiece", piece.m_name);
//            Gogan.LogEvent("Game", "PieceFound", piece.m_name, 0L);
//        }

//        public void AddKnownStation(CraftingStation station)
//        {
//            int level = station.GetLevel();
//            int num;
//            if (this.m_knownStations.TryGetValue(station.m_name, out num))
//            {
//                if (num >= level)
//                    return;
//                this.m_knownStations[station.m_name] = level;
//                MessageHud.instance.QueueUnlockMsg(station.m_icon, "$msg_newstation_level", station.m_name + " $msg_level " + (object)level);
//                this.UpdateKnownRecipesList();
//            }
//            else
//            {
//                this.m_knownStations.Add(station.m_name, level);
//                MessageHud.instance.QueueUnlockMsg(station.m_icon, "$msg_newstation", station.m_name);
//                Gogan.LogEvent("Game", "StationFound", station.m_name, 0L);
//                this.UpdateKnownRecipesList();
//            }
//        }

//        private bool KnowStationLevel(string name, int level)
//        {
//            int num;
//            return this.m_knownStations.TryGetValue(name, out num) && num >= level;
//        }

//        public void AddKnownText(string label, string text)
//        {
//            if (label.Length == 0)
//            {
//                ZLog.LogWarning((object)("Text " + text + " Is missing label"));
//            }
//            else
//            {
//                if (this.m_knownTexts.ContainsKey(label))
//                    return;
//                this.m_knownTexts.Add(label, text);
//                this.Message(MessageHud.MessageType.TopLeft, Localization.instance.Localize("$msg_newtext", label), 0, this.m_textIcon);
//            }
//        }

//        public List<KeyValuePair<string, string>> GetKnownTexts()
//        {
//            return this.m_knownTexts.ToList<KeyValuePair<string, string>>();
//        }

//        public void AddKnownItem(ItemData item)
//        {
//            if (item.m_shared.m_itemType == ItemData.ItemType.Trophie)
//                this.AddTrophie(item);
//            if (this.m_knownMaterial.Contains(item.m_shared.m_name))
//                return;
//            this.m_knownMaterial.Add(item.m_shared.m_name);
//            if (item.m_shared.m_itemType == ItemData.ItemType.Material)
//                MessageHud.instance.QueueUnlockMsg(item.GetIcon(), "$msg_newmaterial", item.m_shared.m_name);
//            else if (item.m_shared.m_itemType == ItemData.ItemType.Trophie)
//                MessageHud.instance.QueueUnlockMsg(item.GetIcon(), "$msg_newtrophy", item.m_shared.m_name);
//            else
//                MessageHud.instance.QueueUnlockMsg(item.GetIcon(), "$msg_newitem", item.m_shared.m_name);
//            Gogan.LogEvent("Game", "ItemFound", item.m_shared.m_name, 0L);
//            this.UpdateKnownRecipesList();
//        }

//        private void AddTrophie(ItemData item)
//        {
//            if (item.m_shared.m_itemType != ItemData.ItemType.Trophie || this.m_trophies.Contains(item.m_dropPrefab.name))
//                return;
//            this.m_trophies.Add(item.m_dropPrefab.name);
//        }

//        public List<string> GetTrophies()
//        {
//            List<string> stringList = new List<string>();
//            stringList.AddRange((IEnumerable<string>)this.m_trophies);
//            return stringList;
//        }

//        private void UpdateKnownRecipesList()
//        {
//            if ((UnityEngine.Object)Game.instance == (UnityEngine.Object)null)
//                return;
//            foreach (Recipe recipe in ObjectDB.instance.m_recipes)
//            {
//                if (recipe.m_enabled && !this.m_knownRecipes.Contains(recipe.m_item.m_itemData.m_shared.m_name) && this.HaveRequirements(recipe, true, 0))
//                    this.AddKnownRecipe(recipe);
//            }
//            this.m_tempOwnedPieceTables.Clear();
//            this.m_inventory.GetAllPieceTables(this.m_tempOwnedPieceTables);
//            bool flag = false;
//            foreach (PieceTable tempOwnedPieceTable in this.m_tempOwnedPieceTables)
//            {
//                foreach (GameObject piece in tempOwnedPieceTable.m_pieces)
//                {
//                    Piece component = piece.GetComponent<Piece>();
//                    if (component.m_enabled && !this.m_knownRecipes.Contains(component.m_name) && this.HaveRequirements(component, Player.RequirementMode.IsKnown))
//                    {
//                        this.AddKnownPiece(component);
//                        flag = true;
//                    }
//                }
//            }
//            if (!flag)
//                return;
//            this.UpdateAvailablePiecesList();
//        }

//        private void UpdateAvailablePiecesList()
//        {
//            if ((UnityEngine.Object)this.m_buildPieces != (UnityEngine.Object)null)
//                this.m_buildPieces.UpdateAvailable(this.m_knownRecipes, this, this.m_hideUnavailable, this.m_noPlacementCost);
//            this.SetupPlacementGhost();
//        }

//        public override void Message(MessageHud.MessageType type, string msg, int amount = 0, Sprite icon = null)
//        {
//            if ((UnityEngine.Object)this.m_nview == (UnityEngine.Object)null || !this.m_nview.IsValid())
//                return;
//            if (this.m_nview.IsOwner())
//            {
//                if (!(bool)(UnityEngine.Object)MessageHud.instance)
//                    return;
//                MessageHud.instance.ShowMessage(type, msg, amount, icon);
//            }
//            else
//                this.m_nview.InvokeRPC(nameof(Message), (object)(int)type, (object)msg, (object)amount);
//        }

//        private void RPC_Message(long sender, int type, string msg, int amount)
//        {
//            if (!this.m_nview.IsOwner() || !(bool)(UnityEngine.Object)MessageHud.instance)
//                return;
//            MessageHud.instance.ShowMessage((MessageHud.MessageType)type, msg, amount, (Sprite)null);
//        }

//        public static Player GetPlayer(long playerID)
//        {
//            foreach (Player player in Player.m_players)
//            {
//                if (player.GetPlayerID() == playerID)
//                    return player;
//            }
//            return (Player)null;
//        }

//        public static Player GetClosestPlayer(Vector3 point, float maxRange)
//        {
//            Player player1 = (Player)null;
//            float num1 = 999999f;
//            foreach (Player player2 in Player.m_players)
//            {
//                float num2 = Vector3.Distance(player2.transform.position, point);
//                if ((double)num2 < (double)num1 && (double)num2 < (double)maxRange)
//                {
//                    num1 = num2;
//                    player1 = player2;
//                }
//            }
//            return player1;
//        }

//        public static bool IsPlayerInRange(Vector3 point, float range, long playerID)
//        {
//            foreach (Player player in Player.m_players)
//            {
//                if (player.GetPlayerID() == playerID)
//                    return (double)Utils.DistanceXZ(player.transform.position, point) < (double)range;
//            }
//            return false;
//        }

//        public static void MessageAllInRange(
//          Vector3 point,
//          float range,
//          MessageHud.MessageType type,
//          string msg,
//          Sprite icon = null)
//        {
//            foreach (Player player in Player.m_players)
//            {
//                if ((double)Vector3.Distance(player.transform.position, point) < (double)range)
//                    player.Message(type, msg, 0, icon);
//            }
//        }

//        public static int GetPlayersInRangeXZ(Vector3 point, float range)
//        {
//            int num = 0;
//            foreach (Component player in Player.m_players)
//            {
//                if ((double)Utils.DistanceXZ(player.transform.position, point) < (double)range)
//                    ++num;
//            }
//            return num;
//        }

//        public static void GetPlayersInRange(Vector3 point, float range, List<Player> players)
//        {
//            foreach (Player player in Player.m_players)
//            {
//                if ((double)Vector3.Distance(player.transform.position, point) < (double)range)
//                    players.Add(player);
//            }
//        }

//        public static bool IsPlayerInRange(Vector3 point, float range)
//        {
//            foreach (Component player in Player.m_players)
//            {
//                if ((double)Vector3.Distance(player.transform.position, point) < (double)range)
//                    return true;
//            }
//            return false;
//        }

//        public static bool IsPlayerInRange(Vector3 point, float range, float minNoise)
//        {
//            foreach (Player player in Player.m_players)
//            {
//                if ((double)Vector3.Distance(player.transform.position, point) < (double)range)
//                {
//                    float noiseRange = player.GetNoiseRange();
//                    if ((double)range <= (double)noiseRange && (double)noiseRange >= (double)minNoise)
//                        return true;
//                }
//            }
//            return false;
//        }

//        public static Player GetPlayerNoiseRange(Vector3 point, float noiseRangeScale = 1f)
//        {
//            foreach (Player player in Player.m_players)
//            {
//                if ((double)Vector3.Distance(player.transform.position, point) < (double)player.GetNoiseRange() * (double)noiseRangeScale)
//                    return player;
//            }
//            return (Player)null;
//        }

//        public static List<Player> GetAllPlayers()
//        {
//            return Player.m_players;
//        }

//        public static Player GetRandomPlayer()
//        {
//            return Player.m_players.Count == 0 ? (Player)null : Player.m_players[UnityEngine.Random.Range(0, Player.m_players.Count)];
//        }

//        public void GetAvailableRecipes(ref List<Recipe> available)
//        {
//            available.Clear();
//            foreach (Recipe recipe in ObjectDB.instance.m_recipes)
//            {
//                if (recipe.m_enabled && (recipe.m_item.m_itemData.m_shared.m_dlc.Length <= 0 || DLCMan.instance.IsDLCInstalled(recipe.m_item.m_itemData.m_shared.m_dlc)) && ((this.m_knownRecipes.Contains(recipe.m_item.m_itemData.m_shared.m_name) || this.m_noPlacementCost) && (this.RequiredCraftingStation(recipe, 1, false) || this.m_noPlacementCost)))
//                    available.Add(recipe);
//            }
//        }

//        private void OnInventoryChanged()
//        {
//            if (this.m_isLoading)
//                return;
//            foreach (ItemData allItem in this.m_inventory.GetAllItems())
//            {
//                this.AddKnownItem(allItem);
//                if (allItem.m_shared.m_name == "$item_hammer")
//                    this.ShowTutorial("hammer", false);
//                else if (allItem.m_shared.m_name == "$item_hoe")
//                    this.ShowTutorial("hoe", false);
//                else if (allItem.m_shared.m_name == "$item_pickaxe_antler")
//                    this.ShowTutorial("pickaxe", false);
//                if (allItem.m_shared.m_name == "$item_trophy_eikthyr")
//                    this.ShowTutorial("boss_trophy", false);
//                if (allItem.m_shared.m_name == "$item_wishbone")
//                    this.ShowTutorial("wishbone", false);
//                else if (allItem.m_shared.m_name == "$item_copperore" || allItem.m_shared.m_name == "$item_tinore")
//                    this.ShowTutorial("ore", false);
//                else if ((double)allItem.m_shared.m_food > 0.0)
//                    this.ShowTutorial("food", false);
//            }
//            this.UpdateKnownRecipesList();
//            this.UpdateAvailablePiecesList();
//        }

//        public bool InDebugFlyMode()
//        {
//            return this.m_debugFly;
//        }

//        public void ShowTutorial(string name, bool force = false)
//        {
//            if (this.HaveSeenTutorial(name))
//                return;
//            Tutorial.instance.ShowText(name, force);
//        }

//        public void SetSeenTutorial(string name)
//        {
//            if (name.Length == 0 || this.m_shownTutorials.Contains(name))
//                return;
//            this.m_shownTutorials.Add(name);
//        }

//        public bool HaveSeenTutorial(string name)
//        {
//            return name.Length != 0 && this.m_shownTutorials.Contains(name);
//        }

//        public static bool IsSeenTutorialsCleared()
//        {
//            return !(bool)(UnityEngine.Object)Player.m_localPlayer || Player.m_localPlayer.m_shownTutorials.Count == 0;
//        }

//        public static void ResetSeenTutorials()
//        {
//            if (!(bool)(UnityEngine.Object)Player.m_localPlayer)
//                return;
//            Player.m_localPlayer.m_shownTutorials.Clear();
//        }

//        public void SetMouseLook(Vector2 mouseLook)
//        {
//            this.m_lookYaw = this.m_lookYaw * Quaternion.Euler(0.0f, mouseLook.x, 0.0f);
//            this.m_lookPitch = Mathf.Clamp(this.m_lookPitch - mouseLook.y, -89f, 89f);
//            this.UpdateEyeRotation();
//            this.m_lookDir = this.m_eye.forward;
//        }

//        protected override void UpdateEyeRotation()
//        {
//            this.m_eye.rotation = this.m_lookYaw * Quaternion.Euler(this.m_lookPitch, 0.0f, 0.0f);
//        }

//        public Ragdoll GetRagdoll()
//        {
//            return this.m_ragdoll;
//        }

//        public void OnDodgeMortal()
//        {
//            this.m_dodgeInvincible = false;
//        }

//        private void UpdateDodge(float dt)
//        {
//            this.m_queuedDodgeTimer -= dt;
//            if ((double)this.m_queuedDodgeTimer > 0.0 && this.IsOnGround() && (!this.IsDead() && !this.InAttack()) && (!this.IsEncumbered() && !this.InDodge()))
//            {
//                float num = this.m_dodgeStaminaUsage - this.m_dodgeStaminaUsage * this.m_equipmentMovementModifier;
//                if (this.HaveStamina(num))
//                {
//                    this.AbortEquipQueue();
//                    this.m_queuedDodgeTimer = 0.0f;
//                    this.m_dodgeInvincible = true;
//                    this.transform.rotation = Quaternion.LookRotation(this.m_queuedDodgeDir);
//                    this.m_body.set_rotation(this.transform.rotation);
//                    this.m_zanim.SetTrigger("dodge");
//                    this.AddNoise(5f);
//                    this.UseStamina(num);
//                    this.m_dodgeEffects.Create(this.transform.position, Quaternion.identity, this.transform, 1f);
//                }
//                else
//                    Hud.instance.StaminaBarNoStaminaFlash();
//            }
//            AnimatorStateInfo animatorStateInfo1 = this.m_animator.GetCurrentAnimatorStateInfo(0);
//            AnimatorStateInfo animatorStateInfo2 = this.m_animator.GetNextAnimatorStateInfo(0);
//            bool flag1 = this.m_animator.IsInTransition(0);
//            bool flag2 = this.m_animator.GetBool("dodge") || ((AnimatorStateInfo)ref animatorStateInfo1).get_tagHash() == Player.m_animatorTagDodge && !flag1 || flag1 && ((AnimatorStateInfo)ref animatorStateInfo2).get_tagHash() == Player.m_animatorTagDodge;
//            this.m_nview.GetZDO().Set("dodgeinv", flag2 && this.m_dodgeInvincible);
//            this.m_inDodge = flag2;
//        }

//        public override bool IsDodgeInvincible()
//        {
//            return this.m_nview.IsValid() && this.m_nview.GetZDO().GetBool("dodgeinv", false);
//        }

//        public override bool InDodge()
//        {
//            return this.m_nview.IsValid() && this.m_nview.IsOwner() && this.m_inDodge;
//        }

//        public override bool IsDead()
//        {
//            ZDO zdo = this.m_nview.GetZDO();
//            return zdo != null && zdo.GetBool("dead", false);
//        }

//        protected void Dodge(Vector3 dodgeDir)
//        {
//            this.m_queuedDodgeTimer = 0.5f;
//            this.m_queuedDodgeDir = dodgeDir;
//        }

//        public override bool AlwaysRotateCamera()
//        {
//            return this.GetCurrentWeapon() != null && this.m_currentAttack != null && ((double)this.m_lastCombatTimer < 1.0 && this.m_currentAttack.m_attackType != Attack.AttackType.None) && ZInput.IsMouseActive() || (this.IsHoldingAttack() || this.m_blocking || this.InPlaceMode() && (double)Vector3.Angle(this.GetLookYaw() * Vector3.forward, this.transform.forward) > 90.0);
//        }

//        public override bool TeleportTo(Vector3 pos, Quaternion rot, bool distantTeleport)
//        {
//            if (this.IsTeleporting() || (double)this.m_teleportCooldown < 2.0)
//                return false;
//            this.m_teleporting = true;
//            this.m_distantTeleport = distantTeleport;
//            this.m_teleportTimer = 0.0f;
//            this.m_teleportCooldown = 0.0f;
//            this.m_teleportFromPos = this.transform.position;
//            this.m_teleportFromRot = this.transform.rotation;
//            this.m_teleportTargetPos = pos;
//            this.m_teleportTargetRot = rot;
//            return true;
//        }

//        private void UpdateTeleport(float dt)
//        {
//            if (!this.m_teleporting)
//            {
//                this.m_teleportCooldown += dt;
//            }
//            else
//            {
//                this.m_teleportCooldown = 0.0f;
//                this.m_teleportTimer += dt;
//                if ((double)this.m_teleportTimer <= 2.0)
//                    return;
//                Vector3 dir = this.m_teleportTargetRot * Vector3.forward;
//                this.transform.position = this.m_teleportTargetPos;
//                this.transform.rotation = this.m_teleportTargetRot;
//                this.m_body.set_velocity(Vector3.zero);
//                this.m_maxAirAltitude = this.transform.position.y;
//                this.SetLookDir(dir);
//                if ((double)this.m_teleportTimer <= 8.0 && this.m_distantTeleport || !ZNetScene.instance.IsAreaReady(this.m_teleportTargetPos))
//                    return;
//                float height = 0.0f;
//                if (ZoneSystem.instance.FindFloor(this.m_teleportTargetPos, out height))
//                {
//                    this.m_teleportTimer = 0.0f;
//                    this.m_teleporting = false;
//                    this.ResetCloth();
//                }
//                else
//                {
//                    if ((double)this.m_teleportTimer <= 15.0 && this.m_distantTeleport)
//                        return;
//                    if (this.m_distantTeleport)
//                    {
//                        Vector3 position = this.transform.position;
//                        position.y = ZoneSystem.instance.GetSolidHeight(this.m_teleportTargetPos) + 0.5f;
//                        this.transform.position = position;
//                    }
//                    else
//                    {
//                        this.transform.rotation = this.m_teleportFromRot;
//                        this.transform.position = this.m_teleportFromPos;
//                        this.m_maxAirAltitude = this.transform.position.y;
//                        this.Message(MessageHud.MessageType.Center, "$msg_portal_blocked", 0, (Sprite)null);
//                    }
//                    this.m_teleportTimer = 0.0f;
//                    this.m_teleporting = false;
//                    this.ResetCloth();
//                }
//            }
//        }

//        public override bool IsTeleporting()
//        {
//            return this.m_teleporting;
//        }

//        public bool ShowTeleportAnimation()
//        {
//            return this.m_teleporting && this.m_distantTeleport;
//        }

//        public void SetPlayerModel(int index)
//        {
//            if (this.m_modelIndex == index)
//                return;
//            this.m_modelIndex = index;
//            this.m_visEquipment.SetModel(index);
//        }

//        public int GetPlayerModel()
//        {
//            return this.m_modelIndex;
//        }

//        public void SetSkinColor(Vector3 color)
//        {
//            if (color == this.m_skinColor)
//                return;
//            this.m_skinColor = color;
//            this.m_visEquipment.SetSkinColor(this.m_skinColor);
//        }

//        public void SetHairColor(Vector3 color)
//        {
//            if (this.m_hairColor == color)
//                return;
//            this.m_hairColor = color;
//            this.m_visEquipment.SetHairColor(this.m_hairColor);
//        }

//        protected override void SetupVisEquipment(VisEquipment visEq, bool isRagdoll)
//        {
//            base.SetupVisEquipment(visEq, isRagdoll);
//            visEq.SetModel(this.m_modelIndex);
//            visEq.SetSkinColor(this.m_skinColor);
//            visEq.SetHairColor(this.m_hairColor);
//        }

//        public override bool CanConsumeItem(ItemData item)
//        {
//            if (item.m_shared.m_itemType != ItemData.ItemType.Consumable || (double)item.m_shared.m_food > 0.0 && !this.CanEat(item, true))
//                return false;
//            if ((bool)(UnityEngine.Object)item.m_shared.m_consumeStatusEffect)
//            {
//                StatusEffect consumeStatusEffect = item.m_shared.m_consumeStatusEffect;
//                if (this.m_seman.HaveStatusEffect(item.m_shared.m_consumeStatusEffect.name) || this.m_seman.HaveStatusEffectCategory(consumeStatusEffect.m_category))
//                {
//                    this.Message(MessageHud.MessageType.Center, "$msg_cantconsume", 0, (Sprite)null);
//                    return false;
//                }
//            }
//            return true;
//        }

//        public override bool ConsumeItem(Inventory inventory, ItemData item)
//        {
//            if (!this.CanConsumeItem(item))
//                return false;
//            if ((bool)(UnityEngine.Object)item.m_shared.m_consumeStatusEffect)
//            {
//                StatusEffect consumeStatusEffect = item.m_shared.m_consumeStatusEffect;
//                this.m_seman.AddStatusEffect(item.m_shared.m_consumeStatusEffect, true);
//            }
//            if ((double)item.m_shared.m_food > 0.0)
//                this.EatFood(item);
//            inventory.RemoveOneItem(item);
//            return true;
//        }

//        public void SetIntro(bool intro)
//        {
//            if (this.m_intro == intro)
//                return;
//            this.m_intro = intro;
//            this.m_zanim.SetBool(nameof(intro), intro);
//        }

//        public override bool InIntro()
//        {
//            return this.m_intro;
//        }

//        public override bool InCutscene()
//        {
//            AnimatorStateInfo animatorStateInfo = this.m_animator.GetCurrentAnimatorStateInfo(0);
//            return ((AnimatorStateInfo)ref animatorStateInfo).get_tagHash() == Player.m_animatorTagCutscene || this.InIntro() || this.m_sleeping || base.InCutscene();
//        }

//        public void SetMaxStamina(float stamina, bool flashBar)
//        {
//            if (flashBar && (UnityEngine.Object)Hud.instance != (UnityEngine.Object)null && (double)stamina > (double)this.m_maxStamina)
//                Hud.instance.StaminaBarUppgradeFlash();
//            this.m_maxStamina = stamina;
//            this.m_stamina = Mathf.Clamp(this.m_stamina, 0.0f, this.m_maxStamina);
//        }

//        public void SetMaxHealth(float health, bool flashBar)
//        {
//            if (flashBar && (UnityEngine.Object)Hud.instance != (UnityEngine.Object)null && (double)health > (double)this.GetMaxHealth())
//                Hud.instance.FlashHealthBar();
//            this.SetMaxHealth(health);
//        }

//        public override bool IsPVPEnabled()
//        {
//            if (!this.m_nview.IsValid())
//                return false;
//            return this.m_nview.IsOwner() ? this.m_pvp : this.m_nview.GetZDO().GetBool("pvp", false);
//        }

//        public void SetPVP(bool enabled)
//        {
//            if (this.m_pvp == enabled)
//                return;
//            this.m_pvp = enabled;
//            this.m_nview.GetZDO().Set("pvp", this.m_pvp);
//            if (this.m_pvp)
//                this.Message(MessageHud.MessageType.Center, "$msg_pvpon", 0, (Sprite)null);
//            else
//                this.Message(MessageHud.MessageType.Center, "$msg_pvpoff", 0, (Sprite)null);
//        }

//        public bool CanSwitchPVP()
//        {
//            return (double)this.m_lastCombatTimer > 10.0;
//        }

//        public bool NoCostCheat()
//        {
//            return this.m_noPlacementCost;
//        }

//        public void StartEmote(string emote, bool oneshot = true)
//        {
//            if (!this.CanMove() || this.InAttack() || this.IsHoldingAttack())
//                return;
//            this.SetCrouch(false);
//            this.m_nview.GetZDO().Set("emoteID", this.m_nview.GetZDO().GetInt("emoteID", 0) + 1);
//            this.m_nview.GetZDO().Set(nameof(emote), emote);
//            this.m_nview.GetZDO().Set("emote_oneshot", oneshot);
//        }

//        protected override void StopEmote()
//        {
//            if (!(this.m_nview.GetZDO().GetString("emote", "") != ""))
//                return;
//            this.m_nview.GetZDO().Set("emoteID", this.m_nview.GetZDO().GetInt("emoteID", 0) + 1);
//            this.m_nview.GetZDO().Set("emote", "");
//        }

//        private void UpdateEmote()
//        {
//            if (this.m_nview.IsOwner() && this.InEmote() && this.m_moveDir != Vector3.zero)
//                this.StopEmote();
//            int num1 = this.m_nview.GetZDO().GetInt("emoteID", 0);
//            if (num1 == this.m_emoteID)
//                return;
//            this.m_emoteID = num1;
//            if (!string.IsNullOrEmpty(this.m_emoteState))
//                this.m_animator.SetBool("emote_" + this.m_emoteState, false);
//            this.m_emoteState = "";
//            this.m_animator.SetTrigger("emote_stop");
//            string str = this.m_nview.GetZDO().GetString("emote", "");
//            if (string.IsNullOrEmpty(str))
//                return;
//            int num2 = this.m_nview.GetZDO().GetBool("emote_oneshot", false) ? 1 : 0;
//            this.m_animator.ResetTrigger("emote_stop");
//            if (num2 != 0)
//            {
//                this.m_animator.SetTrigger("emote_" + str);
//            }
//            else
//            {
//                this.m_emoteState = str;
//                this.m_animator.SetBool("emote_" + str, true);
//            }
//        }

//        public override bool InEmote()
//        {
//            if (!string.IsNullOrEmpty(this.m_emoteState))
//                return true;
//            AnimatorStateInfo animatorStateInfo = this.m_animator.GetCurrentAnimatorStateInfo(0);
//            return ((AnimatorStateInfo)ref animatorStateInfo).get_tagHash() == Player.m_animatorTagEmote;
//        }

//        public override bool IsCrouching()
//        {
//            AnimatorStateInfo animatorStateInfo = this.m_animator.GetCurrentAnimatorStateInfo(0);
//            return ((AnimatorStateInfo)ref animatorStateInfo).get_tagHash() == Player.m_animatorTagCrouch;
//        }

//        private void UpdateCrouch(float dt)
//        {
//            if (this.m_crouchToggled)
//            {
//                if (!this.HaveStamina(0.0f) || this.IsSwiming() || (this.InBed() || this.InPlaceMode()) || (this.m_run || this.IsBlocking() || this.IsFlying()))
//                    this.SetCrouch(false);
//                bool flag = this.InAttack() || this.IsHoldingAttack();
//                this.m_zanim.SetBool(Player.crouching, this.m_crouchToggled && !flag);
//            }
//            else
//                this.m_zanim.SetBool(Player.crouching, false);
//        }

//        protected override void SetCrouch(bool crouch)
//        {
//            if (this.m_crouchToggled == crouch)
//                return;
//            this.m_crouchToggled = crouch;
//        }

//        public void SetGuardianPower(string name)
//        {
//            this.m_guardianPower = name;
//            this.m_guardianSE = ObjectDB.instance.GetStatusEffect(this.m_guardianPower);
//        }

//        public string GetGuardianPowerName()
//        {
//            return this.m_guardianPower;
//        }

//        public void GetGuardianPowerHUD(out StatusEffect se, out float cooldown)
//        {
//            se = this.m_guardianSE;
//            cooldown = this.m_guardianPowerCooldown;
//        }

//        public bool StartGuardianPower()
//        {
//            if ((UnityEngine.Object)this.m_guardianSE == (UnityEngine.Object)null || this.InAttack() && !this.HaveQueuedChain() || (this.InDodge() || !this.CanMove() || (this.IsKnockedBack() || this.IsStaggering())) || this.InMinorAction())
//                return false;
//            if ((double)this.m_guardianPowerCooldown > 0.0)
//            {
//                this.Message(MessageHud.MessageType.Center, "$hud_powernotready", 0, (Sprite)null);
//                return false;
//            }
//            this.m_zanim.SetTrigger("gpower");
//            return true;
//        }

//        public bool ActivateGuardianPower()
//        {
//            if ((double)this.m_guardianPowerCooldown > 0.0 || (UnityEngine.Object)this.m_guardianSE == (UnityEngine.Object)null)
//                return false;
//            List<Player> players = new List<Player>();
//            Player.GetPlayersInRange(this.transform.position, 10f, players);
//            foreach (Character character in players)
//                character.GetSEMan().AddStatusEffect(this.m_guardianSE.name, true);
//            this.m_guardianPowerCooldown = this.m_guardianSE.m_cooldown;
//            return false;
//        }

//        private void UpdateGuardianPower(float dt)
//        {
//            this.m_guardianPowerCooldown -= dt;
//            if ((double)this.m_guardianPowerCooldown >= 0.0)
//                return;
//            this.m_guardianPowerCooldown = 0.0f;
//        }

//        public override void AttachStart(
//          Transform attachPoint,
//          bool hideWeapons,
//          bool isBed,
//          string attachAnimation,
//          Vector3 detachOffset)
//        {
//            if (this.m_attached)
//                return;
//            this.m_attached = true;
//            this.m_attachPoint = attachPoint;
//            this.m_detachOffset = detachOffset;
//            this.m_attachAnimation = attachAnimation;
//            this.m_zanim.SetBool(attachAnimation, true);
//            this.m_nview.GetZDO().Set("inBed", isBed);
//            if (hideWeapons)
//                this.HideHandItems();
//            this.ResetCloth();
//        }

//        private void UpdateAttach()
//        {
//            if (!this.m_attached)
//                return;
//            if ((UnityEngine.Object)this.m_attachPoint != (UnityEngine.Object)null)
//            {
//                this.transform.position = this.m_attachPoint.position;
//                this.transform.rotation = this.m_attachPoint.rotation;
//                Rigidbody componentInParent = this.m_attachPoint.GetComponentInParent<Rigidbody>();
//                this.m_body.set_useGravity(false);
//                this.m_body.set_velocity((bool)(UnityEngine.Object)componentInParent ? componentInParent.GetPointVelocity(this.transform.position) : Vector3.zero);
//                this.m_body.set_angularVelocity(Vector3.zero);
//                this.m_maxAirAltitude = this.transform.position.y;
//            }
//            else
//                this.AttachStop();
//        }

//        public override bool IsAttached()
//        {
//            return this.m_attached;
//        }

//        public override bool InBed()
//        {
//            return this.m_nview.IsValid() && this.m_nview.GetZDO().GetBool("inBed", false);
//        }

//        public override void AttachStop()
//        {
//            if (this.m_sleeping || !this.m_attached)
//                return;
//            if ((UnityEngine.Object)this.m_attachPoint != (UnityEngine.Object)null)
//                this.transform.position = this.m_attachPoint.TransformPoint(this.m_detachOffset);
//            this.m_body.set_useGravity(true);
//            this.m_attached = false;
//            this.m_attachPoint = (Transform)null;
//            this.m_zanim.SetBool(this.m_attachAnimation, false);
//            this.m_nview.GetZDO().Set("inBed", false);
//            this.ResetCloth();
//        }

//        public void StartShipControl(ShipControlls shipControl)
//        {
//            this.m_shipControl = shipControl;
//            ZLog.Log((object)("ship controlls set " + shipControl.GetShip().gameObject.name));
//        }

//        public void StopShipControl()
//        {
//            if (!((UnityEngine.Object)this.m_shipControl != (UnityEngine.Object)null))
//                return;
//            if ((bool)(UnityEngine.Object)this.m_shipControl)
//                this.m_shipControl.OnUseStop(this);
//            ZLog.Log((object)"Stop ship controlls");
//            this.m_shipControl = (ShipControlls)null;
//        }

//        private void SetShipControl(ref Vector3 moveDir)
//        {
//            this.m_shipControl.GetShip().ApplyMovementControlls(moveDir);
//            moveDir = Vector3.zero;
//        }

//        public Ship GetControlledShip()
//        {
//            return (bool)(UnityEngine.Object)this.m_shipControl ? this.m_shipControl.GetShip() : (Ship)null;
//        }

//        public ShipControlls GetShipControl()
//        {
//            return this.m_shipControl;
//        }

//        private void UpdateShipControl(float dt)
//        {
//            if (!(bool)(UnityEngine.Object)this.m_shipControl)
//                return;
//            Vector3 forward = this.m_shipControl.GetShip().transform.forward;
//            forward.y = 0.0f;
//            forward.Normalize();
//            this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, Quaternion.LookRotation(forward), 100f * dt);
//            if ((double)Vector3.Distance(this.m_shipControl.transform.position, this.transform.position) <= (double)this.m_maxInteractDistance)
//                return;
//            this.StopShipControl();
//        }

//        public bool IsSleeping()
//        {
//            return this.m_sleeping;
//        }

//        public void SetSleeping(bool sleep)
//        {
//            if (this.m_sleeping == sleep)
//                return;
//            this.m_sleeping = sleep;
//            if (sleep)
//                return;
//            this.Message(MessageHud.MessageType.Center, "$msg_goodmorning", 0, (Sprite)null);
//            this.m_seman.AddStatusEffect("Rested", true);
//        }

//        public void SetControls(
//          Vector3 movedir,
//          bool attack,
//          bool attackHold,
//          bool secondaryAttack,
//          bool block,
//          bool blockHold,
//          bool jump,
//          bool crouch,
//          bool run,
//          bool autoRun)
//        {
//            if (movedir != Vector3.zero | attack | secondaryAttack | block | blockHold | jump | crouch && (UnityEngine.Object)this.GetControlledShip() == (UnityEngine.Object)null)
//            {
//                this.StopEmote();
//                this.AttachStop();
//            }
//            if ((bool)(UnityEngine.Object)this.m_shipControl)
//            {
//                this.SetShipControl(ref movedir);
//                if (jump)
//                    this.StopShipControl();
//            }
//            if (run)
//                this.m_walk = false;
//            if (!this.m_autoRun)
//            {
//                Vector3 lookDir = this.m_lookDir;
//                lookDir.y = 0.0f;
//                lookDir.Normalize();
//                this.m_moveDir = movedir.z * lookDir + movedir.x * Vector3.Cross(Vector3.up, lookDir);
//            }
//            if (!this.m_autoRun & autoRun && !this.InPlaceMode())
//            {
//                this.m_autoRun = true;
//                this.SetCrouch(false);
//                this.m_moveDir = this.m_lookDir;
//                this.m_moveDir.y = 0.0f;
//                this.m_moveDir.Normalize();
//            }
//            else if (this.m_autoRun)
//            {
//                if (((attack | jump | crouch || movedir != Vector3.zero ? 1 : (this.InPlaceMode() ? 1 : 0)) | (attackHold ? 1 : 0)) != 0)
//                    this.m_autoRun = false;
//                else if (autoRun | blockHold)
//                {
//                    this.m_moveDir = this.m_lookDir;
//                    this.m_moveDir.y = 0.0f;
//                    this.m_moveDir.Normalize();
//                    blockHold = false;
//                    block = false;
//                }
//            }
//            this.m_attack = attack;
//            this.m_attackDraw = attackHold;
//            this.m_secondaryAttack = secondaryAttack;
//            this.m_blocking = blockHold;
//            this.m_run = run;
//            if (crouch)
//                this.SetCrouch(!this.m_crouchToggled);
//            if (!jump)
//                return;
//            if (this.m_blocking)
//            {
//                Vector3 dodgeDir = this.m_moveDir;
//                if ((double)dodgeDir.magnitude < 0.100000001490116)
//                {
//                    dodgeDir = -this.m_lookDir;
//                    dodgeDir.y = 0.0f;
//                    dodgeDir.Normalize();
//                }
//                this.Dodge(dodgeDir);
//            }
//            else if (this.IsCrouching() || this.m_crouchToggled)
//            {
//                Vector3 dodgeDir = this.m_moveDir;
//                if ((double)dodgeDir.magnitude < 0.100000001490116)
//                {
//                    dodgeDir = this.m_lookDir;
//                    dodgeDir.y = 0.0f;
//                    dodgeDir.Normalize();
//                }
//                this.Dodge(dodgeDir);
//            }
//            else
//                this.Jump();
//        }

//        private void UpdateTargeted(float dt)
//        {
//            this.m_timeSinceTargeted += dt;
//            this.m_timeSinceSensed += dt;
//        }

//        public override void OnTargeted(bool sensed, bool alerted)
//        {
//            if (sensed)
//            {
//                if ((double)this.m_timeSinceSensed <= 0.5)
//                    return;
//                this.m_timeSinceSensed = 0.0f;
//                this.m_nview.InvokeRPC(nameof(OnTargeted), (object)sensed, (object)alerted);
//            }
//            else
//            {
//                if ((double)this.m_timeSinceTargeted <= 0.5)
//                    return;
//                this.m_timeSinceTargeted = 0.0f;
//                this.m_nview.InvokeRPC(nameof(OnTargeted), (object)sensed, (object)alerted);
//            }
//        }

//        private void RPC_OnTargeted(long sender, bool sensed, bool alerted)
//        {
//            this.m_timeSinceTargeted = 0.0f;
//            if (sensed)
//                this.m_timeSinceSensed = 0.0f;
//            if (!alerted)
//                return;
//            MusicMan.instance.ResetCombatTimer();
//        }

//        protected override void OnDamaged(HitData hit)
//        {
//            base.OnDamaged(hit);
//            Hud.instance.DamageFlash();
//        }

//        public bool IsTargeted()
//        {
//            return (double)this.m_timeSinceTargeted < 1.0;
//        }

//        public bool IsSensed()
//        {
//            return (double)this.m_timeSinceSensed < 1.0;
//        }

//        protected override void ApplyArmorDamageMods(ref HitData.DamageModifiers mods)
//        {
//            if (this.m_chestItem != null)
//                mods.Apply(this.m_chestItem.m_shared.m_damageModifiers);
//            if (this.m_legItem != null)
//                mods.Apply(this.m_legItem.m_shared.m_damageModifiers);
//            if (this.m_helmetItem != null)
//                mods.Apply(this.m_helmetItem.m_shared.m_damageModifiers);
//            if (this.m_shoulderItem == null)
//                return;
//            mods.Apply(this.m_shoulderItem.m_shared.m_damageModifiers);
//        }

//        public override float GetBodyArmor()
//        {
//            float num = 0.0f;
//            if (this.m_chestItem != null)
//                num += this.m_chestItem.GetArmor();
//            if (this.m_legItem != null)
//                num += this.m_legItem.GetArmor();
//            if (this.m_helmetItem != null)
//                num += this.m_helmetItem.GetArmor();
//            if (this.m_shoulderItem != null)
//                num += this.m_shoulderItem.GetArmor();
//            return num;
//        }

//        protected override void OnSneaking(float dt)
//        {
//            float num = Mathf.Lerp(1f, 0.25f, Mathf.Pow(this.m_skills.GetSkillFactor(Skills.SkillType.Sneak), 0.5f));
//            this.UseStamina(dt * this.m_sneakStaminaDrain * num);
//            if (!this.HaveStamina(0.0f))
//                Hud.instance.StaminaBarNoStaminaFlash();
//            this.m_sneakSkillImproveTimer += dt;
//            if ((double)this.m_sneakSkillImproveTimer <= 1.0)
//                return;
//            this.m_sneakSkillImproveTimer = 0.0f;
//            if (BaseAI.InStealthRange((Character)this))
//                this.RaiseSkill(Skills.SkillType.Sneak, 1f);
//            else
//                this.RaiseSkill(Skills.SkillType.Sneak, 0.1f);
//        }

//        private void UpdateStealth(float dt)
//        {
//            this.m_stealthFactorUpdateTimer += dt;
//            if ((double)this.m_stealthFactorUpdateTimer > 0.5)
//            {
//                this.m_stealthFactorUpdateTimer = 0.0f;
//                this.m_stealthFactorTarget = 0.0f;
//                if (this.IsCrouching())
//                {
//                    this.m_lastStealthPosition = this.transform.position;
//                    float skillFactor = this.m_skills.GetSkillFactor(Skills.SkillType.Sneak);
//                    float lightFactor = StealthSystem.instance.GetLightFactor(this.GetCenterPoint());
//                    this.m_stealthFactorTarget = Mathf.Lerp((float)(0.5 + (double)lightFactor * 0.5), (float)(0.200000002980232 + (double)lightFactor * 0.400000005960464), skillFactor);
//                    this.m_stealthFactorTarget = Mathf.Clamp01(this.m_stealthFactorTarget);
//                    this.m_seman.ModifyStealth(this.m_stealthFactorTarget, ref this.m_stealthFactorTarget);
//                    this.m_stealthFactorTarget = Mathf.Clamp01(this.m_stealthFactorTarget);
//                }
//                else
//                    this.m_stealthFactorTarget = 1f;
//            }
//            this.m_stealthFactor = Mathf.MoveTowards(this.m_stealthFactor, this.m_stealthFactorTarget, dt / 4f);
//            this.m_nview.GetZDO().Set("Stealth", this.m_stealthFactor);
//        }

//        public override float GetStealthFactor()
//        {
//            if (!this.m_nview.IsValid())
//                return 0.0f;
//            return this.m_nview.IsOwner() ? this.m_stealthFactor : this.m_nview.GetZDO().GetFloat("Stealth", 0.0f);
//        }

//        public override bool InAttack()
//        {
//            if (this.m_animator.IsInTransition(0))
//            {
//                AnimatorStateInfo animatorStateInfo1 = this.m_animator.GetNextAnimatorStateInfo(0);
//                if (((AnimatorStateInfo)ref animatorStateInfo1).get_tagHash() == Humanoid.m_animatorTagAttack)
//                    return true;
//                AnimatorStateInfo animatorStateInfo2 = this.m_animator.GetNextAnimatorStateInfo(1);
//                return ((AnimatorStateInfo)ref animatorStateInfo2).get_tagHash() == Humanoid.m_animatorTagAttack;
//            }
//            AnimatorStateInfo animatorStateInfo3 = this.m_animator.GetCurrentAnimatorStateInfo(0);
//            if (((AnimatorStateInfo)ref animatorStateInfo3).get_tagHash() == Humanoid.m_animatorTagAttack)
//                return true;
//            AnimatorStateInfo animatorStateInfo4 = this.m_animator.GetCurrentAnimatorStateInfo(1);
//            return ((AnimatorStateInfo)ref animatorStateInfo4).get_tagHash() == Humanoid.m_animatorTagAttack;
//        }

//        public override float GetEquipmentMovementModifier()
//        {
//            return this.m_equipmentMovementModifier;
//        }

//        protected override float GetJogSpeedFactor()
//        {
//            return 1f + this.m_equipmentMovementModifier;
//        }

//        protected override float GetRunSpeedFactor()
//        {
//            return (float)((1.0 + (double)this.m_skills.GetSkillFactor(Skills.SkillType.Run) * 0.25) * (1.0 + (double)this.m_equipmentMovementModifier * 1.5));
//        }

//        public override bool InMinorAction()
//        {
//            AnimatorStateInfo animatorStateInfo = this.m_animator.IsInTransition(1) ? this.m_animator.GetNextAnimatorStateInfo(1) : this.m_animator.GetCurrentAnimatorStateInfo(1);
//            return ((AnimatorStateInfo)ref animatorStateInfo).get_tagHash() == Player.m_animatorTagMinorAction;
//        }

//        public override bool GetRelativePosition(
//          out ZDOID parent,
//          out Vector3 relativePos,
//          out Vector3 relativeVel)
//        {
//            if (this.m_attached && (bool)(UnityEngine.Object)this.m_attachPoint)
//            {
//                ZNetView componentInParent = this.m_attachPoint.GetComponentInParent<ZNetView>();
//                if ((bool)(UnityEngine.Object)componentInParent && componentInParent.IsValid())
//                {
//                    parent = componentInParent.GetZDO().m_uid;
//                    relativePos = componentInParent.transform.InverseTransformPoint(this.transform.position);
//                    relativeVel = Vector3.zero;
//                    return true;
//                }
//            }
//            return base.GetRelativePosition(out parent, out relativePos, out relativeVel);
//        }

//        public override Skills GetSkills()
//        {
//            return this.m_skills;
//        }

//        public override float GetRandomSkillFactor(Skills.SkillType skill)
//        {
//            return this.m_skills.GetRandomSkillFactor(skill);
//        }

//        public override float GetSkillFactor(Skills.SkillType skill)
//        {
//            return this.m_skills.GetSkillFactor(skill);
//        }

//        protected override void DoDamageCameraShake(HitData hit)
//        {
//            if (!(bool)(UnityEngine.Object)GameCamera.instance || (double)hit.GetTotalPhysicalDamage() <= 0.0)
//                return;
//            GameCamera.instance.AddShake(this.transform.position, 50f, this.m_baseCameraShake * Mathf.Clamp01(hit.GetTotalPhysicalDamage() / this.GetMaxHealth()), false);
//        }

//        protected override bool ToggleEquiped(ItemData item)
//        {
//            if (!item.IsEquipable())
//                return false;
//            if (this.InAttack())
//                return true;
//            if ((double)item.m_shared.m_equipDuration <= 0.0)
//            {
//                if (this.IsItemEquiped(item))
//                    this.UnequipItem(item, true);
//                else
//                    this.EquipItem(item, true);
//            }
//            else if (this.IsItemEquiped(item))
//                this.QueueUnequipItem(item);
//            else
//                this.QueueEquipItem(item);
//            return true;
//        }

//        public void GetActionProgress(out string name, out float progress)
//        {
//            if (this.m_equipQueue.Count > 0)
//            {
//                Player.EquipQueueData equip = this.m_equipQueue[0];
//                if ((double)equip.m_duration > 0.5)
//                {
//                    name = !equip.m_equip ? "$hud_unequipping " + equip.m_item.m_shared.m_name : "$hud_equipping " + equip.m_item.m_shared.m_name;
//                    progress = Mathf.Clamp01(equip.m_time / equip.m_duration);
//                    return;
//                }
//            }
//            name = (string)null;
//            progress = 0.0f;
//        }

//        private void UpdateEquipQueue(float dt)
//        {
//            if ((double)this.m_equipQueuePause > 0.0)
//            {
//                this.m_equipQueuePause -= dt;
//                this.m_zanim.SetBool("equipping", false);
//            }
//            else
//            {
//                this.m_zanim.SetBool("equipping", this.m_equipQueue.Count > 0);
//                if (this.m_equipQueue.Count == 0)
//                    return;
//                Player.EquipQueueData equip = this.m_equipQueue[0];
//                if ((double)equip.m_time == 0.0 && (double)equip.m_duration >= 1.0)
//                    this.m_equipStartEffects.Create(this.transform.position, Quaternion.identity, (Transform)null, 1f);
//                equip.m_time += dt;
//                if ((double)equip.m_time <= (double)equip.m_duration)
//                    return;
//                this.m_equipQueue.RemoveAt(0);
//                if (equip.m_equip)
//                    this.EquipItem(equip.m_item, true);
//                else
//                    this.UnequipItem(equip.m_item, true);
//                this.m_equipQueuePause = 0.3f;
//            }
//        }

//        private void QueueEquipItem(ItemData item)
//        {
//            if (item == null)
//                return;
//            if (this.IsItemQueued(item))
//                this.RemoveFromEquipQueue(item);
//            else
//                this.m_equipQueue.Add(new Player.EquipQueueData()
//                {
//                    m_item = item,
//                    m_equip = true,
//                    m_duration = item.m_shared.m_equipDuration
//                });
//        }

//        private void QueueUnequipItem(ItemData item)
//        {
//            if (item == null)
//                return;
//            if (this.IsItemQueued(item))
//                this.RemoveFromEquipQueue(item);
//            else
//                this.m_equipQueue.Add(new Player.EquipQueueData()
//                {
//                    m_item = item,
//                    m_equip = false,
//                    m_duration = item.m_shared.m_equipDuration
//                });
//        }

//        public override void AbortEquipQueue()
//        {
//            this.m_equipQueue.Clear();
//        }

//        public override void RemoveFromEquipQueue(ItemData item)
//        {
//            if (item == null)
//                return;
//            foreach (Player.EquipQueueData equip in this.m_equipQueue)
//            {
//                if (equip.m_item == item)
//                {
//                    this.m_equipQueue.Remove(equip);
//                    break;
//                }
//            }
//        }

//        public bool IsItemQueued(ItemData item)
//        {
//            if (item == null)
//                return false;
//            foreach (Player.EquipQueueData equip in this.m_equipQueue)
//            {
//                if (equip.m_item == item)
//                    return true;
//            }
//            return false;
//        }

//        public void ResetCharacter()
//        {
//            this.m_guardianPowerCooldown = 0.0f;
//            Player.ResetSeenTutorials();
//            this.m_knownRecipes.Clear();
//            this.m_knownStations.Clear();
//            this.m_knownMaterial.Clear();
//            this.m_uniques.Clear();
//            this.m_trophies.Clear();
//            this.m_skills.Clear();
//            this.m_knownBiome.Clear();
//            this.m_knownTexts.Clear();
//        }

//        public enum RequirementMode
//        {
//            CanBuild,
//            IsKnown,
//            CanAlmostBuild,
//        }

//        public class Food
//        {
//            public string m_name = "";
//            public ItemData m_item;
//            public float m_health;
//            public float m_stamina;

//            public bool CanEatAgain()
//            {
//                return (double)this.m_health < (double)this.m_item.m_shared.m_food / 2.0;
//            }
//        }

//        public class EquipQueueData
//        {
//            public bool m_equip = true;
//            public ItemData m_item;
//            public float m_time;
//            public float m_duration;
//        }

//        private enum PlacementStatus
//        {
//            Valid,
//            Invalid,
//            BlockedbyPlayer,
//            NoBuildZone,
//            PrivateZone,
//            MoreSpace,
//            NoTeleportArea,
//            ExtensionMissingStation,
//            WrongBiome,
//            NeedCultivated,
//            NotInDungeon,
//        }


//        #endregion
//    }
//}
