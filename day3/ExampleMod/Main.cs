using BepInEx;
using HarmonyLib;
using UnityEngine.UI;

namespace ExampleMod
{
    [BepInPlugin(ModGuid, ModName, ModVersion)]
    public class Main : BaseUnityPlugin
    {
        private const string ModName = "7in7 Day 3";
        private const string ModAuthor  = "reddust9";
        private const string ModGuid = "com.reddust9.7in7.day3";
        private const string ModVersion = "1.0.0";
        private static FPSCounter Counter;
        internal void Awake()
        {
            // Creating new harmony instance
            var harmony = new Harmony(ModGuid);

            // Applying patches
            harmony.PatchAll();
            Logger.LogInfo($"{ModName} successfully loaded! Made by {ModAuthor}");
        }

        internal void Update()
        {
            var fps = Math.Ceiling(Fps.fps).ToString();
            var text = Counter.GetComponent<Text>();
            text.text = "FPS: " + fps;
        }

        [HarmonyPatch(typeof(FPSCounter), "Awake")]
        public class Patch1
        {
            public static void Postfix(ref FPSCounter __instance)
            {
                __instance.gameObject.SetActive(true);
                __instance.gameObject.AddComponent<Fps>();
                Counter = __instance;
            }
        }
    }
}
