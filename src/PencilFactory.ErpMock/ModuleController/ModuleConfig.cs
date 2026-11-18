using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Moryx.Configuration;

namespace PencilFactory.ErpMock;

[DataContract]
public class ModuleConfig : ConfigBase
{
    [DataMember]
    [Display(Name = "Customer")]
    [DefaultValue("Stationery Mustermann")]
    public string Customer { get; set; } = "Stationery Mustermann";

    [DataMember]
    [Display(Name = "ERP order id")]
    [DefaultValue("PO-2026-1000")]
    public string ErpOrderId { get; set; } = "PO-2026-1000";

    [DataMember]
    [Display(Name = "ERP article")]
    [Description("PEN-GREEN, PEN-BROWN or PEN-BLUE")]
    [DefaultValue("PEN-GREEN")]
    public string ErpArticle { get; set; } = "PEN-GREEN";

    [DataMember]
    [Display(Name = "Amount")]
    [DefaultValue(500)]
    public int Amount { get; set; } = 500;
}
