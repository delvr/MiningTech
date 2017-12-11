using Harmony;
using RimWorld;
using Verse;

namespace Delvr.MiningTech
{
    [HarmonyPatch(typeof(GameRules), "DesignatorAllowed")]
    public static class GameRules_DesignatorAllowed_Patch
    {

        [HarmonyPostfix]
        public static void CheckForMiningResearch(GameRules __instance, Designator d, ref bool __result)
        {
            if (d is Designator_Mine && !MiningTechMod.MiningResearchSetting.Value().IsFinished) __result = false;
        }
    }
}
