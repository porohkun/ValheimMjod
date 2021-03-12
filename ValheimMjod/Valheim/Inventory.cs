using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Valheim
{
    public class Inventory
    {
        private int currentVersion = 103;
        private string m_name = "";
        public List<ItemData> m_inventory = new List<ItemData>();
        private int m_width = 4;
        private int m_height = 4;

        public Inventory(string name, int w, int h)
        {
            this.m_name = name;
            this.m_width = w;
            this.m_height = h;
        }


        public void Save(ZPackage pkg)
        {
            pkg.Write(this.currentVersion);
            pkg.Write(this.m_inventory.Count);
            foreach (ItemData itemData in this.m_inventory.Where(i => !string.IsNullOrWhiteSpace(i.m_name)))
            {
                pkg.Write(itemData.m_name);
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
                    m_inventory.Add(new ItemData()
                    {
                        m_name = name,
                        m_stack = stack,
                        m_durability = durability,
                        m_gridPos = pos,
                        m_equiped = equiped,
                        m_quality = quality,
                        m_variant = variant,
                        m_crafterID = crafterID,
                        m_crafterName = crafterName
                    });
            }

            for (int x = 0; x < m_width; x++)
                for (int y = 0; y < m_height; y++)
                    if (!m_inventory.Any(i => i.m_gridPos == new Vector2i(x, y)))
                        m_inventory.Add(new ItemData() { m_gridPos = new Vector2i(x, y) });
        }
    }
}