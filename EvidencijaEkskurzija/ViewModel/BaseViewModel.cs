using EvidencijaEkskurzija.PristupBaziPodataka;
using EvidencijaEkskurzija.Modeli;
using GalaSoft.MvvmLight;

namespace EvidencijaEkskurzija.ViewModel
{
	public class BaseViewModel<TModel> : ViewModelBase where TModel : BaseModel, new()
	{
		public BaseViewModel()
		{
			PristupBazi = new PristupBazi();
			Model = new TModel();
		}

		public TModel Model { get; set; }
		public PristupBazi PristupBazi { get; }
	}
}
