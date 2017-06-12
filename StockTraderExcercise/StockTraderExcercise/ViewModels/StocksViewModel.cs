using Infrastructure;
using StockTraderExcercise.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockTraderExcercise.Models;
using Prism.Events;
using StockTraderServices.Interfaces;
using System.Collections.ObjectModel;
using AutoMapper;
using StockTraderExcercise.Helpers;
using StockTraderExcercise.Events;
using System.Windows.Input;

namespace StockTraderExcercise.ViewModels
{
    [Export(typeof(IStocksViewModel))]
    class StocksViewModel : ViewModelBase, IStocksViewModel
    {
        private IEventAggregator eventAggregator;
        private StocksModel model;
        private IStocksService stocksService;
        private bool isInitialized;
        private ICommand refreshCommand;

        [ImportingConstructor]
        public StocksViewModel(IEventAggregator eventAggregator, IStocksService stocksService)
        {
            model = new StocksModel();
            model.Stocks = new List<Stock>();
            this.eventAggregator = eventAggregator;
            this.stocksService = stocksService;
            SubscribeEvents();
        }

        public IList<Stock> Stocks => model.Stocks;
        
        public ICommand RefreshCommand => refreshCommand ?? (refreshCommand = new RelayCommand(OnRefreshCommandExecute));
        
        public void Initialize(StocksModel model)
        {
            if (model == null || isInitialized)
            {
                return;
            }
            this.model = model;
            GetStocks();
            isInitialized = true;
        }
        public void Refresh()
        {
            GetStocks();
        }

        public void OnNewStockAdded(int id)
        {
            Refresh();
        }

        private void OnRefreshCommandExecute(object arg)
        {
            Refresh();
        }
        private void GetStocks()
        {
            var stocks = stocksService.GetStocks();
            this.model.Stocks = new List<Stock>();
            this.model.TotalMarketValue = 0;
            foreach (var stock in stocks)
            {
                var converted = StocksConvertor.Convert(stock);
                this.model.Stocks.Add(converted);
                this.model.TotalMarketValue += converted.MarketValue;
            }
            EnrichStocks();
            OnPropertyChanged("Stocks");
            eventAggregator.GetEvent<StocksLoaded>().Publish(this.model.Stocks);
        }
        private void EnrichStocks()
        {
            foreach (var stock in this.model.Stocks)
            {
                stock.SetTotalMarketValue(this.model.TotalMarketValue);
            }
        }
        private void SubscribeEvents()
        {
            eventAggregator.GetEvent<NewStockAdded>().Subscribe(OnNewStockAdded);
        }

    }
}
