using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Moryx.Configuration;

namespace PencilFactory.ProductionBridge;

[DataContract]
public class ModuleConfig : ConfigBase
{
    [DataMember]
    [Display(Name = "Order prefix")]
    [DefaultValue("PF")]
    public string OrderPrefix { get; set; } = "PF";

    [DataMember]
    [Display(Name = "Min batch size")]
    [DefaultValue(10)]
    public int MinBatchSize { get; set; } = 10;
}
