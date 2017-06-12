using AutoMapper;
using Infrastructure;
using StockTraderExcercise.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using ExternalDTOs = StockTraderModels;

namespace StockTraderExcercise
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            try
            {
                base.OnStartup(e);
                // Set DB folder to App_Data
                AppDomain.CurrentDomain.SetData("DataDirectory", ApplicationFolders.SharedDataDiractory);

                //var config = new MapperConfiguration(cfg =>
                //{
                //    cfg.CreateMap<ExternalDTOs.Bond, Bond>();
                //});

                //config.AssertConfigurationIsValid();
                Mapper.Initialize(automapperConfiguration);
                var bootStrapper = new Bootstrapper();
                bootStrapper.Run(true);
                App.Current.MainWindow.Show();
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
        }

        private void automapperConfiguration(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<ExternalDTOs.Bond, Bond>();
            cfg.CreateMap<ExternalDTOs.Equity, Equity>();
            cfg.CreateMap<ExternalDTOs.Stock, Stock>();
        }
    }
}
