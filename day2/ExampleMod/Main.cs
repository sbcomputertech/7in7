using BepInEx;
using BepInEx.Logging;
using HarmonyLib;

namespace SevenSeven.Day2
{
    [BepInPlugin(ModGuid, ModName, ModVersion)]
    public class Main : BaseUnityPlugin
    {
        private const string ModName = "7in7 Day 2";
        private const string ModAuthor  = "reddust9";
        private const string ModGuid = "com.reddust9.7in7.day2";
        private const string ModVersion = "1.0.0";
        public static ManualLogSource logger;
        internal void Awake()
        {
            // Creating new harmony instance
            var harmony = new Harmony(ModGuid);

            // Applying patches
            harmony.PatchAll();
            Logger.LogInfo($"{ModName} successfully loaded! Made by {ModAuthor}");
            logger = Logger;
        }

        [HarmonyPatch(typeof(SelectionHelper), nameof(SelectionHelper.PickRandomWeapon))]
        public class P_1
        {
            public static bool Prefix(List<SpawnableWeapon> weapons, ref SpawnableWeapon __result)
            {
                foreach (var wep in weapons)
                {
                    if (wep.weaponObject.name is not "BigGrenade") continue;
                    __result = wep;
                    return false;
                }

                return true;
            }
        }
    }
}
