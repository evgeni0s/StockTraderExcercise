using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace StockTraderExcercise.Controls
{
    /// <summary>
    /// Interaction logic for ValidationStatusIndicator.xaml
    /// </summary>
    public partial class ValidationStatusIndicator : UserControl
    {
        
        public bool? IsValid
        {
            get { return (bool?)GetValue(IsValidProperty); }
            set { SetValue(IsValidProperty, value); }
        }
        
        public static readonly DependencyProperty IsValidProperty =
            DependencyProperty.Register("IsValid", typeof(bool?),
              typeof(ValidationStatusIndicator), new PropertyMetadata(null));

        
        public string ErrorMessage
        {
            get { return (string)GetValue(ErrorMessageProperty); }
            set { SetValue(ErrorMessageProperty, value); }
        }

        public static readonly DependencyProperty ErrorMessageProperty =
            DependencyProperty.Register("ErrorMessage", typeof(string),
              typeof(ValidationStatusIndicator), new PropertyMetadata(null));

        public ValidationStatusIndicator()
        {
            InitializeComponent();
            this.DataContext = this;
        }
    }
}
