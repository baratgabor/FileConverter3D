using NUnit.Framework;
using System;
using FileConverter3D.Core.ObjAscii;

namespace FileConverter3D.UnitTests
{
    [TestFixture]
    class VertexParser_Tests
    {
        [TestCase("v 0 0 0")]
        [TestCase("v 1 2 3")]
        [TestCase("v 1.456 2.456 3.255")]
        [TestCase("v -1.456 -2.456 -3.255")]
        public void CanParse_ValidString_ShouldReturnTrue(string parsable)
        {
            Assert.IsTrue(new VertexParser().CanParse(parsable));
        }

        [TestCase("v 0 0 0", 0, 0, 0)]
        [TestCase("v 1 2 3", 1, 2, 3)]
        [TestCase("v 1.456 2.456 3.255", 1.456f, 2.456f, 3.255f)]
        [TestCase("v -1.456 -2.456 -3.255", -1.456f, -2.456f, -3.255f)]
        public void Parse_ValidString_ShouldReturnCorrectVertex(string parsable, float expX, float expY, float expZ)
        {
            var expectedVertex = new Vertex(expX, expY, expZ);
            Assert.AreEqual(new VertexParser().Parse(parsable), expectedVertex);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("x 1 2 3")]
        public void CanParse_InvalidString_ShouldReturnFalse(string parsable)
        {
            Assert.IsFalse(new Core.ObjAscii.VertexParser().CanParse(parsable));
        }

        [TestCase("")]
        [TestCase("x")]
        [TestCase("x 0 0 0")]
        [TestCase("v 1 x 3")]
        [TestCase("v 1.1 2.2")]
        [TestCase("v 1,4 2,4 3,2")]
        public void Parse_InvalidString_ShouldThrow(string parsable)
        {
            Assert.Throws<ArgumentException>(() => new VertexParser().Parse(parsable));
        }
    }
}
