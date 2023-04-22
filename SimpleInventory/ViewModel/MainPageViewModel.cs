using SimpleInventory.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SimpleInventory.ViewModel
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Product> Items { get; set; }

        private Product selectedItem = null;
        public Product SelectedItem
        {
            get => selectedItem;
            set
            {
                selectedItem = value;
                OnPropertyChanged();
            }
        }

        private bool addAndDeleteButtonsEnabled = false;
        public bool AddAndDeleteButtonsEnabled
        {
            get => addAndDeleteButtonsEnabled;
            set
            {
                addAndDeleteButtonsEnabled = value;
                OnPropertyChanged();
            }
        }

        public MainPageViewModel()
        {
            Items = new ObservableCollection<Product>();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
