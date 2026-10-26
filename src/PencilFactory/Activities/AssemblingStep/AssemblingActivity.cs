// Copyright (c) 2026, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using Moryx.AbstractionLayer.Activities;
using Moryx.AbstractionLayer.Capabilities;
using PencilFactory.Capabilities;

namespace PencilFactory.Activities.AssemblingStep;

[ActivityResults(typeof(AssemblingActivityResults))]
public class AssemblingActivity : Activity<AssemblingParameters>
{
    public override ProcessRequirement ProcessRequirement => ProcessRequirement.NotRequired;

    public override ICapabilities RequiredCapabilities => new AssemblingCapabilities();

    protected override ActivityResult CreateResult(long resultNumber)
    {
        return ActivityResult.Create((AssemblingActivityResults)resultNumber);
    }

    protected override ActivityResult CreateFailureResult()
    {
        return ActivityResult.Create(AssemblingActivityResults.Failed);
    }
}
