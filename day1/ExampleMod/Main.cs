using BepInEx;
using BepInEx.Logging;
using HarmonyLib;

namespace SevenSeven.Day1
{
    [BepInPlugin(ModGuid, ModName, ModVersion)]
    public class Main : BaseUnityPlugin
    {
        private const string ModName = "7in7 Day 1";
        private const string ModAuthor  = "reddust9";
        private const string ModGuid = "com.reddust9.7in7.day1";
        private const string ModVersion = "1.0.0";
        private static ManualLogSource? Log;
        internal void Awake()
        {
            // Creating new harmony instance
            var harmony = new Harmony(ModGuid);

            // Applying patches
            harmony.PatchAll();
            Logger.LogInfo($"{ModName} successfully loaded! Made by {ModAuthor}");
            Log = Logger;
        }
        [HarmonyPatch(typeof(Weapon), "FixedUpdate")]
        internal class Patch__Weapon__FixedUpdate
        {
            public static void Postfix(ref Weapon __instance) 
            {
                Log?.LogInfo($"RRM: {__instance._recoilReductionMod}");
                __instance._recoilReductionMod = 0.2f; // lower the value, higher the base weapon recoil // 1 is vanilla normal
            }
        }
    }
}
