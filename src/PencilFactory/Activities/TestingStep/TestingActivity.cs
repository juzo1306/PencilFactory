// Copyright (c) 2026, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using Moryx.AbstractionLayer.Activities;
using Moryx.AbstractionLayer.Capabilities;
using PencilFactory.Capabilities;

namespace PencilFactory.Activities.TestingStep;

[ActivityResults(typeof(TestingActivityResults))]
public class TestingActivity : Activity<TestingParameters>
{
    public override ProcessRequirement ProcessRequirement => ProcessRequirement.NotRequired;

    public override ICapabilities RequiredCapabilities => new TestingCapabilities();

    protected override ActivityResult CreateResult(long resultNumber)
    {
        return ActivityResult.Create((TestingActivityResults)resultNumber);
    }

    protected override ActivityResult CreateFailureResult()
    {
        return ActivityResult.Create(TestingActivityResults.Failed);
    }
}
