// Copyright (c) 2026, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System.Runtime.Serialization;
using Moryx.ControlSystem.Setups;
using Moryx.Serialization;
using PencilFactory.Products;

namespace PencilFactory.ControlSystem.SetupTriggers;

public class ResetColorAfterProductionConfig : SetupTriggerConfig
{
    public override string PluginName => nameof(ResetColorAfterProductionTrigger);
    /// <summary>
    /// Color the colorizing cell is reset to after production (cleanup).
    /// </summary>
    [DataMember]
    [EntrySerialize]
    public PencilColor DefaultColor { get; set; } = PencilColor.Green;
}
