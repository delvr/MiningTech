using System.Diagnostics.CodeAnalysis;
using HarmonyLib;
using RimWorld;
using Verse;

namespace MiningTech
{
    [SuppressMessage("ReSharper", "UnusedType.Global")]
    [HarmonyPatch(typeof(GameRules), "DesignatorAllowed")]
    public static class GameRulesDesignatorAllowedPatch
    {
        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        [SuppressMessage("ReSharper", "UnusedParameter.Global")]
        [SuppressMessage("ReSharper", "InconsistentNaming")]
        [HarmonyPostfix]
        public static void CheckForMiningResearch(GameRules __instance, Designator d, ref bool __result) {
            if(d is Designator_Mine && !MiningTechMod.MiningResearchSetting.Value().IsFinished) __result = false;
        }
    }
}
