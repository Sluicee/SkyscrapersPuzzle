using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text.RegularExpressions;

public class Config : MonoBehaviour
{

    private static string dir = Directory.GetCurrentDirectory();

    private static string file = @"\board_data.ini";
    private static string path = dir + file;

    public static void DeleteDataFile()
    {
        if (File.Exists(path))
        {
            File.Delete(path);
        }
    }

    public static void Save()
    {
        File.WriteAllText(path, string.Empty);
        StreamWriter sw = new StreamWriter(path, true);
        string solved4 = "#solved4:";
        string solved5 = "#solved5:";
        string solved6 = "#solved6:";
        string avg_time = "#avg_time:";
        string errors = "#errors:";

        sw.WriteLine(solved4);
        sw.WriteLine(solved5);
        sw.WriteLine(solved6);
        sw.WriteLine(avg_time);
        sw.WriteLine(errors);

        sw.Close();
    }

    public static int ReadSolved4(int size)
    {
        int solved = 0;
        string line;
        string keyword = "#solved4";

        switch (size)
        {
            case 4:
                keyword = "#solved4";
                break;
            case 5:
                keyword = "#solved5";
                break;
            case 6:
                keyword = "#solved6";
                break;
        }

        StreamReader sr = new StreamReader(path);

        while ((line = sr.ReadLine()) != null)
        {
            string[] parts = line.Split(':');
            if(parts[0] == keyword)
            {
                int.TryParse(parts[1], out solved);
            }
        }

        sr.Close();
        return solved;
    }
}
