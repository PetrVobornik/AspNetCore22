namespace BlazorPiskvorky.Data;

public static class HerniServer
{
    static Dictionary<string, HerniData> Hry = new Dictionary<string, HerniData>();

    public static HerniData NovaHra(byte velikostPlochy = 10)
    {
        var hra = new HerniData(velikostPlochy);
        lock (Hry)
            Hry.Add(hra.KodHry, hra);
        return hra;
    }

    public static HerniData NajdiHru(string kodHry)
        => Hry.TryGetValue(kodHry, out var hra) ? hra : null;
}
