namespace ZamestnanciVilimek;

abstract class Pracovnik
{
	public string? Jmeno { get; init; }
	public string? Prijmeni { get; init; }
	public string? RodneCislo { get; init; }
	public string? Bydliste { get; set; }
	public int? PocetOdpracovanychHodin { get; set; }
	public static int HrubaMzdaNaHodinu { get; set; }
	public abstract int SpocitatCistouMzdu();
	public string MzdoveInformace { get => ToString() + $": {SpocitatCistouMzdu()} Kč"; }
	public Pracovnik(string jmeno, string prijmeni, string rodneCislo, string bydliste)
	{
		Jmeno = jmeno;
		Prijmeni = prijmeni;
		RodneCislo = rodneCislo;
		Bydliste = bydliste;
	}
	public override string ToString()
	{
		return $"{Jmeno} {Prijmeni}";
	}
}
