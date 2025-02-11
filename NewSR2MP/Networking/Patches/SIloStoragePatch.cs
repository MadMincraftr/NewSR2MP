﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;

using NewSR2MP.Networking.Component;
using UnityEngine;
namespace NewSR2MP.Networking.Patches
{
    [HarmonyPatch(typeof(SiloStorage), nameof(SiloStorage.InitAmmo))]
    public class SiloStorageInitAmmo
    {
        public static bool Prefix(SiloStorage __instance)
        {
            try
            {
                var aid = __instance.transform.GetComponentInParent<LandPlotLocation>()._id;

                if (NetworkAmmo.all.ContainsKey(aid))
                {
                    aid += "-1";
                }
                if (NetworkAmmo.all.ContainsKey(aid))
                {
                    aid += "-2";
                }
                if (NetworkAmmo.all.ContainsKey(aid))
                {
                    aid += "-3";
                }
                if (NetworkAmmo.all.ContainsKey(aid))
                {
                    aid += "-4";
                }
                if (NetworkAmmo.all.ContainsKey(aid))
                {
                    aid += "-5";
                } // idk why this happens, i only ever indended for it to go to 1.
                if (__instance.Ammo == null)
                {
                    __instance.Ammo = new NetworkAmmo(aid, __instance.AmmoSlotDefinitions);
                }
                else if (!(__instance.Ammo is NetworkAmmo))
                {
                    __instance.Ammo = new NetworkAmmo(aid,  __instance.AmmoSlotDefinitions);
                }
                return false;
            }
            catch (Exception e)
            {
                SRMP.Error($"Error in network ammo!\n{e}\nThis can cause major desync!");
            }
            return true;
        }
    }
}
