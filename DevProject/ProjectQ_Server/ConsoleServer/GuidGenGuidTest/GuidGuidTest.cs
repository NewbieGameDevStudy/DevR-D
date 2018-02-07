using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Threading;

namespace GuidGen.GuidTest
{
    [TestClass()]
    public class GuidGuidTest
    {
        [TestMethod()]
        public void GuidCreateGuidTest()
        {
            var guid = new Guid(1);
            Dictionary<ulong, bool> testDictionary = new Dictionary<ulong, bool>();

            int index = 0;
            while (true) {
                index++;
                Thread.Sleep(1);
                var guidValue = guid.GuidCreate();
                if (testDictionary.ContainsKey(guidValue)) {
                    Console.WriteLine("인덱스 : " + index);
                    Assert.Fail();
                }

                if(index == 1000) {
                    Assert.IsTrue(true);
                    break;
                }
                testDictionary.Add(guidValue, true);
            }
        }
    }
}