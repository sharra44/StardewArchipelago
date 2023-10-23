﻿using System.Collections.Generic;
using HarmonyLib;
using StardewArchipelago.Archipelago;
using StardewArchipelago.Locations.CodeInjections.Initializers;
using StardewArchipelago.Locations.CodeInjections.Vanilla.MonsterSlayer;
using StardewArchipelago.Serialization;
using StardewArchipelago.Stardew;
using StardewModdingAPI;

namespace StardewArchipelago.Locations.Patcher
{
    public class LocationPatcher
    {
        private List<ILocationPatcher> _patchers;

        public LocationPatcher(IMonitor monitor, IModHelper modHelper, Harmony harmony, ArchipelagoClient archipelago, ArchipelagoStateDto state, LocationChecker locationChecker, BundleReader bundleReader, StardewItemManager itemManager, WeaponsManager weaponsManager)
        {
            CodeInjectionInitializer.Initialize(monitor, modHelper, archipelago, state, bundleReader, locationChecker, itemManager, weaponsManager);
            _patchers = new List<ILocationPatcher>();
            _patchers.Add(new VanillaLocationPatcher(monitor, modHelper, harmony, archipelago, locationChecker));
            if (archipelago.SlotData.Mods.IsModded)
            {
                _patchers.Add(new ModLocationPatcher(harmony, modHelper, archipelago));
            }
        }

        public void ReplaceAllLocationsRewardsWithChecks()
        {
            foreach (var patcher in _patchers)
            {
                patcher.ReplaceAllLocationsRewardsWithChecks();
            }
        }

    }
}
