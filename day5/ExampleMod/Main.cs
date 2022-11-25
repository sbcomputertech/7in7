using BepInEx;
using HarmonyLib;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ExampleMod
{
    [BepInPlugin(ModGuid, ModName, ModVersion)]
    public class Main : BaseUnityPlugin
    {
        private const string ModName = "7in7 Day 5";
        private const string ModAuthor  = "reddust9";
        private const string ModGuid = "com.reddust9.7in7.day5";
        private const string ModVersion = "1.0.0";

        private bool slow = false;
        private bool speed = false;
        private float baseTS = 0;
        private float value = 1.75f;
        internal void Awake()
        {
            // Creating new harmony instance
            var harmony = new Harmony(ModGuid);

            // Applying patches
            harmony.PatchAll();
            Logger.LogInfo($"{ModName} successfully loaded! Made by {ModAuthor}");

            baseTS = Time.timeScale;
        }

        internal void Update()
        {
            if (Keyboard.current.rKey.wasPressedThisFrame)
            {
                SlowDown();
            } else if (Keyboard.current.tKey.wasPressedThisFrame)
            {
                ResetSpeed();
            } else if (Keyboard.current.yKey.wasPressedThisFrame)
            {
                SpeedUp();
            }
            
            TSUpdate();
        }

        internal void SpeedUp()
        {
            slow = false;
            speed = true;
        }

        internal void SlowDown()
        {
            speed = false;
            slow = true;
        }

        internal void ResetSpeed()
        {
            speed = false;
            slow = false;
        }

        internal void TSUpdate()
        {
            if (speed)
            {
                Time.timeScale = baseTS * value;
            }

            if (slow)
            {
                Time.timeScale = baseTS / value;
            }

            if (!slow && !speed)
            {
                Time.timeScale = baseTS;
            }
        }
    }
}
