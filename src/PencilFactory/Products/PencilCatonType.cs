// Copyright (c) 2026, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Moryx.AbstractionLayer.Products;
using Moryx.Serialization;

namespace PencilFactory.Products;

[Display(Name = "Pencil Carton")]
public class PencilCartonType : ProductType
{
    [EntrySerialize]
    [DataMember]
    public CartonColor Color { get; set; }

    protected override ProductInstance Instantiate()
        => new PencilCartonInstance();
}
