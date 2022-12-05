namespace BlazorMAUI.Code;

public class Bunka
{
    public bool Odkryto { get; private set; } = false;
    public bool Oznaceno { get; private set; } = false;
    public bool Klikatelne => !Odkryto && !Oznaceno;

    public bool Vysvitit { get; private set; } = false;
    public string CssClass =>
        (Vysvitit ? " svitit" : "") +
        (Typ == 9 ? " mina" : "") +
        (Typ > 0 && Typ < 9 ? $" cislo-{Typ}" : "");

    public int X { get; set; }
    public int Y { get; set; }

    int typ = 0; // Typ pole (0 - nic, 1-8 - číslo, 9 - mina)
    public int Typ
    {
        get => typ;
        set { if (value >= 0 && value <= 9) typ = value; }
    }

    public event EventHandler Odryti;

    public Bunka(int y, int x, EventHandler odryti)
    {
        X = x;
        Y = y;
        Odryti += odryti;
    }

    public void Odkryj(bool spustitUdalost)
    {
        if (!Klikatelne) return;
        Odkryto = true;
        if (spustitUdalost)
        {
            if (Typ == 9) Vysvitit = true;
            Odryti?.Invoke(this, EventArgs.Empty);
        }
    }

    public void Oznac()
    {
        if (Odkryto) return;
        Oznaceno = !Oznaceno;
    }
}