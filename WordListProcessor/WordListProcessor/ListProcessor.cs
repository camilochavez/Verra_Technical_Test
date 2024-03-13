using System.Collections.Generic;
using System.Linq;

namespace WordListProcessor
{
    internal static class ListProcessor
    {
        const int WordMaxLenght = 6;
        public static IEnumerable<string> FindSixLetterWords(HashSet<string> wordList)
        {
            var wordsToPrint = new HashSet<string>();
            var fiveLetterWords = wordList.Where(word => word.Length <= WordMaxLenght - 1);
            fiveLetterWords.AsParallel().ForAll(firstWord =>
            {
                var wordsToConcat = fiveLetterWords.Where(otherWord => otherWord.Length == WordMaxLenght - firstWord.Length);
                wordsToConcat.AsParallel().ForAll(secondWord =>
                {
                    var wordTocheck = $"{firstWord}{secondWord}".ToLower();
                    if (wordList.Any(word => word.ToLower() == wordTocheck))
                        wordsToPrint.Add(wordTocheck);
                });
            });
            return wordsToPrint;
        }
    }
}
