// Copyright (c) 2026, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using Moryx.AbstractionLayer.Products;
using System;

namespace PencilFactory.Products;

public class GraphitePencilInstance : ProductInstance<GraphitePencilType>
{
    public DateTime ManufacturingDate { get; set; }
}
