// Copyright (c) 2026, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using Moryx.AbstractionLayer.Capabilities;
using PencilFactory.Products;

namespace PencilFactory.Capabilities;

public class ColorizingCapabilities : CapabilitiesBase
{
    /// <summary>
    /// Required color for production matching.
    /// Null means "any colorizing cell" (used by setup / color change).
    /// </summary>
    public PencilColor? Color { get; set; }

    protected override bool ProvidedBy(ICapabilities provided)
    {
        if (provided is not ColorizingCapabilities providedColorizing)
        {
            return false;
        }

        // Setup: no specific color required -> any colorizing cell matches
        if (Color == null)
        {
            return true;
        }

        return providedColorizing.Color == Color;
    }
}
