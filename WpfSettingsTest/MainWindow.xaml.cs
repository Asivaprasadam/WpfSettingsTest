using System.Collections.Generic;
using System.Configuration;
using System.Drawing.Text;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

using WpfSettingsTest.Properties;

namespace WpfSettingsTest;
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        this.DataContext = MainViewModel.Instance;

        var settingsManager = SettingsManager.Instance;
        settingsManager.Reload();

        //var fontSize = Properties.Settings.Default.FontSize;
        //Properties.Settings.Default.FontSize = 12.0;
        //settingsManager.SetPropertyValue(nameof(MainViewModel.FontSize), 18);

        //settingsManager.Save(nameof(MainViewModel.FontSize));

    }

    private void ResetBtn_Click(object sender, RoutedEventArgs e) => MainViewModel.Instance.Restore();

    private void CancelBtn_Click(object sender, RoutedEventArgs e) { }

    private void SaveBtn_Click(object sender, RoutedEventArgs e) => MainViewModel.Instance.Save();
}