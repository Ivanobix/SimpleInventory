using SimpleInventory.Common;
using System.Text.Json;

namespace SimpleInventory.Models
{
    public class MasterData
    {
        //Singleton
        private static MasterData _instance;
        public static MasterData Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new MasterData();
                return _instance;
            }
        }

        public readonly List<Product> Comidas = new();
        public readonly List<Product> Bebidas = new();

        private MasterData()
        {
            try
            {
                string comidasStored = Preferences.Get(Constants.ComidasKey, string.Empty);
                if (!string.IsNullOrEmpty(comidasStored))
                    Comidas.AddRange(JsonSerializer.Deserialize<List<Product>>(comidasStored));

                string bebidasStored = Preferences.Get(Constants.BebidasKey, string.Empty);
                if (!string.IsNullOrEmpty(bebidasStored))
                    Bebidas.AddRange(JsonSerializer.Deserialize<List<Product>>(bebidasStored));
            }
            catch
            {
                Preferences.Clear();
            }
        }

        public void SaveStatus()
        {
            string comidasSerialized = JsonSerializer.Serialize(Comidas);
            Preferences.Set(Constants.ComidasKey, comidasSerialized);

            string bebidasSerialized = JsonSerializer.Serialize(Bebidas);
            Preferences.Set(Constants.BebidasKey, bebidasSerialized);
        }
    }
}
