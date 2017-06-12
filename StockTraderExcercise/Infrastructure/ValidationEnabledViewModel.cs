using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class ValidationEnabledViewModel : ViewModelBase, INotifyDataErrorInfo, IDataErrorInfo
    {
        public Dictionary<string, List<string>> ValidationErrors { get; set; } = new Dictionary<string, List<string>>();
        protected Dictionary<string, List<Infrastructure.ValidationRule>> ValidationRules { get; set; } = new Dictionary<string, List<Infrastructure.ValidationRule>>();
        #region INotifyDataErrorInfo members
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
        private void RaiseErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }

        public IEnumerable GetErrors(string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName)
                || !ValidationErrors.ContainsKey(propertyName))
                return null;

            return ValidationErrors[propertyName];
        }

        public bool HasErrors
        {
            get { return AllErrors.Count > 0; }
        }
        #endregion

        public List<string> AllErrors => ValidationErrors.Values.SelectMany(e => e).ToList();

        
        #region IDataErrorInfo
        public string this[string propertyName]
        {
            get
            {
                if (PropertyHasErrors(propertyName))
                {
                    return ValidationErrors[propertyName].FirstOrDefault();
                }
                return string.Empty;
            }
        }
        public string Error => "Not implemented";

        #endregion

        #region validation engine

        private void CleanValidationErrors(string propertyName)
        {
            if (ValidationErrors.ContainsKey(propertyName))
            {
                ValidationErrors.Remove(propertyName);
            }
            ValidationErrors.Add(propertyName, new List<string>());
        }

        protected void Validate(object value, [CallerMemberName]string propertyName = null)
        {
            CleanValidationErrors(propertyName);
            if (ValidationRules.ContainsKey(propertyName))
            {
                foreach (var propertySpecicRule in ValidationRules[propertyName])
                {
                    if (!propertySpecicRule.Validation(value))
                    {
                        ValidationErrors[propertyName].Add(propertySpecicRule.Message);
                    }
                    propertySpecicRule.IsDirty = true;
                }
            }
            OnPropertyChanged(nameof(AllErrors));
            OnPropertyChanged(nameof(HasErrors));
        }

        protected bool PropertyHasErrors(string propertyName)
        {
            return ValidationErrors.ContainsKey(propertyName) && ValidationErrors[propertyName] != null && ValidationErrors[propertyName].Any();
        }
        
        protected void AddValidation(string key, ValidationRule rule)
        {
            if (!ValidationRules.ContainsKey(key))
            {
                ValidationRules.Add(key, new List<Infrastructure.ValidationRule>());
            };
            ValidationRules[key].Add(rule);
        }
        #endregion
    }
}
