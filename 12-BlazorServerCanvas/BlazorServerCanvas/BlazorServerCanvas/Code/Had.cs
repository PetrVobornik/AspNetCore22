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


    public Had(SpravceHry hra)
    {
        Hra = hra;
        Smer = new Point(0, 0);
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
        if (novaPozice.X > Plocha.Width) novaPozice.X = 0;
        if (novaPozice.Y > Plocha.Height) novaPozice.Y = 0;
        // Kontrola srážk s vlastním ocasem
        if (Kolize(novaPozice))
            Skoncil = true;
        // Posun všech částí
        Ocas.Insert(0, novaPozice);
        Ocas.RemoveAt(Ocas.Count - 1);
    }

    //public void ZmenaPlochy(Size novaPlocha)
    //{
    //    Plocha = novaPlocha;
    //    if (Ocas.Any(o => o.X + Velikost > novaPlocha.Width ||
    //                      o.X + Velikost > novaPlocha.Width))
    //    {
    //        Ocas.Clear();
    //        Ocas.Add(NahodnaPozice());
    //        Smer = Point.Empty;
    //    }
    //}


    public bool Kolize(Point pozice)
        => Ocas.Any(o => o == pozice);
}
