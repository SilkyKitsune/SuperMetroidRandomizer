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

    private static void PlaceItems(LookupTable<LocationAddress, ItemID> itemTable, AutoSizedArray<ItemID> itemsToPlace, ref string spoiler, Random r, LocationAddress[] locations, ItemID[] excludedItems = null)
    {
        bool moveExcludedItems = excludedItems != null;
        AutoSizedArray<ItemID> excludedItemsBuffer = null;

        if (moveExcludedItems)
        {
            itemsToPlace.DeepCopy(out excludedItemsBuffer);

            foreach (ItemID excludedItem in excludedItems) itemsToPlace.Remove(excludedItem);

            foreach (ItemID item in itemsToPlace.ToArray()) excludedItemsBuffer.Remove(item);
        }

        foreach (LocationAddress location in locations)
        {
            int n = r.Next(itemsToPlace.Length);
            ItemID item = itemsToPlace[n];
            itemTable.Add(location, item);
            spoiler += $"{location} => {item}\n";
            itemsToPlace.RemoveAt(n);
        }

        if (moveExcludedItems) itemsToPlace.Add(excludedItemsBuffer.ToArray());
    }

    private static Patch[] VanillaLogic(ref string spoiler, Random r, bool torizoNoSpeedBooster = false)
    {
        r ??= new(GetSeed());

        LookupTable<LocationAddress, ItemID> itemTable = new();
        AutoSizedArray<ItemID> itemsToPlace = new(torizoItems);

        if (torizoNoSpeedBooster) itemsToPlace.Remove(ItemID.SpeedBooster);
        PlaceItems(itemTable, itemsToPlace, ref spoiler, r, new LocationAddress[] { LocationAddress.Bombs });
        if (torizoNoSpeedBooster) itemsToPlace.Add(ItemID.SpeedBooster);

        itemsToPlace.Add(springBallItems);
        PlaceItems(itemTable, itemsToPlace, ref spoiler, r, new LocationAddress[] { LocationAddress.SpringBall });

        itemsToPlace.Add(ItemID.GrappleBeam);
        PlaceItems(itemTable, itemsToPlace, ref spoiler, r, new LocationAddress[] { LocationAddress.ScrewAttack });
        itemsToPlace.Remove(ItemID.GrappleBeam);

        itemsToPlace.Add(maridiaItems);
        PlaceItems(itemTable, itemsToPlace, ref spoiler, r, maridiaLocations);

        itemsToPlace.Add(generalItems);
        PlaceItems(itemTable, itemsToPlace, ref spoiler, r, generalLocations);

        return ConvertTableToPatches(itemTable);
    }

    internal static void Generate(ref IPS ips, ref int seed, out string spoiler, bool torizoNoSpeedBooster = false)
    {
        if (seed == 0) seed = GetSeed();
        ips ??= new();
        spoiler = $"--- SMMIR Spoiler Log ---\nSeed: {seed}\n\n";
        Random r = new(seed);

        ips.Add(MergeMode.None, VanillaLogic(ref spoiler, r, torizoNoSpeedBooster));
    }
}