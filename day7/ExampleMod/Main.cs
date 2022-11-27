using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using Unity.Netcode;
using UnityEngine;
using Random = System.Random;

namespace ExampleMod
{
    [BepInPlugin(ModGuid, ModName, ModVersion)]
    public class Main : BaseUnityPlugin
    {
        private const string ModName = "7in7 Day 7";
        private const string ModAuthor  = "reddust9";
        private const string ModGuid = "com.reddust9.7in7.day7";
        private const string ModVersion = "1.0.0";
        public static ManualLogSource mLog;
        public static bool readyToSwitch;
        internal void Awake()
        {
            // Creating new harmony instance
            var harmony = new Harmony(ModGuid);

            // Applying patches
            harmony.PatchAll();
            Logger.LogInfo($"{ModName} successfully loaded! Made by {ModAuthor}");
            mLog = Logger;
        }
        
        [HarmonyPatch(typeof(PickUpVacuum), nameof(PickUpVacuum.FixedUpdate))]
        public class Patch1
        {
            public static void Postfix(ref PickUpVacuum __instance)
            {
                if (readyToSwitch)
                {
                    try
                    {
                        var manager = __instance.spiderWeaponManager;
                        manager.equippedWeapon.Disintegrate();
                        Random rand = new();
                        bool rareWeapon = rand.Next(5) == 0;

                        GameObject randWeapon;
                        if (SurvivalMode.instance != null)
                        {
                            randWeapon = SurvivalMode.instance.GetRandomWeapon(rareWeapon);
                        }
                        else if (VersusMode.instance != null)
                        {
                            randWeapon = VersusMode.instance.GetRandomWeapon(rareWeapon);
                        }
                        else
                        {
                            throw new Exception("Can't get random weapon");
                        }

                        Transform transform1;
                        GameObject spawnedWeapon = Instantiate(randWeapon,
                            (transform1 = __instance.transform).position, transform1.rotation);

                        mLog.LogInfo("Weapon to spawn: " + spawnedWeapon.name);

                        var component = spawnedWeapon.GetComponent<NetworkObject>();
                        component.Spawn(true);

                        mLog.LogInfo("Ran network spawn");

                        var weapon = spawnedWeapon.GetComponent<Weapon>();
                        manager.EquipWeapon(weapon);

                        mLog.LogInfo("Equipped weapon");
                    }
                    catch
                    {
                        mLog.LogError("Unable to spawn new weapon");
                    }
                    finally
                    {
                        readyToSwitch = false;
                    }
                }
            }
        }
        [HarmonyPatch(typeof(Weapon), nameof(Weapon.Use))]
        public class Patch2
        {
            public static void Postfix(bool use)
            {
                if (use)
                {
                    readyToSwitch = true;
                }
            }
        }
    }
}
