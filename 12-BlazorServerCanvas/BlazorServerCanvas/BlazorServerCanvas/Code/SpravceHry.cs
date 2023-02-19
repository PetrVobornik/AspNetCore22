using System.Drawing;

namespace BlazorServerCanvas.Code;

public class SpravceHry
{
    static SpravceHry aktualni;
    public static SpravceHry Aktualni = 
        aktualni ?? (aktualni = new SpravceHry());
    
    static DateTime casPoslednihoPosunu;

    public int FPS = 10;

    public Size Plocha { get; private set; }
    public int Velikost { get; init; }
    public List<Had> Hadi { get; init; }
    public Jidlo Potrava { get; private set; }


    private SpravceHry()
    {
        Plocha = new Size(600, 400);
        Velikost = 20;
        Potrava = new Jidlo(this);
        Hadi = new List<Had>();
    }

    public Point NahodnaPozice() =>
        new Point(Random.Shared.Next(Plocha.Width / Velikost) * Velikost,
                  Random.Shared.Next(Plocha.Height / Velikost) * Velikost);

    public Point NahodnaPozicePrazdna()
    {
        var pozice = NahodnaPozice();
        while (Potrava?.Pozice == pozice || 
               Hadi?.Any(x => x.Kolize(pozice)) == true)
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

    private void Posun()
    {
        foreach (var had in Hadi)
            had.Posun();
        foreach (var had in Hadi)
        {
            // Srazil se tento had s jiným?
            if (Hadi?.Any(x => x.Kolize(had.Ocas[0], x == had)) == true)
            {
                had.Skoncil = true;          // Skončil
                had.CasSkonu = DateTime.Now; // Zahájit odpočet jeho zmizení
            }
            // Sebral jídlo?
            else if (had.Ocas[0] == Potrava?.Pozice)
            {
                had.Ocas.Add(Potrava.Pozice); // Vyroste
                Potrava.Umisti();             // Nové umístění jídla
            }
        }
        // Zrušit mrtvé a prošlé hady
        Hadi.RemoveAll(x => x.Skoncil && 
                       (DateTime.Now - x.CasSkonu).TotalSeconds > 10);
    }

    public void Update()
    {
        lock (this)
        {
            // Posun všech hadů
            if ((DateTime.Now - casPoslednihoPosunu).TotalMilliseconds >= 1000/FPS)
            {
                Posun();
                casPoslednihoPosunu = DateTime.Now;
            }
        }
    }

}
