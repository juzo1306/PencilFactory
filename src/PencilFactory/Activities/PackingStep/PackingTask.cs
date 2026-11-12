// Copyright (c) 2026, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using Moryx.AbstractionLayer.Workplans;
using System.ComponentModel.DataAnnotations;

namespace PencilFactory.Activities.PackingStep;

[Display(Name = "Packing Task", Description = "Task which does something with a product")]
public class PackingTask : TaskStep<PackingActivity, PackingParameters>
{
}
