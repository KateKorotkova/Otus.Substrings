using System;
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

        public int GetIndexByBoyerMooreAlgorithm(string text, string mask)
        {
            var badCharactersTable = GenerateBadCharactersTable(mask);
            var offsetTable = GenerateGoodOffsetsTable(mask);

            var lastMaskCharacterIndex = mask.Length - 1;

            for (var i = lastMaskCharacterIndex; i < text.Length;)
            {
                var j = 0;
                for (j = lastMaskCharacterIndex; mask[j] == text[i]; i--, j--)
                {
                    if (j == 0)
                    {
                        return i;
                    }
                }

                i += Math.Max(offsetTable[lastMaskCharacterIndex - j], badCharactersTable[text[i]]);
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

        private int[] GenerateBadCharactersTable(string mask)
        {
            var table = Enumerable.Repeat(mask.Length, 128).ToArray();

            for (var i = 0; i < mask.Length; i++)
            {
                table[mask[i]] = mask.Length - 1 - i;
            }

            return table;
        }

        private int[] GenerateGoodOffsetsTable(string mask)
        {
            var offsetTable = new int[mask.Length];

            var lastPrefixPosition = mask.Length;
            for (var i = mask.Length; i > 0; i--)
            {
                if (IsPrefix(mask, i))
                {
                    lastPrefixPosition = i;
                }

                offsetTable[mask.Length - i] = lastPrefixPosition - i + mask.Length;
            }

            for (var i = 0; i < mask.Length - 1; ++i)
            {
                var suffixLength = GetSuffixLength(mask, i);
                
                offsetTable[suffixLength] = mask.Length - 1 - i + suffixLength;
            }

            return offsetTable;
        }

        //проверяет, что подстрока, начиная с индекса possiblePrefixStartIndex и до конца маски, уже есть в этой маске
        //маска = bc.abc.bc.c.abc, p = 13, проверяем, что bc.abc.bc.c.a[bc] встречается в начале маски
        // (bc).abc.bc.c.a[bc]
        private bool IsPrefix(string mask, int possiblePrefixStartIndex)
        {
            for (int i = possiblePrefixStartIndex, j = 0; i < mask.Length; ++i, ++j)
            {
                if (mask[i] != mask[j])
                {
                    return false;
                }
            }
            return true;
        }

        //private int GetSuffixLength(string mask, int suffixStartIndex)
        //{
        //    var length = 0;

        //    for (int i = suffixStartIndex, j = mask.Length - 1;
        //        i >= 0 && mask[i] == mask[j]; i--, j--)
        //    {
        //        length++;
        //    }

        //    return length;
        //}

        private int GetSuffixLength(string mask, int suffixStartIndex)
        {
            var length = 0;

            for (int i = suffixStartIndex, j = mask.Length - 1; i >= 0; i--, j--)
            {
                if (mask[i] != mask[j])
                {
                    break;
                }

                length++;
            }

            return length;
        }

        #endregion
    }
}
