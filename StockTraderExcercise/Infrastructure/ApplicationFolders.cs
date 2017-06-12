using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public static class ApplicationFolders
    {
        static ApplicationFolders()
        {
            var assembly = new AssemblyCatalog(typeof(ApplicationFolders).Assembly);
            var location = assembly.Assembly.Location;
            BaseSolutionDirectory = Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(location))));
            SharedDataDiractory = Path.Combine(BaseSolutionDirectory, @"StockTraderExcercise\App_Data");
        }

        public static string BaseSolutionDirectory { get; private set; }
        public static string SharedDataDiractory { get; private set; }
    }
}
