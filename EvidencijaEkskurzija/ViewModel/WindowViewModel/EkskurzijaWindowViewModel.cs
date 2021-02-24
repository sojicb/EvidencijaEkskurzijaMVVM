using EvidencijaEkskurzija.PristupBaziPodataka;
using EvidencijaEkskurzija.PristupBaziPodataka.Modeli;
using EvidencijaEkskurzija.Modeli.WindowModeli;
using EvidencijaEkskurzija.View;
using GalaSoft.MvvmLight.Command;
using System.Collections.ObjectModel;
using System.Windows.Data;
using Newtonsoft.Json;
using System.IO;
using System.Xml.Serialization;
using System.Windows;

namespace EvidencijaEkskurzija.ViewModel.WindowViewModel
{
	public class EkskurzijaWindowViewModel : BaseViewModel<EkskurzijaWindowModel>
	{
		public EkskurzijaWindowViewModel()
		{
			Model.NazivProzora = "Evidencija Ekskurzija";

			Model.Ekskurzije = new ObservableCollection<EkskurzijaModel>(PristupBazi.EkskurzijaRepo.GetAllEkskurzije());

			Model.EkskurzijeView = CollectionViewSource.GetDefaultView(Model.Ekskurzije) as ListCollectionView;

			PretragaKomanda = new RelayCommand(PretragaEkskurzija);
			DodavanjeEkskurzijeKomanda = new RelayCommand(OtvoriProzorZaDodavanje);
			IzmeniEkskurzijuKomanda = new RelayCommand(IzmenaEkskurzije);
			ObrisiEkskurzijuKomanda = new RelayCommand(ObrisiEkskuriju);
			PrikaziSveEkskurijeKomanda = new RelayCommand(PrikaziSveEkskurzije);
			EksportXMLKomanda = new RelayCommand(ExportXML);
		}

		#region[Komande]
		public RelayCommand PretragaKomanda { get; set; }
		public RelayCommand DodavanjeEkskurzijeKomanda { get; set; }
		public RelayCommand IzmeniEkskurzijuKomanda { get; set; }
		public RelayCommand ObrisiEkskurzijuKomanda { get; set; }
		public RelayCommand EksportXMLKomanda { get; set; }
		public RelayCommand PrikaziSveEkskurijeKomanda { get; set; }
		#endregion

		#region[Komande]
		private void PrikaziSveEkskurzije()
		{
			Model.Ekskurzije = new ObservableCollection<EkskurzijaModel>(PristupBazi.EkskurzijaRepo.GetAllEkskurzije());

			Model.EkskurzijeView = CollectionViewSource.GetDefaultView(Model.Ekskurzije) as ListCollectionView;

			Model.Pretraga = string.Empty;
		}

		private void IzmenaEkskurzije()
		{
			if(Model.SelektovanaEkskurzija != null)
			{
				FormaDodavanje forma = new FormaDodavanje
				{
					DataContext = new DodavanjeWindowViewModel(Model.SelektovanaEkskurzija)
				};
				forma.Show();
			}
		}

		private void ObrisiEkskuriju()
		{
			PristupBazi.EkskurzijaRepo.Delete(Model.SelektovanaEkskurzija.Id);

			Model.Ekskurzije = new ObservableCollection<EkskurzijaModel>(PristupBazi.EkskurzijaRepo.GetAllEkskurzije());
			Model.EkskurzijeView = CollectionViewSource.GetDefaultView(Model.Ekskurzije) as ListCollectionView;
		}

		private void PretragaEkskurzija()
		{
			if(Model.Pretraga == string.Empty)
			{
				PrikaziSveEkskurzije();
			}
			else
			{
				Model.Ekskurzije = new ObservableCollection<EkskurzijaModel>(PristupBazi.EkskurzijaRepo.PretragaEkskurzije(Model.Pretraga));

				Model.EkskurzijeView = CollectionViewSource.GetDefaultView(Model.Ekskurzije) as ListCollectionView;
			}
		}

		private void OtvoriProzorZaDodavanje()
		{
			FormaDodavanje forma = new FormaDodavanje
			{
				DataContext = new DodavanjeWindowViewModel()
			};
			forma.Show();
		}


		private void ExportXML()
		{
			XmlSerializer xml = new XmlSerializer(typeof(EkskurzijaModel));
			string path = @"Ekskurzija.xml";

			if(Model.SelektovanaEkskurzija != null)
			{
				using (TextWriter streamWriter = new StreamWriter(path))
				{
					xml.Serialize(streamWriter, Model.SelektovanaEkskurzija);
				}

				MessageBox.Show("Ekskurzija uspesno eksportovana kao XML", "Uspeh");
			}
			else
			{
				MessageBox.Show("Izaberite ekskurzjiu", "Greska");
			}
			
		}
		#endregion
	}
}
