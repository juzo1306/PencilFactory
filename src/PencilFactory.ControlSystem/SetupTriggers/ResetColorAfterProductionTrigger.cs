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

/// <summary>
/// After the production job finished: reset the colorizing cell to the default color (cleanup)
/// Runs once per production job (e.g. after all 20 pencils), not after each single piece
/// </summary>
[ExpectedConfig(typeof(ResetColorAfterProductionConfig))]
[Plugin(LifeCycle.Transient, typeof(ISetupTrigger), Name = nameof(ResetColorAfterProductionTrigger))]
public class ResetColorAfterProductionTrigger : SetupTriggerBase<ResetColorAfterProductionConfig>
{
    public override SetupExecution Execution => SetupExecution.AfterProduction;

    public override SetupEvaluation Evaluate(IProductRecipe recipe)
    {
        if (recipe.Product is not GraphitePencilType product)
            return false;

        // Product already uses the default color, nothing to reset
        if (product.Color == Config.DefaultColor)
            return false;

        // Remove the production color, target state is the default color on the cell
        // SetupProvider can skip cleanup if no cell still provides the production color
        return SetupEvaluation.Remove(
            new ColorizingCapabilities { Color = product.Color },
            new ColorizingCapabilities { Color = Config.DefaultColor },
            SetupClassification.Manual);
    }

    public override IReadOnlyList<IWorkplanStep> CreateSteps(IProductRecipe recipe)
    {
        return
        [
            new ColorChangeTask
            {
                Parameters = new ColorChangeParameters
                {
                    Color = Config.DefaultColor,
                    Instructions =
                    [
                        new VisualInstruction
                        {
                            Type = InstructionContentType.Text,
                            Content = $"Cleanup: reset colorizing cell to default color '{Config.DefaultColor}'"
                        }
                    ]
                }
            }
        ];
    }
}
