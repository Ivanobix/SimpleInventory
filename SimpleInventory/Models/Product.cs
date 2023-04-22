using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace SimpleInventory.Models
{
    public class Product : INotifyPropertyChanged
    {
        private string _text;
        public string Text
        {
            get { return _text; }
            set { _text = value; OnPropertyChanged(); }
        }

        private int _count;
        public int Count
        {
            get { return _count; }
            set { _count = value; OnPropertyChanged(); }
        }

        private string _background;
        public string Background
        {
            get { return _background; }
            set { _background = value; OnPropertyChanged(); }
        }

        public Color BackgroundColor => Color.FromArgb(Background);


        public Product(string text, int count, string type = null)
        {
            Text = text;
            Count = count;
            Background = type switch
            {
                "Entrante" => "#424242",
                "Postre" => "#686868",
                _ => "#7c7c7c"
            };

            InitCommands();
        }

        public Product()
        {
            InitCommands();
        }

        private void InitCommands()
        {
            IncreaseCountCommand = new Command(() =>
            {
                if (Count < 99)
                    Count++;
            });
            DecreaseCountCommand = new Command(() =>
            {
                if (Count > 0)
                    Count--;
            });
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public ICommand IncreaseCountCommand { get; private set; }
        public ICommand DecreaseCountCommand { get; private set; }
    }

}
