namespace Be.Vlaanderen.Basisregisters.Utilities.Tests
{
    using FluentAssertions;
    using System;
    using System.Collections.Generic;
    using Xunit;

    public class ToStringBuilderExtensionTests
    {
        [Fact]
        public void WithObjectAndValuesFunc()
        {
            var obj = new ToStringObject();

            obj
                .ToString(x => new List<object> { x.Id, x.Name })
                .Should().Be($"{obj.Id}, {obj.Name}");
        }

        [Fact]
        public void WithObjectAndValuesFuncAndConcatFunc()
        {
            var obj = new ToStringObject();

            obj
                .ToString(x => new List<object> { x.Id, x.Name }, (s1, s2) => $"{s1}/{s2}")
                .Should().Be($"{obj.Id}/{obj.Name}");
        }

        [Fact]
        public void NoObjectWithValuesFunc()
        {
            var guid = Guid.NewGuid();
            var name = "Bar Foo";

            ToStringBuilder
                .ToString(() => new List<object> { guid, name })
                .Should().Be($"{guid}, {name}");
        }

        [Fact]
        public void NoObjectWithValues()
        {
            var guid = Guid.NewGuid();
            var name = "Bar Foo";

            ToStringBuilder
                .ToString(new List<object> { name, guid })
                .Should().Be($"{name}, {guid}");
        }

        [Fact]
        public void NoObjectWithValuesAndConcatFunc()
        {
            var guid = Guid.NewGuid();
            var name = "Bar Foo";

            ToStringBuilder
                .ToString(new List<object> { name, guid }, (s1, s2) => $"{s1}--{s2}")
                .Should().Be($"{name}--{guid}");
        }

        public class ToStringObject
        {
            public string Name { get; set; }
            public Guid Id { get; set; }

            public ToStringObject()
            {
                Id = Guid.NewGuid();
                Name = "Foo Bar";
            }
        }
    }
}
