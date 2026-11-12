// Copyright (c) 2026, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using Moryx.AbstractionLayer.Workplans;
using System.ComponentModel.DataAnnotations;

namespace PencilFactory.Activities.ColorChangeStep;

[Display(Name = "ColorChange", Description = "Change the paint color on a colorizing cell (typically used as setup)")]
public class ColorChangeTask : TaskStep<ColorChangeActivity, ColorChangeParameters>
{
}
