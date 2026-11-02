// Copyright (c) 2026, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using Moryx.StateMachines;

namespace PencilFactory.Resources.Colorizing;

internal abstract class ColorizingStateBase(ColorizingCell context, StateBase.StateMap stateMap) : AsyncStateBase<ColorizingCell>(context, stateMap)
{
}
