// Copyright (c) 2026, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Moryx.AbstractionLayer.Products;
using Moryx.Serialization;

namespace PencilFactory.Products;

[Display(Name = "GraphitePencil")]
public class GraphitePencilType : ProductType
{
    [EntrySerialize]
    [DataMember]
    public PencilColor Color { get; set; }

    [EntrySerialize]
    [DataMember]
    public GraphiteHardness Hardness { get; set; }
    protected override ProductInstance Instantiate()
    {
        return new GraphitePencilInstance();
    }
}
