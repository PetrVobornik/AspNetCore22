using System.Drawing;

namespace BlazorServerCanvas.Code;

public class Jidlo
{
    public Point Pozice { get; set; } = new Point(-1, -1);
    public SpravceHry Hra { get; set; }

    public Jidlo(SpravceHry hra) 
    {
        Hra = hra;
        Umisti();
    }

    public void Umisti()
    {
        Pozice = Hra.NahodnaPozicePrazdna();
    }

    public bool Kolize(Had had)
        => had.Kolize(Pozice);
}
