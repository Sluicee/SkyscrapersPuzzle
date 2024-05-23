using UnityEngine;

public static class Themes
{
    private static Color pink = new Color(0.867f, 0.31f, 0.557f);
    private static Color blue = new Color(0.341f, 0.31f, 0.867f);
    private static Color aqua = new Color(0.31f, 0.835f, 0.867f);
    private static Color purple = new Color(0.502f, 0.224f, 0.69f);
    private static Color lightBlue = new Color(0.592f, 0.749f, 0.953f);
    private static Color biege = new Color(0.933f, 0.859f, 0.776f);
    private static Color brown = new Color(0.655f, 0.478f, 0.478f);
    private static Color darkRed = new Color(0.475f, 0.098f, 0.216f);

    public static Color[,] themes = new Color[,] { { darkRed, brown }, 
                                                   { pink, blue },
                                                   { aqua, purple },
                                                   { pink, lightBlue },
                                                   { biege, brown },
                                                   { lightBlue, blue },
                                                   { lightBlue, purple }
                                                 };

    static int rand = Random.Range(0, themes.Length / 2);
    public static Color[] theme = { themes[rand, 0], themes[rand, 1] };
}
