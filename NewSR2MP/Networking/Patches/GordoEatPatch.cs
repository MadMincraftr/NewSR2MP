﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using Mirror;
using Il2CppMonomiPark.SlimeRancher.DataModel;
using NewSR2MP.Networking.Component;
using NewSR2MP.Networking.Packet;
using UnityEngine;
namespace NewSR2MP.Networking.Patches
{
    [HarmonyPatch(typeof(GordoEat), nameof(GordoEat.DoEat))]
    public class GordoDoEat
    {
        public static void Postfix(GordoEat __instance, GameObject obj)
        {
            try
            {
                if ((NetworkServer.active || NetworkClient.active) && !__instance.GetComponent<HandledDummy>())
                {
                    var packet = new GordoEatMessage()
                    {
                        id = __instance._id,
                        count = __instance.GordoModel.gordoEatCount
                    };

                    SRNetworkManager.NetworkSend(packet);
                }
            }
            catch { }
        }

    }
    [HarmonyPatch(typeof(GordoEat), nameof(GordoEat.ImmediateReachedTarget))]
    public class GordoEatImmediateReachedTarget
    {
        public static void Postfix(GordoEat __instance)
        {
            try
            {
                if ((NetworkServer.active || NetworkClient.active) && !__instance.GetComponent<HandledDummy>())
                {
                    var packet = new GordoBurstMessage()
                    {
                        id = __instance._id
                    };

                    SRNetworkManager.NetworkSend(packet);
                }
            }
            catch { }
        }

    }
}
