using System.Drawing;

namespace BlazorServerCanvas.Code;

public class Had
{
    public Point Smer { get; set; } // -1/0/1
    public List<Point> Ocas { get; init; }
    public bool Skoncil { get; set; } = false;
    public SpravceHry Hra { get; set; }
    public Size Plocha => Hra.Plocha;
    public int Velikost => Hra.Velikost;
    public Color BarvaHlavy { get; init; }
    public Color BarvaOcasu { get; init; }
    public DateTime CasSkonu { get; set; }


    public Had(SpravceHry hra, Had staryHad = null)
    {
        Hra = hra;
        Smer = new Point(0, 0);
        if (staryHad == null)
            BarvaHlavy = Color.FromArgb(Random.Shared.Next(256), Random.Shared.Next(256), Random.Shared.Next(256));
        else
            BarvaHlavy = staryHad.BarvaHlavy;
        BarvaOcasu = Color.FromArgb((int)(BarvaHlavy.R * 0.8), (int)(BarvaHlavy.G * 0.8), (int)(BarvaHlavy.B * 0.8));
        Ocas = new List<Point>() { Hra.NahodnaPozicePrazdna() };
    }

    public void Posun()
    {
        // Hra pozastavena
        if (Skoncil || Smer == Point.Empty) return;
        // Výpočet pozice, kam se posunout
        var novaPozice = Ocas.First() + new Size(Velikost * Smer.X, Velikost * Smer.Y);
        // Nekonečná plocha
        if (novaPozice.X < 0) novaPozice.X = (Plocha.Width / Velikost - 1) * Velikost;
        if (novaPozice.Y < 0) novaPozice.Y = (Plocha.Height / Velikost - 1) * Velikost;
        if (novaPozice.X >= Plocha.Width) novaPozice.X = 0;
        if (novaPozice.Y >= Plocha.Height) novaPozice.Y = 0;
        // Posun všech částí
        Ocas.Insert(0, novaPozice);
        Ocas.RemoveAt(Ocas.Count - 1);
    }

    public bool Kolize(Point pozice, bool vynechatHlavu = false)
        => (vynechatHlavu ? Ocas.Skip(1) : Ocas).Any(o => o == pozice);
}
