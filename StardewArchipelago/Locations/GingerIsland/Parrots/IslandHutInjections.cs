﻿using System;
using Microsoft.Xna.Framework;
using StardewArchipelago.Archipelago;
using StardewModdingAPI;
using StardewValley;
using StardewValley.BellsAndWhistles;
using StardewValley.Locations;

namespace StardewArchipelago.Locations.GingerIsland.Parrots
{
    public class IslandHutInjections : IParrotReplacer
    {
        private const string AP_LEO_PARROT = "Leo's Parrot";

        private static IMonitor _monitor;
        private static IModHelper _modHelper;
        private static ArchipelagoClient _archipelago;
        private static LocationChecker _locationChecker;

        private IslandLocation _islandLocation;

        public static void Initialize(IMonitor monitor, IModHelper modHelper, ArchipelagoClient archipelago, LocationChecker locationChecker)
        {
            _monitor = monitor;
            _modHelper = modHelper;
            _archipelago = archipelago;
            _locationChecker = locationChecker;
        }

        public IslandHutInjections()
        {
            _islandLocation = (IslandHut)Game1.getLocationFromName("IslandHut");
        }
        
        public void ReplaceParrots()
        {
            _islandLocation.parrotUpgradePerches.Clear();
            AddLeoParrot(_islandLocation);
        }

        private static void AddLeoParrot(IslandLocation islandLocation)
        {
            islandLocation.parrotUpgradePerches.Add(new ParrotUpgradePerch(islandLocation, new Point(7, 6), new Microsoft.Xna.Framework.Rectangle(-1000, -1000, 1, 1), 1, BefriendLeoParrot, IsLeoParrotBefriended, "Hut"));
        }

        private static void BefriendLeoParrot()
        {
            _locationChecker.AddCheckedLocation(AP_LEO_PARROT);
        }

        private static bool IsLeoParrotBefriended()
        {
            return _locationChecker.IsLocationChecked(AP_LEO_PARROT);
        }
    }
}
