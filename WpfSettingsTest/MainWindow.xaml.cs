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

        var settingsManager = new SettingsManager(Properties.Settings.Default);
        settingsManager.Load();

        var fontSize = Properties.Settings.Default.TextFontSize;
        Properties.Settings.Default.TextFontSize = 12.0;
        settingsManager.SetPropertyValue("TextFontSize", 18);

        settingsManager.Save("TextFontSize");

    }
}