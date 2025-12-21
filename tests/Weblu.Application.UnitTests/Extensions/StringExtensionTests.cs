using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Weblu.Application.Extensions;
using Xunit;

namespace Weblu.Application.UnitTests.Extensions
{
    public class StringExtensionTests
    {
        [Fact]
        public void StringExtension_Slugify_ReturnSluggedPhrase()
        {
            // Arrange
            string phrase = "How to center a div";

            // Act
            string act = phrase.Slugify();

            // Assert
            act.Should().BeOfType<string>();
            act.Should().NotBeNullOrWhiteSpace();
            act.Should().NotContain(" ");
            act.Should().BeLowerCased();

        }
        [Theory]
        [InlineData("Café", "Cafe")]
        [InlineData("à la crème", "a la creme")]
        [InlineData("Übermensch", "Ubermensch")]
        [InlineData("fiancée", "fiancee")]
        [InlineData("São Paulo", "Sao Paulo")]
        public void StringExtension_RemoveAccents_ReturnStringWithoutAccents(string input, string expected)
        {
            // Arrange

            // Act
            string act = input.RemoveAccents();

            // Assert
            act.Should().BeOfType<string>();
            act.Should().NotBeNullOrWhiteSpace();
            act.Should().NotContainAny("à", "á", "â", "ä", "ã", "å", "è", "é", "ê", "ë", "ì", "í", "î", "ï", "ò", "ó", "ô", "ö", "õ", "ù", "ú", "û", "ü", "ç", "ñ", "ý", "ÿ");
            act.Should().Be(expected);
        }

    }
}