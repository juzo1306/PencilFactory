// Copyright (c) 2026, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System.ComponentModel.DataAnnotations;

namespace PencilFactory.Activities.ColorizingStep;

public enum ColorizingActivityResults
{
    [Display(Name = "Success")]
    Success,

    Failed
}
