// Copyright (c) 2026, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System.ComponentModel.DataAnnotations;

namespace PencilFactory.Activities.AssemblingStep;

public enum AssemblingActivityResults
{
    [Display(Name = "Success")]
    Success,

    Failed
}
