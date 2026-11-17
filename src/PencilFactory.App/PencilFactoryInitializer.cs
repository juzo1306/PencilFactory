// Copyright (c) 2026, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using Moryx.AbstractionLayer.Resources;
using Moryx.Container;
using Moryx.Factory;
using Moryx.Resources.VisualInstructions;
using PencilFactory.Products;
using PencilFactory.Resources.Assembling;
using PencilFactory.Resources.Colorizing;
using PencilFactory.Resources.Packing;
using PencilFactory.Resources.Testing;

namespace PencilFactory.App;

[Plugin(LifeCycle.Transient, typeof(IResourceInitializer), Name = nameof(PencilFactoryInitializer))]
[Display(Name = "Pencil Factory Initializer", Description = "Creates cells, visual instructor and factory layout.")]
public class PencilFactoryInitializer : ResourceInitializerBase
{
    public override string Name => nameof(PencilFactoryInitializer);
    public override string Description => "Setup the simulated pencil factory resources";

    // Add ExecuteAsync and Place below
    public override Task<ResourceInitializerResult> ExecuteAsync(IResourceGraph graph, object parameters, CancellationToken cancellationToken)
    {
        var instructor = graph.Instantiate<VisualInstructor>();
        instructor.Name = "Visual Instructor";
        instructor.Description = "Shared display for assembling, colorizing, color change and packing";

        var lineGroup = graph.Instantiate<MachineGroup>();
        lineGroup.Name = "Pencil Line";
        lineGroup.Description = "Assembling, colorizing, testing and packing";

        // Manual: VisualInstructor
        var assemblingCell = graph.Instantiate<AssemblingCell>();
        assemblingCell.Name = "Assembling Cell";
        assemblingCell.VisualInstructor = instructor;
        assemblingCell.Value = 0;
        lineGroup.Children.Add(Place(graph, "A-1", "Assembling station", "scale", 0.18, 0.32, assemblingCell));

        // Manual + Automatic: VisualInstructor + Simulated Driver
        var colorizingDriver = graph.Instantiate<SimulatedColorizingDriver>();
        colorizingDriver.Name = "Colorizing Simulated Driver";

        var colorizingCell = graph.Instantiate<ColorizingCell>();
        colorizingCell.Name = "Colorizing Cell";
        colorizingCell.VisualInstructor = instructor;
        colorizingCell.Driver = colorizingDriver;
        colorizingCell.Color = PencilColor.Green;
        lineGroup.Children.Add(Place(graph, "C-1", "Colorizing station", "science", 0.42, 0.32, colorizingCell));

        // Automatic: Simulated Driver
        var testingDriver = graph.Instantiate<TestingSimulatedInOutDriver>();
        testingDriver.Name = "Testing Simulated Driver";

        var testingCell = graph.Instantiate<TestingCell>();
        testingCell.Name = "Testing Cell";
        testingCell.Driver = testingDriver;
        testingCell.Value = 0;
        lineGroup.Children.Add(Place(graph, "T-1", "Testing station", "precision_manufacturing", 0.66, 0.32, testingCell));

        // Manual: VisualInstructor
        var packingCell = graph.Instantiate<PackingCell>();
        packingCell.Name = "Packing Cell";
        packingCell.VisualInstructor = instructor;
        lineGroup.Children.Add(Place(graph, "P-1", "Packing workplace", "inventory_2", 0.42, 0.68, packingCell));

        var factory = graph.Instantiate<ManufacturingFactory>();
        factory.Name = "Pencil Manufactory";
        factory.Description = "Simulated graphite pencil production and retail packing";
        factory.Children.Add(instructor);
        factory.Children.Add(lineGroup);
        factory.Children.Add(colorizingDriver);
        factory.Children.Add(testingDriver);

        return Task.FromResult(new ResourceInitializerResult
        {
            InitializedResources = [factory],
            Saved = false
        });
    }

    private static MachineLocation Place(IResourceGraph graph, string name, string description, string icon, double x, double y, Resource cell)
    {
        var location = graph.Instantiate<MachineLocation>();
        location.Name = name;
        location.Description = description;
        location.SpecificIcon = icon;
        location.PositionX = x;
        location.PositionY = y;
        location.Children.Add(cell);
        return location;
    }

}
