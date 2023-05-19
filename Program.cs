namespace ZamestnanciVilimek
{
    internal static class Program
    {
        static List<Pracovnik> pracovnici = new List<Pracovnik>()
        {
            new Brigadnik("Petr", "Vilímek", "-1", "někde") {PocetOdpracovanychHodin = 0 },
            new Brigadnik("Honza", "Novák", "-2", "nevím") { PocetOdpracovanychHodin = 30 },
            new Zamestnanec("Ignác", "Brychta", "007", "tajné", 3, 4) { PocetOdpracovanychHodin = 200 }
        };
        static void Main(string[] args)
        {
            Brigadnik.HrubaMzdaNaHodinu = 150;
            Zamestnanec.HrubaMzdaNaHodinu = 200;
            VypsatBrigadniky();
            Console.ReadKey();
            VypsatZamestnance();
            Console.ReadKey();
            Mzda();
            Console.ReadKey();
            VypsatSumuStravenekZamestnancu();
            Console.ReadKey();
        }
        static void Mzda()
        {
            Console.WriteLine("Jméno | Přijmení | Čistá mzda");
            foreach (var pracovnik in pracovnici)
            {
                Console.WriteLine(pracovnik.MzdoveInformace);
            }
        }
        static void VypsatSumuStravenekZamestnancu()
        {
            Console.WriteLine("Jméno | Přijmení | Suma stravenek");
            foreach (var pracovnik in pracovnici)
            {
                if(pracovnik is Zamestnanec zamestnanec)
                {
                    Console.WriteLine(zamestnanec.StravenkoveInformace);
                }
            }
        }
        static void VypsatZamestnance()
        {
            Console.WriteLine("Zaměstnanci:");
            foreach (var pracovnik in pracovnici)
            {
                if(pracovnik is Zamestnanec)
                {
                    Console.WriteLine(pracovnik);
                }
            }
        }
        static void VypsatBrigadniky()
        {
            Console.WriteLine("Brigádníci:");
            foreach (var pracovnik in pracovnici)
            {
                if (pracovnik is Brigadnik)
                {
                    Console.WriteLine(pracovnik);
                }
            }
        }
    }
    abstract class Pracovnik
    {
        public string? Jmeno { get; init; }
        public string? Prijmeni { get; init; }
        public string? RodneCislo { get; init; }
        public string? Bydliste { get; set; }
        public int? PocetOdpracovanychHodin { get; set; }

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
    sealed class Brigadnik : Pracovnik
    {
        const float DAN_Z_PRIJMU = 0.15f;
        public static int HrubaMzdaNaHodinu { get; set; }

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
    sealed class Zamestnanec : Pracovnik
    {
        const float DAN_Z_PRIJMU = 0.30f;
        const float SLEVA_NA_DITE = 0.02f;
        public static int HrubaMzdaNaHodinu { get; set; }
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
}