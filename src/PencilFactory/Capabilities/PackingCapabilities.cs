// Copyright (c) 2026, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using Moryx.AbstractionLayer.Capabilities;

namespace PencilFactory.Capabilities;

public class PackingCapabilities : CapabilitiesBase
{
    //public int Value { get; set; }

    protected override bool ProvidedBy(ICapabilities provided)
    {
        var providedPacking = provided as PackingCapabilities;
        if (providedPacking == null)
        {
            return false;
        }

        //if (providedPacking.Value < Value) // Provided must be greater or equal
        //{
        //    return false;
        //}

        return true;

    }
}
