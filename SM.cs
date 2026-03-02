using System;
using ProjectFox.CoreEngine.Collections;
using ProjectFox.CoreEngine.Data;
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
        springBallItems =
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
    },
        maridiaItems =
    {
        ItemID.SpaceJump,
    },
        generalItems =
    {
        ItemID.GravitySuit,
        ItemID.GrappleBeam,
    };

    private static readonly LocationAddress[]//move to arg fields?
        maridiaLocations =
    {
        LocationAddress.PlasmaBeam,
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
        LocationAddress.GravitySuit,
    };

    private static Patch[] ConvertTableToPatches(LookupTable<LocationAddress, ItemID> table)
    {
        Patch[] patches = new Patch[table.Length];

        LocationAddress[] locations = table.GetCodes();
        ItemID[] items = table.GetValues();
        for (int i = 0; i < locations.Length; i++)
            patches[i] = new((int)locations[i], Data.GetBytes((short)items[i], true));

        return patches;
    }

    private static int GetSeed()
    {
        int ms = Environment.TickCount;
        DateTime now = DateTime.Now;
        return ms ^ ((now.Year * 10000) + (now.Month * 100) + now.Day);
    }

    private static void PlaceItems(LookupTable<LocationAddress, ItemID> itemTable, ref string spoiler, Random r, AutoSizedArray<ItemID> itemsToPlace, params LocationAddress[] locations)
    {
        foreach (LocationAddress location in locations)
        {
            int i = r.Next(itemsToPlace.Length);
            ItemID item = itemsToPlace[i];
            itemTable.Add(location, item);
            spoiler += $"{location} => {item}\n";
            itemsToPlace.RemoveAt(i);
        }
    }

    internal static void Generate(ref IPSOld ips, ref int seed, out string spoiler, bool torizoNoSpeedBooster = false)
    {
        if (seed == 0) seed = GetSeed();
        ips ??= new();
        spoiler = $"--- SMMIR Spoiler Log ---\nSeed: {seed}\n\n";
        Random r = new(seed);

        AutoSizedArray<ItemID> itemsToPlace = new(torizoItems);
        LookupTable<LocationAddress, ItemID> itemTable = new();
        if (torizoNoSpeedBooster) itemsToPlace.Remove(ItemID.SpeedBooster);
        PlaceItems(itemTable, ref spoiler, r, itemsToPlace, LocationAddress.Bombs);
        if (torizoNoSpeedBooster) itemsToPlace.Add(ItemID.SpeedBooster);

        itemsToPlace.Add(springBallItems);
        PlaceItems(itemTable, ref spoiler, r, itemsToPlace, LocationAddress.SpringBall);

        itemsToPlace.Add(ItemID.GrappleBeam);
        PlaceItems(itemTable, ref spoiler, r, itemsToPlace, LocationAddress.ScrewAttack);
        itemsToPlace.Remove(ItemID.GrappleBeam);

        itemsToPlace.Add(maridiaItems);
        PlaceItems(itemTable, ref spoiler, r, itemsToPlace, maridiaLocations);

        itemsToPlace.Add(generalItems);
        PlaceItems(itemTable, ref spoiler, r, itemsToPlace, generalLocations);

        //use table.join() to make spoiler?

        LocationAddress[] locations = itemTable.GetCodes();
        ItemID[] items = itemTable.GetValues();
        for (int i = 0; i < locations.Length; i++)
            ips.Add(false, (int)locations[i], Data.GetBytes((short)items[i], true));
    }
}