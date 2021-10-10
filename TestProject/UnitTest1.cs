using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using ClassLibrary;

namespace TestProject
{
    [TestClass]
    public class UnitTest1
    {
        /// <summary>
        /// -----------Below is preparing for test to use
        /// -----------func : generate one 3 * 3 matrix;
        /// </summary>
        List<List<int>> testMap = new List<List<int>>
        {
            new List<int>{ 1, 1, 1 },
            new List<int>{ 1, 1, 1 },
            new List<int>{ 1, 1, 1 }
        };

        /// <summary>
        /// ------------Test_live_count_1() ~~~ Test_live_count_9()
        /// ------------test all 9 cells's live_count()
        /// </summary>
        [TestMethod]
        public void Test_live_count_1()
        {
            Map map = new Map();
            map.receive_map(ref testMap);
            int actual = map.live_count(0, 0);

            int expected = 3;

            Assert.AreEqual(expected, actual, 0.001, "function live_count() returns a wrong value");
        }

        [TestMethod]
        public void Test_live_count_2()
        {
            Map map = new Map();
            map.receive_map(ref testMap);
            int actual = map.live_count(0, 1);

            int expected = 5;

            Assert.AreEqual(expected, actual, 0.001, "function live_count() returns a wrong value");
        }

        [TestMethod]
        public void Test_live_count_3()
        {
            Map map = new Map();
            map.receive_map(ref testMap);
            int actual = map.live_count(0, 2);

            int expected = 3;

            Assert.AreEqual(expected, actual, 0.001, "function live_count() returns a wrong value");
        }

        [TestMethod]
        public void Test_live_count_4()
        {
            Map map = new Map();
            map.receive_map(ref testMap);
            int actual = map.live_count(1, 0);

            int expected = 5;

            Assert.AreEqual(expected, actual, 0.001, "function live_count() returns a wrong value");
        }

        [TestMethod]
        public void Test_live_count_5()
        {
            Map map = new Map();
            map.receive_map(ref testMap);
            int actual = map.live_count(1, 1);

            int expected = 8;

            Assert.AreEqual(expected, actual, 0.001, "function live_count() returns a wrong value");
        }

        [TestMethod]
        public void Test_live_count_6()
        {
            Map map = new Map();
            map.receive_map(ref testMap);
            int actual = map.live_count(1, 2);

            int expected = 5;

            Assert.AreEqual(expected, actual, 0.001, "function live_count() returns a wrong value");
        }

        [TestMethod]
        public void Test_live_count_7()
        {
            Map map = new Map();
            map.receive_map(ref testMap);
            int actual = map.live_count(2, 0);

            int expected = 3;

            Assert.AreEqual(expected, actual, 0.001, "function live_count() returns a wrong value");
        }

        [TestMethod]
        public void Test_live_count_8()
        {
            Map map = new Map();
            map.receive_map(ref testMap);
            int actual = map.live_count(2, 1);

            int expected = 5;

            Assert.AreEqual(expected, actual, 0.001, "function live_count() returns a wrong value");
        }

        [TestMethod]
        public void Test_live_count_9()
        {
            Map map = new Map();
            map.receive_map(ref testMap);
            int actual = map.live_count(2, 2);

            int expected = 3;

            Assert.AreEqual(expected, actual, 0.001, "function live_count() returns a wrong value");
        }

        /// <summary>
        /// ------------Test_refresh_1() ~~~ Test_refresh_4()
        /// ------------test all 3 kinds of changing situations
        /// </summary>
        int live = 1, die = 0;
        [TestMethod]
        public void Test_refresh_1()    /// (< 2) --> die
        {
            List<List<int>> vs = new List<List<int>>
            {
                new List<int> { 1, 0, 0 },
                new List<int> { 0, 1, 0 },
                new List<int> { 0, 0, 0 }
            };

            Map map = new Map();
            map.receive_map(ref vs);
            map.refresh();

            Assert.AreEqual(die, map.map[1][1]);
            Assert.AreEqual(die, map.map[0][0]);
        }
        [TestMethod]
        public void Test_refresh_2()    /// (== 2) --> keep
        {
            List<List<int>> vs = new List<List<int>>
            {
                new List<int> { 0, 1, 0 },
                new List<int> { 0, 1, 0 },
                new List<int> { 0, 1, 0 }
            };

            Map map = new Map();
            map.receive_map(ref vs);
            map.refresh();

            Assert.AreEqual(vs[1][1], map.map[1][1]);
            Assert.AreEqual(vs[0][0], map.map[0][0]);
        }
        [TestMethod]
        public void Test_refresh_3()    /// (== 3) --> live / reborn
        {
            List<List<int>> vs = new List<List<int>>
            {
                new List<int> { 0, 1, 0 },
                new List<int> { 0, 1, 1 },
                new List<int> { 0, 1, 0 }
            };

            Map map = new Map();
            map.receive_map(ref vs);
            map.refresh();

            Assert.AreEqual(live, map.map[1][0]);
            Assert.AreEqual(live, map.map[1][1]);
            Assert.AreEqual(live, map.map[1][2]);
        }
        [TestMethod]
        public void Test_refresh_4()    /// (> 3) --> die
        {
            List<List<int>> vs = new List<List<int>>
            {
                new List<int> { 0, 1, 0 },
                new List<int> { 1, 1, 1 },
                new List<int> { 0, 1, 0 }
            };

            Map map = new Map();
            map.receive_map(ref vs);
            map.refresh();

            Assert.AreEqual(die, map.map[1][1]);
        }
    }
}
