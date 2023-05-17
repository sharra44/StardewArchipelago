﻿using HarmonyLib;
using StardewArchipelago.Archipelago;
using StardewArchipelago.Constants;
using StardewArchipelago.Locations.CodeInjections.Modded;
using StardewArchipelago.Locations.CodeInjections.Vanilla;
using StardewArchipelago.Locations.GingerIsland;

namespace StardewArchipelago.Locations.Patcher
{
    public class ModLocationPatcher : ILocationPatcher
    {
        private readonly ArchipelagoClient _archipelago;
        private readonly Harmony _harmony;
        private readonly GingerIslandPatcher _gingerIslandPatcher;

        public ModLocationPatcher(Harmony harmony, ArchipelagoClient archipelago)
        {
            _archipelago = archipelago;
            _harmony = harmony;
        }

        public void ReplaceAllLocationsRewardsWithChecks()
        {
            AddModSkillInjections();
            AddDeepWoodsModInjections();
        }

        private void AddModSkillInjections()
        {
            if (_archipelago.SlotData.Mods.HasMod(ModNames.LUCK) || _archipelago.SlotData.Mods.HasMod(ModNames.BINNING)
             || _archipelago.SlotData.Mods.HasMod(ModNames.COOKING) || _archipelago.SlotData.Mods.HasMod(ModNames.MAGIC)
             || _archipelago.SlotData.Mods.HasMod(ModNames.SOCIALIZING) || _archipelago.SlotData.Mods.HasMod(ModNames.ARCHAEOLOGY))
            {
                var _spaceCoreType = AccessTools.TypeByName("SpaceCore.Skills");
                _harmony.Patch(
                    original: AccessTools.Method(_spaceCoreType, "AddExperience"),
                    prefix: new HarmonyMethod(typeof(SkillInjections), nameof(SkillInjections.AddExperience_ArchipelagoModExperience_Prefix))
                );
            }
        }

        private void AddDeepWoodsModInjections()
        {
            if (_archipelago.SlotData.Mods.HasMod(ModNames.DEEP_WOODS))
            {
                var _deepWoodsType = AccessTools.TypeByName("DeepWoodsMod.DeepWoods");
                var _unicornType = AccessTools.TypeByName("DeepWoodsMod.Unicorn");
                var _gingerbreadType = AccessTools.TypeByName("DeepWoodsMod.GingerBreadHouse");
                var _iridiumtreeType = AccessTools.TypeByName("DeepWoodsMod.IridiumTree");
                var _treasureType = AccessTools.TypeByName("DeepWoodsMod.TreasureChest");
                var _fountainType = AccessTools.TypeByName("DeepWoodsMod.HealingFountain");

                /*_harmony.Patch(
                    original: AccessTools.Method(_deepWoodsType, "DetermineExits"),
                    postfix: new HarmonyMethod(typeof(DeepWoodsModInjections), nameof(DeepWoodsModInjections.DetermineExits_ChangeFromLevelHook_Postfix))
                );*/
                _harmony.Patch(
                    original: AccessTools.Method(_unicornType, "checkAction"),
                    prefix: new HarmonyMethod(typeof(DeepWoodsModInjections), nameof(DeepWoodsModInjections.CheckAction_PetUnicornLocation_Prefix))
                );
                _harmony.Patch(
                    original: AccessTools.Method(_unicornType, "CheckScared"),
                    prefix: new HarmonyMethod(typeof(DeepWoodsModInjections), nameof(DeepWoodsModInjections.CheckScared_MakeUnicornLessScared_Prefix))
                );
                _harmony.Patch(
                    original: AccessTools.Method(_treasureType, "checkForAction"),
                    postfix: new HarmonyMethod(typeof(DeepWoodsModInjections), nameof(DeepWoodsModInjections.CheckForAction_TreasureChestLocation_Postfix))
                );
                _harmony.Patch(
                    original: AccessTools.Method(_gingerbreadType, "PlayDestroyedSounds"),
                    postfix: new HarmonyMethod(typeof(DeepWoodsModInjections), nameof(DeepWoodsModInjections.PlayDestroyedSounds_GingerbreadLocation_Postfix))
                );
                _harmony.Patch(
                    original: AccessTools.Method(_iridiumtreeType, "PlayDestroyedSounds"),
                    postfix: new HarmonyMethod(typeof(DeepWoodsModInjections), nameof(DeepWoodsModInjections.PlayDestroyedSounds_IridiumTreeLocation_Postfix))
                );
                _harmony.Patch(
                    original: AccessTools.Method(_fountainType, "performUseAction"),
                    prefix: new HarmonyMethod(typeof(DeepWoodsModInjections), nameof(DeepWoodsModInjections.PerformUseAction_HealingFountainLocation_Prefix))
                );
            }
        }

    }
}
