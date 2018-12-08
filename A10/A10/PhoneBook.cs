using System;
using System.Linq;
using System.Collections.Generic;
using TestCommon;

namespace A10
{
    public class Contact
    {
        public string Name;
        public int Number;

        public Contact(string name, int number)
        {
            Name = name;
            Number = number;
        }
    }

    public class PhoneBook : Processor
    {
        public PhoneBook(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<string[], string[]>)Solve);

        List<Contact>[] PhoneBookList;

        public string[] Solve(string[] commands)
        {
            PhoneBookList = new List<Contact>[1000];
            List<string> result = new List<string>();
            foreach (var cmd in commands)
            {
                var toks = cmd.Split();
                var cmdType = toks[0];
                var args = toks.Skip(1).ToArray();
                int number = int.Parse(args[0]);
                switch (cmdType)
                {
                    case "add":
                        Add(args[1], number);
                        break;
                    case "del":
                        Delete(number);
                        break;
                    case "find":
                        result.Add(Find(number));
                        break;
                }
            }
            return result.ToArray();
        }

        public void Add(string name, int number)
        {
            var numberHash = GetHash(number);
            if (PhoneBookList[numberHash] == null)
                PhoneBookList[numberHash] = new List<Contact>();
            for (int i = 0; i < PhoneBookList[numberHash].Count; i++)
                if (PhoneBookList[numberHash][i].Number == number)
                {
                    PhoneBookList[numberHash][i].Name = name;
                    return;
                }
            PhoneBookList[numberHash].Add(new Contact(name, number));
        }

        public string Find(int number)
        {
            var numberHash = GetHash(number);
            if (PhoneBookList[numberHash] == null)
                return "not found";
            for (int i = 0; i < PhoneBookList[numberHash].Count; i++)
                if (PhoneBookList[numberHash][i].Number == number)
                    return PhoneBookList[numberHash][i].Name;
            return "not found";
        }

        public void Delete(int number)
        {
            var numberHash = GetHash(number);
            if (PhoneBookList[numberHash] == null)
                return;
            for (int i = 0; i < PhoneBookList[numberHash].Count; i++)
            {
                if (PhoneBookList[numberHash][i].Number == number)
                {
                    PhoneBookList[numberHash].RemoveAt(i);
                    return;
                }
            }
            return;
        }

        public int GetHash(int number)
        {
            number *= 43;
            number += 13;
            number = number % 10000019;
            number = number % 1000;
            return number;
        }
    }
}
