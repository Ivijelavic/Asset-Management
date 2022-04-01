using System.Collections.Generic;


namespace MVCappCoreWeb.Constants
{
    public static class Permissions
    {
        public static List<string> GeneratePermissionsForModule(string module)
        {
            return new List<string>()
            {
                $"Permissions.{module}.Create",
                $"Permissions.{module}.View",
                $"Permissions.{module}.Edit",
                $"Permissions.{module}.Delete",
                $"Permissions.{module}.View.Ugovor",
                $"Permissions.{module}.View.Inventar",
                $"Permissions.{module}.View.Faktura"
            };
        }

        public static class Asset
        {
            public const string View = "Permissions.Asset.View";
            public const string Ugovor = "Permissions.Asset.View.Ugovor";
            public const string Inventar = "Permissions.Asset.View.Inventar";
            public const string Faktura = "Permissions.Asset.View.Faktura";
            public const string TokenView = "Permissions.Token.View";
            public const string TokenCreate = "Permissions.Token.Create";
            public const string TokenEdit = "Permissions.Token.Edit";
            public const string TokenDelete = "Permissions.Token.Delete";
        }

 
   
    
    }
}
