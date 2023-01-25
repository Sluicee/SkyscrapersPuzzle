using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Utils
{
    public static void shuffle(int[] num)
    {
        for (int x = 0; x < num.Length; x++)
            swap(num, x, Random.Range(0, num.Length));
    }

    public static void swap(int[] num, int a, int b)
    {
        int temp = num[a];
        num[a] = num[b];
        num[b] = temp;
    }

    public static int[] createOrderedArray(int size, int startValue)
    {
        int[] num = new int[size];
        for (int x = 0; x < num.Length; x++)
            num[x] = x + startValue;
        return num;
    }

    //Return TRUE if array vs arrays is COMPLETELY different
    public static bool compare2DArray(int[] num1, int[][] num2, int start, int end)
    {
        for (int x = start; x < end; x++)
            if (!compareArray(num1, num2[x]))
                return false;
        return true;
    }

    //Return TRUE if arrays are COMPLETELY different 
    public static bool compareArray(int[] num1, int[] num2)
    {
        if (num1.Length != num2.Length)
            return false;
        for (int x = 0; x < num1.Length; x++)
            if (num1[x] == num2[x])
                return false;
        return true;
    }
}
