// Copyright (c) 2026, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using Moryx.AbstractionLayer.Products;
using System.ComponentModel.DataAnnotations;

namespace PencilFactory.Products;

[Display(Name = "GraphitePencil")]
public class GraphitePencilType : ProductType
{
    protected override ProductInstance Instantiate()
    {
        return new GraphitePencilInstance();
    }
}
