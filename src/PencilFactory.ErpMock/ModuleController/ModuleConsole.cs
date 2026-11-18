using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Moryx.Runtime.Modules;
using Moryx.Serialization;
using MyApplication.MyModule.Facade;
using PencilFactory.ProductionBridge;

namespace PencilFactory.ErpMock;

[ServerModuleConsole]
public class ModuleConsole : IServerModuleConsole
{
    public IPencilOrderService PencilOrderService { get; set; }
    public ModuleConfig Config { get; set; }

    [EntrySerialize]
    [Display(Name = "Receive ERP purchase order")]
    public string ReceiveErpPurchaseOrder()
    {
        if (!ErpArticleMapper.TryMap(Config.ErpArticle, out var materialNumber))
            return "Rejected: unknown ERP article '" + Config.ErpArticle + "'.";

        var header =
            "Customer: " + Config.Customer + "\n"
            + "ERP order: " + Config.ErpOrderId + "\n"
            + "ERP article: " + Config.ErpArticle + "\n"
            + "Material: " + materialNumber + "\n"
            + "Amount: " + Config.Amount;

        var result = PencilOrderService.CreateProductionOrderAsync(
            materialNumber, Config.Amount, Config.Customer, Config.ErpOrderId)
            .GetAwaiter().GetResult();

        return header + "\n\n" + result;
    }
}
