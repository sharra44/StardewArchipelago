﻿using System.Linq;
using StardewArchipelago.Archipelago;
using StardewArchipelago.Serialization;
using StardewModdingAPI;
using StardewArchipelago.Stardew;

namespace StardewArchipelago.Locations.CodeInjections.Initializers
{
    public static class CodeInjectionInitializer
    {
        public static void Initialize(IMonitor monitor, IModHelper modHelper, ArchipelagoClient archipelago, BundleReader bundleReader, LocationChecker locationChecker, StardewItemManager itemManager, ArchipelagoStateDto archipelagoState)
        {
            VanillaCodeInjectionInitializer.Initialize(monitor, modHelper, archipelago, bundleReader, locationChecker, itemManager, archipelagoState);
            if (archipelago.SlotData.Mods.IsModded)
            {
                ModCodeInjectionInitializer.Initialize(monitor, modHelper, archipelago, locationChecker);
            }
        }
    }
}
