using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using WpfApp6.ViewModel;
using System.Diagnostics;
using System.Windows.Input;

namespace WpfApp6.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private ViewModelBase _currentViewModel;

        public ViewModelBase CurrentViewModel
        {
            get { return _currentViewModel; }
            set { Set(ref _currentViewModel, value); }
        }

        public ICommand OpenFirstViewModelCommand { get; }
        public ICommand OpenSecondViewModelCommand { get; }
        public ICommand OpenHledaniViewModelCommand { get; }
        public ICommand OpenRozvrhViewModelCommand { get; }

        public MainViewModel()
        {
            OpenFirstViewModelCommand = new RelayCommand(OpenFirstViewModel);
            OpenSecondViewModelCommand = new RelayCommand(OpenSecondViewModel);
            OpenHledaniViewModelCommand   = new RelayCommand(OpenHledaniViewModel);
            OpenRozvrhViewModelCommand = new RelayCommand(OpenRozvrhViewModel);

            CurrentViewModel = new FirstViewModel();
        }

        private void OpenFirstViewModel()
        {
            
            Debug.WriteLine("Setting FirstViewModel");
            CurrentViewModel = new FirstViewModel();
        }

        private void OpenHledaniViewModel()
        {

            Debug.WriteLine("Hledani");
            CurrentViewModel = new HledaniViewModel();
        }

        private void OpenRozvrhViewModel()
        {

            Debug.WriteLine("rozvrh");
            CurrentViewModel = new RozvrhViewModel ();
        }

        private void OpenSecondViewModel()
        {
            Debug.WriteLine("Setting SecondViewModel");
            CurrentViewModel = new SecondViewModel();
        }
    }
}
