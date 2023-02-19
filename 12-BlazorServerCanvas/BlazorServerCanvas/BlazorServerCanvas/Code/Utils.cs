using System.Drawing;

namespace BlazorServerCanvas.Code;

public static class ColorUtils
{
    public static string Hexadecimalne(this Color c)
        => "#" + c.R.ToString("X2") 
               + c.G.ToString("X2") 
               + c.B.ToString("X2");

    public static Color Nahodna()
       => Color.FromArgb(Random.Shared.Next(256), 
                         Random.Shared.Next(256), 
                         Random.Shared.Next(256));

    public static Color Odvozena(this Color c, double nasobitel = 0.8)
        => Color.FromArgb((int)(c.R * nasobitel),
                          (int)(c.G * nasobitel),
                          (int)(c.B * nasobitel));
}
