using System.Linq;

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

                var theSameCharactersCount = 0;
                for (var maskCounter = 0; maskCounter < mask.Length; maskCounter++)
                {
                    if (text[textCounter + maskCounter] == mask[maskCounter])
                    {
                        theSameCharactersCount++;
                    }
                    else
                    {
                        break;
                    }
                }

                if (theSameCharactersCount == mask.Length)
                    return textCounter;
            }

            return UnExistedSubstringIndex;
        }

        public int GetIndexByShiftScan(string text, string mask)
        {
            for (var textCounter = 0; textCounter < text.Length; textCounter++)
            {
                var leftCharactersInTextCount = text.Length - textCounter;
                if (leftCharactersInTextCount < mask.Length)
                    return UnExistedSubstringIndex;

                var lastMaskCharacterIndex = mask.Length - 1;

                if (!mask.Contains(text[textCounter + lastMaskCharacterIndex]))
                {
                    textCounter += lastMaskCharacterIndex;
                    continue;
                }

                var theSameCharactersCount = 0;
                for (var maskCounter = lastMaskCharacterIndex; maskCounter >= 0; maskCounter--)
                {
                    if (text[textCounter + maskCounter] == mask[maskCounter])
                    {
                        theSameCharactersCount++;
                    }
                    else
                    {
                        break;
                    }
                }

                if (theSameCharactersCount == mask.Length)
                    return textCounter;
            }

            return UnExistedSubstringIndex;
        }


        public int GetIndexByShiftArray(string text, string mask)
        {
            var shift = GenerateShiftArrayForAlphabet(mask);

            for (var textCounter = 0; textCounter < text.Length; textCounter++)
            {
                var leftCharactersInTextCount = text.Length - textCounter;
                if (leftCharactersInTextCount < mask.Length)
                    return UnExistedSubstringIndex;

                var lastMaskCharacterIndex = mask.Length - 1;

                //if (!mask.Contains(text[textCounter + lastMaskCharacterIndex]))
                //{
                //    textCounter = textCounter + lastMaskCharacterIndex;
                //    continue;
                //}

                var theSameCharactersCount = 0;
                for (var maskCounter = lastMaskCharacterIndex; maskCounter >= 0; maskCounter--)
                {
                    if (text[textCounter + maskCounter] == mask[maskCounter])
                    {
                        theSameCharactersCount++;
                    }
                    else
                    {
                        break;
                    }
                }

                if (theSameCharactersCount == mask.Length)
                    return textCounter;

                var unMatchedCharacterInText = text[textCounter + lastMaskCharacterIndex];
                textCounter = textCounter + shift[unMatchedCharacterInText] - 1;
            }

            return UnExistedSubstringIndex;
        }

        
        #region Support Methods

        private int[] GenerateShiftArrayForAlphabet(string mask)
        {
            var shift = Enumerable.Repeat(mask.Length, 128).ToArray();

            for (var i = 0; i < mask.Length - 1; i++)
            {
                shift[mask[i]] = 1;
            }

            return shift;
        }

        #endregion
    }
}
