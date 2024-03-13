using System.Collections.Generic;
using System.Linq;

namespace WordListProcessor
{
    internal class Program
    {
        static void Main(string[] args)
        {        
            HashSet<string> wordList = WordList.wordlist.Split("\n").ToHashSet();
            ListProcessor.FindSixLetterWords(wordList);
            foreach (string word in wordList)
            {
                System.Console.WriteLine(word);
            }
        }        
    }
}
