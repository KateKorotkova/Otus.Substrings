using NUnit.Framework;
using Otus.SubStrings.Logic;

namespace Tests
{
    public class Tests
    {
        private static object[] _testCases =
        {
            new object[] { "test", "es", 1 },
            new object[] { "test", "abc", -1 },
            new object[] { "strongstring", "string", 6 },
            new object[] { "strongstring", "strings", -1 },
            new object[] { "strst.strstring", "string", 9 },
            new object[] { "abcd.fabcdeabcdef", "abcdef", 11 }
        };


        [TestCaseSource(nameof(_testCases))]
        public void Can_Get_Index_By_Full_Scan(string text, string mask, int expectedResult)
        {
            var searcher = new SubstringSearcher();

            var result = searcher.GetIndexByFullScan(text, mask);

            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [TestCaseSource(nameof(_testCases))]
        public void Can_Get_Index_By_Shift_Scan(string text, string mask, int expectedResult)
        {
            var searcher = new SubstringSearcher();

            var result = searcher.GetIndexByShiftScan(text, mask);

            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [TestCaseSource(nameof(_testCases))]
        public void Can_Get_Index_By_Shift_Array(string text, string mask, int expectedResult)
        {
            var searcher = new SubstringSearcher();

            var result = searcher.GetIndexByShiftArray(text, mask);

            Assert.That(result, Is.EqualTo(expectedResult));
        }
    }
}