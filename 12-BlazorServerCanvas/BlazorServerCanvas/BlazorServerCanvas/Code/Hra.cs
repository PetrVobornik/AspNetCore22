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

    public Had NovyHad(Had staryHad = null)
    {
        var had = new Had(this, staryHad);
        lock (Hadi)
        {
            if (staryHad != null)
                Hadi.Remove(staryHad);
            Hadi.Add(had);
        }
        return had;
    }

    public void Posun()
    {
        foreach (var had in Hadi)
            had.Posun();
        foreach (var had in Hadi)
        {
            if (Hadi?.Any(x => x.Kolize(had.Ocas[0], x == had)) == true)
            {
                had.Skoncil = true;
                had.CasSkonu = DateTime.Now;
            }
            else if (had.Ocas[0] == Potrava?.Pozice)
            {
                had.Ocas.Add(Potrava.Pozice);
                Potrava.Umisti();
            }
        }
        Hadi.RemoveAll(x => x.Skoncil && (DateTime.Now - x.CasSkonu).TotalSeconds > 10);
    }

}
