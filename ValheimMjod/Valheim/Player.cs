using System;
using System.Collections.Generic;
using UnityEngine;

namespace Valheim
{
    public class Player
    {
        public class Food
        {
            public string m_name = "";
            //public ItemData m_item;
            public float m_health;
            public float m_stamina;

            public bool CanEatAgain()
            {
                //return (double)this.m_health < (double)this.m_item.m_shared.m_food / 2.0;
                throw new NotImplementedException();
            }
        }

        protected long m_playerId;
        protected string m_playerName = "";
        private bool m_isLoading;
        protected ZNetView m_nview;
        public float m_health;
        public float m_maxHealth;
        private float m_maxStamina = 100f;
        private float m_stamina = 100f;
        private bool m_firstSpawn = true;
        private float m_timeSinceDeath = 999999f;
        private string m_guardianPower = "";
        private float m_guardianPowerCooldown;
        protected string m_beardItem = "";
        protected string m_hairItem = "";
        private Vector3 m_skinColor = Vector3.one;
        private Vector3 m_hairColor = Vector3.one;
        public int ModelIndex { get; set; }
        public Skills Skills { get; } = new Skills();
        protected Inventory m_inventory = new Inventory("Inventory", 8, 4);
        public List<Player.Food> m_foods = new List<Player.Food>();
        private HashSet<string> m_knownRecipes = new HashSet<string>();
        private Dictionary<string, int> m_knownStations = new Dictionary<string, int>();
        private HashSet<string> m_knownMaterial = new HashSet<string>();
        private HashSet<string> m_shownTutorials = new HashSet<string>();
        private HashSet<string> m_uniques = new HashSet<string>();
        private HashSet<string> m_trophies = new HashSet<string>();
        private HashSet<Biome> m_knownBiome = new HashSet<Biome>();
        private Dictionary<string, string> m_knownTexts = new Dictionary<string, string>();

        public float GetMaxHealth()
        {
            return this.m_maxHealth;
        }

        public void SetMaxHealth(float health, bool flashBar)
        {
            this.SetMaxHealth(health);
        }

        public void SetMaxHealth(float health)
        {
            this.m_maxHealth = health;
            if ((double)this.GetHealth() <= (double)health)
                return;
            this.SetHealth(health);
        }

        public float GetHealth()
        {
            return m_health;
        }

        public void SetHealth(float health)
        {
            if ((double)health < 0.0)
                health = 0.0f;
            m_health = health;
        }

        public float GetMaxStamina()
        {
            return this.m_maxStamina;
        }

        public void SetMaxStamina(float stamina, bool flashBar)
        {
            this.m_maxStamina = stamina;
            this.m_stamina = Mathf.Clamp(this.m_stamina, 0.0f, this.m_maxStamina);
        }

        public void SetGuardianPower(string name)
        {
            this.m_guardianPower = name;
            //this.m_guardianSE = ObjectDB.instance.GetStatusEffect(this.m_guardianPower);
        }

        public void SetBeard(string name)
        {
            this.m_beardItem = name;
            //this.SetupEquipment();
        }

        public void SetHair(string hair)
        {
            this.m_hairItem = hair;
            //this.SetupEquipment();
        }

        public void SetSkinColor(Vector3 color)
        {
            if (color == this.m_skinColor)
                return;
            this.m_skinColor = color;
            //this.m_visEquipment.SetSkinColor(this.m_skinColor);
        }

        public void SetHairColor(Vector3 color)
        {
            if (this.m_hairColor == color)
                return;
            this.m_hairColor = color;
            //this.m_visEquipment.SetHairColor(this.m_hairColor);
        }

        public void SetPlayerModel(int index)
        {
            if (this.ModelIndex == index)
                return;
            this.ModelIndex = index;
            //this.m_visEquipment.SetModel(index);
        }

        public void SetPlayerID(long playerID, string name)
        {
            m_playerId = playerID;
            m_playerName = name;
        }

        public long GetPlayerID()
        {
            return m_playerId;
        }

        public string GetPlayerName()
        {
            return m_playerName;
        }
















        public void Save(ZPackage pkg)
        {
            pkg.Write(24);
            pkg.Write(this.GetMaxHealth());
            pkg.Write(this.GetHealth());
            pkg.Write(this.GetMaxStamina());
            pkg.Write(this.m_firstSpawn);
            pkg.Write(this.m_timeSinceDeath);
            pkg.Write(this.m_guardianPower);
            pkg.Write(this.m_guardianPowerCooldown);
            this.m_inventory.Save(pkg);
            pkg.Write(this.m_knownRecipes.Count);
            foreach (string knownRecipe in this.m_knownRecipes)
                pkg.Write(knownRecipe);
            pkg.Write(this.m_knownStations.Count);
            foreach (KeyValuePair<string, int> knownStation in this.m_knownStations)
            {
                pkg.Write(knownStation.Key);
                pkg.Write(knownStation.Value);
            }
            pkg.Write(this.m_knownMaterial.Count);
            foreach (string data in this.m_knownMaterial)
                pkg.Write(data);
            pkg.Write(this.m_shownTutorials.Count);
            foreach (string shownTutorial in this.m_shownTutorials)
                pkg.Write(shownTutorial);
            pkg.Write(this.m_uniques.Count);
            foreach (string unique in this.m_uniques)
                pkg.Write(unique);
            pkg.Write(this.m_trophies.Count);
            foreach (string trophy in this.m_trophies)
                pkg.Write(trophy);
            pkg.Write(this.m_knownBiome.Count);
            foreach (Biome biome in this.m_knownBiome)
                pkg.Write((int)biome);
            pkg.Write(this.m_knownTexts.Count);
            foreach (KeyValuePair<string, string> knownText in this.m_knownTexts)
            {
                pkg.Write(knownText.Key);
                pkg.Write(knownText.Value);
            }
            pkg.Write(this.m_beardItem);
            pkg.Write(this.m_hairItem);
            pkg.Write(this.m_skinColor);
            pkg.Write(this.m_hairColor);
            pkg.Write(this.ModelIndex);
            pkg.Write(this.m_foods.Count);
            foreach (Player.Food food in this.m_foods)
            {
                pkg.Write(food.m_name);
                pkg.Write(food.m_health);
                pkg.Write(food.m_stamina);
            }
            this.Skills.Save(pkg);
        }

        public void Load(ZPackage pkg)
        {
            this.m_isLoading = true;
            //this.UnequipAllItems();
            int num1 = pkg.ReadInt();
            if (num1 >= 7)
                this.SetMaxHealth(pkg.ReadSingle(), false);
            float num2 = pkg.ReadSingle();
            float maxHealth = this.GetMaxHealth();
            if ((double)num2 <= 0.0 || (double)num2 > (double)maxHealth || float.IsNaN(num2))
                num2 = maxHealth;
            this.SetHealth(num2);
            if (num1 >= 10)
            {
                float stamina = pkg.ReadSingle();
                this.SetMaxStamina(stamina, false);
                this.m_stamina = stamina;
            }
            if (num1 >= 8)
                this.m_firstSpawn = pkg.ReadBool();
            if (num1 >= 20)
                this.m_timeSinceDeath = pkg.ReadSingle();
            if (num1 >= 23)
                this.SetGuardianPower(pkg.ReadString());
            if (num1 >= 24)
                this.m_guardianPowerCooldown = pkg.ReadSingle();
            if (num1 == 2)
                pkg.ReadZDOID();
            this.m_inventory.Load(pkg);
            int num3 = pkg.ReadInt();
            for (int index = 0; index < num3; ++index)
                this.m_knownRecipes.Add(pkg.ReadString());
            if (num1 < 15)
            {
                int num4 = pkg.ReadInt();
                for (int index = 0; index < num4; ++index)
                    pkg.ReadString();
            }
            else
            {
                int num4 = pkg.ReadInt();
                for (int index = 0; index < num4; ++index)
                    this.m_knownStations.Add(pkg.ReadString(), pkg.ReadInt());
            }
            int num5 = pkg.ReadInt();
            for (int index = 0; index < num5; ++index)
                this.m_knownMaterial.Add(pkg.ReadString());
            if (num1 < 19 || num1 >= 21)
            {
                int num4 = pkg.ReadInt();
                for (int index = 0; index < num4; ++index)
                    this.m_shownTutorials.Add(pkg.ReadString());
            }
            if (num1 >= 6)
            {
                int num4 = pkg.ReadInt();
                for (int index = 0; index < num4; ++index)
                    this.m_uniques.Add(pkg.ReadString());
            }
            if (num1 >= 9)
            {
                int num4 = pkg.ReadInt();
                for (int index = 0; index < num4; ++index)
                    this.m_trophies.Add(pkg.ReadString());
            }
            if (num1 >= 18)
            {
                int num4 = pkg.ReadInt();
                for (int index = 0; index < num4; ++index)
                    this.m_knownBiome.Add((Biome)pkg.ReadInt());
            }
            if (num1 >= 22)
            {
                int num4 = pkg.ReadInt();
                for (int index = 0; index < num4; ++index)
                    this.m_knownTexts.Add(pkg.ReadString(), pkg.ReadString());
            }
            //=========================================
            if (num1 >= 4)
            {
                string name = pkg.ReadString();
                string hair = pkg.ReadString();
                this.SetBeard(name);
                this.SetHair(hair);
            }
            if (num1 >= 5)
            {
                Vector3 color1 = pkg.ReadVector3();
                Vector3 color2 = pkg.ReadVector3();
                this.SetSkinColor(color1);
                this.SetHairColor(color2);
            }
            if (num1 >= 11)
                this.SetPlayerModel(pkg.ReadInt());
            if (num1 >= 12)
            {
                this.m_foods.Clear();
                int num4 = pkg.ReadInt();
                for (int index = 0; index < num4; ++index)
                {
                    if (num1 >= 14)
                    {
                        Player.Food food = new Player.Food();
                        food.m_name = pkg.ReadString();
                        food.m_health = pkg.ReadSingle();
                        if (num1 >= 16)
                            food.m_stamina = pkg.ReadSingle();
                        //GameObject itemPrefab = ObjectDB.instance.GetItemPrefab(food.m_name);
                        //if ((UnityEngine.Object)itemPrefab == (UnityEngine.Object)null)
                        //{
                        //    ZLog.LogWarning((object)("FAiled to find food item " + food.m_name));
                        //}
                        //else
                        {
                            //food.m_item = itemPrefab.GetComponent<ItemDrop>().m_itemData;
                            this.m_foods.Add(food);
                        }
                    }
                    else
                    {
                        pkg.ReadString();
                        double num6 = (double)pkg.ReadSingle();
                        double num7 = (double)pkg.ReadSingle();
                        double num8 = (double)pkg.ReadSingle();
                        double num9 = (double)pkg.ReadSingle();
                        double num10 = (double)pkg.ReadSingle();
                        double num11 = (double)pkg.ReadSingle();
                        if (num1 >= 13)
                        {
                            double num12 = (double)pkg.ReadSingle();
                        }
                    }
                }
            }
            if (num1 >= 17)
                this.Skills.Load(pkg);
            this.m_isLoading = false;
            //this.UpdateAvailablePiecesList();
            //this.EquipIventoryItems();
        }
    }
}
