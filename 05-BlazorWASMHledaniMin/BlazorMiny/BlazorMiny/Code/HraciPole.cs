namespace BlazorMiny.Code;

public enum StavHry { Nacita, Probiha, Vyhra, Prohra }

public class HraciPole
{
    public Bunka[,] MinovePole { get; private set; }

    public int Sirka { get; set; }
    public int Vyska { get; set; }
    public int PocetMin { get; set; }
    public StavHry Stav { get; private set; } = StavHry.Nacita;

    public void VytvorPole()
    {
        Stav = StavHry.Nacita;
        // Vytvoření a umístění buněk
        MinovePole = new Bunka[Vyska, Sirka];
        for (int i = 0; i < Vyska; i++)
            for (int j = 0; j < Sirka; j++)
                MinovePole[i, j] = new Bunka(i, j, OdkrytiBunky);

        // Rozmístění min
        for (int k = 0; k < PocetMin; k++)
        {
            (int y, int x) = (Random.Shared.Next(Vyska), Random.Shared.Next(Sirka));
            while (MinovePole[y, x].Typ == 9)
                (y, x) = (Random.Shared.Next(Vyska), Random.Shared.Next(Sirka));
            MinovePole[y, x].Typ = 9;

            // Očíslovat buňky okolo miny
            for (int i = Math.Max(y - 1, 0); i <= Math.Min(y + 1, Vyska - 1); i++)
                for (int j = Math.Max(x - 1, 0); j <= Math.Min(x + 1, Sirka - 1); j++)
                    if (MinovePole[i, j].Typ != 9)
                        MinovePole[i, j].Typ++;
        }
        Stav = StavHry.Probiha;
    }

    Queue<Bunka> queue = new Queue<Bunka>();

    void OdkrytiBunky(object sender, EventArgs e)
    {
        var bunka = (Bunka)sender;
        // Kliknutí na 0 => odkrytí i sousedů
        if (bunka.Typ == 0)
        {
            queue.Clear();
            queue.Enqueue(bunka);
            OdkryjBunkuASousedy();
        }
        // Při kliknutí na minu odkrýt všechny miny a prohra
        else if (bunka.Typ == 9)
        {
            foreach (var b in MinovePole)
                if (b.Typ == 9)
                    b.Odkryj(false);
            Stav = StavHry.Prohra;
            return;
        }

        // Kontrola výhry - vše krom min je odkryto
        foreach (var b in MinovePole)
            if (!b.Odkryto && b.Typ != 9)
                return;
        Stav = StavHry.Vyhra;
    }

    private void OdkryjBunkuASousedy()
    {
        while (queue.Count > 0)
        {
            var b = queue.Dequeue();
            if (b.Typ == 0)
            {
                b.Odkryj(false);
                // Odkrytí sousedů
                for (int i = Math.Max(b.Y - 1, 0); i <= Math.Min(b.Y + 1, Vyska - 1); i++)
                    for (int j = Math.Max(b.X - 1, 0); j <= Math.Min(b.X + 1, Sirka - 1); j++)
                        if (MinovePole[i, j] != b && 
                            MinovePole[i, j].Klikatelne &&
                            !queue.Contains(MinovePole[i, j]))
                            queue.Enqueue(MinovePole[i, j]);
            }
            else 
                b.Odkryj(false);
        }
    }
}
