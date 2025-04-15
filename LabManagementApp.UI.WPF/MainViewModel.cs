using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace MVVMExample
{
  public class MainViewModel : INotifyPropertyChanged, IDataErrorInfo
  {
    private string? _userInput;
    public string? UserInput
    {
      get => _userInput;
      set
      {
        _userInput = value;
        OnPropertyChanged();
      }
    }

    private string? _selectedItem;
    public string? SelectedItem
    {
      get => _selectedItem;
      set
      {
        _selectedItem = value;
        OnPropertyChanged();
        // Notify that the RemoveCommand can execute
        ((RelayCommand)RemoveCommand).RaiseCanExecuteChanged();
      }
    }

    public ObservableCollection<string> Items { get; } = new ObservableCollection<string>();

    public ICommand SubmitCommand { get; }
    public ICommand RemoveCommand { get; }

    public MainViewModel()
    {
      SubmitCommand = new RelayCommand(Submit, CanSubmit);
      RemoveCommand = new RelayCommand(Remove, CanRemove);
    }

    private void Submit()
    {
      if (!string.IsNullOrEmpty(UserInput))
      {
        Items.Add(UserInput);
        UserInput = string.Empty;
      }
    }

    private bool CanSubmit()
    {
      return !string.IsNullOrWhiteSpace(UserInput);
    }

    private void Remove()
    {
      if (SelectedItem != null)
      {
        Items.Remove(SelectedItem);
        SelectedItem = null;
      }
    }

    private bool CanRemove()
    {
      return SelectedItem != null;
    }

    public string this[string columnName]
    {
      get
      {
        if (columnName == nameof(UserInput) && string.IsNullOrWhiteSpace(UserInput))
        {
          return "Input cannot be empty.";
        }
        return string.Empty;
      }
    }

    public string Error => string.Empty;

    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
  }
}