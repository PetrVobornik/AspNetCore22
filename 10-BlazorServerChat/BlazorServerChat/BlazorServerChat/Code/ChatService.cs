using System.Collections.ObjectModel;
using System.Security.Cryptography.X509Certificates;

namespace BlazorServerChat.Code;

public record Zprava
{
    public string Text { get; set; }
    public DateTime Datum { get; set; }
    public string Odesilatel { get; set; }
}

public class ChatService
{
    static ChatService aktualni;
    public static ChatService Aktualni => aktualni ?? new ChatService();

    Dictionary<string, ObservableCollection<Zprava>> data;

    private ChatService() 
    {
        aktualni = this;
        data = new Dictionary<string, ObservableCollection<Zprava>>();
    }

    /// <summary>
    /// Vrátí seznam místností
    /// </summary>
    public List<string> Mistnosti()
        => data.Keys.OrderBy(x => x).ToList();

    /// <summary>
    /// Vytvoří novou chatovací místnost
    /// </summary>
    /// <param name="nazev">Název místnosti</param>
    /// <returns>
    /// TRUE - nová místnost vytvořena, 
    /// FALSE - místnost již existuje,
    /// NULL - místnost se nepodařilo vytvořit
    /// </returns>
    public bool? VytvoritMistnost(string nazev)
    {
        nazev = nazev.Trim();
        if (String.IsNullOrWhiteSpace(nazev) || nazev.Contains('+'))
            return null;
        if (data.ContainsKey(nazev))
            return false;
        data.Add(nazev, new ObservableCollection<Zprava>());
        return true;
    }

    /// <summary>
    /// Přidá novou zprávu do chatu
    /// </summary>
    public void Odeslat(string mistnost, string odesilatel, string zprava)
    {
        if (String.IsNullOrWhiteSpace(mistnost) ||
            String.IsNullOrWhiteSpace(odesilatel) ||
            String.IsNullOrWhiteSpace(zprava))
            return;

        if (!data.TryGetValue(mistnost, out var zpravy))
        {
            zpravy = new ObservableCollection<Zprava>();
            data[mistnost] = zpravy;
        }

        var datum = DateTime.Now;

        zpravy.Add(new Zprava()
        {
            Datum = datum,
            Odesilatel = odesilatel,
            Text = zprava
        });
    }

    /// <summary>
    /// Vrátí referenci na seznam zpráv v místnosti
    /// </summary>
    public ObservableCollection<Zprava>? Zpravy(string mistnost)
        => data.TryGetValue(mistnost, out var zpravy) ? zpravy : null;
}
