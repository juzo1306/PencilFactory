// Copyright (c) 2026, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System.Collections.Generic;
using Moryx.AbstractionLayer.Recipes;
using Moryx.Container;
using Moryx.ControlSystem.Setups;
using Moryx.Modules;
using Moryx.VisualInstructions;
using Moryx.Workplans;
using PencilFactory.Activities.ColorChangeStep;
using PencilFactory.Capabilities;
using PencilFactory.Products;

namespace PencilFactory.ControlSystem.SetupTriggers;

[ExpectedConfig(typeof(ProvideColorConfig))]
[Plugin(LifeCycle.Transient, typeof(ISetupTrigger), Name = nameof(ProvideColorTrigger))]
public class ProvideColorTrigger : SetupTriggerBase<ProvideColorConfig>
{
    public override SetupExecution Execution => SetupExecution.BeforeProduction;

    public override SetupEvaluation Evaluate(IProductRecipe recipe)
    {
        if (recipe.Product is not GraphitePencilType product)
            return false;

        return SetupEvaluation.Provide(
            new ColorizingCapabilities { Color = product.Color },
            SetupClassification.Manual);
    }

    public override IReadOnlyList<IWorkplanStep> CreateSteps(IProductRecipe recipe)
    {
        var product = (GraphitePencilType)recipe.Product;

        return
        [
            new ColorChangeTask
            {
                Parameters = new ColorChangeParameters
                {
                    Color = product.Color,
                    Instructions =
                    [
                        new VisualInstruction
                        {
                            Type = InstructionContentType.Text,
                            Content = $"Please change the colorizing cell to color '{product.Color}'."
                        }
                    ]
                }
            }
        ];
    }
}
