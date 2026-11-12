// Copyright (c) 2026, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Moryx.AbstractionLayer.Products;
using Moryx.Serialization;

namespace PencilFactory.Products;

public class GraphitePencilPartLink : ProductPartLink<GraphitePencilType>
{
    [EntrySerialize]
    [DataMember]
    [Display(Name = "Quantity")]
    public int Quantity { get; set; } = 20;
}
