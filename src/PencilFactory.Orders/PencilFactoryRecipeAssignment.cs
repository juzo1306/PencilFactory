// Copyright (c) 2026, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moryx.AbstractionLayer.Recipes;
using Moryx.Container;
using Moryx.Orders;
using Moryx.Orders.Assignment;

namespace PencilFactory.Orders;

/// <summary>
/// Recipe assignment for the PencilFactory
/// </summary>
[Plugin(LifeCycle.Singleton, typeof(IRecipeAssignment), Name = nameof(PencilFactoryRecipeAssignment))]
public class PencilFactoryRecipeAssignment : RecipeAssignmentBase<RecipeAssignmentConfig>
{
    /// <inheritdoc />
    public override async Task<IReadOnlyList<IProductRecipe>> SelectRecipesAsync(Operation operation, IOperationLogger operationLogger, CancellationToken cancellationToken)
    {
        var defaultRecipe = await LoadDefaultRecipeAsync(operation.Product, cancellationToken);

        return [defaultRecipe];
    }

    /// <inheritdoc />
    public override Task<bool> ProcessRecipeAsync(IProductRecipe clone, Operation operation, IOperationLogger operationLogger, CancellationToken cancellationToken)
    {
        return Task.FromResult(true);
    }
}
