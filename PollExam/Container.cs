using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;

namespace PollExam
{
    /// <summary>
    /// Cover MEF framework for register / bind contracts
    /// </summary>
    public static class Container
    {
        private static CompositionContainer _container;

        public static Boolean Init(String pluginDirectory)
        {
            if (_container != null) return false;

            var rootCatalog = new AggregateCatalog();

            if (!String.IsNullOrEmpty(pluginDirectory))
            {
                rootCatalog.Catalogs.Add(new DirectoryCatalog(pluginDirectory, "*.dll"));
            }

            _container = new CompositionContainer(rootCatalog, true);
            return true;
        }

        public static T One<T>()
        {
            return _container.GetExportedValue<T>();
        }

        public static IEnumerable<T> Many<T>()
        {
            return _container.GetExportedValues<T>();
        }

        public static T Bind<T>(this T value)
        {
            _container.SatisfyImportsOnce(value);
            return value;
        }

        public static void Add<T>(String contractName, T value)
        {
            _container.ComposeExportedValue(contractName, value);
        }
    }
}
