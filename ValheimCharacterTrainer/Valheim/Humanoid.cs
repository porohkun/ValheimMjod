//using System;
//using System.Collections.Generic;
//using System.Linq;

//namespace Valheim
//{
//    public class Humanoid : Character
//    {
//        private static List<ItemData> optimalWeapons = new List<ItemData>();
//        private static List<ItemData> outofRangeWeapons = new List<ItemData>();
//        private static List<ItemData> allWeapons = new List<ItemData>();
//        private static int statef = 0;
//        private static int statei = 0;
//        private static int blocking = 0;
//        private static int isBlockingHash = 0;
//        protected static int m_animatorTagAttack = Animator.StringToHash("attack");
//        [Header("Humanoid")]
//        public float m_equipStaminaDrain = 10f;
//        public float m_blockStaminaDrain = 25f;
//        [Header("Effects")]
//        public EffectList m_pickupEffects = new EffectList();
//        public EffectList m_dropEffects = new EffectList();
//        public EffectList m_consumeItemEffects = new EffectList();
//        public EffectList m_equipEffects = new EffectList();
//        public EffectList m_perfectBlockEffect = new EffectList();
//        protected Inventory m_inventory = new Inventory("Inventory", (Sprite)null, 8, 4);
//        protected string m_beardItem = "";
//        protected string m_hairItem = "";
//        private float m_blockTimer = 9999f;
//        protected float m_lastCombatTimer = 999f;
//        private HashSet<StatusEffect> m_eqipmentStatusEffects = new HashSet<StatusEffect>();
//        [Header("Default items")]
//        public GameObject[] m_defaultItems;
//        public GameObject[] m_randomWeapon;
//        public GameObject[] m_randomArmor;
//        public GameObject[] m_randomShield;
//        public Humanoid.ItemSet[] m_randomSets;
//        public ItemDrop m_unarmedWeapon;
//        protected ItemData m_rightItem;
//        protected ItemData m_leftItem;
//        protected ItemData m_chestItem;
//        protected ItemData m_legItem;
//        protected ItemData m_ammoItem;
//        protected ItemData m_helmetItem;
//        protected ItemData m_shoulderItem;
//        protected ItemData m_utilityItem;
//        private int m_lastEquipEffectFrame;
//        protected ItemData m_hiddenLeftItem;
//        protected ItemData m_hiddenRightItem;
//        protected Attack m_currentAttack;
//        protected Attack m_previousAttack;
//        private float m_timeSinceLastAttack;
//        private bool m_internalBlockingState;
//        private const float m_perfectBlockInterval = 0.25f;
//        protected float m_attackDrawTime;
//        protected VisEquipment m_visEquipment;

//        protected override void Awake()
//        {
//            base.Awake();
//            this.m_visEquipment = this.GetComponent<VisEquipment>();
//            if (Humanoid.statef == 0)
//            {
//                Humanoid.statef = ZSyncAnimation.GetHash("statef");
//                Humanoid.statei = ZSyncAnimation.GetHash("statei");
//                Humanoid.blocking = ZSyncAnimation.GetHash("blocking");
//            }
//            if (Humanoid.isBlockingHash != 0)
//                return;
//            Humanoid.isBlockingHash = "IsBlocking".GetStableHashCode();
//        }

//        protected override void Start()
//        {
//            base.Start();
//            if (this.IsPlayer())
//                return;
//            this.GiveDefaultItems();
//        }

//        public void GiveDefaultItems()
//        {
//            foreach (GameObject defaultItem in this.m_defaultItems)
//                this.GiveDefaultItem(defaultItem);
//            if (this.m_randomWeapon.Length == 0 && this.m_randomArmor.Length == 0 && (this.m_randomShield.Length == 0 && this.m_randomSets.Length == 0))
//                return;
//            UnityEngine.Random.State state = UnityEngine.Random.state;
//            UnityEngine.Random.InitState(this.m_nview.GetZDO().m_uid.GetHashCode());
//            if (this.m_randomShield.Length != 0)
//            {
//                GameObject prefab = this.m_randomShield[UnityEngine.Random.Range(0, this.m_randomShield.Length)];
//                if ((bool)(UnityEngine.Object)prefab)
//                    this.GiveDefaultItem(prefab);
//            }
//            if (this.m_randomWeapon.Length != 0)
//            {
//                GameObject prefab = this.m_randomWeapon[UnityEngine.Random.Range(0, this.m_randomWeapon.Length)];
//                if ((bool)(UnityEngine.Object)prefab)
//                    this.GiveDefaultItem(prefab);
//            }
//            if (this.m_randomArmor.Length != 0)
//            {
//                GameObject prefab = this.m_randomArmor[UnityEngine.Random.Range(0, this.m_randomArmor.Length)];
//                if ((bool)(UnityEngine.Object)prefab)
//                    this.GiveDefaultItem(prefab);
//            }
//            if (this.m_randomSets.Length != 0)
//            {
//                foreach (GameObject prefab in this.m_randomSets[UnityEngine.Random.Range(0, this.m_randomSets.Length)].m_items)
//                    this.GiveDefaultItem(prefab);
//            }
//            UnityEngine.Random.state = state;
//        }

//        private void GiveDefaultItem(GameObject prefab)
//        {
//            ItemData itemData = this.PickupPrefab(prefab, 0);
//            if (itemData == null || itemData.IsWeapon())
//                return;
//            this.EquipItem(itemData, false);
//        }

//        protected override void FixedUpdate()
//        {
//            if (!this.m_nview.IsValid())
//                return;
//            if ((UnityEngine.Object)this.m_nview == (UnityEngine.Object)null || this.m_nview.IsOwner())
//            {
//                this.UpdateAttack(Time.fixedDeltaTime);
//                this.UpdateEquipment(Time.fixedDeltaTime);
//                this.UpdateBlock(Time.fixedDeltaTime);
//            }
//            base.FixedUpdate();
//        }

//        public override bool InAttack()
//        {
//            if (this.m_animator.IsInTransition(0))
//            {
//                AnimatorStateInfo animatorStateInfo1 = this.m_animator.GetCurrentAnimatorStateInfo(0);
//                if (((AnimatorStateInfo)ref animatorStateInfo1).get_tagHash() == Humanoid.m_animatorTagAttack)
//                    return true;
//                AnimatorStateInfo animatorStateInfo2 = this.m_animator.GetNextAnimatorStateInfo(0);
//                return ((AnimatorStateInfo)ref animatorStateInfo2).get_tagHash() == Humanoid.m_animatorTagAttack;
//            }
//            AnimatorStateInfo animatorStateInfo = this.m_animator.GetCurrentAnimatorStateInfo(0);
//            return ((AnimatorStateInfo)ref animatorStateInfo).get_tagHash() == Humanoid.m_animatorTagAttack;
//        }

//        public override bool StartAttack(Character target, bool secondaryAttack)
//        {
//            this.AbortEquipQueue();
//            if (this.InAttack() && !this.HaveQueuedChain() || (this.InDodge() || !this.CanMove()) || (this.IsKnockedBack() || this.IsStaggering() || this.InMinorAction()))
//                return false;
//            ItemData currentWeapon = this.GetCurrentWeapon();
//            if (currentWeapon == null)
//                return false;
//            if (this.m_currentAttack != null)
//            {
//                this.m_currentAttack.Stop();
//                this.m_previousAttack = this.m_currentAttack;
//                this.m_currentAttack = (Attack)null;
//            }
//            Attack attack;
//            if (secondaryAttack)
//            {
//                if (!currentWeapon.HaveSecondaryAttack())
//                    return false;
//                attack = currentWeapon.m_shared.m_secondaryAttack.Clone();
//            }
//            else
//            {
//                if (!currentWeapon.HavePrimaryAttack())
//                    return false;
//                attack = currentWeapon.m_shared.m_attack.Clone();
//            }
//            if (!attack.Start(this, this.m_body, this.m_zanim, this.m_animEvent, this.m_visEquipment, currentWeapon, this.m_previousAttack, this.m_timeSinceLastAttack, this.GetAttackDrawPercentage()))
//                return false;
//            this.m_currentAttack = attack;
//            this.m_lastCombatTimer = 0.0f;
//            return true;
//        }

//        public float GetAttackDrawPercentage()
//        {
//            ItemData currentWeapon = this.GetCurrentWeapon();
//            if (currentWeapon == null || (double)currentWeapon.m_shared.m_holdDurationMin <= 0.0 || (double)this.m_attackDrawTime <= 0.0)
//                return 0.0f;
//            float skillFactor = this.GetSkillFactor(currentWeapon.m_shared.m_skillType);
//            float num = currentWeapon.m_shared.m_holdDurationMin * (1f - skillFactor);
//            return (double)num <= 0.0 ? 1f : Mathf.Clamp01(this.m_attackDrawTime / num);
//        }

//        private void UpdateEquipment(float dt)
//        {
//            if (!this.IsPlayer())
//                return;
//            if (this.IsSwiming() && !this.IsOnGround())
//                this.HideHandItems();
//            if (this.m_rightItem != null && this.m_rightItem.m_shared.m_useDurability)
//                this.DrainEquipedItemDurability(this.m_rightItem, dt);
//            if (this.m_leftItem != null && this.m_leftItem.m_shared.m_useDurability)
//                this.DrainEquipedItemDurability(this.m_leftItem, dt);
//            if (this.m_chestItem != null && this.m_chestItem.m_shared.m_useDurability)
//                this.DrainEquipedItemDurability(this.m_chestItem, dt);
//            if (this.m_legItem != null && this.m_legItem.m_shared.m_useDurability)
//                this.DrainEquipedItemDurability(this.m_legItem, dt);
//            if (this.m_helmetItem != null && this.m_helmetItem.m_shared.m_useDurability)
//                this.DrainEquipedItemDurability(this.m_helmetItem, dt);
//            if (this.m_shoulderItem != null && this.m_shoulderItem.m_shared.m_useDurability)
//                this.DrainEquipedItemDurability(this.m_shoulderItem, dt);
//            if (this.m_utilityItem == null || !this.m_utilityItem.m_shared.m_useDurability)
//                return;
//            this.DrainEquipedItemDurability(this.m_utilityItem, dt);
//        }

//        private void DrainEquipedItemDurability(ItemData item, float dt)
//        {
//            item.m_durability -= item.m_shared.m_durabilityDrain * dt;
//            if ((double)item.m_durability > 0.0)
//                return;
//            this.Message(MessageHud.MessageType.TopLeft, Localization.instance.Localize("$msg_broke", item.m_shared.m_name), 0, item.GetIcon());
//            this.UnequipItem(item, false);
//            if (!item.m_shared.m_destroyBroken)
//                return;
//            this.m_inventory.RemoveItem(item);
//        }

//        protected override void OnDamaged(HitData hit)
//        {
//            this.SetCrouch(false);
//        }

//        protected override void DamageArmorDurability(HitData hit)
//        {
//            List<ItemData> itemDataList = new List<ItemData>();
//            if (this.m_chestItem != null)
//                itemDataList.Add(this.m_chestItem);
//            if (this.m_legItem != null)
//                itemDataList.Add(this.m_legItem);
//            if (this.m_helmetItem != null)
//                itemDataList.Add(this.m_helmetItem);
//            if (this.m_shoulderItem != null)
//                itemDataList.Add(this.m_shoulderItem);
//            if (itemDataList.Count == 0)
//                return;
//            float num = hit.GetTotalPhysicalDamage() + hit.GetTotalElementalDamage();
//            if ((double)num <= 0.0)
//                return;
//            int index = UnityEngine.Random.Range(0, itemDataList.Count);
//            ItemData itemData = itemDataList[index];
//            itemData.m_durability = Mathf.Max(0.0f, itemData.m_durability - num);
//        }

//        public ItemData GetCurrentWeapon()
//        {
//            if (this.m_rightItem != null && this.m_rightItem.IsWeapon())
//                return this.m_rightItem;
//            if (this.m_leftItem != null && this.m_leftItem.IsWeapon() && this.m_leftItem.m_shared.m_itemType != ItemData.ItemType.Torch)
//                return this.m_leftItem;
//            return (bool)(UnityEngine.Object)this.m_unarmedWeapon ? this.m_unarmedWeapon.m_itemData : (ItemData)null;
//        }

//        protected ItemData GetCurrentBlocker()
//        {
//            return this.m_leftItem != null ? this.m_leftItem : this.GetCurrentWeapon();
//        }

//        private void UpdateAttack(float dt)
//        {
//            this.m_lastCombatTimer += dt;
//            if (this.GetCurrentWeapon() != null && this.m_currentAttack != null)
//                this.m_currentAttack.Update(dt);
//            if (this.InAttack())
//                this.m_timeSinceLastAttack = 0.0f;
//            else
//                this.m_timeSinceLastAttack += dt;
//        }

//        protected override float GetAttackSpeedFactorMovement()
//        {
//            return this.InAttack() && this.m_currentAttack != null && (this.IsFlying() || this.IsOnGround()) ? this.m_currentAttack.m_speedFactor : 1f;
//        }

//        protected override float GetAttackSpeedFactorRotation()
//        {
//            return this.InAttack() && this.m_currentAttack != null ? this.m_currentAttack.m_speedFactorRotation : 1f;
//        }

//        protected virtual bool HaveQueuedChain()
//        {
//            return false;
//        }

//        public override void OnWeaponTrailStart()
//        {
//            if (!this.m_nview.IsValid() || !this.m_nview.IsOwner() || (this.GetCurrentWeapon() == null || this.m_currentAttack == null))
//                return;
//            this.m_currentAttack.OnTrailStart();
//        }

//        public override void OnAttackTrigger()
//        {
//            if (!this.m_nview.IsValid() || !this.m_nview.IsOwner() || (this.GetCurrentWeapon() == null || this.m_currentAttack == null))
//                return;
//            this.m_currentAttack.OnAttackTrigger();
//        }

//        public override void OnStopMoving()
//        {
//            if (!this.m_nview.IsValid() || !this.m_nview.IsOwner() || (!this.InAttack() || this.GetCurrentWeapon() == null) || this.m_currentAttack == null)
//                return;
//            this.m_currentAttack.m_speedFactorRotation = 0.0f;
//            this.m_currentAttack.m_speedFactorRotation = 0.0f;
//        }

//        public virtual Vector3 GetAimDir(Vector3 fromPoint)
//        {
//            return this.GetLookDir();
//        }

//        public ItemData PickupPrefab(GameObject prefab, int stackSize = 0)
//        {
//            ZNetView.m_forceDisableInit = true;
//            GameObject go = UnityEngine.Object.Instantiate<GameObject>(prefab);
//            ZNetView.m_forceDisableInit = false;
//            if (stackSize > 0)
//            {
//                ItemDrop component = go.GetComponent<ItemDrop>();
//                component.m_itemData.m_stack = Mathf.Clamp(stackSize, 1, component.m_itemData.m_shared.m_maxStackSize);
//            }
//            if (this.Pickup(go))
//                return go.GetComponent<ItemDrop>().m_itemData;
//            UnityEngine.Object.Destroy((UnityEngine.Object)go);
//            return (ItemData)null;
//        }

//        public virtual bool HaveUniqueKey(string name)
//        {
//            return false;
//        }

//        public virtual void AddUniqueKey(string name)
//        {
//        }

//        public bool Pickup(GameObject go)
//        {
//            if (this.IsTeleporting())
//                return false;
//            ItemDrop component = go.GetComponent<ItemDrop>();
//            if ((UnityEngine.Object)component == (UnityEngine.Object)null || !component.CanPickup() || this.m_inventory.ContainsItem(component.m_itemData))
//                return false;
//            if (component.m_itemData.m_shared.m_questItem && this.HaveUniqueKey(component.m_itemData.m_shared.m_name))
//            {
//                this.Message(MessageHud.MessageType.Center, "$msg_cantpickup", 0, (Sprite)null);
//                return false;
//            }
//            bool flag = this.m_inventory.AddItem(component.m_itemData);
//            if (this.m_nview.GetZDO() == null)
//            {
//                UnityEngine.Object.Destroy((UnityEngine.Object)go);
//                return true;
//            }
//            if (!flag)
//            {
//                this.Message(MessageHud.MessageType.Center, "$msg_noroom", 0, (Sprite)null);
//                return false;
//            }
//            if (component.m_itemData.m_shared.m_questItem)
//                this.AddUniqueKey(component.m_itemData.m_shared.m_name);
//            ZNetScene.instance.Destroy(go);
//            if (flag && this.IsPlayer() && (this.m_rightItem == null && this.m_hiddenRightItem == null) && component.m_itemData.IsWeapon())
//                this.EquipItem(component.m_itemData, true);
//            this.m_pickupEffects.Create(this.transform.position, Quaternion.identity, (Transform)null, 1f);
//            if (this.IsPlayer())
//                this.ShowPickupMessage(component.m_itemData, component.m_itemData.m_stack);
//            return flag;
//        }

//        public void EquipBestWeapon(
//          Character targetCreature,
//          StaticTarget targetStatic,
//          Character hurtFriend,
//          Character friend)
//        {
//            List<ItemData> allItems = this.m_inventory.GetAllItems();
//            if (allItems.Count == 0 || this.InAttack())
//                return;
//            float num = 0.0f;
//            if ((bool)(UnityEngine.Object)targetCreature)
//            {
//                float radius = targetCreature.GetRadius();
//                num = Vector3.Distance(targetCreature.transform.position, this.transform.position) - radius;
//            }
//            else if ((bool)(UnityEngine.Object)targetStatic)
//                num = Vector3.Distance(targetStatic.transform.position, this.transform.position);
//            float time = Time.time;
//            this.IsFlying();
//            this.IsSwiming();
//            Humanoid.optimalWeapons.Clear();
//            Humanoid.outofRangeWeapons.Clear();
//            Humanoid.allWeapons.Clear();
//            foreach (ItemData itemData in allItems)
//            {
//                if (itemData.IsWeapon() && BaseAI.CanUseAttack((Character)this, itemData))
//                {
//                    if (itemData.m_shared.m_aiTargetType == ItemData.AiTarget.Enemy)
//                    {
//                        if ((double)num >= (double)itemData.m_shared.m_aiAttackRangeMin)
//                        {
//                            Humanoid.allWeapons.Add(itemData);
//                            if ((!((UnityEngine.Object)targetCreature == (UnityEngine.Object)null) || !((UnityEngine.Object)targetStatic == (UnityEngine.Object)null)) && (double)time - (double)itemData.m_lastAttackTime >= (double)itemData.m_shared.m_aiAttackInterval)
//                            {
//                                if ((double)num > (double)itemData.m_shared.m_aiAttackRange)
//                                {
//                                    Humanoid.outofRangeWeapons.Add(itemData);
//                                }
//                                else
//                                {
//                                    if (itemData.m_shared.m_aiPrioritized)
//                                    {
//                                        this.EquipItem(itemData, true);
//                                        return;
//                                    }
//                                    Humanoid.optimalWeapons.Add(itemData);
//                                }
//                            }
//                        }
//                    }
//                    else if (itemData.m_shared.m_aiTargetType == ItemData.AiTarget.FriendHurt)
//                    {
//                        if (!((UnityEngine.Object)hurtFriend == (UnityEngine.Object)null) && (double)time - (double)itemData.m_lastAttackTime >= (double)itemData.m_shared.m_aiAttackInterval)
//                        {
//                            if (itemData.m_shared.m_aiPrioritized)
//                            {
//                                this.EquipItem(itemData, true);
//                                return;
//                            }
//                            Humanoid.optimalWeapons.Add(itemData);
//                        }
//                    }
//                    else if (itemData.m_shared.m_aiTargetType == ItemData.AiTarget.Friend && !((UnityEngine.Object)friend == (UnityEngine.Object)null) && (double)time - (double)itemData.m_lastAttackTime >= (double)itemData.m_shared.m_aiAttackInterval)
//                    {
//                        if (itemData.m_shared.m_aiPrioritized)
//                        {
//                            this.EquipItem(itemData, true);
//                            return;
//                        }
//                        Humanoid.optimalWeapons.Add(itemData);
//                    }
//                }
//            }
//            if (Humanoid.optimalWeapons.Count > 0)
//                this.EquipItem(Humanoid.optimalWeapons[UnityEngine.Random.Range(0, Humanoid.optimalWeapons.Count)], true);
//            else if (Humanoid.outofRangeWeapons.Count > 0)
//                this.EquipItem(Humanoid.outofRangeWeapons[UnityEngine.Random.Range(0, Humanoid.outofRangeWeapons.Count)], true);
//            else if (Humanoid.allWeapons.Count > 0)
//            {
//                this.EquipItem(Humanoid.allWeapons[UnityEngine.Random.Range(0, Humanoid.allWeapons.Count)], true);
//            }
//            else
//            {
//                ItemData currentWeapon = this.GetCurrentWeapon();
//                if (currentWeapon == null)
//                    return;
//                this.UnequipItem(currentWeapon, false);
//            }
//        }

//        public bool DropItem(Inventory inventory, ItemData item, int amount)
//        {
//            if (amount == 0)
//                return false;
//            if (item.m_shared.m_questItem)
//            {
//                this.Message(MessageHud.MessageType.Center, "$msg_cantdrop", 0, (Sprite)null);
//                return false;
//            }
//            if (amount > item.m_stack)
//                amount = item.m_stack;
//            this.RemoveFromEquipQueue(item);
//            this.UnequipItem(item, false);
//            if (this.m_hiddenLeftItem == item)
//            {
//                this.m_hiddenLeftItem = (ItemData)null;
//                this.SetupVisEquipment(this.m_visEquipment, false);
//            }
//            if (this.m_hiddenRightItem == item)
//            {
//                this.m_hiddenRightItem = (ItemData)null;
//                this.SetupVisEquipment(this.m_visEquipment, false);
//            }
//            if (amount == item.m_stack)
//            {
//                ZLog.Log((object)("drop all " + (object)amount + "  " + (object)item.m_stack));
//                if (!inventory.RemoveItem(item))
//                {
//                    ZLog.Log((object)"Was not removed");
//                    return false;
//                }
//            }
//            else
//            {
//                ZLog.Log((object)("drop some " + (object)amount + "  " + (object)item.m_stack));
//                inventory.RemoveItem(item, amount);
//            }
//            ItemDrop itemDrop = ItemDrop.DropItem(item, amount, this.transform.position + this.transform.forward + this.transform.up, this.transform.rotation);
//            if (this.IsPlayer())
//                itemDrop.OnPlayerDrop();
//            itemDrop.GetComponent<Rigidbody>().set_velocity((this.transform.forward + Vector3.up) * 5f);
//            this.m_zanim.SetTrigger("interact");
//            this.m_dropEffects.Create(this.transform.position, Quaternion.identity, (Transform)null, 1f);
//            this.Message(MessageHud.MessageType.TopLeft, "$msg_dropped " + itemDrop.m_itemData.m_shared.m_name, itemDrop.m_itemData.m_stack, itemDrop.m_itemData.GetIcon());
//            return true;
//        }

//        protected virtual void SetPlaceMode(PieceTable buildPieces)
//        {
//        }

//        public Inventory GetInventory()
//        {
//            return this.m_inventory;
//        }

//        public void UseItem(Inventory inventory, ItemData item, bool fromInventoryGui)
//        {
//            if (inventory == null)
//                inventory = this.m_inventory;
//            if (!inventory.ContainsItem(item))
//                return;
//            GameObject hoverObject = this.GetHoverObject();
//            Hoverable hoverable = (bool)(UnityEngine.Object)hoverObject ? hoverObject.GetComponentInParent<Hoverable>() : (Hoverable)null;
//            if (hoverable != null && !fromInventoryGui)
//            {
//                Interactable componentInParent = hoverObject.GetComponentInParent<Interactable>();
//                if (componentInParent != null && componentInParent.UseItem(this, item))
//                    return;
//            }
//            if (item.m_shared.m_itemType == ItemData.ItemType.Consumable)
//            {
//                if (!this.ConsumeItem(inventory, item))
//                    return;
//                this.m_consumeItemEffects.Create(Player.m_localPlayer.transform.position, Quaternion.identity, (Transform)null, 1f);
//                this.m_zanim.SetTrigger("eat");
//            }
//            else
//            {
//                if (inventory == this.m_inventory && this.ToggleEquiped(item) || fromInventoryGui)
//                    return;
//                if (hoverable != null)
//                    this.Message(MessageHud.MessageType.Center, Localization.instance.Localize("$msg_cantuseon", item.m_shared.m_name, hoverable.GetHoverName()), 0, (Sprite)null);
//                else
//                    this.Message(MessageHud.MessageType.Center, Localization.instance.Localize("$msg_useonwhat", item.m_shared.m_name), 0, (Sprite)null);
//            }
//        }

//        public virtual void AbortEquipQueue()
//        {
//        }

//        public virtual void RemoveFromEquipQueue(ItemData item)
//        {
//        }

//        protected virtual bool ToggleEquiped(ItemData item)
//        {
//            if (!item.IsEquipable())
//                return false;
//            if (this.InAttack())
//                return true;
//            if (this.IsItemEquiped(item))
//                this.UnequipItem(item, true);
//            else
//                this.EquipItem(item, true);
//            return true;
//        }

//        public virtual bool CanConsumeItem(ItemData item)
//        {
//            return item.m_shared.m_itemType == ItemData.ItemType.Consumable;
//        }

//        public virtual bool ConsumeItem(Inventory inventory, ItemData item)
//        {
//            this.CanConsumeItem(item);
//            return false;
//        }

//        public bool EquipItem(ItemData item, bool triggerEquipEffects = true)
//        {
//            if (this.IsItemEquiped(item) || !this.m_inventory.ContainsItem(item) || (this.InAttack() || this.InDodge()) || this.IsPlayer() && !this.IsDead() && (this.IsSwiming() && !this.IsOnGround()) || item.m_shared.m_useDurability && (double)item.m_durability <= 0.0)
//                return false;
//            if (item.m_shared.m_dlc.Length > 0 && !DLCMan.instance.IsDLCInstalled(item.m_shared.m_dlc))
//            {
//                this.Message(MessageHud.MessageType.Center, "$msg_dlcrequired", 0, (Sprite)null);
//                return false;
//            }
//            if (item.m_shared.m_itemType == ItemData.ItemType.Tool)
//            {
//                this.UnequipItem(this.m_rightItem, triggerEquipEffects);
//                this.UnequipItem(this.m_leftItem, triggerEquipEffects);
//                this.m_rightItem = item;
//                this.m_hiddenRightItem = (ItemData)null;
//                this.m_hiddenLeftItem = (ItemData)null;
//            }
//            else if (item.m_shared.m_itemType == ItemData.ItemType.Torch)
//            {
//                if (this.m_rightItem != null && this.m_leftItem == null && this.m_rightItem.m_shared.m_itemType == ItemData.ItemType.OneHandedWeapon)
//                {
//                    this.m_leftItem = item;
//                }
//                else
//                {
//                    this.UnequipItem(this.m_rightItem, triggerEquipEffects);
//                    if (this.m_leftItem != null && this.m_leftItem.m_shared.m_itemType != ItemData.ItemType.Shield)
//                        this.UnequipItem(this.m_leftItem, triggerEquipEffects);
//                    this.m_rightItem = item;
//                }
//                this.m_hiddenRightItem = (ItemData)null;
//                this.m_hiddenLeftItem = (ItemData)null;
//            }
//            else if (item.m_shared.m_itemType == ItemData.ItemType.OneHandedWeapon)
//            {
//                if (this.m_rightItem != null && this.m_rightItem.m_shared.m_itemType == ItemData.ItemType.Torch && this.m_leftItem == null)
//                {
//                    ItemData rightItem = this.m_rightItem;
//                    this.UnequipItem(this.m_rightItem, triggerEquipEffects);
//                    this.m_leftItem = rightItem;
//                    this.m_leftItem.m_equiped = true;
//                }
//                this.UnequipItem(this.m_rightItem, triggerEquipEffects);
//                if (this.m_leftItem != null && this.m_leftItem.m_shared.m_itemType != ItemData.ItemType.Shield && this.m_leftItem.m_shared.m_itemType != ItemData.ItemType.Torch)
//                    this.UnequipItem(this.m_leftItem, triggerEquipEffects);
//                this.m_rightItem = item;
//                this.m_hiddenRightItem = (ItemData)null;
//                this.m_hiddenLeftItem = (ItemData)null;
//            }
//            else if (item.m_shared.m_itemType == ItemData.ItemType.Shield)
//            {
//                this.UnequipItem(this.m_leftItem, triggerEquipEffects);
//                if (this.m_rightItem != null && this.m_rightItem.m_shared.m_itemType != ItemData.ItemType.OneHandedWeapon && this.m_rightItem.m_shared.m_itemType != ItemData.ItemType.Torch)
//                    this.UnequipItem(this.m_rightItem, triggerEquipEffects);
//                this.m_leftItem = item;
//                this.m_hiddenRightItem = (ItemData)null;
//                this.m_hiddenLeftItem = (ItemData)null;
//            }
//            else if (item.m_shared.m_itemType == ItemData.ItemType.Bow)
//            {
//                this.UnequipItem(this.m_leftItem, triggerEquipEffects);
//                this.UnequipItem(this.m_rightItem, triggerEquipEffects);
//                this.m_leftItem = item;
//                this.m_hiddenRightItem = (ItemData)null;
//                this.m_hiddenLeftItem = (ItemData)null;
//            }
//            else if (item.m_shared.m_itemType == ItemData.ItemType.TwoHandedWeapon)
//            {
//                this.UnequipItem(this.m_leftItem, triggerEquipEffects);
//                this.UnequipItem(this.m_rightItem, triggerEquipEffects);
//                this.m_rightItem = item;
//                this.m_hiddenRightItem = (ItemData)null;
//                this.m_hiddenLeftItem = (ItemData)null;
//            }
//            else if (item.m_shared.m_itemType == ItemData.ItemType.Chest)
//            {
//                this.UnequipItem(this.m_chestItem, triggerEquipEffects);
//                this.m_chestItem = item;
//            }
//            else if (item.m_shared.m_itemType == ItemData.ItemType.Legs)
//            {
//                this.UnequipItem(this.m_legItem, triggerEquipEffects);
//                this.m_legItem = item;
//            }
//            else if (item.m_shared.m_itemType == ItemData.ItemType.Ammo)
//            {
//                this.UnequipItem(this.m_ammoItem, triggerEquipEffects);
//                this.m_ammoItem = item;
//            }
//            else if (item.m_shared.m_itemType == ItemData.ItemType.Helmet)
//            {
//                this.UnequipItem(this.m_helmetItem, triggerEquipEffects);
//                this.m_helmetItem = item;
//            }
//            else if (item.m_shared.m_itemType == ItemData.ItemType.Shoulder)
//            {
//                this.UnequipItem(this.m_shoulderItem, triggerEquipEffects);
//                this.m_shoulderItem = item;
//            }
//            else if (item.m_shared.m_itemType == ItemData.ItemType.Utility)
//            {
//                this.UnequipItem(this.m_utilityItem, triggerEquipEffects);
//                this.m_utilityItem = item;
//            }
//            if (this.IsItemEquiped(item))
//                item.m_equiped = true;
//            this.SetupEquipment();
//            if (triggerEquipEffects)
//                this.TriggerEquipEffect(item);
//            return true;
//        }

//        public void UnequipItem(ItemData item, bool triggerEquipEffects = true)
//        {
//            if (item == null)
//                return;
//            if (this.m_hiddenLeftItem == item)
//            {
//                this.m_hiddenLeftItem = (ItemData)null;
//                this.SetupVisEquipment(this.m_visEquipment, false);
//            }
//            if (this.m_hiddenRightItem == item)
//            {
//                this.m_hiddenRightItem = (ItemData)null;
//                this.SetupVisEquipment(this.m_visEquipment, false);
//            }
//            if (!this.IsItemEquiped(item))
//                return;
//            if (item.IsWeapon())
//            {
//                if (this.m_currentAttack != null && this.m_currentAttack.GetWeapon() == item)
//                {
//                    this.m_currentAttack.Stop();
//                    this.m_previousAttack = this.m_currentAttack;
//                    this.m_currentAttack = (Attack)null;
//                }
//                if (!string.IsNullOrEmpty(item.m_shared.m_holdAnimationState))
//                    this.m_zanim.SetBool(item.m_shared.m_holdAnimationState, false);
//                this.m_attackDrawTime = 0.0f;
//            }
//            if (this.m_rightItem == item)
//                this.m_rightItem = (ItemData)null;
//            else if (this.m_leftItem == item)
//                this.m_leftItem = (ItemData)null;
//            else if (this.m_chestItem == item)
//                this.m_chestItem = (ItemData)null;
//            else if (this.m_legItem == item)
//                this.m_legItem = (ItemData)null;
//            else if (this.m_ammoItem == item)
//                this.m_ammoItem = (ItemData)null;
//            else if (this.m_helmetItem == item)
//                this.m_helmetItem = (ItemData)null;
//            else if (this.m_shoulderItem == item)
//                this.m_shoulderItem = (ItemData)null;
//            else if (this.m_utilityItem == item)
//                this.m_utilityItem = (ItemData)null;
//            item.m_equiped = false;
//            this.SetupEquipment();
//            if (!triggerEquipEffects)
//                return;
//            this.TriggerEquipEffect(item);
//        }

//        private void TriggerEquipEffect(ItemData item)
//        {
//            if (this.m_nview.GetZDO() == null || Time.frameCount == this.m_lastEquipEffectFrame)
//                return;
//            this.m_lastEquipEffectFrame = Time.frameCount;
//            this.m_equipEffects.Create(this.transform.position, Quaternion.identity, (Transform)null, 1f);
//        }

//        public void UnequipAllItems()
//        {
//            if (this.m_rightItem != null)
//                this.UnequipItem(this.m_rightItem, false);
//            if (this.m_leftItem != null)
//                this.UnequipItem(this.m_leftItem, false);
//            if (this.m_chestItem != null)
//                this.UnequipItem(this.m_chestItem, false);
//            if (this.m_legItem != null)
//                this.UnequipItem(this.m_legItem, false);
//            if (this.m_helmetItem != null)
//                this.UnequipItem(this.m_helmetItem, false);
//            if (this.m_ammoItem != null)
//                this.UnequipItem(this.m_ammoItem, false);
//            if (this.m_shoulderItem != null)
//                this.UnequipItem(this.m_shoulderItem, false);
//            if (this.m_utilityItem == null)
//                return;
//            this.UnequipItem(this.m_utilityItem, false);
//        }

//        protected override void OnRagdollCreated(Ragdoll ragdoll)
//        {
//            VisEquipment component = ragdoll.GetComponent<VisEquipment>();
//            if (!(bool)(UnityEngine.Object)component)
//                return;
//            this.SetupVisEquipment(component, true);
//        }

//        protected virtual void SetupVisEquipment(VisEquipment visEq, bool isRagdoll)
//        {
//            if (!isRagdoll)
//            {
//                visEq.SetLeftItem(this.m_leftItem != null ? this.m_leftItem.m_dropPrefab.name : "", this.m_leftItem != null ? this.m_leftItem.m_variant : 0);
//                visEq.SetRightItem(this.m_rightItem != null ? this.m_rightItem.m_dropPrefab.name : "");
//                if (this.IsPlayer())
//                {
//                    visEq.SetLeftBackItem(this.m_hiddenLeftItem != null ? this.m_hiddenLeftItem.m_dropPrefab.name : "", this.m_hiddenLeftItem != null ? this.m_hiddenLeftItem.m_variant : 0);
//                    visEq.SetRightBackItem(this.m_hiddenRightItem != null ? this.m_hiddenRightItem.m_dropPrefab.name : "");
//                }
//            }
//            visEq.SetChestItem(this.m_chestItem != null ? this.m_chestItem.m_dropPrefab.name : "");
//            visEq.SetLegItem(this.m_legItem != null ? this.m_legItem.m_dropPrefab.name : "");
//            visEq.SetHelmetItem(this.m_helmetItem != null ? this.m_helmetItem.m_dropPrefab.name : "");
//            visEq.SetShoulderItem(this.m_shoulderItem != null ? this.m_shoulderItem.m_dropPrefab.name : "", this.m_shoulderItem != null ? this.m_shoulderItem.m_variant : 0);
//            visEq.SetUtilityItem(this.m_utilityItem != null ? this.m_utilityItem.m_dropPrefab.name : "");
//            if (!this.IsPlayer())
//                return;
//            visEq.SetBeardItem(this.m_beardItem);
//            visEq.SetHairItem(this.m_hairItem);
//        }

//        private void SetupEquipment()
//        {
//            if ((bool)(UnityEngine.Object)this.m_visEquipment && (this.m_nview.GetZDO() == null || this.m_nview.IsOwner()))
//                this.SetupVisEquipment(this.m_visEquipment, false);
//            if (this.m_nview.GetZDO() == null)
//                return;
//            this.UpdateEquipmentStatusEffects();
//            if (this.m_rightItem != null && (bool)(UnityEngine.Object)this.m_rightItem.m_shared.m_buildPieces)
//                this.SetPlaceMode(this.m_rightItem.m_shared.m_buildPieces);
//            else
//                this.SetPlaceMode((PieceTable)null);
//            this.SetupAnimationState();
//        }

//        private void SetupAnimationState()
//        {
//            if (this.m_leftItem != null)
//            {
//                if (this.m_leftItem.m_shared.m_itemType == ItemData.ItemType.Torch)
//                    this.SetAnimationState(ItemData.AnimationState.LeftTorch);
//                else
//                    this.SetAnimationState(this.m_leftItem.m_shared.m_animationState);
//            }
//            else if (this.m_rightItem != null)
//            {
//                this.SetAnimationState(this.m_rightItem.m_shared.m_animationState);
//            }
//            else
//            {
//                if (!((UnityEngine.Object)this.m_unarmedWeapon != (UnityEngine.Object)null))
//                    return;
//                this.SetAnimationState(this.m_unarmedWeapon.m_itemData.m_shared.m_animationState);
//            }
//        }

//        private void SetAnimationState(ItemData.AnimationState state)
//        {
//            this.m_zanim.SetFloat(Humanoid.statef, (float)state);
//            this.m_zanim.SetInt(Humanoid.statei, (int)state);
//        }

//        public bool IsSitting()
//        {
//            AnimatorStateInfo animatorStateInfo = this.m_animator.GetCurrentAnimatorStateInfo(0);
//            return ((AnimatorStateInfo)ref animatorStateInfo).get_tagHash() == Character.m_animatorTagSitting;
//        }

//        private void UpdateEquipmentStatusEffects()
//        {
//            HashSet<StatusEffect> statusEffectSet = new HashSet<StatusEffect>();
//            if (this.m_leftItem != null && (bool)(UnityEngine.Object)this.m_leftItem.m_shared.m_equipStatusEffect)
//                statusEffectSet.Add(this.m_leftItem.m_shared.m_equipStatusEffect);
//            if (this.m_rightItem != null && (bool)(UnityEngine.Object)this.m_rightItem.m_shared.m_equipStatusEffect)
//                statusEffectSet.Add(this.m_rightItem.m_shared.m_equipStatusEffect);
//            if (this.m_chestItem != null && (bool)(UnityEngine.Object)this.m_chestItem.m_shared.m_equipStatusEffect)
//                statusEffectSet.Add(this.m_chestItem.m_shared.m_equipStatusEffect);
//            if (this.m_legItem != null && (bool)(UnityEngine.Object)this.m_legItem.m_shared.m_equipStatusEffect)
//                statusEffectSet.Add(this.m_legItem.m_shared.m_equipStatusEffect);
//            if (this.m_helmetItem != null && (bool)(UnityEngine.Object)this.m_helmetItem.m_shared.m_equipStatusEffect)
//                statusEffectSet.Add(this.m_helmetItem.m_shared.m_equipStatusEffect);
//            if (this.m_shoulderItem != null && (bool)(UnityEngine.Object)this.m_shoulderItem.m_shared.m_equipStatusEffect)
//                statusEffectSet.Add(this.m_shoulderItem.m_shared.m_equipStatusEffect);
//            if (this.m_utilityItem != null && (bool)(UnityEngine.Object)this.m_utilityItem.m_shared.m_equipStatusEffect)
//                statusEffectSet.Add(this.m_utilityItem.m_shared.m_equipStatusEffect);
//            if (this.HaveSetEffect(this.m_leftItem))
//                statusEffectSet.Add(this.m_leftItem.m_shared.m_setStatusEffect);
//            if (this.HaveSetEffect(this.m_rightItem))
//                statusEffectSet.Add(this.m_rightItem.m_shared.m_setStatusEffect);
//            if (this.HaveSetEffect(this.m_chestItem))
//                statusEffectSet.Add(this.m_chestItem.m_shared.m_setStatusEffect);
//            if (this.HaveSetEffect(this.m_legItem))
//                statusEffectSet.Add(this.m_legItem.m_shared.m_setStatusEffect);
//            if (this.HaveSetEffect(this.m_helmetItem))
//                statusEffectSet.Add(this.m_helmetItem.m_shared.m_setStatusEffect);
//            if (this.HaveSetEffect(this.m_shoulderItem))
//                statusEffectSet.Add(this.m_shoulderItem.m_shared.m_setStatusEffect);
//            if (this.HaveSetEffect(this.m_utilityItem))
//                statusEffectSet.Add(this.m_utilityItem.m_shared.m_setStatusEffect);
//            foreach (StatusEffect eqipmentStatusEffect in this.m_eqipmentStatusEffects)
//            {
//                if (!statusEffectSet.Contains(eqipmentStatusEffect))
//                    this.m_seman.RemoveStatusEffect(eqipmentStatusEffect.name, false);
//            }
//            foreach (StatusEffect statusEffect in statusEffectSet)
//            {
//                if (!this.m_eqipmentStatusEffects.Contains(statusEffect))
//                    this.m_seman.AddStatusEffect(statusEffect, false);
//            }
//            this.m_eqipmentStatusEffects.Clear();
//            this.m_eqipmentStatusEffects.UnionWith((IEnumerable<StatusEffect>)statusEffectSet);
//        }

//        private bool HaveSetEffect(ItemData item)
//        {
//            return item != null && !((UnityEngine.Object)item.m_shared.m_setStatusEffect == (UnityEngine.Object)null) && (item.m_shared.m_setName.Length != 0 && item.m_shared.m_setSize > 1) && this.GetSetCount(item.m_shared.m_setName) >= item.m_shared.m_setSize;
//        }

//        private int GetSetCount(string setName)
//        {
//            int num = 0;
//            if (this.m_leftItem != null && this.m_leftItem.m_shared.m_setName == setName)
//                ++num;
//            if (this.m_rightItem != null && this.m_rightItem.m_shared.m_setName == setName)
//                ++num;
//            if (this.m_chestItem != null && this.m_chestItem.m_shared.m_setName == setName)
//                ++num;
//            if (this.m_legItem != null && this.m_legItem.m_shared.m_setName == setName)
//                ++num;
//            if (this.m_helmetItem != null && this.m_helmetItem.m_shared.m_setName == setName)
//                ++num;
//            if (this.m_shoulderItem != null && this.m_shoulderItem.m_shared.m_setName == setName)
//                ++num;
//            if (this.m_utilityItem != null && this.m_utilityItem.m_shared.m_setName == setName)
//                ++num;
//            return num;
//        }

//        public void SetBeard(string name)
//        {
//            this.m_beardItem = name;
//            this.SetupEquipment();
//        }

//        public string GetBeard()
//        {
//            return this.m_beardItem;
//        }

//        public void SetHair(string hair)
//        {
//            this.m_hairItem = hair;
//            this.SetupEquipment();
//        }

//        public string GetHair()
//        {
//            return this.m_hairItem;
//        }

//        public bool IsItemEquiped(ItemData item)
//        {
//            return this.m_rightItem == item || this.m_leftItem == item || (this.m_chestItem == item || this.m_legItem == item) || (this.m_ammoItem == item || this.m_helmetItem == item || (this.m_shoulderItem == item || this.m_utilityItem == item));
//        }

//        public ItemData GetRightItem()
//        {
//            return this.m_rightItem;
//        }

//        public ItemData GetLeftItem()
//        {
//            return this.m_leftItem;
//        }

//        protected override bool CheckRun(Vector3 moveDir, float dt)
//        {
//            return !this.IsHoldingAttack() && !this.IsBlocking() && base.CheckRun(moveDir, dt);
//        }

//        public override bool IsHoldingAttack()
//        {
//            ItemData currentWeapon = this.GetCurrentWeapon();
//            return currentWeapon != null && (double)currentWeapon.m_shared.m_holdDurationMin > 0.0 && (double)this.m_attackDrawTime > 0.0;
//        }

//        protected override bool BlockAttack(HitData hit, Character attacker)
//        {
//            if ((double)Vector3.Dot(hit.m_dir, this.transform.forward) > 0.0)
//                return false;
//            ItemData currentBlocker = this.GetCurrentBlocker();
//            if (currentBlocker == null)
//                return false;
//            bool flag1 = (double)currentBlocker.m_shared.m_timedBlockBonus > 1.0 && ((double)this.m_blockTimer != -1.0 && (double)this.m_blockTimer < 0.25);
//            float skillFactor = this.GetSkillFactor(Skills.SkillType.Blocking);
//            float blockPower = currentBlocker.GetBlockPower(skillFactor);
//            if (flag1)
//                blockPower *= currentBlocker.m_shared.m_timedBlockBonus;
//            float totalBlockableDamage = hit.GetTotalBlockableDamage();
//            float num1 = Mathf.Min(totalBlockableDamage, blockPower);
//            float num2 = Mathf.Clamp01(num1 / blockPower);
//            this.UseStamina(this.m_blockStaminaDrain * num2);
//            int num3 = this.HaveStamina(0.0f) ? 1 : 0;
//            bool flag2 = num3 != 0 && (double)blockPower >= (double)totalBlockableDamage;
//            if (num3 != 0)
//            {
//                hit.m_statusEffect = "";
//                hit.BlockDamage(num1);
//                DamageText.instance.ShowText(DamageText.TextType.Blocked, hit.m_point + Vector3.up * 0.5f, num1, false);
//            }
//            if (num3 == 0 || !flag2)
//                this.Stagger(hit.m_dir);
//            if (currentBlocker.m_shared.m_useDurability)
//            {
//                float num4 = currentBlocker.m_shared.m_useDurabilityDrain * num2;
//                currentBlocker.m_durability -= num4;
//            }
//            this.RaiseSkill(Skills.SkillType.Blocking, flag1 ? 2f : 1f);
//            currentBlocker.m_shared.m_blockEffect.Create(hit.m_point, Quaternion.identity, (Transform)null, 1f);
//            if ((bool)(UnityEngine.Object)attacker & flag1 & flag2)
//            {
//                this.m_perfectBlockEffect.Create(hit.m_point, Quaternion.identity, (Transform)null, 1f);
//                if (attacker.m_staggerWhenBlocked)
//                    attacker.Stagger(-hit.m_dir);
//            }
//            if (flag2)
//            {
//                float num4 = Mathf.Clamp01(num2 * 0.5f);
//                hit.m_pushForce *= num4;
//                if ((bool)(UnityEngine.Object)attacker & flag1)
//                {
//                    HitData hit1 = new HitData()
//                    {
//                        m_pushForce = currentBlocker.GetDeflectionForce() * (1f - num4),
//                        m_dir = attacker.transform.position - this.transform.position
//                    };
//                    hit1.m_dir.y = 0.0f;
//                    hit1.m_dir.Normalize();
//                    attacker.Damage(hit1);
//                }
//            }
//            return true;
//        }

//        public override bool IsBlocking()
//        {
//            if (this.m_nview.IsValid() && !this.m_nview.IsOwner())
//                return this.m_nview.GetZDO().GetBool(Humanoid.isBlockingHash, false);
//            return this.m_blocking && !this.InAttack() && (!this.InDodge() && !this.InPlaceMode()) && !this.IsEncumbered() && !this.InMinorAction();
//        }

//        private void UpdateBlock(float dt)
//        {
//            if (this.IsBlocking())
//            {
//                if (!this.m_internalBlockingState)
//                {
//                    this.m_internalBlockingState = true;
//                    this.m_nview.GetZDO().Set(Humanoid.isBlockingHash, true);
//                    this.m_zanim.SetBool(Humanoid.blocking, true);
//                }
//                if ((double)this.m_blockTimer < 0.0)
//                    this.m_blockTimer = 0.0f;
//                else
//                    this.m_blockTimer += dt;
//            }
//            else
//            {
//                if (this.m_internalBlockingState)
//                {
//                    this.m_internalBlockingState = false;
//                    this.m_nview.GetZDO().Set(Humanoid.isBlockingHash, false);
//                    this.m_zanim.SetBool(Humanoid.blocking, false);
//                }
//                this.m_blockTimer = -1f;
//            }
//        }

//        public void HideHandItems()
//        {
//            if (this.m_leftItem == null && this.m_rightItem == null)
//                return;
//            ItemData leftItem = this.m_leftItem;
//            ItemData rightItem = this.m_rightItem;
//            this.UnequipItem(this.m_leftItem, true);
//            this.UnequipItem(this.m_rightItem, true);
//            this.m_hiddenLeftItem = leftItem;
//            this.m_hiddenRightItem = rightItem;
//            this.SetupVisEquipment(this.m_visEquipment, false);
//            this.m_zanim.SetTrigger("equip_hip");
//        }

//        public void ShowHandItems()
//        {
//            ItemData hiddenLeftItem = this.m_hiddenLeftItem;
//            ItemData hiddenRightItem = this.m_hiddenRightItem;
//            if (hiddenLeftItem == null && hiddenRightItem == null)
//                return;
//            this.m_hiddenLeftItem = (ItemData)null;
//            this.m_hiddenRightItem = (ItemData)null;
//            if (hiddenLeftItem != null)
//                this.EquipItem(hiddenLeftItem, true);
//            if (hiddenRightItem != null)
//                this.EquipItem(hiddenRightItem, true);
//            this.m_zanim.SetTrigger("equip_hip");
//        }

//        public ItemData GetAmmoItem()
//        {
//            return this.m_ammoItem;
//        }

//        public virtual GameObject GetHoverObject()
//        {
//            return (GameObject)null;
//        }

//        public bool IsTeleportable()
//        {
//            return this.m_inventory.IsTeleportable();
//        }

//        public override bool UseMeleeCamera()
//        {
//            ItemData currentWeapon = this.GetCurrentWeapon();
//            return currentWeapon != null && currentWeapon.m_shared.m_centerCamera;
//        }

//        public float GetEquipmentWeight()
//        {
//            float num = 0.0f;
//            if (this.m_rightItem != null)
//                num += this.m_rightItem.m_shared.m_weight;
//            if (this.m_leftItem != null)
//                num += this.m_leftItem.m_shared.m_weight;
//            if (this.m_chestItem != null)
//                num += this.m_chestItem.m_shared.m_weight;
//            if (this.m_legItem != null)
//                num += this.m_legItem.m_shared.m_weight;
//            if (this.m_helmetItem != null)
//                num += this.m_helmetItem.m_shared.m_weight;
//            if (this.m_shoulderItem != null)
//                num += this.m_shoulderItem.m_shared.m_weight;
//            if (this.m_utilityItem != null)
//                num += this.m_utilityItem.m_shared.m_weight;
//            return num;
//        }

//        [Serializable]
//        public class ItemSet
//        {
//            public string m_name = "";
//            public GameObject[] m_items = new GameObject[0];
//        }
//    }
