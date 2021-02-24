using EvidencijaEkskurzija.PristupBaziPodataka.Modeli;
using System;
using System.Collections.ObjectModel;
using System.Windows.Data;

namespace EvidencijaEkskurzija.Modeli.WindowModeli
{
	public class DodavanjeWindowModel : BaseModel
	{
		public int IdEkskurzije { get; set; }
		public int Cena { get; set; }
		public int IdDestinacije { get; set; }
		public int BrojDana { get; set; }
		public string NazivProzora { get; set; }
		public string NazivLabeleProzora { get; set; }
		public string NazivDugmeta { get; set; }
		public string NovaDestinacija { get; set; }
		public string NazivDestinacije { get; set; }
		public DateTime DatumOd { get; set; }
		public DateTime DatumDo { get; set; }
		public CollectionView ListaDestinacija { get; set; }
		public ObservableCollection<DestinacijaModel> ComboListaDestinacija { get; set; }
	}
}
