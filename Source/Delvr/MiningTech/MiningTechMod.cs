using Delvr.Farseek;
using HugsLib;
using Verse;

namespace Delvr.MiningTech
{
    public class MiningTechMod : ModBase
    {
        public override string ModIdentifier => "MiningTech";

        public static DefListSetting<ResearchProjectDef> MiningResearchSetting { get; set; }

        public override void DefsLoaded() => MiningResearchSetting = new DefListSetting<ResearchProjectDef>(Settings,
            "MiningResearch", "MiningResearchSettingTitle".Translate(), "MiningResearchSettingDesc".Translate(), "Machining");
    }
}
