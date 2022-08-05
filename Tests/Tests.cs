using NUnit.Framework;
using Otus.SubStrings.Logic;

namespace Tests
{
    public class Tests
    {
        [TestCase("test", "es", ExpectedResult = 1)]
        [TestCase("test", "abc", ExpectedResult = -1)]
        [TestCase("strongstring", "string", ExpectedResult = 6)]
        [TestCase("strongstring", "strings", ExpectedResult = -1)]
        public int Can_Get_Index_By_Full_Scan(string text, string mask)
        {
            var searcher = new SubstringSearcher();

            return searcher.GetIndexByFullScan(text, mask);
        }
    }
}