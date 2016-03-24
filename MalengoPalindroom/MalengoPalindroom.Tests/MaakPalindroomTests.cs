using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MalengoPalindroom.Tests
{
    [TestClass]
    public class MaakPalindroomTests
    {
        static bool IsPalindrome(string value)
        {
            int min = 0;
            int max = value.Length - 1;
            while (true)
            {
                if (min > max)
                {
                    return true;
                }
                char a = value[min];
                char b = value[max];
                if (char.ToLower(a) != char.ToLower(b))
                {
                    return false;
                }
                min++;
                max--;
            }
        }

        [TestMethod]
        public void MaakPalindroomTest()
        {
            //palindroom pal = new palindroom(5);
            palindroom pal = new palindroom(5);
            pal.maakVolgendePalindroom();
            Assert.AreEqual(new string('a', 5), pal.palindroomString);
            //
            pal = null;
        }
        [TestMethod]
        public void MaakPalindroomTrueTest()
        {
            palindroom pal = new palindroom(5);
            while (!pal.laatste)
            {
                pal.maakVolgendePalindroom();
                Assert.IsTrue(IsPalindrome(pal.palindroomString));
            }

            //
            pal = null;
        }
        [TestMethod]
        public void aantal_3()
        {
            palindroom pal = new palindroom(3);
            while (!pal.laatste)
            {
                pal.maakVolgendePalindroom();
            }

           //26 * 26
            Assert.AreEqual(676, pal.aantal);
            //
            pal = null;
        }
        [TestMethod]
        public void aantal_6()
        {
            palindroom pal = new palindroom(6);
            while (!pal.laatste)
            {
                pal.maakVolgendePalindroom();
            }
            //26 * 26 * 26
            Assert.AreEqual(17576, pal.aantal);
            //
            pal = null;
        }
        [TestMethod]
        public void aantal_7()
        {
            palindroom pal = new palindroom(7);
            while (!pal.laatste)
            {
                pal.maakVolgendePalindroom();
            }
            //26 * 26 * 26 * 26
            Assert.AreEqual(456976, pal.aantal);
            //
            pal = null;
        }
   }
}
