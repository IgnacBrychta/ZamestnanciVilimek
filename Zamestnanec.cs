namespace ZamestnanciVilimek;

sealed class Zamestnanec : Pracovnik
{
	const float DAN_Z_PRIJMU = 0.30f;
	const float SLEVA_NA_DITE = 0.02f;
	public int PocetDeti { get; set; }
	public int PocetLetPraxe { get; set; }
	public int SumaStravenek { get => (PocetOdpracovanychHodin ?? 0) / 8 * 100; }
	public string StravenkoveInformace { get => ToString() + $": {SumaStravenek} Kč"; }
	public Zamestnanec(string jmeno, string prijmeni, string rodneCislo, string bydliste, int pocetDeti, int pocetLetPraxe)
		: base(jmeno, prijmeni, rodneCislo, bydliste)
	{
		PocetDeti = pocetDeti;
		PocetLetPraxe = pocetLetPraxe;
	}

	public override int SpocitatCistouMzdu()
	{
		int hrubaMzda = (PocetOdpracovanychHodin ?? 0) * HrubaMzdaNaHodinu;
		int danZePrijmu = (int)Math.Round(hrubaMzda * DAN_Z_PRIJMU, 0);

		int slevaDikyDetem = (int)Math.Round(
			PocetDeti * SLEVA_NA_DITE * hrubaMzda,
			0);
		int cistaMzda = hrubaMzda + slevaDikyDetem - danZePrijmu;
		return cistaMzda;
	}
}