using System;
using System.Threading;
using System.Threading.Tasks;
using Moryx.AbstractionLayer.Products;
using Moryx.AbstractionLayer.Recipes;
using Moryx.AbstractionLayer.Workplans;
using Moryx.Logging;
using Moryx.Modules;
using Moryx.Products.Management;
using Moryx.VisualInstructions;
using Moryx.Workplans;
using PencilFactory.Activities.AssemblingStep;
using PencilFactory.Activities.ColorizingStep;
using PencilFactory.Activities.PackingStep;
using PencilFactory.Activities.TestingStep;

namespace PencilFactory.Products.Importer;

/// <summary>
/// Imports products for PencilFactory
/// </summary>
[ExpectedConfig(typeof(PencilFactoryProductImporterConfig))]
[ProductImporter(nameof(PencilFactoryProductImporter))]
public class PencilFactoryProductImporter : ProductImporterBase<PencilFactoryProductImporterConfig, PencilFactoryImportParameters>, ILoggingComponent
{
    public IModuleLogger Logger { get; set; }
    public IProductStorage Storage { get; set; }
    public IWorkplans WorkplanStorage { get; set; }

    protected override async Task<ProductImporterResult> ImportAsync(ProductImportContext context, PencilFactoryImportParameters parameters, CancellationToken cancellationToken)
    {
        var quantity = parameters.PackQuantity > 0 ? parameters.PackQuantity : 20;

        var green = CreateGraphitePencil("100001", 1, "Green Classic", PencilColor.Green, GraphiteHardness.B);
        var brown = CreateGraphitePencil("100002", 1, "Brown Classic", PencilColor.Brown, GraphiteHardness.HB);
        var bluePremium = CreateGraphitePencil("100003", 1, "Blue Premium", PencilColor.Blue, GraphiteHardness.HB);
        var greenCarton = CreateCarton("200001", 1, "Green Pencil Carton", CartonColor.Green);
        var greenPack = CreateRetailPack("300001", 1, "Green 20er", green, quantity, greenCarton);

        await Storage.SaveTypeAsync(green, cancellationToken);
        await Storage.SaveTypeAsync(brown, cancellationToken);
        await Storage.SaveTypeAsync(bluePremium, cancellationToken);
        await Storage.SaveTypeAsync(greenCarton, cancellationToken);
        await Storage.SaveTypeAsync(greenPack, cancellationToken);

        var pencilWorkplan = CreateGraphitePencilWorkplan();
        var packWorkplan = CreatePackingWorkplan();
        await WorkplanStorage.SaveWorkplanAsync(pencilWorkplan, cancellationToken);
        await WorkplanStorage.SaveWorkplanAsync(packWorkplan, cancellationToken);

        await Storage.SaveRecipeAsync(CreateRecipe("Graphite pencil production", green, pencilWorkplan), cancellationToken);
        await Storage.SaveRecipeAsync(CreateRecipe("Graphite pencil production", brown, pencilWorkplan), cancellationToken);
        await Storage.SaveRecipeAsync(CreateRecipe("Graphite pencil production", bluePremium, pencilWorkplan), cancellationToken);
        await Storage.SaveRecipeAsync(CreateRecipe("Retail packing", greenPack, packWorkplan), cancellationToken);

        return new ProductImporterResult { Saved = true };
    }

    private static GraphitePencilType CreateGraphitePencil(string materialNumber, short revision, string name, PencilColor color, GraphiteHardness hardness)
    {
        return new GraphitePencilType
        {
            Identity = new ProductIdentity(materialNumber, revision),
            Name = name,
            Color = color,
            Hardness = hardness
        };
    }

    private static PencilCartonType CreateCarton(string materialNumber, short revision, string name, CartonColor color)
    {
        return new PencilCartonType
        {
            Identity = new ProductIdentity(materialNumber, revision),
            Name = name,
            Color = color
        };
    }

    private static PencilPackType CreateRetailPack(string materialNumber, short revision, string name, GraphitePencilType pencil, int quantity, PencilCartonType carton)
    {
        return new PencilPackType
        {
            Identity = new ProductIdentity(materialNumber, revision),
            Name = name,
            GraphitePencil = new GraphitePencilPartLink { Product = pencil, Quantity = quantity },
            Carton = new ProductPartLink<PencilCartonType> { Product = carton }
        };
    }

    private static ProductionRecipe CreateRecipe(string name, ProductType product, Workplan workplan)
    {
        return new ProductionRecipe
        {
            Name = name,
            Product = product,
            Workplan = workplan,
            Classification = RecipeClassification.Default,
            State = RecipeState.Released
        };
    }

    private static Workplan CreateGraphitePencilWorkplan()
    {
        var workplan = new Workplan
        {
            Name = "Graphite pencil production (Imported)",
            State = WorkplanState.Released
        };

        var start = workplan.AddConnector("Start", NodeClassification.Start);
        var assembled = workplan.AddConnector("Assembled", NodeClassification.Intermediate);
        var colorized = workplan.AddConnector("Colorized", NodeClassification.Intermediate);
        var end = workplan.AddConnector("End", NodeClassification.End);
        var failed = workplan.AddConnector("Failed", NodeClassification.Failed);

        workplan.AddStep(new AssemblingTask(), new AssemblingParameters
        {
            Instructions =
            [
                new VisualInstruction
            {
                Type = InstructionContentType.Text,
                Content = "Assemble the graphite pencil, then confirm SUCCESS or FAILED."
            }
            ]
        }, start, assembled, failed);

        workplan.AddStep(new ColorizingTask(), new ColorizingParameters
        {
            Instructions =
            [
                new VisualInstruction
            {
                Type = InstructionContentType.Text,
                Content = "Colorize the pencil with the required color, then confirm SUCCESS."
            }
            ]
        }, assembled, colorized, failed);

        workplan.AddStep(new TestingTask(), new TestingParameters(), colorized, end, failed);

        return workplan;
    }

    private static Workplan CreatePackingWorkplan()
    {
        var workplan = new Workplan
        {
            Name = "Retail packing (Imported)",
            State = WorkplanState.Released
        };

        var start = workplan.AddConnector("Start", NodeClassification.Start);
        var end = workplan.AddConnector("End", NodeClassification.End);
        var failed = workplan.AddConnector("Failed", NodeClassification.Failed);

        workplan.AddStep(new PackingTask(), new PackingParameters(), start, end, failed);

        return workplan;
    }
}
