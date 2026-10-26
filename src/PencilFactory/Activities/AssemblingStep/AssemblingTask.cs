// Copyright (c) 2026, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using Moryx.AbstractionLayer.Workplans;
using System.ComponentModel.DataAnnotations;

namespace PencilFactory.Activities.AssemblingStep;

[Display(Name = "Assembling Task", Description = "Task which does something with a product")]
public class AssemblingTask : TaskStep<AssemblingActivity, AssemblingParameters>
{
}
