using System;
using System.Threading.Tasks;
using Moryx.AbstractionLayer.Products;
using Moryx.Orders;
using Moryx.Runtime.Modules;
using MyApplication.MyModule.Facade;
using MyApplication.MyModule.ModuleController;
using PencilFactory.ProductionBridge;
using PencilFactory.Products;

namespace PencilFactory.ProductionBridge;

public class PencilOrderFacade : FacadeBase, IPencilOrderService
{
    private IOrderManagement _orders;
    private IProductManagement _products;
    private ModuleConfig _config;

    public void Initialize(IOrderManagement orders, IProductManagement products, ModuleConfig config)
    {
        _orders = orders;
        _products = products;
        _config = config;
    }

    public async Task<string> CreateProductionOrderAsync(string materialNumber, int amount, string customerName, string erpOrderId)
    {
        ValidateHealthState?.Invoke();

        var product = await _products.LoadTypeAsync(new ProductIdentity(materialNumber, 1));
        if (product == null)
            return "Rejected: material " + materialNumber + " is not in the product data.";

        if (product is PencilCartonType)
            return "Rejected: " + product.Name + " is a purchased part.";

        if (product is PencilPackType)
            return "Rejected: " + product.Name + " belongs on the packing line.";

        if (product is not GraphitePencilType)
            return "Rejected: only graphite pencils can be ordered through this interface.";

        if (amount < _config.MinBatchSize)
            return "Rejected: quantity " + amount
                + " is below minimum batch size " + _config.MinBatchSize + ".";

        var orderNumber = _config.OrderPrefix + "-" + DateTime.Now.ToString("HHmmss");
        var orderName = string.IsNullOrWhiteSpace(erpOrderId)
            ? customerName
            : customerName + " / " + erpOrderId;

        await _orders.AddOperationAsync(new OperationCreationContext
        {
            Order = new OrderCreationContext { Number = orderNumber, Type = "PencilFactory" },
            Number = Random.Shared.Next(1000, 9999).ToString(),
            Name = orderName,
            ProductIdentifier = materialNumber,
            ProductRevision = 1,
            TotalAmount = amount,
            PlannedStart = DateTime.Now,
            PlannedEnd = DateTime.Now.AddHours(8)
        });

        return "Production order " + orderNumber + " created: "
            + amount + "x " + product.Name + " for " + orderName
            + ". Release and start it in the Orders UI.";
    }
}
