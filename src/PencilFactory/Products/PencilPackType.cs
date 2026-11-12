// Copyright (c) 2026, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System.ComponentModel.DataAnnotations;
using Moryx.AbstractionLayer.Products;

namespace PencilFactory.Products;

[Display(Name = "Retail Pack")]
public class PencilPackType : ProductType
{
    [Display(Name = "Graphite Pencil")]
    public GraphitePencilPartLink GraphitePencil { get; set; }

    [Display(Name = "Pencil Carton")]
    public ProductPartLink<PencilCartonType> Carton { get; set; }

    protected override ProductInstance Instantiate()
        => new PencilPackInstance();
}
