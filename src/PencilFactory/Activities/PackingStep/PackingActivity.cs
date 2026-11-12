// Copyright (c) 2026, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using Moryx.AbstractionLayer.Activities;
using Moryx.AbstractionLayer.Capabilities;
using PencilFactory.Capabilities;

namespace PencilFactory.Activities.PackingStep;

[ActivityResults(typeof(PackingActivityResults))]
public class PackingActivity : Activity<PackingParameters>
{
    public override ProcessRequirement ProcessRequirement => ProcessRequirement.NotRequired;

    public override ICapabilities RequiredCapabilities => new PackingCapabilities();

    protected override ActivityResult CreateResult(long resultNumber)
    {
        return ActivityResult.Create((PackingActivityResults)resultNumber);
    }

    protected override ActivityResult CreateFailureResult()
    {
        return ActivityResult.Create(PackingActivityResults.Failed);
    }
}
