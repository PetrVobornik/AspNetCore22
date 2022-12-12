using System.Drawing;

namespace BlazorCanvas.Code;

public class Jidlo
{
    public Point Pozice { get; set; }

	public Jidlo(Had had) => Umisti(had);

	public void Umisti(Had had)
	{
		Pozice = had.Ocas.First();
		while (Kolize(had))
			Pozice = had.NahodnaPozice();
	}

    public void ZmenaPlochy(Had had)
	{
		if (Pozice.X + had.Velikost > had.Plocha.Width ||
			Pozice.Y + had.Velikost > had.Plocha.Height)
			Umisti(had);
	}


    public bool Kolize(Had had)
		=> had.Kolize(Pozice);
}
