using CommonServiceLocator;
using EvidencijaEkskurzija.ViewModel.WindowViewModel;
using GalaSoft.MvvmLight.Ioc;

namespace EvidencijaEkskurzija.ViewModel
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register<EkskurzijaWindowViewModel>();
            SimpleIoc.Default.Register<DodavanjeWindowViewModel>();
        }
        public EkskurzijaWindowViewModel EkskurzijaView => ServiceLocator.Current.GetInstance<EkskurzijaWindowViewModel>();
        public DodavanjeWindowViewModel DodavanjeView => ServiceLocator.Current.GetInstance<DodavanjeWindowViewModel>();

        public static void Cleanup(){ }
    }
}