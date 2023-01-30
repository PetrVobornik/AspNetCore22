namespace BlazorServerCanvas.Code;

public static class Utils
{
    public static String ColorToHex(System.Drawing.Color c)
    {
        return "#" + c.R.ToString("X2") + c.G.ToString("X2") + c.B.ToString("X2");
    }
}
