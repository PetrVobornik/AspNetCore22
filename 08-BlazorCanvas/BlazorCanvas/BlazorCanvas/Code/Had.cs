using System.Drawing;

namespace BlazorCanvas.Code;

public class Had
{
    public Size Plocha { get; private set; }
    public int Velikost { get; init; }
    public Point Smer { get; set; } // -1/0/1
    public List<Point> Ocas { get; init; }
    public bool Skoncil { get; set; } = false;

    public Had(Size plocha, int velikost = 20)
    {
        Plocha = plocha;
        Velikost = velikost;
        Ocas = new List<Point>() { NahodnaPozice() };
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

    public void ZmenaPlochy(Size novaPlocha)
    {
        Plocha = novaPlocha;
        if (Ocas.Any(o => o.X + Velikost > novaPlocha.Width ||
                          o.X + Velikost > novaPlocha.Width))
        {
            Ocas.Clear();
            Ocas.Add(NahodnaPozice());
            Smer = Point.Empty;
        }
    }

    public Point NahodnaPozice()
        => new Point(Random.Shared.Next(Plocha.Width / Velikost) * Velikost,
                     Random.Shared.Next(Plocha.Height / Velikost) * Velikost);

    public bool Kolize(Point pozice)
        => Ocas.Any(o => o == pozice);
}
