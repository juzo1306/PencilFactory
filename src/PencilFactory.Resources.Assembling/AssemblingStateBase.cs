// Copyright (c) 2026, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using Moryx.StateMachines;

namespace PencilFactory.Resources.Assembling;

internal abstract class AssemblingStateBase(AssemblingCell context, StateBase.StateMap stateMap) : AsyncStateBase<AssemblingCell>(context, stateMap)
{
}
