using System.Drawing;

namespace BlazorServerCanvas.Code;

public class SpravceHry
{
    static SpravceHry aktualni;
    public static SpravceHry Aktualni = aktualni ?? (aktualni = new SpravceHry());

    public Size Plocha { get; private set; }
    public int Velikost { get; init; }
    public List<Had> Hadi { get; init; }
    public Jidlo Potrava { get; set; }


    private SpravceHry()
    {
        Plocha = new Size(600, 400);
        Velikost = 20;
        Potrava = new Jidlo(this);
        Hadi = new List<Had>();
    }

    public Point NahodnaPozice()
    => new Point(Random.Shared.Next(Plocha.Width / Velikost) * Velikost,
                 Random.Shared.Next(Plocha.Height / Velikost) * Velikost);

    public Point NahodnaPozicePrazdna()
    {
        var pozice = NahodnaPozice();
        while (Potrava?.Pozice == pozice || Hadi?.Any(x => x.Kolize(pozice)) == true)
            pozice = NahodnaPozice();
        return pozice;
    }

    public Had NovyHad()
    {
        var had = new Had(this);
        lock (Hadi)
            Hadi.Add(had);
        return had;
    }


}
