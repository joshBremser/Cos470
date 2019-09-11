using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
namespace DollarWords
{
    class DollarWords
    {
        readonly static string path = "Assignment1/words.txt";
        private class AggregateDataDollarWords
        {
            private string longestDollarWord;
            private string shortestDollarWord;
            private string mostExpensiveDollarWord;
            private int mostExpensiveDollarWordCost;
            private List<string> exactDollarWords;
            private List<string> dollarWords;
            private Stopwatch calculationTime;

            public List<string> getExactDollarWords()
            {
                return exactDollarWords;
            }

            public List<string> getDollarWords()
            {
                return dollarWords;
            }


            public void addDollarWord(string word, int cents)
            {
                if (cents < 100) throw new Exception($"Word '{word}' should not be added due to only having {cents} cents.");
                dollarWords.Add(word);
                if (cents == 100) 
                    exactDollarWords.Add(word);
                if (cents > mostExpensiveDollarWordCost) 
                { 
                    mostExpensiveDollarWord = word; 
                    mostExpensiveDollarWordCost = cents; 
                }
                if (shortestDollarWord == null || word.Length < shortestDollarWord.Length) 
                    shortestDollarWord = word;
                if (longestDollarWord == null || word.Length > longestDollarWord.Length) 
                    longestDollarWord = word;
            }

            public void startTimer()
            {
                calculationTime.Start();
            }

            public void endTimer()
            {
                calculationTime.Stop();
            }

            public AggregateDataDollarWords()
            {
                exactDollarWords = new List<string>();
                dollarWords = new List<string>();
                calculationTime = new Stopwatch();

            }

            override public string ToString()
            {
                string s = "";
                s += $"Longest dollar word was '{longestDollarWord}'";
                s += $"\nShortest dollar word was '{shortestDollarWord}'";
                s += $"\nMost expensive dollar word was '{mostExpensiveDollarWord}' for {mostExpensiveDollarWordCost} cents";
                s += $"\nTime taken {calculationTime.Elapsed.TotalMilliseconds}ms";
                s += $"\nThere were {dollarWords.Count} dollar words";
                s += $"\nThere were {exactDollarWords.Count} exact dollar words";
                return s;
            }
        }

        private static int calculateCost(string word)
        {
            int cents = 0;
            foreach (char ch in word.ToLower().ToCharArray())
                if (ch >= 'a' && ch <= 'z') cents += ch - 'a' + 1;
            return cents;
        }
        private static AggregateDataDollarWords calculateDollarWords(StreamReader reader)
        {
            AggregateDataDollarWords aggregateData = new AggregateDataDollarWords();
            string word;
            int cents;
            aggregateData.startTimer();
            while ((word = reader.ReadLine()) != null)
            {
                cents = calculateCost(word);
                if (cents >= 100) 
                    aggregateData.addDollarWord(word, cents);
            }
            aggregateData.endTimer();
            return aggregateData;
        }

        static AggregateDataDollarWords openFileAndCalculateDollarWords(string path)
        {
            using (StreamReader reader = File.OpenText(path))
            {
                return calculateDollarWords(reader);
            }
        }

        static void outputAggregateData(AggregateDataDollarWords aggregateData)
        {
            Console.WriteLine(aggregateData.ToString());
            Console.Write("Enter x for exact dollar words, or d for dollar words, nothing to end: ");
            int input = Console.Read();
            switch (input)
            {
                case 'x':
                    foreach (string word in aggregateData.getDollarWords())
                        Console.WriteLine(word);
                    break;
                case 'd':
                    foreach (string word in aggregateData.getExactDollarWords())
                        Console.WriteLine(word);
                    break;
                default:
                    break;
            }
        }

        static void Main()
        {
            AggregateDataDollarWords aggregateData = openFileAndCalculateDollarWords(path);
            outputAggregateData(aggregateData);
            //Keep console open
            Console.Read();
        }
    }
}