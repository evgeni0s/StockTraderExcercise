using Infrastructure;
using Prism.Events;
using StockTraderExcercise.Interfaces;
using StockTraderExcercise.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockTraderExcercise.Enums;
using StockTraderExcercise.Events;

namespace StockTraderExcercise.ViewModels
{
    [Export(typeof(IStockProprtiesViewModel))]
    public class StockProprtiesViewModel : ViewModelBase, IStockProprtiesViewModel
    {
        private IEventAggregator eventAggregator;
        private StockPropertiesModel model;
        private bool isInitialized;
        private IStockPropertiesService stockPropertiesService;
        [ImportingConstructor]
        public StockProprtiesViewModel(IEventAggregator eventAggregator, IStockPropertiesService stockPropertiesService)
        {
            model = new StockPropertiesModel();
            this.eventAggregator = eventAggregator;
            this.stockPropertiesService = stockPropertiesService;
            SubscribeEvents();
        }
        public StockProperties EquityProperties
        {
            get => model.EquityProperties;
            set
            {
                model.EquityProperties = value;
                OnPropertyChanged();
            }
        }
        public StockProperties BondProperties
        {
            get => model.BondProperties;
            set
            {
                model.BondProperties = value;
                OnPropertyChanged();
            }
        }


        public void Initialize(StockPropertiesModel model)
        {
            if (model == null || isInitialized)
            {
                return;
            }
            this.model = model;
            isInitialized = true;
        }
        public void OnStocksLoaded(IList<Stock> newItems)
        {
            var properties = stockPropertiesService.GetStockProperties(newItems);
            foreach (var kvp in properties)
            {
                UpdateInformation(kvp.Key, kvp.Value);
            }
        }

        private void UpdateInformation(StockType stockType, StockProperties properties)
        {
            switch (stockType)
            {
                case StockType.Bond:
                    BondProperties = properties;
                    break;
                case StockType.Equity:
                    EquityProperties = properties;
                    break;
                default:
                    break;
            }
        }

        private void SubscribeEvents()
        {
            eventAggregator.GetEvent<StocksLoaded>().Subscribe(OnStocksLoaded);
        }

    }
}
