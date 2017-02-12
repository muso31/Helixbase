#region GlassMapperScCustom generated code
using Glass.Mapper.Configuration;
using Glass.Mapper.IoC;
using Glass.Mapper.Maps;
using Glass.Mapper.Sc.IoC;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using IDependencyResolver = Glass.Mapper.Sc.IoC.IDependencyResolver;

namespace Helixbase.Foundation.ORM.App_Start
{
    public static  class GlassMapperScCustom
    {
		public static IDependencyResolver CreateResolver(){
			var config = new Glass.Mapper.Sc.Config();

			var dependencyResolver = new DependencyResolver(config);
			// add any changes to the standard resolver here
			return dependencyResolver;
		}

		public static IConfigurationLoader[] GlassLoaders(){			
			
			/* USE THIS AREA TO ADD FLUENT CONFIGURATION LOADERS
             * 
             * If you are using Attribute Configuration or automapping/on-demand mapping you don't need to do anything!
             * 
             */

			return new IConfigurationLoader[]{};
		}
		public static void PostLoad(){
			//Remove the comments to activate CodeFist
			/* CODE FIRST START
            var dbs = Sitecore.Configuration.Factory.GetDatabases();
            foreach (var db in dbs)
            {
                var provider = db.GetDataProviders().FirstOrDefault(x => x is GlassDataProvider) as GlassDataProvider;
                if (provider != null)
                {
                    using (new SecurityDisabler())
                    {
                        provider.Initialise(db);
                    }
                }
            }
             * CODE FIRST END
             */
		}
		public static void AddMaps(IConfigFactory<IGlassMap> mapsConfigFactory)
        {
            // Add maps here
            string binPath = System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "bin");

            foreach (string dll in Directory.GetFiles(binPath, "Helixbase*.dll", SearchOption.AllDirectories))
            {
                try
                {
                    Assembly loadedAssembly = Assembly.LoadFile(dll);

                    Type glassmapType = typeof(IGlassMap);
                    var maps = loadedAssembly.GetTypes().Where(x => glassmapType.IsAssignableFrom(x));

                    if (maps != null)
                    {
                        foreach (var map in maps)
                            mapsConfigFactory.Add(() => Activator.CreateInstance(map) as IGlassMap);
                    }
                }
                catch (FileLoadException loadEx)
                { }
                catch (BadImageFormatException imgEx)
                { }
            }
        }
    }
}
#endregion