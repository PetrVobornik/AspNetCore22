using System.Drawing;

namespace BlazorServerCanvas.Code;

public class Had
{
    public SpravceHry Hra { get; init; }
    public Point Smer { get; set; } = new Point(0, 0); // -1/0/1
    public List<Point> Ocas { get; private init; }
    public Color BarvaHlavy { get; init; }
    public Color BarvaOcasu { get; init; }
    public bool Skoncil { get; set; } = false;
    public DateTime CasSkonu { get; set; }


    public Had(SpravceHry hra, Had staryHad = null)
    {
        Hra = hra;
        BarvaHlavy = staryHad?.BarvaHlavy.Odvozena() ?? ColorUtils.Nahodna();
        BarvaOcasu = BarvaHlavy.Odvozena();
        Ocas = new List<Point>() { Hra.NahodnaPozicePrazdna() };
    }

    public void Posun()
    {
        var p = Hra.Plocha;
        var v = Hra.Velikost;
        // Hra pozastavena
        if (Skoncil || Smer == Point.Empty) return;
        // Výpočet pozice, kam se posunout
        var novaPozice = Ocas.First() + 
            new Size(v * Smer.X, v * Smer.Y);
        // Nekonečná plocha
        if (novaPozice.X < 0) 
            novaPozice.X = (p.Width / v - 1) * v;
        if (novaPozice.Y < 0) 
            novaPozice.Y = (p.Height / v - 1) * v;
        if (novaPozice.X >= p.Width) 
            novaPozice.X = 0;
        if (novaPozice.Y >= p.Height) 
            novaPozice.Y = 0;
        // Posun všech částí
        Ocas.Insert(0, novaPozice);
        Ocas.RemoveAt(Ocas.Count - 1);
    }

    public bool Kolize(Point pozice, bool vynechatHlavu = false)
        => (vynechatHlavu ? Ocas.Skip(1) : Ocas).Any(o => o == pozice);
}
