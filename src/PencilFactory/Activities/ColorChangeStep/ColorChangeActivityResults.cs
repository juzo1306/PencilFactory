// Copyright (c) 2026, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System.ComponentModel.DataAnnotations;

namespace PencilFactory.Activities.ColorChangeStep;

public enum ColorChangeActivityResults
{
    [Display(Name = "Success")]
    Success = 0,

    [Display(Name = "Technical Error")]
    TechnicalError = 1,

    [Display(Name = "Failed")]
    Failed = 2
}
