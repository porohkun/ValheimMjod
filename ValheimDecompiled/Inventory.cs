// Decompiled with JetBrains decompiler
// Type: Inventory
// Assembly: assembly_valheim, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F48D6A22-6962-45BF-8D82-0AAD6AFA4FDB
// Assembly location: D:\Games\Steam\steamapps\common\Valheim\valheim_Data\Managed\assembly_valheim.dll

using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    private int currentVersion = 103;
    private string m_name = "";
    private List<ItemDrop.ItemData> m_inventory = new List<ItemDrop.ItemData>();
    private int m_width = 4;
    private int m_height = 4;
    public Action m_onChanged;
    private Sprite m_bkg;
    private float m_totalWeight;

    public Inventory(string name, Sprite bkg, int w, int h)
    {
        this.m_bkg = bkg;
        this.m_name = name;
        this.m_width = w;
        this.m_height = h;
    }

    private bool AddItem(ItemDrop.ItemData item, int amount, int x, int y)
    {
        amount = Mathf.Min(amount, item.m_stack);
        if (x < 0 || y < 0 || (x >= this.m_width || y >= this.m_height))
            return false;
        ItemDrop.ItemData itemAt = this.GetItemAt(x, y);
        bool flag;
        if (itemAt != null)
        {
            if (itemAt.m_shared.m_name != item.m_shared.m_name || itemAt.m_shared.m_maxQuality > 1 && itemAt.m_quality != item.m_quality)
                return false;
            int a = itemAt.m_shared.m_maxStackSize - itemAt.m_stack;
            if (a <= 0)
                return false;
            int num = Mathf.Min(a, amount);
            itemAt.m_stack += num;
            item.m_stack -= num;
            flag = num == amount;
            ZLog.Log((object)("Added to stack" + (object)itemAt.m_stack + " " + (object)item.m_stack));
        }
        else
        {
            ItemDrop.ItemData itemData = item.Clone();
            itemData.m_stack = amount;
            itemData.m_gridPos = new Vector2i(x, y);
            this.m_inventory.Add(itemData);
            item.m_stack -= amount;
            flag = true;
        }
        this.Changed();
        return flag;
    }

    public bool CanAddItem(GameObject prefab, int stack = -1)
    {
        ItemDrop component = prefab.GetComponent<ItemDrop>();
        return !((UnityEngine.Object)component == (UnityEngine.Object)null) && this.CanAddItem(component.m_itemData, stack);
    }

    public bool CanAddItem(ItemDrop.ItemData item, int stack = -1)
    {
        if (this.HaveEmptySlot())
            return true;
        if (stack <= 0)
            stack = item.m_stack;
        return this.FindFreeStackSpace(item.m_shared.m_name) >= stack;
    }

    public bool AddItem(ItemDrop.ItemData item)
    {
        bool flag = true;
        if (item.m_shared.m_maxStackSize > 1)
        {
            for (int index = 0; index < item.m_stack; ++index)
            {
                ItemDrop.ItemData freeStackItem = this.FindFreeStackItem(item.m_shared.m_name, item.m_quality);
                if (freeStackItem != null)
                {
                    ++freeStackItem.m_stack;
                }
                else
                {
                    int num = item.m_stack - index;
                    item.m_stack = num;
                    Vector2i emptySlot = this.FindEmptySlot(this.TopFirst(item));
                    if (emptySlot.x >= 0)
                    {
                        item.m_gridPos = emptySlot;
                        this.m_inventory.Add(item);
                        break;
                    }
                    flag = false;
                    break;
                }
            }
        }
        else
        {
            Vector2i emptySlot = this.FindEmptySlot(this.TopFirst(item));
            if (emptySlot.x >= 0)
            {
                item.m_gridPos = emptySlot;
                this.m_inventory.Add(item);
            }
            else
                flag = false;
        }
        this.Changed();
        return flag;
    }

    private bool TopFirst(ItemDrop.ItemData item)
    {
        return item.IsWeapon() || item.m_shared.m_itemType == ItemDrop.ItemData.ItemType.Tool || (item.m_shared.m_itemType == ItemDrop.ItemData.ItemType.Shield || item.m_shared.m_itemType == ItemDrop.ItemData.ItemType.Utility);
    }

    public void MoveAll(Inventory fromInventory)
    {
        List<ItemDrop.ItemData> itemDataList1 = new List<ItemDrop.ItemData>((IEnumerable<ItemDrop.ItemData>)fromInventory.GetAllItems());
        List<ItemDrop.ItemData> itemDataList2 = new List<ItemDrop.ItemData>();
        foreach (ItemDrop.ItemData itemData in itemDataList1)
        {
            if (this.AddItem(itemData, itemData.m_stack, itemData.m_gridPos.x, itemData.m_gridPos.y))
                fromInventory.RemoveItem(itemData);
            else
                itemDataList2.Add(itemData);
        }
        foreach (ItemDrop.ItemData itemData in itemDataList2)
        {
            if (this.AddItem(itemData))
                fromInventory.RemoveItem(itemData);
            else
                break;
        }
        this.Changed();
        fromInventory.Changed();
    }

    public void MoveItemToThis(Inventory fromInventory, ItemDrop.ItemData item)
    {
        if (this.AddItem(item))
            fromInventory.RemoveItem(item);
        this.Changed();
        fromInventory.Changed();
    }

    public bool MoveItemToThis(
      Inventory fromInventory,
      ItemDrop.ItemData item,
      int amount,
      int x,
      int y)
    {
        int num = this.AddItem(item, amount, x, y) ? 1 : 0;
        if (item.m_stack == 0)
        {
            fromInventory.RemoveItem(item);
            return num != 0;
        }
        fromInventory.Changed();
        return num != 0;
    }

    public bool RemoveItem(int index)
    {
        if (index < 0 || index >= this.m_inventory.Count)
            return false;
        this.m_inventory.RemoveAt(index);
        this.Changed();
        return true;
    }

    public bool ContainsItem(ItemDrop.ItemData item)
    {
        return this.m_inventory.Contains(item);
    }

    public bool RemoveOneItem(ItemDrop.ItemData item)
    {
        if (!this.m_inventory.Contains(item))
            return false;
        if (item.m_stack > 1)
        {
            --item.m_stack;
            this.Changed();
        }
        else
        {
            this.m_inventory.Remove(item);
            this.Changed();
        }
        return true;
    }

    public bool RemoveItem(ItemDrop.ItemData item)
    {
        if (!this.m_inventory.Contains(item))
        {
            ZLog.Log((object)"Item is not in this container");
            return false;
        }
        this.m_inventory.Remove(item);
        this.Changed();
        return true;
    }

    public bool RemoveItem(ItemDrop.ItemData item, int amount)
    {
        amount = Mathf.Min(item.m_stack, amount);
        if (amount == item.m_stack)
            return this.RemoveItem(item);
        if (!this.m_inventory.Contains(item))
            return false;
        item.m_stack -= amount;
        this.Changed();
        return true;
    }

    public void RemoveItem(string name, int amount)
    {
        foreach (ItemDrop.ItemData itemData in this.m_inventory)
        {
            if (itemData.m_shared.m_name == name)
            {
                int num = Mathf.Min(itemData.m_stack, amount);
                itemData.m_stack -= num;
                amount -= num;
                if (amount <= 0)
                    break;
            }
        }
        this.m_inventory.RemoveAll((Predicate<ItemDrop.ItemData>)(x => x.m_stack <= 0));
        this.Changed();
    }

    public bool HaveItem(string name)
    {
        foreach (ItemDrop.ItemData itemData in this.m_inventory)
        {
            if (itemData.m_shared.m_name == name)
                return true;
        }
        return false;
    }

    public void GetAllPieceTables(List<PieceTable> tables)
    {
        foreach (ItemDrop.ItemData itemData in this.m_inventory)
        {
            if ((UnityEngine.Object)itemData.m_shared.m_buildPieces != (UnityEngine.Object)null && !tables.Contains(itemData.m_shared.m_buildPieces))
                tables.Add(itemData.m_shared.m_buildPieces);
        }
    }

    public int CountItems(string name)
    {
        int num = 0;
        foreach (ItemDrop.ItemData itemData in this.m_inventory)
        {
            if (itemData.m_shared.m_name == name)
                num += itemData.m_stack;
        }
        return num;
    }

    public ItemDrop.ItemData GetItem(int index)
    {
        return this.m_inventory[index];
    }

    public ItemDrop.ItemData GetItem(string name)
    {
        foreach (ItemDrop.ItemData itemData in this.m_inventory)
        {
            if (itemData.m_shared.m_name == name)
                return itemData;
        }
        return (ItemDrop.ItemData)null;
    }

    public ItemDrop.ItemData GetAmmoItem(string ammoName)
    {
        int num1 = 0;
        ItemDrop.ItemData itemData1 = (ItemDrop.ItemData)null;
        foreach (ItemDrop.ItemData itemData2 in this.m_inventory)
        {
            if ((itemData2.m_shared.m_itemType == ItemDrop.ItemData.ItemType.Ammo || itemData2.m_shared.m_itemType == ItemDrop.ItemData.ItemType.Consumable) && itemData2.m_shared.m_ammoType == ammoName)
            {
                int num2 = itemData2.m_gridPos.y * this.m_width + itemData2.m_gridPos.x;
                if (num2 < num1 || itemData1 == null)
                {
                    num1 = num2;
                    itemData1 = itemData2;
                }
            }
        }
        return itemData1;
    }

    private int FindFreeStackSpace(string name)
    {
        int num = 0;
        foreach (ItemDrop.ItemData itemData in this.m_inventory)
        {
            if (itemData.m_shared.m_name == name && itemData.m_stack < itemData.m_shared.m_maxStackSize)
                num += itemData.m_shared.m_maxStackSize - itemData.m_stack;
        }
        return num;
    }

    private ItemDrop.ItemData FindFreeStackItem(string name, int quality)
    {
        foreach (ItemDrop.ItemData itemData in this.m_inventory)
        {
            if (itemData.m_shared.m_name == name && itemData.m_quality == quality && itemData.m_stack < itemData.m_shared.m_maxStackSize)
                return itemData;
        }
        return (ItemDrop.ItemData)null;
    }

    public int NrOfItems()
    {
        return this.m_inventory.Count;
    }

    public float SlotsUsedPercentage()
    {
        return (float)((double)this.m_inventory.Count / (double)(this.m_width * this.m_height) * 100.0);
    }

    public void Print()
    {
        for (int index = 0; index < this.m_inventory.Count; ++index)
        {
            ItemDrop.ItemData itemData = this.m_inventory[index];
            ZLog.Log((object)(index.ToString() + ": " + itemData.m_shared.m_name + "  " + (object)itemData.m_stack + " / " + (object)itemData.m_shared.m_maxStackSize));
        }
    }

    public int GetEmptySlots()
    {
        return this.m_height * this.m_width - this.m_inventory.Count;
    }

    public bool HaveEmptySlot()
    {
        return this.m_inventory.Count < this.m_width * this.m_height;
    }

    private Vector2i FindEmptySlot(bool topFirst)
    {
        if (topFirst)
        {
            for (int index1 = 0; index1 < this.m_height; ++index1)
            {
                for (int index2 = 0; index2 < this.m_width; ++index2)
                {
                    if (this.GetItemAt(index2, index1) == null)
                        return new Vector2i(index2, index1);
                }
            }
        }
        else
        {
            for (int index1 = this.m_height - 1; index1 >= 0; --index1)
            {
                for (int index2 = 0; index2 < this.m_width; ++index2)
                {
                    if (this.GetItemAt(index2, index1) == null)
                        return new Vector2i(index2, index1);
                }
            }
        }
        return new Vector2i(-1, -1);
    }

    public ItemDrop.ItemData GetOtherItemAt(int x, int y, ItemDrop.ItemData oldItem)
    {
        foreach (ItemDrop.ItemData itemData in this.m_inventory)
        {
            if (itemData != oldItem && itemData.m_gridPos.x == x && itemData.m_gridPos.y == y)
                return itemData;
        }
        return (ItemDrop.ItemData)null;
    }

    public ItemDrop.ItemData GetItemAt(int x, int y)
    {
        foreach (ItemDrop.ItemData itemData in this.m_inventory)
        {
            if (itemData.m_gridPos.x == x && itemData.m_gridPos.y == y)
                return itemData;
        }
        return (ItemDrop.ItemData)null;
    }

    public List<ItemDrop.ItemData> GetEquipedtems()
    {
        List<ItemDrop.ItemData> itemDataList = new List<ItemDrop.ItemData>();
        foreach (ItemDrop.ItemData itemData in this.m_inventory)
        {
            if (itemData.m_equiped)
                itemDataList.Add(itemData);
        }
        return itemDataList;
    }

    public void GetWornItems(List<ItemDrop.ItemData> worn)
    {
        foreach (ItemDrop.ItemData itemData in this.m_inventory)
        {
            if (itemData.m_shared.m_useDurability && (double)itemData.m_durability < (double)itemData.GetMaxDurability())
                worn.Add(itemData);
        }
    }

    public void GetValuableItems(List<ItemDrop.ItemData> items)
    {
        foreach (ItemDrop.ItemData itemData in this.m_inventory)
        {
            if (itemData.m_shared.m_value > 0)
                items.Add(itemData);
        }
    }

    public List<ItemDrop.ItemData> GetAllItems()
    {
        return this.m_inventory;
    }

    public void GetAllItems(string name, List<ItemDrop.ItemData> items)
    {
        foreach (ItemDrop.ItemData itemData in this.m_inventory)
        {
            if (itemData.m_shared.m_name == name)
                items.Add(itemData);
        }
    }

    public void GetAllItems(ItemDrop.ItemData.ItemType type, List<ItemDrop.ItemData> items)
    {
        foreach (ItemDrop.ItemData itemData in this.m_inventory)
        {
            if (itemData.m_shared.m_itemType == type)
                items.Add(itemData);
        }
    }

    public int GetWidth()
    {
        return this.m_width;
    }

    public int GetHeight()
    {
        return this.m_height;
    }

    public string GetName()
    {
        return this.m_name;
    }

    public Sprite GetBkg()
    {
        return this.m_bkg;
    }

    public void Save(ZPackage pkg)
    {
        pkg.Write(this.currentVersion);
        pkg.Write(this.m_inventory.Count);
        foreach (ItemDrop.ItemData itemData in this.m_inventory)
        {
            if ((UnityEngine.Object)itemData.m_dropPrefab == (UnityEngine.Object)null)
            {
                ZLog.Log((object)("Item missing prefab " + itemData.m_shared.m_name));
                pkg.Write("");
            }
            else
                pkg.Write(itemData.m_dropPrefab.name);
            pkg.Write(itemData.m_stack);
            pkg.Write(itemData.m_durability);
            pkg.Write(itemData.m_gridPos);
            pkg.Write(itemData.m_equiped);
            pkg.Write(itemData.m_quality);
            pkg.Write(itemData.m_variant);
            pkg.Write(itemData.m_crafterID);
            pkg.Write(itemData.m_crafterName);
        }
    }

    public void Load(ZPackage pkg)
    {
        int num1 = pkg.ReadInt();
        int num2 = pkg.ReadInt();
        this.m_inventory.Clear();
        for (int index = 0; index < num2; ++index)
        {
            string name = pkg.ReadString();
            int stack = pkg.ReadInt();
            float durability = pkg.ReadSingle();
            Vector2i pos = pkg.ReadVector2i();
            bool equiped = pkg.ReadBool();
            int quality = 1;
            if (num1 >= 101)
                quality = pkg.ReadInt();
            int variant = 0;
            if (num1 >= 102)
                variant = pkg.ReadInt();
            long crafterID = 0;
            string crafterName = "";
            if (num1 >= 103)
            {
                crafterID = pkg.ReadLong();
                crafterName = pkg.ReadString();
            }
            if (name != "")
                this.AddItem(name, stack, durability, pos, equiped, quality, variant, crafterID, crafterName);
        }
        this.Changed();
    }

    public ItemDrop.ItemData AddItem(
      string name,
      int stack,
      int quality,
      int variant,
      long crafterID,
      string crafterName)
    {
        GameObject itemPrefab = ObjectDB.instance.GetItemPrefab(name);
        if ((UnityEngine.Object)itemPrefab == (UnityEngine.Object)null)
        {
            ZLog.Log((object)("Failed to find item prefab " + name));
            return (ItemDrop.ItemData)null;
        }
        ItemDrop component1 = itemPrefab.GetComponent<ItemDrop>();
        if ((UnityEngine.Object)component1 == (UnityEngine.Object)null)
        {
            ZLog.Log((object)("Invalid item " + name));
            return (ItemDrop.ItemData)null;
        }
        if (this.FindEmptySlot(this.TopFirst(component1.m_itemData)).x == -1)
            return (ItemDrop.ItemData)null;
        ItemDrop.ItemData itemData = (ItemDrop.ItemData)null;
        int a = stack;
        while (a > 0)
        {
            ZNetView.m_forceDisableInit = true;
            GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(itemPrefab);
            ZNetView.m_forceDisableInit = false;
            ItemDrop component2 = gameObject.GetComponent<ItemDrop>();
            if ((UnityEngine.Object)component2 == (UnityEngine.Object)null)
            {
                ZLog.Log((object)("Missing itemdrop in " + name));
                UnityEngine.Object.Destroy((UnityEngine.Object)gameObject);
                return (ItemDrop.ItemData)null;
            }
            int num = Mathf.Min(a, component2.m_itemData.m_shared.m_maxStackSize);
            a -= num;
            component2.m_itemData.m_stack = num;
            component2.m_itemData.m_quality = quality;
            component2.m_itemData.m_variant = variant;
            component2.m_itemData.m_durability = component2.m_itemData.GetMaxDurability();
            component2.m_itemData.m_crafterID = crafterID;
            component2.m_itemData.m_crafterName = crafterName;
            this.AddItem(component2.m_itemData);
            itemData = component2.m_itemData;
            UnityEngine.Object.Destroy((UnityEngine.Object)gameObject);
        }
        return itemData;
    }

    private bool AddItem(
      string name,
      int stack,
      float durability,
      Vector2i pos,
      bool equiped,
      int quality,
      int variant,
      long crafterID,
      string crafterName)
    {
        GameObject itemPrefab = ObjectDB.instance.GetItemPrefab(name);
        if ((UnityEngine.Object)itemPrefab == (UnityEngine.Object)null)
        {
            ZLog.Log((object)("Failed to find item prefab " + name));
            return false;
        }
        ZNetView.m_forceDisableInit = true;
        GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(itemPrefab);
        ZNetView.m_forceDisableInit = false;
        ItemDrop component = gameObject.GetComponent<ItemDrop>();
        if ((UnityEngine.Object)component == (UnityEngine.Object)null)
        {
            ZLog.Log((object)("Missing itemdrop in " + name));
            UnityEngine.Object.Destroy((UnityEngine.Object)gameObject);
            return false;
        }
        component.m_itemData.m_stack = Mathf.Min(stack, component.m_itemData.m_shared.m_maxStackSize);
        component.m_itemData.m_durability = durability;
        component.m_itemData.m_equiped = equiped;
        component.m_itemData.m_quality = quality;
        component.m_itemData.m_variant = variant;
        component.m_itemData.m_crafterID = crafterID;
        component.m_itemData.m_crafterName = crafterName;
        this.AddItem(component.m_itemData, component.m_itemData.m_stack, pos.x, pos.y);
        UnityEngine.Object.Destroy((UnityEngine.Object)gameObject);
        return true;
    }

    public void MoveInventoryToGrave(Inventory original)
    {
        this.m_inventory.Clear();
        this.m_width = original.m_width;
        this.m_height = original.m_height;
        foreach (ItemDrop.ItemData itemData in original.m_inventory)
        {
            if (!itemData.m_shared.m_questItem && !itemData.m_equiped)
                this.m_inventory.Add(itemData);
        }
        original.m_inventory.RemoveAll((Predicate<ItemDrop.ItemData>)(x => !x.m_shared.m_questItem && !x.m_equiped));
        original.Changed();
        this.Changed();
    }

    private void Changed()
    {
        this.UpdateTotalWeight();
        if (this.m_onChanged == null)
            return;
        this.m_onChanged();
    }

    public void RemoveAll()
    {
        this.m_inventory.Clear();
        this.Changed();
    }

    private void UpdateTotalWeight()
    {
        this.m_totalWeight = 0.0f;
        foreach (ItemDrop.ItemData itemData in this.m_inventory)
            this.m_totalWeight += itemData.GetWeight();
    }

    public float GetTotalWeight()
    {
        return this.m_totalWeight;
    }

    public void GetBoundItems(List<ItemDrop.ItemData> bound)
    {
        bound.Clear();
        foreach (ItemDrop.ItemData itemData in this.m_inventory)
        {
            if (itemData.m_gridPos.y == 0)
                bound.Add(itemData);
        }
    }

    public bool IsTeleportable()
    {
        foreach (ItemDrop.ItemData itemData in this.m_inventory)
        {
            if (!itemData.m_shared.m_teleportable)
                return false;
        }
        return true;
    }
}
