using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moryx.Configuration;
using Moryx.Container;
using Moryx.Runtime.Modules;
using MyApplication.MyModule.Facade;
using PencilFactory.ProductionBridge;

namespace PencilFactory.ErpMock;

[Description("ERP mock: receives wholesale purchase orders for the graphite pencil line")]
public class ModuleController : ServerModuleBase<ModuleConfig>
{
    public override string Name => "ERP-Mock-Adapter";

    [RequiredModuleApi(IsOptional = false, IsStartDependency = true)]
    public IPencilOrderService PencilOrderService { get; set; }

    public ModuleController(
        IModuleContainerFactory containerFactory,
        IConfigManager configManager,
        ILoggerFactory loggerFactory)
        : base(containerFactory, configManager, loggerFactory) { }

    protected override Task OnInitializeAsync(CancellationToken cancellationToken)
    {
        Container.SetInstance(PencilOrderService);
        return Task.CompletedTask;
    }

    protected override Task OnStartAsync(CancellationToken cancellationToken) => Task.CompletedTask;

    protected override Task OnStopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}
