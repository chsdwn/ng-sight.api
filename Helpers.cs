using System;
using System.Collections.Generic;

namespace Advantage.API
{
    public class Helpers
    {
        private static readonly List<string> dummyNames = new List<string>()
        {
            "Ahmet",
            "Mehmet",
            "Ali",
            "Veli",
            "Hasan",
            "Hüseyin"
        };

        private static readonly List<string> dummySurnames = new List<string>()
        {
            "Şahin",
            "Yılmaz",
            "Kartal",
            "Atay"
        };

        private static readonly List<string> dummyStates = new List<string>()
        {
            "Urfa",
            "Kayseri",
            "Sivas",
            "Ankara",
            "Konya"
        };

        private static Random rand = new Random();
        private static string name;
        private static string surname;

        private static string GetRandom(IList<string> items)
        {
            return items[rand.Next(items.Count)];
        }

        internal static DateTime GetRandomOrderPlaced()
        {
            var end = DateTime.Now;
            var start = end.AddDays(-90);

            TimeSpan possibleSpan = end - start;
            TimeSpan newSpan = new TimeSpan(0, rand.Next(0, (int)possibleSpan.TotalMinutes), 0);

            return start + newSpan;
        }

        internal static DateTime? GetRandomOrderCompleted(DateTime orderPlaced)
        {
            var now = DateTime.Now;
            var minLeadTime = TimeSpan.FromDays(7);
            var timePassed = now - orderPlaced;

            if(timePassed < minLeadTime)
            {
                return null;
            }

            return orderPlaced.AddDays(rand.Next(7, 14));
        } 

        internal static decimal GetRandomOrderTotal()
        {
            return rand.Next(100, 5000);
        }

        internal static string GetRandomState()
        {
            return GetRandom(dummyStates);
        }

        internal static string MakeUniqueCustomerName(List<string> names)
        {
            var maxNames = dummyNames.Count * dummySurnames.Count;

            if(names.Count >= maxNames)
            {
                throw new System.InvalidOperationException("Maximum number of unique names exceeded");
            }

            name = GetRandom(dummyNames);
            surname = GetRandom(dummySurnames);

            var fullname = name + " " + surname;

            if(names.Contains(fullname))
            {
                MakeUniqueCustomerName(names);
            }

            return fullname;
        }

        internal static string MakeCustomerEmail()
        {
            return (surname + name + "@mail.com").ToLower();
        }
    }
}