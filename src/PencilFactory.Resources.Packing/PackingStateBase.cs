// Copyright (c) 2026, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using Moryx.StateMachines;

namespace PencilFactory.Resources.Packing;

internal abstract class PackingStateBase(PackingCell context, StateBase.StateMap stateMap) : AsyncStateBase<PackingCell>(context, stateMap)
{
}
