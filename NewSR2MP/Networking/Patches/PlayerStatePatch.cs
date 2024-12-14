﻿using HarmonyLib;

using NewSR2MP;
using NewSR2MP.Networking;
using NewSR2MP.Networking.Component;
using NewSR2MP.Networking.Packet;
using System;
using Il2CppMonomiPark.SlimeRancher.UI.Map;


namespace NewSR2MP.Networking.Patches
{
    [HarmonyPatch(typeof(PlayerState), nameof(PlayerState.AddCurrency))]
    internal class PlayerStateAddCurrency
    {
        public static void Postfix(PlayerState __instance, int adjust, PlayerState.CoinsType coinsType)
        {

            if (ClientActive() || ServerActive())
            {
                if (ServerActive() && savedGame.sharedMoney)
                {
                    return;
                }
                SetMoneyMessage message = new SetMoneyMessage()
                {
                    newMoney = __instance.GetCurrency()
                };
                MultiplayerManager.NetworkSend(message);
            }
        }
    }
    [HarmonyPatch(typeof(PlayerState), nameof(PlayerState.SpendCurrency))]
    internal class PlayerStateSpendCurrency
    {
        public static void Postfix(PlayerState __instance, int adjust)
        {

            if (ClientActive() || ServerActive())
            {
                if (ServerActive() && savedGame.sharedMoney)
                {
                    return;
                }
                SetMoneyMessage message = new SetMoneyMessage()
                {
                    newMoney = __instance.GetCurrency()
                };
                MultiplayerManager.NetworkSend(message);
            }
        }
    }
    
}
