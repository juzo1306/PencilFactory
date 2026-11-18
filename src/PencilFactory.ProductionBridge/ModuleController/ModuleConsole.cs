using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Moryx.Runtime.Modules;
using Moryx.Serialization;
using MyApplication.MyModule.Facade;

namespace PencilFactory.ProductionBridge;

[ServerModuleConsole]
public class ModuleConsole : IServerModuleConsole
{
    public IPencilOrderService PencilOrderService { get; set; }

    [EntrySerialize]
    [Display(Name = "Create production order")]
    public string CreateProductionOrder(
        [Description("100001 Green, 100002 Brown Classic")]
        string materialNumber,
        int amount,
        string customerName)
    {
        return PencilOrderService.CreateProductionOrderAsync(
            materialNumber.Trim(), amount, customerName.Trim(), "MANUAL")
            .GetAwaiter().GetResult();
    }
}
