using SimpleInventory.Common;
using SimpleInventory.Models;
using SimpleInventory.ViewModel;

namespace SimpleInventory;

public partial class MainPage : ContentPage
{
    private readonly MainPageViewModel vm = new();
    private string LastSectionSelected { get; set; }

    public MainPage()
    {
        InitializeComponent();
        BindingContext = vm;
    }

    private void OnComidaSectionClick(object sender, EventArgs e)
    {
        LastSectionSelected = Constants.ComidasKey;
        vm.Items.Clear();
        MasterData.Instance.Comidas.ForEach(x => vm.Items.Add(x));

        if (!vm.AddAndDeleteButtonsEnabled)
            vm.AddAndDeleteButtonsEnabled = true;
    }

    private void OnBebidaSectionClick(object sender, EventArgs e)
    {
        LastSectionSelected = Constants.BebidasKey;
        vm.Items.Clear();
        MasterData.Instance.Bebidas.ForEach(x => vm.Items.Add(x));

        if (!vm.AddAndDeleteButtonsEnabled)
            vm.AddAndDeleteButtonsEnabled = true;
    }

    private async void OnAddClick(object sender, EventArgs e)
    {
        string productName = await DisplayPromptAsync("Añadir", "Nombre del producto:");
        if (!string.IsNullOrEmpty(productName))
        {
            switch (LastSectionSelected)
            {
                case Constants.ComidasKey:
                    {
                        string type = await DisplayActionSheet("¿Qué tipo de comida es?", "Cancelar", null, "Hamburguesa", "Entrante", "Postre");
                        if (!string.IsNullOrEmpty(type))
                        {
                            vm.Items.Clear();
                            MasterData.Instance.Comidas.Add(new Product(productName, 0, type));
                            MasterData.Instance.Comidas.ForEach(x => vm.Items.Add(x));
                        }
                        break;
                    }
                case Constants.BebidasKey:
                    {
                        vm.Items.Clear();
                        MasterData.Instance.Bebidas.Add(new Product(productName, 0));
                        MasterData.Instance.Bebidas.ForEach(x => vm.Items.Add(x));
                    }
                    break;
            }
        }
    }

    private async void OnDeleteClick(object sender, EventArgs e)
    {
        bool confirmation = await DisplayAlert("Eliminar", "¿Estás seguro de que quieres eliminar este producto?", "Sí", "No");
        if (confirmation && vm.SelectedItem != null)
        {
            vm.Items.Remove(vm.SelectedItem);
            switch (LastSectionSelected)
            {
                case Constants.ComidasKey:
                    MasterData.Instance.Comidas.Remove(vm.SelectedItem);
                    break;
                case Constants.BebidasKey:
                    MasterData.Instance.Bebidas.Remove(vm.SelectedItem);
                    break;
            }
        }
    }
}