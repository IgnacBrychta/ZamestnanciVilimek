namespace ZamestnanciVilimek;

sealed class Brigadnik : Pracovnik
{
	const float DAN_Z_PRIJMU = 0.15f;
	public Brigadnik(string jmeno, string prijmeni, string rodneCislo, string bydliste)
		: base(jmeno, prijmeni, rodneCislo, bydliste)
	{ }
	public override int SpocitatCistouMzdu()
	{
		int hrubaMzda = (PocetOdpracovanychHodin ?? 0) * HrubaMzdaNaHodinu;
		int danZePrijmu = (int)Math.Round(hrubaMzda * DAN_Z_PRIJMU, 0);
		int cistaMzda = hrubaMzda - danZePrijmu;
		return cistaMzda;
	}

}
