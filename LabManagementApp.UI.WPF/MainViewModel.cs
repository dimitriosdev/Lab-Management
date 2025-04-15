using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;
using System.Threading.Tasks;
using LabManagementApp.Infrastructure.Models;
using LabManagementApp.UI.WPF;
using Microsoft.Extensions.Logging;

namespace LabManagementApp.UI.WPF
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

    private readonly HttpClient _httpClient;

    public ObservableCollection<Probe> Probes { get; } = new ObservableCollection<Probe>();
    public ObservableCollection<TestSession> TestSessions { get; } = new ObservableCollection<TestSession>();

    public ICommand LoadProbesCommand { get; }
    public ICommand LoadTestSessionsCommand { get; }
    public ICommand AddProbeCommand { get; }
    public ICommand AddTestSessionCommand { get; }

    public MainViewModel()
    {
      SubmitCommand = new RelayCommand(Submit, CanSubmit);
      RemoveCommand = new RelayCommand(Remove, CanRemove);

      _httpClient = new HttpClient { BaseAddress = new Uri("http://localhost:5288/api/") };

      LoadProbesCommand = new RelayCommand(async () => await LoadProbes());
      LoadTestSessionsCommand = new RelayCommand(async () => await LoadTestSessions());
      AddProbeCommand = new RelayCommand(async () => await AddProbe(), CanSubmit);
      AddTestSessionCommand = new RelayCommand(async () => await AddTestSession(), CanSubmit);
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

    public async Task TriggerLoadProbes()
    {
      await LoadProbes();
    }

    private async Task LoadProbes()
    {
      try
      {
        var response = await _httpClient.GetAsync("Probe");
        if (response.IsSuccessStatusCode)
        {
          var probes = JsonSerializer.Deserialize<List<Probe>>(await response.Content.ReadAsStringAsync());
          if (probes != null)
          {
            Probes.Clear();
            foreach (var probe in probes)
            {
              Probes.Add(probe);
            }
          }
        }
        else
        {
          // Log or handle non-success status codes
          throw new HttpRequestException($"Failed to load probes. Status code: {response.StatusCode}");
        }
      }
      catch (Exception ex)
      {
        // Log the exception or show a message to the user
        Console.WriteLine($"An error occurred while loading probes: {ex.Message}");
      }
    }

    public async Task TriggerLoadTestSessions()
    {
      await LoadTestSessions();
    }
    private async Task LoadTestSessions()
    {
      var response = await _httpClient.GetAsync("TestSession");
      if (response.IsSuccessStatusCode)
      {
        var testSessions = JsonSerializer.Deserialize<List<TestSession>>(await response.Content.ReadAsStringAsync());
        if (testSessions != null)
        {
          TestSessions.Clear();
          foreach (var testSession in testSessions)
          {
            TestSessions.Add(testSession);
          }
        }
      }
    }

    private async Task AddProbe()
    {
      if (!string.IsNullOrEmpty(UserInput))
      {
        var probe = new Probe { ProbeType = UserInput };
        var content = new StringContent(JsonSerializer.Serialize(probe), Encoding.UTF8, new MediaTypeHeaderValue("application/json"));
        var response = await _httpClient.PostAsync("Probe", content);
        if (response.IsSuccessStatusCode)
        {
          Probes.Add(probe);
          UserInput = string.Empty;
        }
      }
    }

    private async Task AddTestSession()
    {
      if (!string.IsNullOrEmpty(UserInput))
      {
        var testSession = new TestSession { Notes = UserInput };
        var content = new StringContent(JsonSerializer.Serialize(testSession), Encoding.UTF8, new MediaTypeHeaderValue("application/json"));
        var response = await _httpClient.PostAsync("TestSession", content);
        if (response.IsSuccessStatusCode)
        {
          TestSessions.Add(testSession);
          UserInput = string.Empty;
        }
      }
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