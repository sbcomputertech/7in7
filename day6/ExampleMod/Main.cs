using BepInEx;
using HarmonyLib;
using UnityEngine;

namespace ExampleMod
{
    [BepInPlugin(ModGuid, ModName, ModVersion)]
    public class Main : BaseUnityPlugin
    {
        private const string ModName = "7in7 Day 6";
        private const string ModAuthor  = "reddust9";
        private const string ModGuid = "com.reddust9.7in7.day";
        private const string ModVersion = "1.0.0";
        internal void Awake()
        {
            // Creating new harmony instance
            var harmony = new Harmony(ModGuid);

            // Applying patches
            harmony.PatchAll();
            Logger.LogInfo($"{ModName} successfully loaded! Made by {ModAuthor}");
        }
        [HarmonyPatch("SeasonChecker", "IsItHalloween")]
        public class P_1
        {
            public static bool Prefix(ref bool __result)
            {
                __result = true;
                return false;
            }
        }
    }
}
