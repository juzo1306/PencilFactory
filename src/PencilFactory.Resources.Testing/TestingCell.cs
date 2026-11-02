// Copyright (c) 2026, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Threading;
using System.Threading.Tasks;
using Moryx.AbstractionLayer.Activities;
using Moryx.AbstractionLayer.Drivers.InOut;
using Moryx.AbstractionLayer.Resources;
using Moryx.ControlSystem.Activities;
using Moryx.ControlSystem.Cells;
using Moryx.Serialization;
using Moryx.StateMachines;
using Moryx.VisualInstructions;
using PencilFactory.Activities.ColorizingStep;
using PencilFactory.Activities.TestingStep;
using PencilFactory.Capabilities;

namespace PencilFactory.Resources.Testing;

[ResourceRegistration] // Only necessary for dependency injection like logging or parallel operations
public class TestingCell : Cell, IAsyncStateContext
{
    private const string ProcessStart = "ProcessStart";
    private const string ProcessResult = "ProcessResult";
    private const string Ready = "Ready";
    private Session _currentSession;
    private long _currentInstruction;

    [ResourceReference(ResourceRelationType.Driver)]
    public IInOutDriver Driver
    {
        get => field;
        set
        {
            if (field?.Input != null)
            {
                field.Input.InputChanged -= OnInputChanged;
            }
            field = value;
            if (field?.Input != null)
            {
                field.Input.InputChanged += OnInputChanged;
            }
        }
    }

    [ResourceReference(ResourceRelationType.Extension)]
    public IVisualInstructor VisualInstructor { get; set; }

    [DataMember, EntrySerialize]
    [Description("Configured value for the capabilities")]
    public int Value { get; set; }

    protected override async Task OnInitializeAsync(CancellationToken cancellationToken)
    {
        await base.OnInitializeAsync(cancellationToken);
        Capabilities = new TestingCapabilities { Value = Value };

        if (Driver != null)
        {
            Driver.Input.InputChanged += OnInputChanged;
        }
    }

    protected override async Task OnStartAsync(CancellationToken cancellationToken)
    {
        await base.OnStartAsync(cancellationToken);
    }

    protected override async Task OnStopAsync(CancellationToken cancellationToken)
    {
        await base.OnStopAsync(cancellationToken);
    }

    protected override IEnumerable<Session> ProcessEngineAttached()
    {
        yield return Session.StartSession(ActivityClassification.Production, ReadyToWorkType.Push);
    }

    protected override IEnumerable<Session> ProcessEngineDetached()
    {
        yield break;
    }

    public override void StartActivity(ActivityStart activityStart)
    {
        _currentSession = activityStart;
        switch (activityStart.Activity)
        {
            case ColorizingActivity:
                Driver.Output[ProcessStart] = true;
                break;
        }
    }

    public override void ProcessAborting(Activity affectedActivity)
    {
        // Default behavior: Clear instruction and abort execution
        if (_currentSession is ActivityStart activityStart)
        {
            // Clear driver and/or instructor
            VisualInstructor?.Clear(_currentInstruction);
            if (Driver != null)
            {
                Driver.Output["Start"] = false;
            }

            // Report current activity as failed
            activityStart.CreateResult((int)TestingActivityResults.Failed);
        }
    }

    public override void SequenceCompleted(SequenceCompleted completed)
    {
        _currentSession = completed;

        var rtw = Session.StartSession(ActivityClassification.Production, ReadyToWorkType.Push);
        PublishReadyToWork(rtw);
        _currentSession = rtw;
    }

    private void OnInputChanged(object sender, InputChangedEventArgs args)
    {
        if (args.Key == "ProcessResult" && _currentSession is ActivityStart activityStart)
        {
            Driver.Output[ProcessStart] = false;
            var processResult = (bool)Driver.Input[ProcessResult];
            var result = activityStart.CreateResult(processResult ? (int)TestingActivityResults.Success : (int)TestingActivityResults.Failed);
            _currentSession = result;
            PublishActivityCompleted(result);
        }
        if (args.Key == "Ready" && _currentSession is not ActivityStart notActivityStart)
        {
            var rtw = Session.StartSession(ActivityClassification.Production, ReadyToWorkType.Pull);
            _currentSession = rtw;
            PublishReadyToWork(rtw);
        }
        else if (args.Key == "Completed" && _currentSession is ActivityStart activitySession)
        {
            var result = activitySession.CreateResult(0);
            _currentSession = result;
            PublishActivityCompleted(result);
        }
    }

    Task IAsyncStateContext.SetStateAsync(StateBase state, CancellationToken cancellationToken)
    {
        /* Use for a resource state machine */
        throw new System.NotImplementedException();
    }
}
