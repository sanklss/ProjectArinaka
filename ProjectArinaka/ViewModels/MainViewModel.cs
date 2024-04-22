using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;

using Microsoft.EntityFrameworkCore;

using ProjectArinaka.Models;

public class MainViewModel : INotifyPropertyChanged
{
    private ObservableCollection<Ww2table> _searchResults;
    private string _searchText;

    public ObservableCollection<Ww2table> SearchResults
    {
        get => _searchResults;
        set
        {
            _searchResults = value;
            OnPropertyChanged();
        }
    }

    public string SearchText
    {
        get => _searchText;
        set
        {
            _searchText = value;
            OnPropertyChanged();
        }
    }

    public MainViewModel()
    {
        SearchResults = new ObservableCollection<Ww2table>();

        SearchCommand = new RelayCommand(async () => await Search());
    }



    public class RelayCommand : ICommand
    {
        private readonly Action _execute;
        private readonly Func<bool> _canExecute;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public RelayCommand(Action execute, Func<bool> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute();
        }

        public void Execute(object parameter)
        {
            _execute();
        }
    }



    public ICommand SearchCommand { get; }




    public async Task Search()
    {
        using (var context = new Ww2Context())
        {
            IQueryable<Ww2table> query;
            if (long.TryParse(SearchText, out long inventoryNumber))
            {
                query = context.Ww2tables
                    .Where(p => p.ИнвентарныйНомер == inventoryNumber);
            }
            else
            {
                query = context.Ww2tables
                    .Where(p => EF.Functions.Like(p.Фио, $"%{SearchText}%"));
            }
            SearchResults.Clear();
            foreach (var item in await query.ToListAsync())
            {
                SearchResults.Add(item);
            }
        }
    }



    public event PropertyChangedEventHandler PropertyChanged;
    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
