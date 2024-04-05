using System;
using ProjectFox.CoreEngine.Collections;
using static ProjectFox.CoreEngine.Data.Data;
using IPSLib;

namespace SuperMetroidRandomizer;

internal static class SM
{
    private static readonly ItemID[]
        torizoItems =
    {
        ItemID.Bombs,
        ItemID.ScrewAttack,
        ItemID.SpeedBooster,
    },
        maridiaItems =
    {
        ItemID.ChargeBeam,
        ItemID.IceBeam,
        ItemID.HighJump,
        ItemID.WaveBeam,
        ItemID.SpazerBeam,
        ItemID.SpringBall,
        ItemID.VariaSuit,
        ItemID.XRayScope,
        ItemID.PlasmaBeam,
        ItemID.SpaceJump,
    },
        generalItems =
    {
        ItemID.GravitySuit,
        ItemID.GrappleBeam,
    };

    private static readonly LocationAddress[]
        maridiaLocations =
    {
        LocationAddress.PlasmaBeam,
        LocationAddress.SpringBall,
        LocationAddress.SpaceJump,
    },
        generalLocations =
    {
        LocationAddress.ChargeBeam,
        LocationAddress.XRayScope,
        LocationAddress.SpazerBeam,
        LocationAddress.VariaSuit,
        LocationAddress.IceBeam,
        LocationAddress.HighJump,
        LocationAddress.GrappleBeam,
        LocationAddress.SpeedBooster,
        LocationAddress.WaveBeam,
        LocationAddress.ScrewAttack,
        LocationAddress.GravitySuit,
    };

    private static int GetSeed()
    {
        int ms = Environment.TickCount;
        DateTime now = DateTime.Now;
        return ms ^ ((now.Year * 10000) + (now.Month * 100) + now.Day);
    }

    private static void PlaceItems(IPS patch, ref string spoiler, Random r, AutoSizedArray<ItemID> items, params LocationAddress[] locations)
    {
        foreach (LocationAddress location in locations)
        {
            int i = r.Next(items.Length);
            ItemID item = items[i];
            patch.Add(false, (int)location, GetBytes((short)item, true));
            spoiler += $"{location} => {item}\n";
            items.RemoveAt(i);
        }
    }

    internal static void Generate(ref IPS ips, ref int seed, out string spoiler, bool torizoNoSpeedBooster = false)
    {
        if (seed == 0) seed = GetSeed();
        ips ??= new();
        spoiler = $"--- SMMIR Spoiler Log ---\nSeed: {seed}\n\n";
        Random r = new(seed);

        AutoSizedArray<ItemID> items = new(torizoItems);

        if (torizoNoSpeedBooster) items.Remove(ItemID.SpeedBooster);
        PlaceItems(ips, ref spoiler, r, items, LocationAddress.Bombs);
        if (torizoNoSpeedBooster) items.Add(ItemID.SpeedBooster);

        items.Add(maridiaItems);
        PlaceItems(ips, ref spoiler, r, items, maridiaLocations);

        items.Add(generalItems);
        PlaceItems(ips, ref spoiler, r, items, generalLocations);
    }
}