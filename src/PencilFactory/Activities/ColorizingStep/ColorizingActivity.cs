// Copyright (c) 2026, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using Moryx.AbstractionLayer.Activities;
using Moryx.AbstractionLayer.Capabilities;
using PencilFactory.Capabilities;

namespace PencilFactory.Activities.ColorizingStep;

[ActivityResults(typeof(ColorizingActivityResults))]
public class ColorizingActivity : Activity<ColorizingParameters>
{
    public override ProcessRequirement ProcessRequirement => ProcessRequirement.NotRequired;

    public override ICapabilities RequiredCapabilities => new ColorizingCapabilities() { Color = Parameters.Color };

    protected override ActivityResult CreateResult(long resultNumber)
    {
        return ActivityResult.Create((ColorizingActivityResults)resultNumber);
    }

    protected override ActivityResult CreateFailureResult()
    {
        return ActivityResult.Create(ColorizingActivityResults.Failed);
    }
}
