﻿using StardewValley;

namespace StardewArchipelago.Locations
{
    internal class ModPersistence
    {
        public int GetAsInt(string key)
        {
            return int.Parse(this[key]);
        }

        public string this[string key]
        {
            get
            {
                var modData = Game1.getFarm().modData;
                return modData[key];
            }
        }

        public void InitializeModDataValue(string key, string defaultValue)
        {
            var modData = Game1.getFarm().modData;
            if (!modData.ContainsKey(key))
            {
                modData.Add(key, defaultValue);
            }
        }

        public void SetToOneModDataValue(string key)
        {
            SetModDataValue(key, "1");
        }

        public void IncrementModDataValue(string key, int increment = 1)
        {
            var modData = Game1.getFarm().modData;
            modData[key] = (int.Parse(modData[key]) + increment).ToString();
        }

        public void SetModDataValue(string key, string value)
        {
            var modData = Game1.getFarm().modData;
            modData[key] = value;
        }
    }
}
