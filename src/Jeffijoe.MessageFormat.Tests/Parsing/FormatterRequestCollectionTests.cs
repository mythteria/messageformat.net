﻿// MessageFormat for .NET
// - FormatterRequestCollectionTests.cs
// Author: Jeff Hansen <jeff@jeffijoe.com>
// Copyright (C) Jeff Hansen 2014. All rights reserved.

using System.Linq;
using System.Text;

using Jeffijoe.MessageFormat.Formatting;
using Jeffijoe.MessageFormat.Parsing;

using Xunit;

namespace Jeffijoe.MessageFormat.Tests.Parsing
{
    public class FormatterRequestCollectionTests
    {
        [Fact]
        public void ShiftIndices()
        {
            var subject = new FormatterRequestCollection();
            subject.Add(
                new FormatterRequest(
                    new Literal(0, 9, 1, 1, new StringBuilder(new string('a', 10))), 
                    "test", 
                    "test", 
                    "test"));
            subject.Add(
                new FormatterRequest(
                    new Literal(10, 19, 1, 1, new StringBuilder(new string('a', 10))), 
                    "test", 
                    "test", 
                    "test"));
            subject.Add(
                new FormatterRequest(
                    new Literal(20, 29, 1, 1, new StringBuilder(new string('a', 10))), 
                    "test", 
                    "test", 
                    "test"));
            subject.ShiftIndices(1, 4);
            Assert.Equal(0, subject[0].SourceLiteral.StartIndex);
            Assert.Equal(10, subject[1].SourceLiteral.StartIndex);

            Assert.Equal(14, subject[2].SourceLiteral.StartIndex);
        }

        [Fact]
        public void Clone()
        {
            var subject = new FormatterRequestCollection();
            subject.Add(
                new FormatterRequest(
                    new Literal(0, 9, 1, 1, new StringBuilder(new string('a', 10))), 
                    "test", 
                    "test", 
                    "test"));
            subject.Add(
                new FormatterRequest(
                    new Literal(10, 19, 1, 1, new StringBuilder(new string('a', 10))), 
                    "test", 
                    "test", 
                    "test"));
            subject.Add(
                new FormatterRequest(
                    new Literal(20, 29, 1, 1, new StringBuilder(new string('a', 10))), 
                    "test", 
                    "test", 
                    "test"));

            var cloned = subject.Clone();
            Assert.Equal(subject.Count, cloned.Count());

            foreach (var clonedReq in cloned)
            {
                Assert.False(subject.Any(x => ReferenceEquals(x, clonedReq)));
                Assert.False(subject.Any(x => x.SourceLiteral == clonedReq.SourceLiteral));
                Assert.True(subject.Any(x => x.SourceLiteral.StartIndex == clonedReq.SourceLiteral.StartIndex));
            }
        }
    }
}