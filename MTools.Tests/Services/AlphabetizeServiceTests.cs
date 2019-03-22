using MTools.Services;
using MTools.Services.Interfaces;
using System;
using Xunit;

namespace MTools.Tests
{
    public class AlphabetizeServiceTests
    {
        private AlphabetizeService _alphabetizeService;

        public AlphabetizeServiceTests()
        {
            _alphabetizeService = new AlphabetizeService();
        }

        [Fact]
        public void AlphabetizeComplexStringArray()
        {
            string[] array = new string[]
            {
                "££aaa",
                "\neee",
                "           ddd",
                "-bbb",
                "     CCCcc    "
            };

            string[] expected = new string[] 
            {
                 "-bbb",
                "££aaa",
                "     CCCcc    ",
                "           ddd",
                "\neee"
            };

            string[] actual = _alphabetizeService.AlphabetizeArray(ref array);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void AlphabetizeSimpleStringArray()
        {
            string[] array = new string[]
            {
                "bbb",
                "aaa",
                "eee",
                "ddd",
                "ccc"
            };

            string[] expected = new string[] 
            {
                "aaa",
                "bbb",
                "ccc",
                "ddd",
                "eee"
            };

            string[] actual = _alphabetizeService.AlphabetizeArray(ref array);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void AlphabetizeStringArrayWithPriorityChars()
        {
            string[] array = new string[]
            {
                "z-index:",
                "border-radius",
                "eee",
                "border:",
                ";somethingelse",
                "{something"
            };

            string[] expected = new string[] 
            {
                "{something",
                ";somethingelse",
                "border:",
                "border-radius",
                "eee",
                "z-index:"
            };

            string[] actual = _alphabetizeService.AlphabetizeArray(ref array);

            Assert.Equal(expected, actual);
        }
    }
}
