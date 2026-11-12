// Copyright (c) 2026, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using Moryx.AbstractionLayer.Activities;
using Moryx.AbstractionLayer.Capabilities;
using Moryx.ControlSystem.Activities;
using PencilFactory.Capabilities;

namespace PencilFactory.Activities.ColorChangeStep;

[ActivityResults(typeof(ColorChangeActivityResults))]
/// <summary>
/// Setup activity: change the paint color on a colorizing cell before production.
/// </summary>
public class ColorChangeActivity : Activity<ColorChangeParameters>, IControlSystemActivity
{
    public ActivityClassification Classification => ActivityClassification.Setup;

    public override ProcessRequirement ProcessRequirement => ProcessRequirement.NotRequired;

    /// <summary>
    /// Any colorizing cell can perform a color change (Color defaults to null = "any").
    /// </summary>
    public override ICapabilities RequiredCapabilities => new ColorizingCapabilities();

    protected override ActivityResult CreateResult(long resultNumber)
    {
        return ActivityResult.Create((ColorChangeActivityResults)resultNumber);
    }

    protected override ActivityResult CreateFailureResult()
    {
        return ActivityResult.Create(ColorChangeActivityResults.Failed);
    }
}

