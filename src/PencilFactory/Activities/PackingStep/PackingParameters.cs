// Copyright (c) 2026, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using Moryx.AbstractionLayer.Activities;
using Moryx.AbstractionLayer.Processes;
using Moryx.VisualInstructions;
using PencilFactory.Products;

namespace PencilFactory.Activities.PackingStep;

public class PackingParameters : VisualInstructionParameters
{
    public int PencilQuantity { get; set; }
    public string PencilName { get; set; }
    public string CartonName { get; set; }
    public CartonColor CartonColor { get; set; }
    protected override void Populate(Process process, Parameters instance)
    {
        base.Populate(process, instance);

        var parameters = (PackingParameters)instance;
        var productionProcess = (ProductionProcess)process;
        var pack = (PencilPackType)productionProcess.ProductInstance.Type;

        var pencilLink = pack.GraphitePencil;
        var cartonLink = pack.Carton;

        parameters.PencilQuantity = pencilLink?.Quantity ?? 0;
        parameters.PencilName = pencilLink?.Product?.Name ?? "(no graphite pencil linked)";
        parameters.CartonName = cartonLink?.Product?.Name ?? "(no carton linked)";
        parameters.CartonColor = cartonLink?.Product?.Color ?? default;

        parameters.Instructions =
        [
            new VisualInstruction
            {
                Type = InstructionContentType.Text,
                Content =
                    $"Pack {parameters.PencilQuantity} graphite pencil(s) of '{parameters.PencilName}' " +
                    $"into carton '{parameters.CartonName}' ({parameters.CartonColor}). " +
                    "Confirm SUCCESS when the retail pack is complete."
            }
        ];

    }
}
