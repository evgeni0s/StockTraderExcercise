using Infrastructure;
using Prism.Events;
using StockTraderExcercise.Events;
using StockTraderExcercise.Interfaces;
using StockTraderExcercise.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace StockTraderExcercise.ViewModels
{
    [Export(typeof(IShellViewModel))]
    internal class ShellViewModel : ViewModelBase, IShellViewModel
    {
        private readonly IEventAggregator eventAggregator;
        private object topPanelDataContext;
        private object rightPanelDataContext;
        private object mainPanelDataContext;
        private ICreateStockViewModel createStockViewModel;
        private IStockProprtiesViewModel stockProprtiesViewModel;
        private IStocksViewModel stocksViewModel;
        private IStockPropertiesService stockPropertiesService;
        private ICommand testCommand;
        [ImportingConstructor]
        public ShellViewModel(IEventAggregator eventAggregator, 
            ICreateStockViewModel createStockViewModel, 
            IStockProprtiesViewModel stockProprtiesViewModel, 
            IStocksViewModel stocksViewModel,
            IStockPropertiesService stockPropertiesService)
        {
            this.eventAggregator = eventAggregator;
            this.createStockViewModel = createStockViewModel;
            this.stockProprtiesViewModel = stockProprtiesViewModel;
            this.stocksViewModel = stocksViewModel;
            this.stockPropertiesService = stockPropertiesService;
            SubscribeEvents();
            LoadPanels();
        }
        
        public object TopPanelDataContext
        {
            get => topPanelDataContext;
            set
            {
                topPanelDataContext = value;
                OnPropertyChanged();
            }
        }

        public object RightPanelDataContext
        {
            get => rightPanelDataContext;
            set
            {
                rightPanelDataContext = value;
                OnPropertyChanged();
            }
        }

        public object MainPanelDataContext
        {
            get => mainPanelDataContext;
            set
            {
                mainPanelDataContext = value;
                OnPropertyChanged();
            }
        }

        public StocksModel StocksModel { get; set; }

        public void OnStocksLoaded(IList<Stock> newValues)
        {

        }

        private void OnTestCommandExecute(object arg)
        {
            MessageBox.Show("Test command Executtes !");
        }

        private void LoadPanels()
        {
            TopPanelDataContext = createStockViewModel;
            RightPanelDataContext = stockProprtiesViewModel;
            MainPanelDataContext = stocksViewModel;
            StocksModel = new StocksModel();
            this.stocksViewModel.Initialize(StocksModel);
        }

        private void SubscribeEvents()
        {
            eventAggregator.GetEvent<StocksLoaded>().Subscribe(OnStocksLoaded);
        }

    }
}
