using System.Threading.Tasks;

namespace MyApplication.MyModule.Facade
{
    public interface IPencilOrderService
    {
        Task<string> CreateProductionOrderAsync(
            string materialNumber, int amount, string customerName, string erpOrderId);
    }
}
