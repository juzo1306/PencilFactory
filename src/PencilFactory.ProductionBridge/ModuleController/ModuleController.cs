using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moryx.AbstractionLayer.Products;
using Moryx.Configuration;
using Moryx.Container;
using Moryx.Orders;
using Moryx.Runtime.Modules;
using MyApplication.MyModule.Facade;

namespace PencilFactory.ProductionBridge;

[Description("Order intake: checks products and factory rules for ERP orders")]
public class ModuleController : ServerModuleBase<ModuleConfig>, IFacadeContainer<IPencilOrderService>
{
    private readonly PencilOrderFacade _facade = new();

    public override string Name => "PencilOrderService";

    [RequiredModuleApi(IsOptional = false, IsStartDependency = true)]
    public IOrderManagement OrderManagement { get; set; }

    [RequiredModuleApi(IsOptional = false, IsStartDependency = true)]
    public IProductManagement ProductManagement { get; set; }

    IPencilOrderService IFacadeContainer<IPencilOrderService>.Facade => _facade;

    public ModuleController(
        IModuleContainerFactory containerFactory,
        IConfigManager configManager,
        ILoggerFactory loggerFactory)
        : base(containerFactory, configManager, loggerFactory) { }

    protected override Task OnInitializeAsync(CancellationToken cancellationToken)
    {
        _facade.Initialize(OrderManagement, ProductManagement, Config);
        Container.SetInstance<IPencilOrderService>(_facade);
        return Task.CompletedTask;
    }

    protected override Task OnStartAsync(CancellationToken cancellationToken)
    {
        ActivateFacade(_facade);
        return Task.CompletedTask;
    }

    protected override Task OnStopAsync(CancellationToken cancellationToken)
    {
        DeactivateFacade(_facade);
        return Task.CompletedTask;
    }
}
