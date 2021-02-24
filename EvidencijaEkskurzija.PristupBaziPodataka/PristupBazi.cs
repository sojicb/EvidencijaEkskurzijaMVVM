using EvidencijaEkskurzija.PristupBaziPodataka.Repozitorijumi;

namespace EvidencijaEkskurzija.PristupBaziPodataka
{
	public class PristupBazi
	{
		private const string _konekcioniString = @"Data Source = .;Initial Catalog=MaturskeEkskurzije;Integrated Security=True";

		public PristupBazi()
		{
			DestinacijaRepo = new DestinacijaRepozitorijum(_konekcioniString);
			EkskurzijaRepo = new EkskurzijaRepozitorijum(_konekcioniString);
		}

		public DestinacijaRepozitorijum DestinacijaRepo { get; }
		public EkskurzijaRepozitorijum EkskurzijaRepo { get; }
	}
}
