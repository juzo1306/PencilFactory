// Copyright (c) 2026, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using Moryx.AbstractionLayer.Activities;
using Moryx.ControlSystem.Simulation;
using Moryx.Drivers.Simulation.InOutDriver;
using PencilFactory.Activities.ColorizingStep;

namespace PencilFactory.Resources.Colorizing;

public class SimulatedColorizingDriver : SimulatedInOutDriver
{
    private const string ProcessStart = "ProcessStart";
    private const string ProcessResult = "ProcessResult";
    private const string ReadyToWork = "Ready";
    public override void Ready(Activity activity)
    {
        SimulatedState = SimulationState.Requested;
        SimulatedInput.Values[ReadyToWork] = true;
        SimulatedInput.RaiseInputChanged(ReadyToWork, SimulatedInput.Values[ReadyToWork]);
    }

    public override void Result(SimulationResult result)
    {
        SimulatedInput.Values[ProcessResult] = result.Result == (int)ColorizingActivityResults.Success;
        SimulatedInput.RaiseInputChanged(ProcessResult, SimulatedInput.Values[ProcessResult]);
    }

    protected override void OnOutputSet(object sender, string key)
    {
        if (key == ProcessStart)
        {
            SimulatedInput.Values[ReadyToWork] = false;
            if ((bool)SimulatedOutput.Values[ProcessStart])
            {
                SimulatedState = SimulationState.Executing;
            }
            else
            {
                SimulatedState = SimulationState.Idle;
            }
        }
    }
}
