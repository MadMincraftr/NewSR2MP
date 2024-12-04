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
    [HarmonyPatch(typeof(LandPlotLocation),nameof(LandPlotLocation.Replace))]
    public class LandPlotLocationReplace
    {
        public static void Prefix(LandPlotLocation __instance, LandPlot oldLandPlot, GameObject replacementPrefab) 
        {
            try
            {
                if ((NetworkServer.active || NetworkClient.active) && !__instance.IsHandling())
                {
                    var packet = new LandPlotMessage()
                    {
                        id = __instance.id,
                        type = replacementPrefab.GetComponent<LandPlot>().typeId,
                        messageType = LandplotUpdateType.SET
                    };

                    SRNetworkManager.NetworkSend(packet);
                }
            }
            catch { }
        }

    }
}