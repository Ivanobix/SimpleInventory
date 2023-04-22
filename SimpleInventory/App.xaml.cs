using SimpleInventory.Models;

namespace SimpleInventory;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        MainPage = new AppShell();
    }

    protected override void OnSleep()
    {
        MasterData.Instance.SaveStatus();
    }
}
