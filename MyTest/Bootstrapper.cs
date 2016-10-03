using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Web;
using System.ComponentModel.Composition;
using System.Reflection;

namespace MyTest
{
    public class Bootstrapper
    {
        private static CompositionContainer compositionContainer;
        private static bool IsLoaded = false;

        public static void Compose(List<string> pluginFolders)
        {
            if (IsLoaded) return;

            var catalog = new AggregateCatalog();

            //catalog.Catalogs.Add(new DirectoryCatalog(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin")));
            catalog.Catalogs.Add(new AssemblyCatalog(Assembly.GetExecutingAssembly()));

            foreach (var plugin in pluginFolders)
            {
                var directoryCatalog = new DirectoryCatalog(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Modules", plugin));
                catalog.Catalogs.Add(directoryCatalog);

            }
            compositionContainer = new CompositionContainer(catalog);

            compositionContainer.ComposeParts();
            IsLoaded = true;
        }

        public static T GetInstance<T>(string contractName = null)
        {
            var type = default(T);
            if (compositionContainer == null) return type;

            try
            {
                if (!string.IsNullOrWhiteSpace(contractName))
                    type = compositionContainer.GetExportedValue<T>(contractName);
                else
                    type = compositionContainer.GetExportedValue<T>();
            }
            catch 
            {

            }

            return type;
        }
    }
}