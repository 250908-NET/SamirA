

using System;
using System.Collections.Generic;

namespace _8_LoopsChallenge
{
    public class Program
    {
        public static void Main(string[] args)
        {
            /* Your code here */

        }

        /// <summary>
        /// Return the number of elements in the List<int> that are odd.
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static int UseFor(List<int> x)
        {
            // throw new NotImplementedException("UseFor() is not implemented yet.");
            int numOdds= 0;
            for (int i = 0; i < x.Count; i++)
            {
                if (x[i] % 2 != 0)
                {
                    numOdds++;
                }
            }
            return numOdds;
        }

        /// <summary>
        /// This method counts the even entries from the provided List<object> 
        /// and returns the total number found.
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static int UseForEach(List<object> x)
        {
            // throw new NotImplementedException("UseForEach() is not implemented yet.");
            int count = 0;
            foreach (object n in x)
            {
                try
                {
                    if (n.GetType() != typeof(char) && Convert.ToInt64(n) % 2L == 0L)
                        count++;

                }
                catch 
                {
                }

            }

            return count;
           
        }

        /// <summary>
        /// This method counts the multiples of 4 from the provided List<int>. 
        /// Exit the loop when the integer 1234 is found.
        /// Return the total number of multiples of 4.
        /// </summary>
        /// <param name="x"></param>
        public static int UseWhile(List<int> x)
        {
            //throw new NotImplementedException("UseFor() is not implemented yet.");

            int numOfMultiples = 0;
            int i = 0;
            while (i < x.Count && x[i] != 1234)
            {
                if (x[i] % 4 == 0)
                {
                    numOfMultiples++;
                }

                i++;

            }
            return numOfMultiples;
        }

        /// <summary>
        /// This method will evaluate the Int Array provided and return how many of its 
        /// values are multiples of 3 and 4.
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static int UseForThreeFour(int[] x)
        {
            // throw new NotImplementedException("UseForThreeFour() is not implemented yet.");
            int numOfMultiples = 0;
            int i = 0;
            do
            {
                if (x[i] % 3 == 0 && x[i] % 4 == 0)
                {
                    numOfMultiples++;
                }

                i++;

            } while (i < x.Length);

            return numOfMultiples;
        }

        /// <summary>
        /// This method takes an array of List<string>'s. 
        /// It concatenates all the strings, with a space between each, in the Lists and returns the resulting string.
        /// </summary>
        /// <param name="stringListArray"></param>
        /// <returns></returns>
        public static string LoopdyLoop(List<string>[] stringListArray)
        {
            // throw new NotImplementedException("LoopdyLoop() is not implemented yet.");
            string result = "";
            foreach (var list in stringListArray)
            {
                foreach (string word in list)
                {
                    result += word + " ";
                }
            }

            return result;
        }
    }
}

