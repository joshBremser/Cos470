using System;
using System.Collections.Generic;
using System.Diagnostics;
    class AggregateDataDollarAddresses
        {
            private string longestDollarWord;
            private string shortestDollarWord;
            private string mostExpensiveDollarWord;
            private int mostExpensiveDollarWordCost;
            private List<string> dollarWords;
            private Stopwatch calculationTime;
            private double averageLatitude;
            private double averageLongitude;

            public List<string> getDollarWords()
            {
                return dollarWords;
            }

            public void addAddress(Address address, int cents){
                AddressAttributes attributes = address.attributes;
                string fullAddress = attributes.ADDRESS_NUMBER + " " + attributes.STREETNAME + " " + attributes.SUFFIX;

                if (cents != attributes.ADDRESS_NUMBER) 
                    throw new Exception($"Word '{fullAddress}' should not be added due to only having {cents} cents.");
            

                if (cents > mostExpensiveDollarWordCost) 
                { 
                    mostExpensiveDollarWord = fullAddress; 
                    mostExpensiveDollarWordCost = cents; 
                }

                if (shortestDollarWord == null || fullAddress.Length < shortestDollarWord.Length) 
                    shortestDollarWord = fullAddress;
                if (longestDollarWord == null || fullAddress.Length > longestDollarWord.Length) 
                    longestDollarWord = fullAddress;

                dollarWords.Add(fullAddress);

                //Would be more efficient to keep a sum and then divide later, but this keeps the value up to date as things are added
                averageLatitude = (averageLatitude * (dollarWords.Count-1) + attributes.Latitude) / (dollarWords.Count);
                averageLongitude = (averageLongitude * (dollarWords.Count-1) + attributes.Longitude) / (dollarWords.Count);
            }

            public void startTimer()
            {
                calculationTime.Start();
            }

            public void endTimer()
            {
                calculationTime.Stop();
            }

            public AggregateDataDollarAddresses()
            {
                dollarWords = new List<string>();
                calculationTime = new Stopwatch();
            }

            override public string ToString()
            {
                string s = "";
                s += $"Longest dollar address was '{longestDollarWord}'";
                s += $"\nShortest dollar address was '{shortestDollarWord}'";
                s += $"\nMost expensive dollar address was '{mostExpensiveDollarWord}' for {mostExpensiveDollarWordCost} cents";
                s += $"\nTime taken {calculationTime.Elapsed.TotalMilliseconds}ms";
                s += $"\nThere were {dollarWords.Count} dollar addresses";
                s += $"\nAverage latitude/longitude for dollar addresses ({averageLatitude},{averageLongitude})";
                return s;
            }
        }