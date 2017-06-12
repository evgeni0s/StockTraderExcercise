using AutoMapper;
using Microsoft.Practices.ServiceLocation;
using Prism.Mef;
using StockTraderExcercise.Interfaces;
using StockTraderExcercise.Models;
using StockTraderExcercise.Views;
using StockTraderServices.Interfaces;
//using StockTraderServices.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExternalDTOs = StockTraderModels;

namespace StockTraderExcercise
{
    public class Bootstrapper: MefBootstrapper
    {
        //public Bootstrapper()
        //{
        //    IServiceLocator serviceLocator = new MefServiceLocatorAdapter(myCompositionContainer);
        //    ServiceLocator.SetLocatorProvider(() => serviceLocator);
        //}
        protected override System.Windows.DependencyObject CreateShell()
        {
            var shell = new Shell();
            var vm = ServiceLocator.Current.GetInstance<IShellViewModel>();
            shell.DataContext = vm;
            return shell;
        }

        protected override void InitializeShell()
        {
            try
            {
                base.InitializeShell();
                App.Current.MainWindow = this.Shell as System.Windows.Window;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        //Mef will check all catalogues and will try to find all Export attributes
        protected override void ConfigureAggregateCatalog()
        {
            AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(Bootstrapper).Assembly));
            AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(IStocksService).Assembly));
            //AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(ModuleMyModule).Assembly));
        }

        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();
            Container.ComposeExportedValue(AggregateCatalog);
        }

        //private void AutoMap()
        //{
        //    Mapper.<ExternalDTOs.Bond, Bond>();
        //}
    }
}
