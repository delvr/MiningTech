﻿using System.Reflection;
using HugsLib;
using Verse;

[assembly: AssemblyTitle("Mining Tech")]
[assembly: AssemblyProduct("MiningTech")]
[assembly: AssemblyVersion("1.5.0.0")]

namespace MiningTech
{
    public class MiningTechMod: ModBase {
		public static DefListSetting<ResearchProjectDef> MiningResearchSetting { get; private set; }

        public override string ModIdentifier => "MiningTech";

        public override void DefsLoaded() => MiningResearchSetting = new DefListSetting<ResearchProjectDef>(Settings,
            "MiningResearch", "MiningResearchSettingTitle".Translate(), "MiningResearchSettingDesc".Translate(), "Machining");
    }
}
