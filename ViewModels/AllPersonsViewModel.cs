using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Zadontseva_01.Annotations;
using Zadontseva_01.Models;
using Zadontseva_01.Tools;
using Zadontseva_01.Tools.Managers;
using Zadontseva_01.Views.UserControls;
using Test.Misc;

namespace Zadontseva_01.ViewModels
{
    class AllPersonsViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Person> List //= new ObservableCollection<Person>((new PersonList()).Generate());
        {
            get { return Person.List; }
            set { Person.List = value; OnPropertyChanged(); }
        }

        private Person _selection;
        public Person Selection
        {
            get { return _selection; }
            set { _selection = value; Person.Selected = value; OnPropertyChanged(); }
        }
 

        public List<ComboBoxItem> SortOptions
        {
            get
            {
                List<ComboBoxItem> options = new List<ComboBoxItem>();
                foreach (string option in (new string[] { "None", "Name", "Surname", "Date of birth", "E-mail", "Sun sign", "Chineese sign" }))
                {
                    ComboBoxItem tmp = new ComboBoxItem();
                    tmp.Content = option;
                    options.Add(tmp);
                }
                return options;
            }
        }

        private bool? _isAsc = true;
        public bool? IsAsc
        {
            get { return _isAsc; }
            set { _isAsc = value; OnPropertyChanged(); }
        }

        private ComboBoxItem _selectedOption;
        public ComboBoxItem SelectedOption
        {
            get { return _selectedOption; }
            set
            {
                _selectedOption = value;

                if (Convert.ToBoolean(IsAsc))
                {
                    switch ((string)_selectedOption.Content)
                    {
                        case "None": { break; }
                        case "Name": { List = new ObservableCollection<Person>(Person.List.OrderBy(o => o.Name).ToList()); break; }
                        case "Surname": { List = new ObservableCollection<Person>(Person.List.OrderBy(o => o.Surname).ToList()); break; }
                        case "Date of birth": { List = new ObservableCollection<Person>(Person.List.OrderBy(o => Convert.ToDateTime(o.BirthDate)).ToList()); break; }
                        case "E-mail": { List = new ObservableCollection<Person>(Person.List.OrderBy(o => o.Email).ToList()); break; }
                        case "Sun sign": { List = new ObservableCollection<Person>(Person.List.OrderBy(o => o.SunSign).ToList()); break; }
                        case "Chineese sign": { List = new ObservableCollection<Person>(Person.List.OrderBy(o => o.ChineeseSign).ToList()); break; }
                    }
                }
                else
                {
                    switch ((string)_selectedOption.Content)
                    {
                        case "None": { break; }
                        case "Name": { List = new ObservableCollection<Person>(Person.List.OrderByDescending(o => o.Name).ToList()); break; }
                        case "Surname": { List = new ObservableCollection<Person>(Person.List.OrderByDescending(o => o.Surname).ToList()); break; }
                        case "Date of birth": { List = new ObservableCollection<Person>(Person.List.OrderByDescending(o => Convert.ToDateTime(o.BirthDate)).ToList()); break; }
                        case "E-mail": { List = new ObservableCollection<Person>(Person.List.OrderByDescending(o => o.Email).ToList()); break; }
                        case "Sun sign": { List = new ObservableCollection<Person>(Person.List.OrderByDescending(o => o.SunSign).ToList()); break; }
                        case "Chineese sign": { List = new ObservableCollection<Person>(Person.List.OrderByDescending(o => o.ChineeseSign).ToList()); break; }
                    }
                }

                OnPropertyChanged();
            }
        }

        private ICommand _addCommand;
        public ICommand AddCommand => _addCommand ?? (_addCommand = new RelayCommand<object>(o => Add()));

        private ICommand _editCommand;
        public ICommand EditCommand => _editCommand ?? (_editCommand = new RelayCommand<object>(o => Edit(), o => Selection != null));

        private ICommand _removeCommand;
        public ICommand RemoveCommand => _removeCommand ?? (_removeCommand = new RelayCommand<object>(o => Remove(), o => Selection != null));

        private ICommand _saveCommand;
        public ICommand SaveCommand => _saveCommand ?? (_saveCommand = new RelayCommand<object>(o => Save()));

        private void Add()
        {
            Person.Selected = null;
            NavigateManager.Instance.Navigate(ViewType.Edit);
        }

        private void Edit()
        {
            ((BirthDateViewModel) ( (BirthDateUserControl) NavigateManager.Instance.Navigate(ViewType.Edit) ).DataContext ).FillFields();
        }
        
        private void Remove()
        {
            Person.List.RemoveAt( Person.List.IndexOf(Person.Selected) );
        }

        private async void Save()
        {
            LoaderManager.Instance.ShowLoader();
            await Task.Run(() => Thread.Sleep(500));
            LoaderManager.Instance.HideLoader();
            Person.Save();
        }


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
