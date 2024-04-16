using HarmonyLib;
using RimWorld;
using Verse;

namespace MiningTech
{
    [HarmonyPatch(typeof(GameRules), "DesignatorAllowed")]
    internal sealed class GameRulesDesignatorAllowedPatch {
        [HarmonyPostfix]
        internal static void CheckForMiningResearch(Designator d, ref bool __result) {
            if(d is Designator_Mine && !MiningTechMod.MiningResearchSetting.Value().IsFinished)
                __result = false;
        }
    }
}
