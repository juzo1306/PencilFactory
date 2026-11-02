// Copyright (c) 2026, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using Moryx.AbstractionLayer.Activities;
using Moryx.AbstractionLayer.Processes;
using Moryx.VisualInstructions;
using PencilFactory.Products;

namespace PencilFactory.Activities.ColorizingStep;

public class ColorizingParameters : VisualInstructionParameters
{
    public PencilColor Color { get; set; }
    public GraphiteHardness Hardness { get; set; }

    protected override void Populate(Process process, Parameters instance)
    {
        base.Populate(process, instance);

        var parameters = (ColorizingParameters)instance;
        var productionProcess = (ProductionProcess)process;

        var product = (GraphitePencilType)productionProcess.ProductInstance.Type;
        parameters.Color = product.Color;
        parameters.Hardness = product.Hardness;
    }
}
