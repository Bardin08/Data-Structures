using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataStructs.Structs;
using System.Collections.Generic;

namespace DataStructs.Structs.Tests
{
    [TestClass()]
    public class ArrayListTests
    {
        [TestMethod("Empty constructor test")]
        public void ArrayListTest()
        {
            ArrayList<int> testList = new();

            Assert.IsTrue(testList is not null);
        }

        [TestMethod("Constructor with given capacity test")]
        public void ArrayListTest1()
        {
            ArrayList<int> testList = new(10);

            Assert.IsTrue(testList.Count is 0);
            Assert.IsTrue(testList.Capacity is 10);
        }

        [TestMethod("Constructor with given collection test")]
        public void ArrayListTest2()
        {
            List<int> inputList = new() { 1, 2, 3, 5, 6 };

            ArrayList<int> outputList = new(inputList);

            Assert.IsTrue(outputList.Count.Equals(inputList.Count));

            for (int i = 0; i < outputList.Count; ++i)
            {
                Assert.IsTrue(outputList[i] == inputList[i]);
            }
        }

        [TestMethod("IndexOf test")]
        public void IndexOfTest()
        {
            ArrayList<int> testList = new() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            for (int i = 0; i < testList.Count; ++i)
            {
                Assert.IsTrue(testList.IndexOf(testList[i]).Equals(i));
            }
        }

        [TestMethod("Insert test")]
        public void InsertTest()
        {
            ArrayList<int> testList = new();

            testList.Insert(0, 1);
            testList.Insert(0, 2);

            Assert.IsTrue(testList[0] == 2);
            Assert.IsTrue(testList[1] == 1);
        }

        [TestMethod("Remove test")]
        public void RemoveAtTest()
        {
            ArrayList<int> testList = new() { 1, 2, 3, 4, 5, 6, 7 };

            testList.RemoveAt(0);

            Assert.IsTrue(testList.Count is 6);
            Assert.IsTrue(testList[0] is 2);
        }

        [TestMethod("Add element test")]
        public void AddTest()
        {
            ArrayList<int> testList = new() { 1, 2, 3, 4, 5, 6, 7 };

            testList.Add(8);

            Assert.IsTrue(testList[7] is 8);
            Assert.IsTrue(testList.Count is 8);
        }

        [TestMethod("Clear test")]
        public void ClearTest()
        {
            ArrayList<int> testList = new() { 1, 2, 3, 4, 5, 6, 7 };

            testList.Clear();

            Assert.IsTrue(testList.Count is 0);
            Assert.IsTrue(testList.Capacity is not 0);
        }

        [TestMethod("Contains test")]
        public void ContainsTest()
        {
            ArrayList<int> testList = new() { 1, 2, 3, 4, 5, 6, 7 };

            foreach (int el in testList)
            {
                Assert.IsTrue(testList.Contains(el));
            }
        }

        [TestMethod("Copy to another array test")]
        public void CopyToTest()
        {
            ArrayList<int> testList = new() { 1, 2, 3, 4, 5, 6, 7 };

            int[] testArray = new int[7];

            testList.CopyTo(testArray);

            for (int i = 0; i < testList.Count; ++i)
            {
                Assert.IsTrue(testList[i] == testArray[i]);
            }
        }

        [TestMethod("Copy to another array from index test")]
        public void CopyToTest1()
        {
            ArrayList<int> testList = new() { 1, 2, 3, 4, 5, 6, 7 };

            int[] testArray = new int[8];

            testList.CopyTo(testArray, 1);

            for (int i = 0; i < testList.Count; ++i)
            {
                Assert.IsTrue(testList[i] == testArray[i + 1]);
            }
        }

        [TestMethod("Remove by element test")]
        public void RemoveTest()
        {
            ArrayList<int> testList = new() { 1, 2, 3, 4, 5, 6, 7 };

            foreach (int el in testList)
            {
                testList.Remove(el);
                Assert.IsTrue(!testList.Contains(el));
            }

        }

        [TestMethod("Get enumerator test")]
        public void GetEnumeratorTest()
        {
            ArrayList<int> testList = new() { 1, 2, 3, 4, 5, 6, 7 };

            Assert.IsTrue(testList.GetEnumerator() is not null);
        }
    }
}