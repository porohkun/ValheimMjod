// Decompiled with JetBrains decompiler
// Type: ItemDrop
// Assembly: assembly_valheim, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F48D6A22-6962-45BF-8D82-0AAD6AFA4FDB
// Assembly location: D:\Games\Steam\steamapps\common\Valheim\valheim_Data\Managed\assembly_valheim.dll

using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class ItemDrop : MonoBehaviour, Hoverable, Interactable
{
    private static List<ItemDrop> m_instances = new List<ItemDrop>();
    private static int m_itemMask = 0;
    private int m_myIndex = -1;
    public bool m_autoPickup = true;
    public bool m_autoDestroy = true;
    public ItemDrop.ItemData m_itemData = new ItemDrop.ItemData();
    private ZNetView m_nview;
    private Character m_pickupRequester;
    private float m_lastOwnerRequest;
    private float m_spawnTime;
    private const double m_autoDestroyTimeout = 3600.0;
    private const double m_autoPickupDelay = 0.5;
    private const float m_autoDespawnBaseMinAltitude = -2f;
    private const int m_autoStackTreshold = 200;
    private const float m_autoStackRange = 4f;
    private bool m_haveAutoStacked;

    private void Awake()
    {
        this.m_myIndex = ItemDrop.m_instances.Count;
        ItemDrop.m_instances.Add(this);
        GameObject itemPrefab = ObjectDB.instance.GetItemPrefab(this.GetPrefabName(this.gameObject.name));
        this.m_itemData.m_dropPrefab = itemPrefab;
        if (Application.isEditor)
            this.m_itemData.m_shared = itemPrefab.GetComponent<ItemDrop>().m_itemData.m_shared;
        Rigidbody component = this.GetComponent<Rigidbody>();
        if ((bool)(UnityEngine.Object)component)
            component.set_maxDepenetrationVelocity(1f);
        this.m_spawnTime = Time.time;
        this.m_nview = this.GetComponent<ZNetView>();
        if (!(bool)(UnityEngine.Object)this.m_nview || !this.m_nview.IsValid())
            return;
        if (this.m_nview.IsOwner() && new DateTime(this.m_nview.GetZDO().GetLong("SpawnTime", 0L)).Ticks == 0L)
            this.m_nview.GetZDO().Set("SpawnTime", ZNet.instance.GetTime().Ticks);
        this.m_nview.Register("RequestOwn", new Action<long>(this.RPC_RequestOwn));
        this.Load();
        this.InvokeRepeating("SlowUpdate", UnityEngine.Random.Range(1f, 2f), 10f);
    }

    private void OnDestroy()
    {
        ItemDrop.m_instances[this.m_myIndex] = ItemDrop.m_instances[ItemDrop.m_instances.Count - 1];
        ItemDrop.m_instances[this.m_myIndex].m_myIndex = this.m_myIndex;
        ItemDrop.m_instances.RemoveAt(ItemDrop.m_instances.Count - 1);
    }

    private void Start()
    {
        this.Save();
        this.gameObject.GetComponentInChildren<IEquipmentVisual>()?.Setup(this.m_itemData.m_variant);
    }

    private double GetTimeSinceSpawned()
    {
        return (ZNet.instance.GetTime() - new DateTime(this.m_nview.GetZDO().GetLong("SpawnTime", 0L))).TotalSeconds;
    }

    private void SlowUpdate()
    {
        if (!this.m_nview.IsValid() || !this.m_nview.IsOwner())
            return;
        this.TerrainCheck();
        if (this.m_autoDestroy)
            this.TimedDestruction();
        if (ItemDrop.m_instances.Count <= 200)
            return;
        this.AutoStackItems();
    }

    private void TerrainCheck()
    {
        float groundHeight = ZoneSystem.instance.GetGroundHeight(this.transform.position);
        if ((double)this.transform.position.y - (double)groundHeight >= -0.5)
            return;
        Vector3 position = this.transform.position;
        position.y = groundHeight + 0.5f;
        this.transform.position = position;
        Rigidbody component = this.GetComponent<Rigidbody>();
        if (!(bool)(UnityEngine.Object)component)
            return;
        component.set_velocity(Vector3.zero);
    }

    private void TimedDestruction()
    {
        if (this.IsInsideBase() || Player.IsPlayerInRange(this.transform.position, 25f) || this.GetTimeSinceSpawned() < 3600.0)
            return;
        this.m_nview.Destroy();
    }

    private bool IsInsideBase()
    {
        return (double)this.transform.position.y > (double)ZoneSystem.instance.m_waterLevel - 2.0 && (bool)(UnityEngine.Object)EffectArea.IsPointInsideArea(this.transform.position, EffectArea.Type.PlayerBase, 0.0f);
    }

    private void AutoStackItems()
    {
        if (this.m_itemData.m_shared.m_maxStackSize <= 1 || this.m_itemData.m_stack >= this.m_itemData.m_shared.m_maxStackSize || this.m_haveAutoStacked)
            return;
        this.m_haveAutoStacked = true;
        if (ItemDrop.m_itemMask == 0)
            ItemDrop.m_itemMask = LayerMask.GetMask("item");
        bool flag = false;
        foreach (Collider collider in Physics.OverlapSphere(this.transform.position, 4f, ItemDrop.m_itemMask))
        {
            if ((bool)(UnityEngine.Object)collider.get_attachedRigidbody())
            {
                ItemDrop component = ((Component)collider.get_attachedRigidbody()).GetComponent<ItemDrop>();
                if (!((UnityEngine.Object)component == (UnityEngine.Object)null) && !((UnityEngine.Object)component == (UnityEngine.Object)this) && (!((UnityEngine.Object)component.m_nview == (UnityEngine.Object)null) && component.m_nview.IsValid()) && (component.m_nview.IsOwner() && !(component.m_itemData.m_shared.m_name != this.m_itemData.m_shared.m_name) && component.m_itemData.m_quality == this.m_itemData.m_quality))
                {
                    int num = this.m_itemData.m_shared.m_maxStackSize - this.m_itemData.m_stack;
                    if (num != 0)
                    {
                        if (component.m_itemData.m_stack <= num)
                        {
                            this.m_itemData.m_stack += component.m_itemData.m_stack;
                            flag = true;
                            component.m_nview.Destroy();
                        }
                    }
                    else
                        break;
                }
            }
        }
        if (!flag)
            return;
        this.Save();
    }

    public string GetHoverText()
    {
        string str = this.m_itemData.m_shared.m_name;
        if (this.m_itemData.m_quality > 1)
            str = str + "[" + (object)this.m_itemData.m_quality + "] ";
        if (this.m_itemData.m_stack > 1)
            str = str + " x" + this.m_itemData.m_stack.ToString();
        return Localization.instance.Localize(str + "\n[<color=yellow><b>$KEY_Use</b></color>] $inventory_pickup");
    }

    public string GetHoverName()
    {
        return this.m_itemData.m_shared.m_name;
    }

    private string GetPrefabName(string name)
    {
        char[] anyOf = new char[2] { '(', ' ' };
        int length = name.IndexOfAny(anyOf);
        return length < 0 ? name : name.Substring(0, length);
    }

    public bool Interact(Humanoid character, bool repeat)
    {
        if (repeat)
            return false;
        this.Pickup(character);
        return true;
    }

    public bool UseItem(Humanoid user, ItemDrop.ItemData item)
    {
        return false;
    }

    public void SetStack(int stack)
    {
        if (!this.m_nview.IsValid() || !this.m_nview.IsOwner())
            return;
        this.m_itemData.m_stack = stack;
        if (this.m_itemData.m_stack > this.m_itemData.m_shared.m_maxStackSize)
            this.m_itemData.m_stack = this.m_itemData.m_shared.m_maxStackSize;
        this.Save();
    }

    public void Pickup(Humanoid character)
    {
        if (!this.m_nview.IsValid())
            return;
        if (this.CanPickup())
        {
            this.Load();
            character.Pickup(this.gameObject);
            this.Save();
        }
        else
        {
            this.m_pickupRequester = (Character)character;
            this.CancelInvoke("PickupUpdate");
            float num = 0.05f;
            this.InvokeRepeating("PickupUpdate", num, num);
            this.RequestOwn();
        }
    }

    public void RequestOwn()
    {
        if ((double)Time.time - (double)this.m_lastOwnerRequest < 0.200000002980232 || this.m_nview.IsOwner())
            return;
        this.m_lastOwnerRequest = Time.time;
        this.m_nview.InvokeRPC(nameof(RequestOwn), (object[])Array.Empty<object>());
    }

    public bool RemoveOne()
    {
        if (!this.CanPickup())
        {
            this.RequestOwn();
            return false;
        }
        if (this.m_itemData.m_stack <= 1)
        {
            this.m_nview.Destroy();
            return true;
        }
        --this.m_itemData.m_stack;
        this.Save();
        return true;
    }

    public void OnPlayerDrop()
    {
        this.m_autoPickup = false;
    }

    public bool CanPickup()
    {
        if ((UnityEngine.Object)this.m_nview == (UnityEngine.Object)null || !this.m_nview.IsValid())
            return true;
        return (double)Time.time - (double)this.m_spawnTime >= 0.5 && this.m_nview.IsOwner();
    }

    private void RPC_RequestOwn(long uid)
    {
        ZLog.Log((object)("Player " + (object)uid + " wants to pickup " + this.gameObject.name + "   im: " + (object)ZDOMan.instance.GetMyID()));
        if (!this.m_nview.IsOwner())
            ZLog.Log((object)"  but im not the owner");
        else
            this.m_nview.GetZDO().SetOwner(uid);
    }

    private void PickupUpdate()
    {
        if (!this.m_nview.IsValid())
            return;
        if (this.CanPickup())
        {
            ZLog.Log((object)"Im finally the owner");
            this.CancelInvoke(nameof(PickupUpdate));
            this.Load();
            (this.m_pickupRequester as Player).Pickup(this.gameObject);
            this.Save();
        }
        else
            ZLog.Log((object)"Im still nto the owner");
    }

    private void Save()
    {
        if ((UnityEngine.Object)this.m_nview == (UnityEngine.Object)null || !this.m_nview.IsValid() || !this.m_nview.IsOwner())
            return;
        ItemDrop.SaveToZDO(this.m_itemData, this.m_nview.GetZDO());
    }

    private void Load()
    {
        ItemDrop.LoadFromZDO(this.m_itemData, this.m_nview.GetZDO());
    }

    public static void SaveToZDO(ItemDrop.ItemData itemData, ZDO zdo)
    {
        zdo.Set("durability", itemData.m_durability);
        zdo.Set("stack", itemData.m_stack);
        zdo.Set("quality", itemData.m_quality);
        zdo.Set("variant", itemData.m_variant);
        zdo.Set("crafterID", itemData.m_crafterID);
        zdo.Set("crafterName", itemData.m_crafterName);
    }

    public static void LoadFromZDO(ItemDrop.ItemData itemData, ZDO zdo)
    {
        itemData.m_durability = zdo.GetFloat("durability", itemData.m_durability);
        itemData.m_stack = zdo.GetInt("stack", itemData.m_stack);
        itemData.m_quality = zdo.GetInt("quality", itemData.m_quality);
        itemData.m_variant = zdo.GetInt("variant", itemData.m_variant);
        itemData.m_crafterID = zdo.GetLong("crafterID", itemData.m_crafterID);
        itemData.m_crafterName = zdo.GetString("crafterName", itemData.m_crafterName);
    }

    public static ItemDrop DropItem(
      ItemDrop.ItemData item,
      int amount,
      Vector3 position,
      Quaternion rotation)
    {
        ItemDrop component = UnityEngine.Object.Instantiate<GameObject>(item.m_dropPrefab, position, rotation).GetComponent<ItemDrop>();
        component.m_itemData = item.Clone();
        if (amount > 0)
            component.m_itemData.m_stack = amount;
        component.Save();
        return component;
    }

    private void OnDrawGizmos()
    {
    }

    [Serializable]
    public class ItemData
    {
        public int m_stack = 1;
        public float m_durability = 100f;
        public int m_quality = 1;
        [NonSerialized]
        public string m_crafterName = "";
        [NonSerialized]
        public Vector2i m_gridPos = Vector2i.zero;
        public int m_variant;
        public ItemDrop.ItemData.SharedData m_shared;
        [NonSerialized]
        public long m_crafterID;
        [NonSerialized]
        public bool m_equiped;
        [NonSerialized]
        public GameObject m_dropPrefab;
        [NonSerialized]
        public float m_lastAttackTime;
        [NonSerialized]
        public GameObject m_lastProjectile;

        public ItemDrop.ItemData Clone()
        {
            return this.MemberwiseClone() as ItemDrop.ItemData;
        }

        public bool IsEquipable()
        {
            return this.m_shared.m_itemType == ItemDrop.ItemData.ItemType.Tool || this.m_shared.m_itemType == ItemDrop.ItemData.ItemType.OneHandedWeapon || (this.m_shared.m_itemType == ItemDrop.ItemData.ItemType.TwoHandedWeapon || this.m_shared.m_itemType == ItemDrop.ItemData.ItemType.Bow) || (this.m_shared.m_itemType == ItemDrop.ItemData.ItemType.Shield || this.m_shared.m_itemType == ItemDrop.ItemData.ItemType.Helmet || (this.m_shared.m_itemType == ItemDrop.ItemData.ItemType.Chest || this.m_shared.m_itemType == ItemDrop.ItemData.ItemType.Legs)) || (this.m_shared.m_itemType == ItemDrop.ItemData.ItemType.Shoulder || this.m_shared.m_itemType == ItemDrop.ItemData.ItemType.Ammo || this.m_shared.m_itemType == ItemDrop.ItemData.ItemType.Torch) || this.m_shared.m_itemType == ItemDrop.ItemData.ItemType.Utility;
        }

        public bool IsWeapon()
        {
            return this.m_shared.m_itemType == ItemDrop.ItemData.ItemType.OneHandedWeapon || this.m_shared.m_itemType == ItemDrop.ItemData.ItemType.Bow || this.m_shared.m_itemType == ItemDrop.ItemData.ItemType.TwoHandedWeapon || this.m_shared.m_itemType == ItemDrop.ItemData.ItemType.Torch;
        }

        public bool HavePrimaryAttack()
        {
            return !string.IsNullOrEmpty(this.m_shared.m_attack.m_attackAnimation);
        }

        public bool HaveSecondaryAttack()
        {
            return !string.IsNullOrEmpty(this.m_shared.m_secondaryAttack.m_attackAnimation);
        }

        public float GetArmor()
        {
            return this.GetArmor(this.m_quality);
        }

        public float GetArmor(int quality)
        {
            return this.m_shared.m_armor + (float)Mathf.Max(0, quality - 1) * this.m_shared.m_armorPerLevel;
        }

        public int GetValue()
        {
            return this.m_shared.m_value * this.m_stack;
        }

        public float GetWeight()
        {
            return this.m_shared.m_weight * (float)this.m_stack;
        }

        public HitData.DamageTypes GetDamage()
        {
            return this.GetDamage(this.m_quality);
        }

        public float GetDurabilityPercentage()
        {
            float maxDurability = this.GetMaxDurability();
            return (double)maxDurability == 0.0 ? 1f : Mathf.Clamp01(this.m_durability / maxDurability);
        }

        public float GetMaxDurability()
        {
            return this.GetMaxDurability(this.m_quality);
        }

        public float GetMaxDurability(int quality)
        {
            return this.m_shared.m_maxDurability + (float)Mathf.Max(0, quality - 1) * this.m_shared.m_durabilityPerLevel;
        }

        public HitData.DamageTypes GetDamage(int quality)
        {
            HitData.DamageTypes damages = this.m_shared.m_damages;
            if (quality > 1)
                damages.Add(this.m_shared.m_damagesPerLevel, quality - 1);
            return damages;
        }

        public float GetBaseBlockPower()
        {
            return this.GetBaseBlockPower(this.m_quality);
        }

        public float GetBaseBlockPower(int quality)
        {
            return this.m_shared.m_blockPower + (float)Mathf.Max(0, quality - 1) * this.m_shared.m_blockPowerPerLevel;
        }

        public float GetBlockPower(float skillFactor)
        {
            return this.GetBlockPower(this.m_quality, skillFactor);
        }

        public float GetBlockPower(int quality, float skillFactor)
        {
            double baseBlockPower = (double)this.GetBaseBlockPower(quality);
            return (float)(baseBlockPower + baseBlockPower * (double)skillFactor * 0.5);
        }

        public float GetBlockPowerTooltip(int quality)
        {
            if ((UnityEngine.Object)Player.m_localPlayer == (UnityEngine.Object)null)
                return 0.0f;
            float skillFactor = Player.m_localPlayer.GetSkillFactor(Skills.SkillType.Blocking);
            return this.GetBlockPower(quality, skillFactor);
        }

        public float GetDeflectionForce()
        {
            return this.GetDeflectionForce(this.m_quality);
        }

        public float GetDeflectionForce(int quality)
        {
            return this.m_shared.m_deflectionForce + (float)Mathf.Max(0, quality - 1) * this.m_shared.m_deflectionForcePerLevel;
        }

        public string GetTooltip()
        {
            return ItemDrop.ItemData.GetTooltip(this, this.m_quality, false);
        }

        public Sprite GetIcon()
        {
            return this.m_shared.m_icons[this.m_variant];
        }

        private static void AddHandedTip(ItemDrop.ItemData item, StringBuilder text)
        {
            switch (item.m_shared.m_itemType)
            {
                case ItemDrop.ItemData.ItemType.OneHandedWeapon:
                case ItemDrop.ItemData.ItemType.Shield:
                case ItemDrop.ItemData.ItemType.Torch:
                    text.Append("\n$item_onehanded");
                    break;
                case ItemDrop.ItemData.ItemType.Bow:
                case ItemDrop.ItemData.ItemType.TwoHandedWeapon:
                case ItemDrop.ItemData.ItemType.Tool:
                    text.Append("\n$item_twohanded");
                    break;
            }
        }

        public static string GetTooltip(ItemDrop.ItemData item, int qualityLevel, bool crafting)
        {
            Player localPlayer = Player.m_localPlayer;
            StringBuilder text = new StringBuilder(256);
            text.Append(item.m_shared.m_description);
            text.Append("\n\n");
            if (item.m_shared.m_dlc.Length > 0)
                text.Append("\n<color=aqua>$item_dlc</color>");
            ItemDrop.ItemData.AddHandedTip(item, text);
            if (item.m_crafterID != 0L)
                text.AppendFormat("\n$item_crafter: <color=orange>{0}</color>", (object)item.m_crafterName);
            if (!item.m_shared.m_teleportable)
                text.Append("\n<color=orange>$item_noteleport</color>");
            if (item.m_shared.m_value > 0)
                text.AppendFormat("\n$item_value: <color=orange>{0}  ({1})</color>", (object)item.GetValue(), (object)item.m_shared.m_value);
            text.AppendFormat("\n$item_weight: <color=orange>{0}</color>", (object)item.GetWeight().ToString("0.0"));
            if (item.m_shared.m_maxQuality > 1)
                text.AppendFormat("\n$item_quality: <color=orange>{0}</color>", (object)qualityLevel);
            float num;
            if (item.m_shared.m_useDurability)
            {
                if (crafting)
                {
                    float maxDurability = item.GetMaxDurability(qualityLevel);
                    text.AppendFormat("\n$item_durability: <color=orange>{0}</color>", (object)maxDurability);
                }
                else
                {
                    float maxDurability = item.GetMaxDurability(qualityLevel);
                    float durability = item.m_durability;
                    StringBuilder stringBuilder = text;
                    num = item.GetDurabilityPercentage() * 100f;
                    string str1 = num.ToString("0");
                    string str2 = durability.ToString("0");
                    string str3 = maxDurability.ToString("0");
                    stringBuilder.AppendFormat("\n$item_durability: <color=orange>{0}%</color> <color=yellow>({1}/{2})</color>", (object)str1, (object)str2, (object)str3);
                }
                if (item.m_shared.m_canBeReparied)
                {
                    Recipe recipe = ObjectDB.instance.GetRecipe(item);
                    if ((UnityEngine.Object)recipe != (UnityEngine.Object)null)
                    {
                        int minStationLevel = recipe.m_minStationLevel;
                        text.AppendFormat("\n$item_repairlevel: <color=orange>{0}</color>", (object)minStationLevel.ToString());
                    }
                }
            }
            switch (item.m_shared.m_itemType)
            {
                case ItemDrop.ItemData.ItemType.Consumable:
                    if ((double)item.m_shared.m_food > 0.0)
                    {
                        text.AppendFormat("\n$item_food_health: <color=orange>{0}</color>", (object)item.m_shared.m_food);
                        text.AppendFormat("\n$item_food_stamina: <color=orange>{0}</color>", (object)item.m_shared.m_foodStamina);
                        text.AppendFormat("\n$item_food_duration: <color=orange>{0}s</color>", (object)item.m_shared.m_foodBurnTime);
                        text.AppendFormat("\n$item_food_regen: <color=orange>{0} hp/tick</color>", (object)item.m_shared.m_foodRegen);
                    }
                    string statusEffectTooltip1 = item.GetStatusEffectTooltip();
                    if (statusEffectTooltip1.Length > 0)
                    {
                        text.Append("\n\n");
                        text.Append(statusEffectTooltip1);
                        break;
                    }
                    break;
                case ItemDrop.ItemData.ItemType.OneHandedWeapon:
                case ItemDrop.ItemData.ItemType.Bow:
                case ItemDrop.ItemData.ItemType.TwoHandedWeapon:
                case ItemDrop.ItemData.ItemType.Torch:
                    text.Append(item.GetDamage(qualityLevel).GetTooltipString(item.m_shared.m_skillType));
                    StringBuilder stringBuilder1 = text;
                    // ISSUE: variable of a boxed type
                    __Boxed<float> baseBlockPower1 = (ValueType)item.GetBaseBlockPower(qualityLevel);
                    num = item.GetBlockPowerTooltip(qualityLevel);
                    string str4 = num.ToString("0");
                    stringBuilder1.AppendFormat("\n$item_blockpower: <color=orange>{0}</color> <color=yellow>({1})</color>", (object)baseBlockPower1, (object)str4);
                    if ((double)item.m_shared.m_timedBlockBonus > 1.0)
                    {
                        text.AppendFormat("\n$item_deflection: <color=orange>{0}</color>", (object)item.GetDeflectionForce(qualityLevel));
                        text.AppendFormat("\n$item_parrybonus: <color=orange>{0}x</color>", (object)item.m_shared.m_timedBlockBonus);
                    }
                    text.AppendFormat("\n$item_knockback: <color=orange>{0}</color>", (object)item.m_shared.m_attackForce);
                    text.AppendFormat("\n$item_backstab: <color=orange>{0}x</color>", (object)item.m_shared.m_backstabBonus);
                    string projectileTooltip = item.GetProjectileTooltip(qualityLevel);
                    if (projectileTooltip.Length > 0)
                    {
                        text.Append("\n\n");
                        text.Append(projectileTooltip);
                    }
                    string statusEffectTooltip2 = item.GetStatusEffectTooltip();
                    if (statusEffectTooltip2.Length > 0)
                    {
                        text.Append("\n\n");
                        text.Append(statusEffectTooltip2);
                        break;
                    }
                    break;
                case ItemDrop.ItemData.ItemType.Shield:
                    StringBuilder stringBuilder2 = text;
                    // ISSUE: variable of a boxed type
                    __Boxed<float> baseBlockPower2 = (ValueType)item.GetBaseBlockPower(qualityLevel);
                    num = item.GetBlockPowerTooltip(qualityLevel);
                    string str5 = num.ToString("0");
                    stringBuilder2.AppendFormat("\n$item_blockpower: <color=orange>{0}</color> <color=yellow>({1})</color>", (object)baseBlockPower2, (object)str5);
                    if ((double)item.m_shared.m_timedBlockBonus > 1.0)
                    {
                        text.AppendFormat("\n$item_deflection: <color=orange>{0}</color>", (object)item.GetDeflectionForce(qualityLevel));
                        text.AppendFormat("\n$item_parrybonus: <color=orange>{0}x</color>", (object)item.m_shared.m_timedBlockBonus);
                        break;
                    }
                    break;
                case ItemDrop.ItemData.ItemType.Helmet:
                case ItemDrop.ItemData.ItemType.Chest:
                case ItemDrop.ItemData.ItemType.Legs:
                case ItemDrop.ItemData.ItemType.Shoulder:
                    text.AppendFormat("\n$item_armor: <color=orange>{0}</color>", (object)item.GetArmor(qualityLevel));
                    string modifiersTooltipString = SE_Stats.GetDamageModifiersTooltipString(item.m_shared.m_damageModifiers);
                    if (modifiersTooltipString.Length > 0)
                        text.Append(modifiersTooltipString);
                    string statusEffectTooltip3 = item.GetStatusEffectTooltip();
                    if (statusEffectTooltip3.Length > 0)
                    {
                        text.Append("\n\n");
                        text.Append(statusEffectTooltip3);
                        break;
                    }
                    break;
                case ItemDrop.ItemData.ItemType.Ammo:
                    text.Append(item.GetDamage(qualityLevel).GetTooltipString(item.m_shared.m_skillType));
                    text.AppendFormat("\n$item_knockback: <color=orange>{0}</color>", (object)item.m_shared.m_attackForce);
                    break;
            }
            if ((double)item.m_shared.m_movementModifier != 0.0 && (UnityEngine.Object)localPlayer != (UnityEngine.Object)null)
            {
                float movementModifier = localPlayer.GetEquipmentMovementModifier();
                StringBuilder stringBuilder3 = text;
                num = item.m_shared.m_movementModifier * 100f;
                string str1 = num.ToString("+0;-0");
                num = movementModifier * 100f;
                string str2 = num.ToString("+0;-0");
                stringBuilder3.AppendFormat("\n$item_movement_modifier: <color=orange>{0}%</color> ($item_total:<color=yellow>{1}%</color>)", (object)str1, (object)str2);
            }
            string statusEffectTooltip4 = item.GetSetStatusEffectTooltip();
            if (statusEffectTooltip4.Length > 0)
                text.AppendFormat("\n\n$item_seteffect (<color=orange>{0}</color> $item_parts):<color=orange>{1}</color>", (object)item.m_shared.m_setSize, (object)statusEffectTooltip4);
            return text.ToString();
        }

        private string GetStatusEffectTooltip()
        {
            if ((bool)(UnityEngine.Object)this.m_shared.m_attackStatusEffect)
                return this.m_shared.m_attackStatusEffect.GetTooltipString();
            return (bool)(UnityEngine.Object)this.m_shared.m_consumeStatusEffect ? this.m_shared.m_consumeStatusEffect.GetTooltipString() : "";
        }

        private string GetSetStatusEffectTooltip()
        {
            if ((bool)(UnityEngine.Object)this.m_shared.m_setStatusEffect)
            {
                StatusEffect setStatusEffect = this.m_shared.m_setStatusEffect;
                if ((UnityEngine.Object)setStatusEffect != (UnityEngine.Object)null)
                    return setStatusEffect.GetTooltipString();
            }
            return "";
        }

        private string GetProjectileTooltip(int itemQuality)
        {
            string str = "";
            if ((bool)(UnityEngine.Object)this.m_shared.m_attack.m_attackProjectile)
            {
                IProjectile component = this.m_shared.m_attack.m_attackProjectile.GetComponent<IProjectile>();
                if (component != null)
                    str += component.GetTooltipString(itemQuality);
            }
            if ((bool)(UnityEngine.Object)this.m_shared.m_spawnOnHit)
            {
                IProjectile component = this.m_shared.m_spawnOnHit.GetComponent<IProjectile>();
                if (component != null)
                    str += component.GetTooltipString(itemQuality);
            }
            return str;
        }

        public enum ItemType
        {
            None = 0,
            Material = 1,
            Consumable = 2,
            OneHandedWeapon = 3,
            Bow = 4,
            Shield = 5,
            Helmet = 6,
            Chest = 7,
            Ammo = 9,
            Customization = 10, // 0x0000000A
            Legs = 11, // 0x0000000B
            Hands = 12, // 0x0000000C
            Trophie = 13, // 0x0000000D
            TwoHandedWeapon = 14, // 0x0000000E
            Torch = 15, // 0x0000000F
            Misc = 16, // 0x00000010
            Shoulder = 17, // 0x00000011
            Utility = 18, // 0x00000012
            Tool = 19, // 0x00000013
            Attach_Atgeir = 20, // 0x00000014
        }

        public enum AnimationState
        {
            Unarmed,
            OneHanded,
            TwoHandedClub,
            Bow,
            Shield,
            Torch,
            LeftTorch,
            Atgeir,
            TwoHandedAxe,
            FishingRod,
        }

        public enum AiTarget
        {
            Enemy,
            FriendHurt,
            Friend,
        }

        [Serializable]
        public class SharedData
        {
            public string m_name = "";
            public string m_dlc = "";
            public ItemDrop.ItemData.ItemType m_itemType = ItemDrop.ItemData.ItemType.Misc;
            public Sprite[] m_icons = new Sprite[0];
            [TextArea]
            public string m_description = "";
            public int m_maxStackSize = 1;
            public int m_maxQuality = 1;
            public float m_weight = 1f;
            public bool m_teleportable = true;
            public float m_equipDuration = 1f;
            public Vector2Int m_trophyPos = Vector2Int.zero;
            public string m_setName = "";
            public Color m_foodColor = Color.white;
            public bool m_helmetHideHair = true;
            public float m_armor = 10f;
            public float m_armorPerLevel = 1f;
            public List<HitData.DamageModPair> m_damageModifiers = new List<HitData.DamageModPair>();
            [Header("Shield settings")]
            public float m_blockPower = 10f;
            public float m_timedBlockBonus = 1.5f;
            [Header("Weapon")]
            public ItemDrop.ItemData.AnimationState m_animationState = ItemDrop.ItemData.AnimationState.OneHanded;
            public Skills.SkillType m_skillType = Skills.SkillType.Swords;
            public float m_attackForce = 30f;
            public float m_backstabBonus = 4f;
            public bool m_destroyBroken = true;
            public bool m_canBeReparied = true;
            public float m_maxDurability = 100f;
            public float m_durabilityPerLevel = 50f;
            public float m_useDurabilityDrain = 1f;
            public string m_holdAnimationState = "";
            [Header("Ammo")]
            public string m_ammoType = "";
            [Header("AI")]
            public float m_aiAttackRange = 2f;
            public float m_aiAttackInterval = 2f;
            public float m_aiAttackMaxAngle = 5f;
            public bool m_aiWhenFlying = true;
            public bool m_aiWhenWalking = true;
            public bool m_aiWhenSwiming = true;
            [Header("Effects")]
            public EffectList m_hitEffect = new EffectList();
            public EffectList m_hitTerrainEffect = new EffectList();
            public EffectList m_blockEffect = new EffectList();
            public EffectList m_startEffect = new EffectList();
            public EffectList m_holdStartEffect = new EffectList();
            public EffectList m_triggerEffect = new EffectList();
            public EffectList m_trailStartEffect = new EffectList();
            public ItemDrop.ItemData.ItemType m_attachOverride;
            public int m_value;
            public bool m_questItem;
            public int m_variants;
            public PieceTable m_buildPieces;
            public bool m_centerCamera;
            public int m_setSize;
            public StatusEffect m_setStatusEffect;
            public StatusEffect m_equipStatusEffect;
            public float m_movementModifier;
            [Header("Food settings")]
            public float m_food;
            public float m_foodStamina;
            public float m_foodBurnTime;
            public float m_foodRegen;
            [Header("Armor settings")]
            public Material m_armorMaterial;
            public float m_blockPowerPerLevel;
            public float m_deflectionForce;
            public float m_deflectionForcePerLevel;
            public int m_toolTier;
            public HitData.DamageTypes m_damages;
            public HitData.DamageTypes m_damagesPerLevel;
            public bool m_dodgeable;
            public bool m_blockable;
            public StatusEffect m_attackStatusEffect;
            public GameObject m_spawnOnHit;
            public GameObject m_spawnOnHitTerrain;
            [Header("Attacks")]
            public Attack m_attack;
            public Attack m_secondaryAttack;
            [Header("Durability")]
            public bool m_useDurability;
            public float m_durabilityDrain;
            [Header("Hold")]
            public float m_holdDurationMin;
            public float m_holdStaminaDrain;
            public float m_aiAttackRangeMin;
            public bool m_aiPrioritized;
            public ItemDrop.ItemData.AiTarget m_aiTargetType;
            [Header("Consumable")]
            public StatusEffect m_consumeStatusEffect;
        }
    }
}
