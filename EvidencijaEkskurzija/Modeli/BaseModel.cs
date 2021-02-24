using System.ComponentModel;

namespace EvidencijaEkskurzija.Modeli
{
	public abstract class BaseModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
	}
}
