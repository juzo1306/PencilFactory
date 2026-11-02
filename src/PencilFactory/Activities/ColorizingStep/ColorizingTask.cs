// Copyright (c) 2026, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using Moryx.AbstractionLayer.Workplans;
using System.ComponentModel.DataAnnotations;

namespace PencilFactory.Activities.ColorizingStep;

[Display(Name = "Colorizing Task", Description = "Task which does something with a product")]
public class ColorizingTask : TaskStep<ColorizingActivity, ColorizingParameters>
{
}
