using MTools.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MTools.Services
{    
    public class AlphabetizeService : IAlphabetizeService
    {
        private char[] _priorityChars = new char[] {'{', ':', ';'};

        public string[] AlphabetizeArray(ref string[] array)
        {
            // Look at every string in the array to determin it's place
            for (int forward = 0; forward < array.Length; forward++)
            {
                // Initially place the string where it started
                int sortedIndex = forward;
                string line = array[forward];

                // Goe back through the array to see if it should go before anything
                for (int backwards = forward; backwards >= 0; backwards--)
                {
                    if (backwards == 0)
                    {
                        sortedIndex = 0;
                    }
                    else if (!GoesBeforeAlphabetically(array[backwards - 1], line))
                    {
                        sortedIndex = backwards;
                        break;
                    }                    
                }

                // Go from where the string originally was backwards
                // Shift any words that should come after 1 to the right
                for (int backwardsSort = forward; backwardsSort > sortedIndex; backwardsSort--)
                {
                    array[backwardsSort] = array[backwardsSort - 1];
                }

                // Place the word in it's rightful spot
                array[sortedIndex] = line;
            }

            return array;
        }

        public IEnumerable<string> AlphabetizeArray(ref IEnumerable<string> list)
        {
            string[] listAsArray = list.ToArray();
            string[] alphabetizedArray = AlphabetizeArray(ref listAsArray).ToArray();

            list = alphabetizedArray;

            return list;
        }

        private bool GoesBeforeAlphabetically(string existingWord, string contender)
        {
            if (existingWord == null && contender == null)
            {
                return false;
            }
            else if (existingWord == null && contender != null)
            {
                return false;
            }
            else if (existingWord != null && contender == null)
            {
                return true;
            }

            existingWord = existingWord.ToLower().Trim();
            contender = contender.ToLower().Trim();

            for (int i = 0; i < existingWord.Length; i++)
            {
                if (contender.Length <= i)
                {
                    // Reached the end of contender and failed to find any char that goes after: contender goes before
                    return true;
                }

                if (contender[i] == existingWord[i])
                {
                    // Draw, keep looking
                    continue;
                }

                if (!char.IsLetter(contender[i]) && char.IsLetter(existingWord[i]))
                {
                    // Contender has a symbol or number where existing word has a letter: contender goes first
                    return true;
                }

                if (char.IsLetter(contender[i]) && !char.IsLetter(existingWord[i]))
                {
                    // Contender has a symbol or number where existing word has a letter: contender goes first
                    return false;
                } 

                if (_priorityChars.Contains(contender[i]) && !_priorityChars.Contains(existingWord[i]))
                {
                    // Contender contains a custom char we have flagged as pioroity and existing word doesn't contender goes before
                    return true;
                }

                if (!_priorityChars.Contains(contender[i]) && _priorityChars.Contains(existingWord[i]))
                {
                    // Existing word contains a custom char we have flagged as pioroity and contender doesn't: contender goes after
                    return false;
                }

                if (_priorityChars.Contains(contender[i]) && _priorityChars.Contains(existingWord[i]))
                {
                    if (Array.IndexOf(_priorityChars, contender[i]) < Array.IndexOf(_priorityChars, existingWord[i]))
                    {
                        // Contender contains a priority char with a lower index: contender comes first
                        return true;
                    }
                    else if (Array.IndexOf(_priorityChars, contender[i]) > Array.IndexOf(_priorityChars, existingWord[i]))
                    {
                        // Contender does contain a priority char but it's now lower than existing word: contender comes after
                        return false;
                    }
                }

                if ((int)contender[i] < (int)existingWord[i])
                {
                    // Found a char that comes first: contender goes before
                    return true;
                }

                if ((int)contender[i] > (int)existingWord[i])
                {
                    // Found a char that doesn't come first: contender goes after
                    return false;
                }

                throw new Exception("Unhandled comparison case in AlphabetizeService");
            }

            // Reached the end of the word with no winner, draw.. so doesn't technically come before
            return false;
        }
    }
}
