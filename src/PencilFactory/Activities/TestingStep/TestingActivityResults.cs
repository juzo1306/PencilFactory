// Copyright (c) 2026, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System.ComponentModel.DataAnnotations;

namespace PencilFactory.Activities.TestingStep;

public enum TestingActivityResults
{
    [Display(Name = "Success")]
    Success,

    Failed
}
