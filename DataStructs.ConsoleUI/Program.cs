using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

using DataStructs.Structs;

namespace DataStructs.ConsoleUI
{
    class Program
    {
        private static Stopwatch _timer = new();
        private static ArrayList<int> _itemsArrayList = new();
        private static List<int> _itemsList = new();

        static void Main(string[] args)
        {
            System.Console.WriteLine("ARRAYLIST: Empty list size is " + GetSize(_itemsArrayList) + " byte(-s)");
            System.Console.WriteLine("LIST: Empty list size is " + GetSize(_itemsList) + " byte(-s)");


            long ms = AddMillionElementsToTheEnd(_itemsArrayList);
            System.Console.WriteLine("ARRAYLIST: 10^6 were added to the beginning of the list in " + ms + " ms. Array size is " + GetSize(_itemsArrayList) + " bytes.");

            ms = AddMillionElementsToTheEnd(_itemsList);
            System.Console.WriteLine("LIST: 10^6 were added to the beginning of the list in " + ms + " ms. Array size is " + GetSize(_itemsList) + " bytes.");

            _itemsArrayList.Clear();
            _itemsList.Clear();


            ms = AddMillionElementsToTheMiddle(_itemsArrayList);
            System.Console.WriteLine("ARRAYLIST: 10^6 were added elements to the middle of the list in " + ms + " ms. Array size is " + GetSize(_itemsArrayList) + " bytes.");

            ms = AddMillionElementsToTheMiddle(_itemsList);
            System.Console.WriteLine("LIST: 10^6 were added elements to the middle of the list in " + ms + " ms. Array size is " + GetSize(_itemsList) + " bytes.");

            
            _itemsArrayList.Clear();
            _itemsList.Clear();

            ms = RemoveMillionElements(_itemsArrayList);
            System.Console.WriteLine("ARRAYLIST: 10^6 elements deleted from the list in " + ms + " ms. Array size is " + GetSize(_itemsArrayList) + " bytes.");

            ms = RemoveMillionElements(_itemsList);
            System.Console.WriteLine("LIST: 10^6 elements deleted from the list in " + ms + " ms. Array size is " + GetSize(_itemsList) + " bytes.");
        }

        private static long RemoveMillionElements(IList<int> list)
        {
            _timer.Reset();
            _timer.Start();

            while (list.Count > 0)
            {
                list.RemoveAt(0);
            }

            _timer.Stop();

            return _timer.ElapsedMilliseconds;

        }

        private static long AddMillionElementsToTheMiddle(IList<int> list)
        {
            _timer.Reset();
            _timer.Start();

            for (int i = 0; i < 1_000_000; ++i)
            {
                list.Insert(list.Count / 2, i);
            }

            _timer.Stop();

            return _timer.ElapsedMilliseconds;
        }

        private static long AddMillionElementsToTheEnd(IList<int> list)
        {
            _timer.Reset();
            _timer.Start();

            for (int i = 0; i < 1_000_000; ++i)
            {
                list.Add(i);
            }
            
            _timer.Stop();

            return _timer.ElapsedMilliseconds;
        }

        private static long GetSize<T>(T obj)
        {
            long size = 0;
            using (Stream s = new MemoryStream())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(s, obj);
                size = s.Length;
            }

            return size;
        }

        private static void PrintCollection(IEnumerable collection)
        {
            foreach (var e in collection)
            {
                System.Console.Write($"{e} ");
            }
        }
    }
}
