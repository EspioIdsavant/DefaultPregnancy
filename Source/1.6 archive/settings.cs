using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

namespace Lurklovers
{
    public class DefaultLurkLoverSettings : ModSettings
    {
        private static DefaultLurkLoverSettings _instance;

        public DefaultLurkLoverSettings()
        {
            _instance = this;
        }

        public static bool forNormal => _instance._forNormal;
        public static bool forAffair => _instance._forAffair;

        public bool _forNormal = false;
        public bool _forAffair = true;

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look(ref _forNormal, "_forNormal", false);
            Scribe_Values.Look(ref _forAffair, "_forAffair", true);

        }
    }

    public class DefaultLurkLoverMod : Mod
    {
        public static DefaultLurkLoverMod Mod;
        private DefaultLurkLoverSettings _settings;

        public DefaultLurkLoverMod(ModContentPack content) : base(content)
        {
            Mod = this;
            _settings = GetSettings<DefaultLurkLoverSettings>();
        }

        public override string SettingsCategory()
        {
            return "DefaultLurkLover_ModName".Translate();
        }

        public override void DoSettingsWindowContents(Rect inRect)
        {
            base.DoSettingsWindowContents(inRect);

            Listing_Standard settingList = new Listing_Standard();
            settingList.Begin(inRect);

            Rect labelRect = new Rect(0, settingList.CurHeight, inRect.width, 25f);
            Widgets.Label(labelRect, "DefaultLurkLover_Settings".Translate());

            settingList.Gap(25f);

            if (settingList.RadioButton("PregnancyApproach_Normal".Translate().CapitalizeFirst(), _settings._defaultApproach == PregnancyApproach.Normal))
                _settings._defaultApproach = PregnancyApproach.Normal;

            

            if (settingList.RadioButton("PregnancyApproach_AvoidPregnancy".Translate().CapitalizeFirst(), _settings._defaultApproach == PregnancyApproach.AvoidPregnancy))
                _settings._defaultApproach = PregnancyApproach.AvoidPregnancy;


            settingList.Gap(25f);

            settingList.Label("DefaultPreg_Note".Translate());

            settingList.End();

            _settings.Write();
        }
    }
}
