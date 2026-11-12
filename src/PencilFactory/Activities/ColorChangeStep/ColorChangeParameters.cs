// Copyright (c) 2026, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using Moryx.AbstractionLayer.Activities;
using Moryx.AbstractionLayer.Processes;
using Moryx.VisualInstructions;
using PencilFactory.Products;

namespace PencilFactory.Activities.ColorChangeStep;

public class ColorChangeParameters : VisualInstructionParameters
{
    public PencilColor Color { get; set; }

    protected override void Populate(Process process, Parameters instance)
    {
        base.Populate(process, instance);
        var parameters = (ColorChangeParameters)instance;
        parameters.Color = Color;
    }
}
