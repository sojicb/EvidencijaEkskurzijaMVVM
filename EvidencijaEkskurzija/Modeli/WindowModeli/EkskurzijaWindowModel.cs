using EvidencijaEkskurzija.PristupBaziPodataka.Modeli;
using System.Collections.ObjectModel;
using System.Windows.Data;

namespace EvidencijaEkskurzija.Modeli.WindowModeli
{
	public class EkskurzijaWindowModel : BaseModel
	{
		public string NazivProzora { get; set; }
		public string Pretraga { get; set; }
		public ObservableCollection<EkskurzijaModel> Ekskurzije { get; set; }
		public ListCollectionView EkskurzijeView { get; set; }
		public EkskurzijaModel SelektovanaEkskurzija { get; set; }
	}
}
