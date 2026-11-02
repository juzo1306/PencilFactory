// Copyright (c) 2026, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using Moryx.StateMachines;

namespace PencilFactory.Resources.Testing;

internal abstract class TestingStateBase(TestingCell context, StateBase.StateMap stateMap) : AsyncStateBase<TestingCell>(context, stateMap)
{
}
