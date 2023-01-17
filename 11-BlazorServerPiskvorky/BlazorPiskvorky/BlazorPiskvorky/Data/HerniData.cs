using System.Text.RegularExpressions;

namespace BlazorPiskvorky.Data;

public enum TypSymbolu { Nic, X, O }
public enum StavHry { Probiha, VyhralX, VyhralO, Remiza }
public record Pos(byte Y, byte X);
public delegate void SymbolZmenenDelegate(byte i, byte j, TypSymbolu symbol);

public class HerniData
{
    public string KodHry { get; init; }
        = Regex.Replace(Convert.ToBase64String(Guid.NewGuid().ToByteArray()), "[/+=]", "");

    public TypSymbolu[,] Plocha { get; init; }
    public byte VelikostPlochy { get; init; }
    public TypSymbolu AktualniHrac { get; private set; } = TypSymbolu.X;
    public StavHry Stav { get; private set; } = StavHry.Probiha;
    public List<Pos> ViteznaRada { get; private set; }

    public event SymbolZmenenDelegate SymbolZmenen;

    public HerniData(byte velikostPlochy)
    {
        VelikostPlochy = velikostPlochy;
        Plocha = new TypSymbolu[velikostPlochy, velikostPlochy];
    }

    public void UmisteniSymbolu(byte i, byte j)
    {
        if (Stav != StavHry.Probiha) return;
        Plocha[i, j] = AktualniHrac;
        Stav = KontrolaStavu(i, j);
        if (Stav == StavHry.Probiha)
            AktualniHrac = AktualniHrac == TypSymbolu.X ? TypSymbolu.O : TypSymbolu.X;
        SymbolZmenen?.Invoke(i, j, Plocha[i, j]);
    }

    StavHry KontrolaStavu(byte i, byte j)
    {
        var rada = new List<Pos>();
        var prohledavaneSmery = new[]
        {
            new { X = 1, Y = 0 },
            new { X = 1, Y = 1 },
            new { X = 0, Y = 1 },
            new { X = -1, Y = 1 },
        };
        foreach (var ps in prohledavaneSmery)
        {
            rada.Clear();
            rada.Add(new Pos(i, j));
            for (sbyte znamenko = -1; znamenko <= 1; znamenko += 2)
            {
                int k = znamenko, y = 0, x = 0;
                while ((y = i + k * ps.Y) >= 0 && y < VelikostPlochy &&
                       (x = j + k * ps.X) >= 0 && x < VelikostPlochy &&
                       Plocha[y, x] == AktualniHrac)
                {
                    k += znamenko;
                    rada.Add(new Pos((byte)y, (byte)x));
                }
            }
            if (rada.Count >= 5)
            {
                ViteznaRada = rada;
                return AktualniHrac == TypSymbolu.X ? StavHry.VyhralX : StavHry.VyhralO;
            }
        }

        foreach (var b in Plocha)
            if (b == TypSymbolu.Nic)
                return StavHry.Probiha;
        return StavHry.Remiza;
    }
}
