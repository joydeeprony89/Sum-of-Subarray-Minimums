using System;
using System.Collections.Generic;

namespace Sum_of_Subarray_Minimums
{
  class Program
  {
    static void Main(string[] args)
    {
      var A = new int[] { 2, 9, 7, 8, 3, 4, 6, 1 };
      Solution s = new Solution();
      var answer = s.SumSubarrayMins(A);
      Console.WriteLine(answer);
    }
  }

  public class Solution
  {
    public int SumSubarrayMins(int[] A)
    {
      // initialize previous less element and next less element of 
      // each element in the array

      Stack<int[]> previousLess = new Stack<int[]>();
      Stack<int[]> nextLess = new Stack<int[]>();
      long[] leftDistance = new long[A.Length];
      long[] rightDistance = new long[A.Length];

      for (int i = 0; i < A.Length; i++)
      {
        // use ">=" to deal with duplicate elements
        while (previousLess.Count > 0 && previousLess.Peek()[0] >= A[i])
        {
          previousLess.Pop();
        }

        leftDistance[i] = previousLess.Count == 0 ? i + 1 : i - previousLess.Peek()[1];
        previousLess.Push(new int[] { A[i], i });
      }

      for (int i = A.Length - 1; i >= 0; i--)
      {
        while (nextLess.Count > 0 && nextLess.Peek()[0] > A[i])
        {
          nextLess.Pop();
        }

        rightDistance[i] = nextLess.Count == 0 ? A.Length - i : nextLess.Peek()[1] - i;
        nextLess.Push(new int[] { A[i], i });
      }

      long ans = 0;
      long mod = 1000000007;
      for (int i = 0; i < A.Length; i++)
      {
        // since we want to add the minimum from each subarray.
        // 9, 7, 8, 3, 4, 6
        // How many subarrays with 3 being its minimum value?
        // The answer is (4 * 3).
        /*
         *  9 7 8 3 
            9 7 8 3 4 
            9 7 8 3 4 6 
            7 8 3 
            7 8 3 4 
            7 8 3 4 6 
            8 3 
            8 3 4 
            8 3 4 6 
            3 
            3 4 
            3 4 6
         */
        // How much the element 3 contributes to the final answer?
        // It is 3 * (4 * 3) => A[i] * leftDistance[i] * rightDistance[i])
        // In this case in the 12 subarrays, 3 is the minimum, so it will contribute 3*12 i.e. 36.
        ans = (ans + A[i] * leftDistance[i] * rightDistance[i]);
      }
      return (int)(ans % mod);
    }
  }
}
