// Copyright (c) 2026, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System.ComponentModel;
using System.Runtime.Serialization;

namespace PencilFactory.Products.Importer;

/// <summary>
/// Parameters for the <see cref="PencilFactoryProductImporter"/>
/// </summary>
[DataContract]
public class PencilFactoryImportParameters
{
    [DataMember]
    [Description("How many graphite pencils go into one retail pack")]
    public int PackQuantity { get; set; } = 20;
}
