using System.Windows;
using System.Windows.Controls;

namespace LabManagementApp.UI.WPF
{
  public partial class MainWindow : Window
  {
    public MainWindow()
    {
      InitializeComponent();
      var viewModel = new MainViewModel();
      DataContext = viewModel;
    }

    private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      if (e.Source is TabControl tabControl)
      {
        var selectedTab = tabControl.SelectedItem as TabItem;
        if (selectedTab != null)
        {
          var header = selectedTab.Header.ToString();
          if (header == "Probes")
          {
            _ = ((MainViewModel)DataContext).TriggerLoadProbes();
          }
          else if (header == "Test Sessions")
          {
            _ = ((MainViewModel)DataContext).TriggerLoadTestSessions();
          }
        }
      }
    }
  }
}