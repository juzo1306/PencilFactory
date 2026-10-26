// Copyright (c) 2026, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using Moryx.AbstractionLayer.Products;

namespace PencilFactory.Products.Importer;

/// <summary>
/// Config for the <see cref="PencilFactoryProductImporter"/>
/// </summary>
public class PencilFactoryProductImporterConfig : ProductImporterConfig
{
    public override string PluginName => nameof(PencilFactoryProductImporter);
}
