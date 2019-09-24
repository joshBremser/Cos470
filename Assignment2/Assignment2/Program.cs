using System;
using System.IO;
using System.Collections.Generic;
using System.Configuration;
using System.Collections.Specialized;
using System.Diagnostics;

namespace Assignment2.Assignment2
{
    public class DollarWords
    {
        //Public so I can test it
        public static int calculateCost(string word)
        {
            int cents = 0;
            foreach (char ch in word.ToLower().ToCharArray())
                if (ch >= 'a' && ch <= 'z') cents += ch - 'a' + 1;
            return cents;
        }

        private static AggregateDataDollarAddresses calculateDollarAddresses(List<Address> addresses)
        {
            AggregateDataDollarAddresses aggregateData = new AggregateDataDollarAddresses();
            string fullAddress;
            int cents;
            aggregateData.startTimer();
            foreach (Address address in addresses)
            {
                fullAddress = address.attributes.STREETNAME + " " + address.attributes.SUFFIX;
                cents = calculateCost(fullAddress);
                if (cents == address.attributes.ADDRESS_NUMBER)
                    aggregateData.addAddress(address, cents);
            }
            aggregateData.endTimer();
            return aggregateData;
        }

        static void outputAggregateData(AggregateDataDollarAddresses aggregateData)
        {
            Console.WriteLine(aggregateData.ToString());
            foreach (string word in aggregateData.getDollarWords())
                Console.WriteLine(word);
        }

        static void saveList(List<string> list, string filename)
        {
            using (TextWriter tw = new StreamWriter(filename))
                foreach (string word in list)
                    tw.WriteLine(word);
        }

        static void Main()
        {
            string url = ConfigurationManager.AppSettings.Get("url");
            string query = ConfigurationManager.AppSettings.Get("query");
            Console.WriteLine($"Trying to contact: {url}\nWith query: {query}");
            string outputFilename = ConfigurationManager.AppSettings.Get("outFilename");
            List<Address> addresses = AddressServiceMethods.GetAddresses(url, query);
            AggregateDataDollarAddresses aggregate = calculateDollarAddresses(addresses);
            outputAggregateData(aggregate);
            saveList(aggregate.getDollarWords(), outputFilename);
            //Keep console open
            Console.Read();
        }
    }
}