// Copyright (c) 2026, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using Moryx.AbstractionLayer.Workplans;
using System.ComponentModel.DataAnnotations;

namespace PencilFactory.Activities.TestingStep;

[Display(Name = "Testing Task", Description = "Task which does something with a product")]
public class TestingTask : TaskStep<TestingActivity, TestingParameters>
{
}
