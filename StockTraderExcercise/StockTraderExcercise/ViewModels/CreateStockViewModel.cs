using Infrastructure;
using Prism.Events;
using StockTraderExcercise.Enums;
using StockTraderExcercise.Helpers;
using StockTraderExcercise.Interfaces;
using StockTraderExcercise.Models;
using StockTraderServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Collections;
using ExternalDTOs = StockTraderModels;
using StockTraderExcercise.Events;

namespace StockTraderExcercise.ViewModels
{
    [Export(typeof(ICreateStockViewModel))]
    public class CreateStockViewModel : ValidationEnabledViewModel, ICreateStockViewModel, IDataErrorInfo//, INotifyDataErrorInfo//, IDataErrorInfo //
    {
        private IEventAggregator eventAggregator;
        private CreateStockModel model;
        private IStocksService stocksService;
        private bool isInitialized;
        private ICommand addStockCommand;
        private readonly Dictionary<string, ICollection<string>> validationErrors = new Dictionary<string, ICollection<string>>();

        [ImportingConstructor]
        public CreateStockViewModel(IEventAggregator eventAggregator, IStocksService stocksService)
        {
            model = new CreateStockModel();
            this.eventAggregator = eventAggregator;
            this.stocksService = stocksService;
            AddValidationRules();
        }

        private void AddValidationRules()
        {
            AddValidation(nameof(PriceString), new ValidationRule()
            {
                Message = "Price cannot be empty",
                Validation = Helpers.ValidationRules.IsNotEmpty
            });

            AddValidation(nameof(PriceString), new ValidationRule()
            {
                Message = "Price must be decimal",
                Validation = Helpers.ValidationRules.IsDecimal
            });
            
            AddValidation(nameof(QuantityString), new ValidationRule()
            {
                Message = "Quantity cannot be empty",
                Validation = Helpers.ValidationRules.IsNotEmpty
            });

            AddValidation(nameof(QuantityString), new ValidationRule()
            {
                Message = "Quantity must be decimal",
                Validation = Helpers.ValidationRules.IsDecimal
            });

            Validate(PriceString);
            Validate(QuantityString);

        }
        public ICommand AddStockCommand => addStockCommand ?? (addStockCommand = new RelayCommand(AddStockCommandExecute));

        /// <summary>
        /// Uncompleate. Planned to add factory
        /// </summary>
        /// <param name="arg"></param>
        private void AddStockCommandExecute(object dummy)
        {
            ExternalDTOs.Stock stock;
            switch (StockType)
            {
                case StockType.Bond:
                    stock = new ExternalDTOs.Bond();
                    break;
                case StockType.Equity:
                    stock = new ExternalDTOs.Equity();
                    break;
                default:
                    stock = new ExternalDTOs.Bond();
                    break;
            }
            stock.Price = Convert.ToDecimal(this.PriceString);
            stock.Quantity = Convert.ToDecimal(this.QuantityString);
            var id = stocksService.AddStock(stock);
            eventAggregator.GetEvent<NewStockAdded>().Publish(id);
        }
        public string PriceString
        {
            get => model.PriceString;
            set
            {
                model.PriceString = value;
                Validate(value);
                OnPropertyChanged();
            }
        }
        

        public string QuantityString
        {
            get => model.QuantityString;
            set
            {
                model.QuantityString = value;
                Validate(value);
                OnPropertyChanged();
            }
        }

        public StockType StockType
        {
            get => model.StockType;
            set
            {
                model.StockType = value;
                OnPropertyChanged();
            }
        }
        public void Initialize(CreateStockModel model)
        {
            if (model == null || isInitialized)
            {
                return;
            }
            this.model = model;
            isInitialized = true;
        }
        
    }
}
