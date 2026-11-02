// Copyright (c) 2026, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using PencilFactory.Resources.Colorizing;
using NUnit.Framework;

namespace PencilFactory.Tests;

[TestFixture]
public class ColorizingResourceTest
{
    [Test]
    public void ResourceKeepsValue()
    {
        // Arrange
        var someCell = new ColorizingCell();
        someCell.Value = 42;

        // Act
        someCell.Value = 1337;

        // Assert
        Assert.That(1337, Is.EqualTo(someCell.Value));
    }
}
