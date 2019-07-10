using NUnit.Framework;

namespace FileConverter3D.UnitTests
{
    //[TestFixture]
    //public class Vector3_Test
    //{
    //    [TestCase]
    //    public void TryParse_EmptyString_ShouldReturnFalse()
    //    {
    //        Assert.IsFalse(
    //            Vector3.TryParse("", out var _)
    //        );
    //    }

    //    [TestCase]
    //    public void TryParse_NullString_ShouldReturnFalse()
    //    {
    //        Assert.IsFalse(
    //            Vector3.TryParse(null, out var _)
    //        );
    //    }

    //    [TestCase]
    //    public void TryParse_OneValueMissing_ShouldReturnFalse()
    //    {
    //        Assert.IsFalse(
    //            Vector3.TryParse("1 2", out var _)
    //        );
    //    }

    //    [TestCase("1 2 3", 1, 2, 3)]
    //    [TestCase("1,2,3", 1, 2, 3)]
    //    [TestCase("1;2;3", 1, 2, 3)]
    //    [TestCase("1.123 2.234 3.345", 1.123f, 2.234f, 3.345f)]
    //    [TestCase("1.123,2.234,3.345", 1.123f, 2.234f, 3.345f)]
    //    [TestCase("1.123;2.234;3.345", 1.123f, 2.234f, 3.345f)]
    //    public void TryParse_ValidVecStrings_ShouldParse(string vecStr, float xExp, float yExp, float zExp)
    //    {
    //        var expVec = new Vector3(xExp, yExp, zExp);

    //        Assert.Multiple(() =>
    //        {
    //             Assert.IsTrue(Vector3.TryParse(vecStr, out var resVec));
    //             Assert.AreEqual(expVec, resVec);
    //        });

    //    }
    //}
}
