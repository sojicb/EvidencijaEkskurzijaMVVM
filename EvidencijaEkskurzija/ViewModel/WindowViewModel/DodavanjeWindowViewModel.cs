using EvidencijaEkskurzija.PristupBaziPodataka.Modeli;
using EvidencijaEkskurzija.Modeli.WindowModeli;
using EvidencijaEkskurzija.View;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace EvidencijaEkskurzija.ViewModel.WindowViewModel
{
	public class DodavanjeWindowViewModel : BaseViewModel<DodavanjeWindowModel>
	{
		[PreferredConstructor]
		public DodavanjeWindowViewModel()
		{
			Model.NazivProzora = "Dodavanje Ekskurzija";
			Model.NazivDugmeta = "Dodaj Ekskurziju";
			Model.NazivLabeleProzora = "Dodavanje nove Ekskurzije";
			Model.ComboListaDestinacija = new ObservableCollection<DestinacijaModel>(PristupBazi.DestinacijaRepo.GetAllDestinacije());

			DodajNovuDestinacijuKomanda = new RelayCommand(DodajNovuDestinaciju);
			DodavanjeIzmenaKomanda = new RelayCommand(DugmeKlik);
			ZatvoriProzorKomanda = new RelayCommand<FormaDodavanje>(ZatvoriProzor);
		}

		public  DodavanjeWindowViewModel(EkskurzijaModel ekskurzija)
		{
			Ekskurzija = ekskurzija;
			Model.NazivProzora = "Izmena postojece Ekskurzije " + Ekskurzija.NazivDestinacije;
			Model.NazivDugmeta = "Izmeni Ekskurziju";
			Model.NazivLabeleProzora = "Izmena ekskurzije";
			Model.IdEkskurzije = Ekskurzija.Id;
			Model.ComboListaDestinacija = new ObservableCollection<DestinacijaModel>(PristupBazi.DestinacijaRepo.GetAllDestinacije());

			Model.Cena = Ekskurzija.Cena;
			Model.IdDestinacije = Ekskurzija.IdDestinacije;
			Model.NazivDestinacije = Ekskurzija.NazivDestinacije;
			Model.DatumOd = Ekskurzija.Datum;
			Model.DatumDo = Ekskurzija.Datum.AddDays(Ekskurzija.DaniBoravka);

			DodavanjeIzmenaKomanda = new RelayCommand(DugmeKlik);
		}

		#region[Properties]
		public EkskurzijaModel Ekskurzija { get; set; }
		public RelayCommand DodajNovuDestinacijuKomanda { get; set; }
		public RelayCommand DodajNovuEkskurzijuKomanda { get; set; }
		public RelayCommand<FormaDodavanje> ZatvoriProzorKomanda { get; set; }
		public RelayCommand DodavanjeIzmenaKomanda { get; set; }
		#endregion

		#region[Komande]
		private void ZatvoriProzor(Window prozor)
		{
			if(prozor != null)
			{
				prozor.Close();
			}
		}

		private void DodajNovuDestinaciju()
		{
			PristupBazi.DestinacijaRepo.Add(new DestinacijaModel
			{
				Naziv = Model.NovaDestinacija
			});

			Model.ComboListaDestinacija = new ObservableCollection<DestinacijaModel>(PristupBazi.DestinacijaRepo.GetAllDestinacije());

			Model.NovaDestinacija = string.Empty;
		}

		private void IzmeniEkskurziju()
		{
			if (Model.DatumOd < DateTime.Now || Model.DatumOd > Model.DatumDo)
			{
				MessageBox.Show("Izabrali ste pogresan datum!", "Greska");
			}
			else
			{
				PristupBazi.EkskurzijaRepo.Update(new EkskurzijaModel
				{
					Id = Model.IdEkskurzije,
					Cena = Model.Cena,
					Datum = Model.DatumOd,
					DaniBoravka = (int)(Model.DatumDo - Model.DatumOd).TotalHours / 24,
					IdDestinacije = Model.IdDestinacije
				});
			}

			MessageBox.Show("Uspesno ste izmenili ekskurziju!", "Uspeh");
		}

		private void DodajNovuEkskurziju()
		{
			if (Model.DatumOd < DateTime.Now || Model.DatumOd > Model.DatumDo)
			{
				MessageBox.Show("Izabrali ste pogresan datum!", "Greska");
			}
			else
			{
				PristupBazi.EkskurzijaRepo.Add(new EkskurzijaModel
				{
					Cena = Model.Cena,
					DaniBoravka = (int)(Model.DatumDo - Model.DatumOd).TotalHours / 24,
					Datum = Model.DatumOd,
					IdDestinacije = Model.IdDestinacije
				});
				MessageBox.Show("Uspesno ste dodali novu ekskurziju!", "Uspeh");
			}
		}

		private void DugmeKlik()
		{
			if (Ekskurzija == null)
			{
				DodajNovuEkskurziju();
			}
			else if(Ekskurzija != null)
			{
				IzmeniEkskurziju();
			}
		}
		#endregion
	}
}