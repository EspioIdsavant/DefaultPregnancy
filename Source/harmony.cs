using System;
using RimWorld;
using Verse;
using HarmonyLib;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Reflection;
using Steamworks;

namespace DefaultPreg
{

    [StaticConstructorOnStartup]
    public static class HarmonyPatches
    {
        private static readonly Type patchType = typeof(HarmonyPatches);

        static HarmonyPatches()
        {
            Harmony harmony = new Harmony(id: "espio.defaultpreg");
            harmony.PatchAll();

        }

        [HarmonyPatch(typeof(Pawn_RelationsTracker), "GetPregnancyApproachForPartner")]
        public static class Pawn_RelationsTracker_GetPregnancyApproachForPartner_Patch
        {
            static readonly MethodInfo m_SetPregnancyApproach = AccessTools.Method(typeof(Pawn_RelationsTracker), nameof(Pawn_RelationsTracker.SetPregnancyApproach), new[] { typeof(Pawn), typeof(PregnancyApproach) });

            static readonly MethodInfo m_GetCurrentSetting = AccessTools.Method(typeof(Pawn_RelationsTracker_GetPregnancyApproachForPartner_Patch), nameof(Pawn_RelationsTracker_GetPregnancyApproachForPartner_Patch.GetCurrentSetting));

            static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
            {

                foreach (var inst in instructions)
                {
                    if (inst.Calls(m_SetPregnancyApproach))
                    {

                        yield return new CodeInstruction(OpCodes.Pop); // Remove the default '0' setting
                        yield return new CodeInstruction(OpCodes.Call, m_GetCurrentSetting);  // Add the replacement setting
                        yield return inst;

                    }
                    else
                        yield return inst;
                }

            }

            public static int GetCurrentSetting()
            {
                Log.Message($"Default preg approach is: {DefaultPregSettings.DefaultApproach}");
                Log.Message($"Returning {(int)DefaultPregSettings.DefaultApproach}");

                return (int)DefaultPregSettings.DefaultApproach;
            }
        }
    }

 }

 





