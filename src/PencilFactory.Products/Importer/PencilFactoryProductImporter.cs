// Copyright (c) 2026, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moryx.AbstractionLayer.Products;
using Moryx.Container;
using Moryx.Logging;
using Moryx.Modules;
using Moryx.Products.Management;

namespace PencilFactory.Products.Importer;

/// <summary>
/// Imports products for PencilFactory
/// </summary>
[ExpectedConfig(typeof(PencilFactoryProductImporterConfig))]
[ProductImporter(nameof(PencilFactoryProductImporter))]
public class PencilFactoryProductImporter : ProductImporterBase<PencilFactoryProductImporterConfig, PencilFactoryImportParameters>, ILoggingComponent
{
    /// <inheritdoc />
    public IModuleLogger Logger { get; set; }

    /// <summary>
    /// Product storage to persist and load imported products or recipes
    /// </summary>
    public IProductStorage Storage { get; set; }

    /// <inheritdoc />
    protected override Task<ProductImporterResult> ImportAsync(ProductImportContext context, PencilFactoryImportParameters parameters, CancellationToken cancellationToken)
    {
        var products = new List<ProductType>();

        // TODO: Create objects from parameters, file or endpoint

        return Task.FromResult(new ProductImporterResult { ImportedTypes = products });
    }
}
