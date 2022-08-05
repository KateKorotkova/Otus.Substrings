namespace Otus.SubStrings.Logic
{
    public class SubstringSearcher
    {
        private const int UnExistedSubstringIndex = -1;


        public int GetIndexByFullScan(string text, string mask)
        {
            for (var textCounter = 0; textCounter < text.Length; textCounter++)
            {
                var leftCharactersInTextCount = text.Length - textCounter;
                if (leftCharactersInTextCount < mask.Length)
                    return UnExistedSubstringIndex;

                var counter = 0;
                for (var maskCounter = 0; maskCounter < mask.Length; maskCounter++)
                {
                    if (text[textCounter + maskCounter] == mask[maskCounter])
                    {
                        counter++;
                    }
                    else
                    {
                        break;
                    }
                }

                if (counter == mask.Length)
                    return textCounter;
            }

            return UnExistedSubstringIndex;
        }
    }
}
