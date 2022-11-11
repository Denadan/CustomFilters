﻿#nullable disable
// ReSharper disable InconsistentNaming
using System;
using BattleTech.UI;
using Harmony;

namespace CustomFilters.MechLabScrolling.Patches;

[HarmonyBefore(Mods.BattleTechPerformanceFix)]
[HarmonyPatch(typeof(MechLabInventoryWidget), nameof(MechLabInventoryWidget.OnItemGrab))]
internal static class MechLabInventoryWidget_OnItemGrab
{
    [HarmonyPrefix]
    public static bool Prefix(MechLabInventoryWidget __instance, ref IMechLabDraggableItem item)
    {
        Logging.Trace?.Log("[LimitItems] OnItemGrab_Pre");
        try
        {
            if (MechLabFixStateTracker.GetInstance(__instance, out var mechLabFixState))
            {
                mechLabFixState.OnItemGrab(ref item);
                return false;
            }
        }
        catch (Exception e)
        {
            Logging.Error?.Log(e);
        }
        return true;
    }
}