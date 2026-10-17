using RimWorld;
using UnityEngine;
using Verse;

namespace DefaultPreg
{
    public class DefaultPregSettings : ModSettings
    {
        private static DefaultPregSettings _instance;

        public DefaultPregSettings()
        {
            _instance = this;
        }

        public static PregnancyApproach DefaultApproach => _instance._defaultApproach;

        public PregnancyApproach _defaultApproach = PregnancyApproach.Normal;

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look(ref _defaultApproach, "_defaultApproach", PregnancyApproach.Normal);
        }
    }

    public class DefaultPregMod : Mod
    {
        public static DefaultPregMod Mod;
        private DefaultPregSettings _settings;

        public DefaultPregMod(ModContentPack content) : base(content)
        {
            Mod = this;
            _settings = GetSettings<DefaultPregSettings>();
        }

        public override string SettingsCategory()
        {
            return "DefaultPreg_ModName".Translate();
        }

        public override void DoSettingsWindowContents(Rect inRect)
        {
            base.DoSettingsWindowContents(inRect);

            Listing_Standard settingList = new Listing_Standard();
            settingList.Begin(inRect);

            Rect labelRect = new Rect(0, settingList.CurHeight, inRect.width, 25f);
            Widgets.Label(labelRect, "DefaultPreg_Settings".Translate());

            settingList.Gap(25f);

            if (settingList.RadioButton("PregnancyApproach_Normal".Translate().CapitalizeFirst(), _settings._defaultApproach == PregnancyApproach.Normal))
                _settings._defaultApproach = PregnancyApproach.Normal;

            if (settingList.RadioButton("PregnancyApproach_AvoidPregnancy".Translate().CapitalizeFirst(), _settings._defaultApproach == PregnancyApproach.AvoidPregnancy))
                _settings._defaultApproach = PregnancyApproach.AvoidPregnancy;

            if (settingList.RadioButton("PregnancyApproach_TryForBaby".Translate().CapitalizeFirst(), _settings._defaultApproach == PregnancyApproach.TryForBaby))
                _settings._defaultApproach = PregnancyApproach.TryForBaby;

            settingList.Gap(25f);

            settingList.Label("DefaultPreg_Note".Translate());

            settingList.End();

            _settings.Write();
        }
    }
}