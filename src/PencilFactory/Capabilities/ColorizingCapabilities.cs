// Copyright (c) 2026, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using Moryx.AbstractionLayer.Capabilities;
using PencilFactory.Products;

namespace PencilFactory.Capabilities;

public class ColorizingCapabilities : CapabilitiesBase
{
    public PencilColor Color { get; set; }
    public int Value { get; set; }

    protected override bool ProvidedBy(ICapabilities provided)
    {
        var providedColorizing = provided as ColorizingCapabilities;
        if (providedColorizing == null)
        {
            return false;
        }

        if (providedColorizing.Value < Value && providedColorizing.Color != Color) // Provided must be greater or equal
        {
            return false;
        }
        return true;

    }
}
