using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FileConverter3D.UnitTests
{
    [TestFixture]
    class TextLineReader_Tests
    {
        [TestCase]
        public void Read_OnValidStream_ShouldReturnLines()
        {
            var testLines = new List<string>() { "dsfpok pokdvpoe", "ds949 99 3kj", "d0 m04k 04kd",  "fps vmrp 948 894"};

            var resultLines =
                new Common.TextLineReader().Read(
                    GenerateStreamFromString(
                        string.Join(
                            Environment.NewLine,
                            testLines)))
                .ToList();

            Assert.That(resultLines, Is.EquivalentTo(testLines));
        }

        public static Stream GenerateStreamFromString(string s)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }
    }
}
