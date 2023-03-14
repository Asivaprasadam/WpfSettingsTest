using System.Collections.Generic;
using System.Drawing.Text;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace WpfSettingsTest;
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public static List<string> FontsList { get; set; } = GetFontNames();

    public MainWindow()
    {
        InitializeComponent();
        this.DataContext = this;
    }

    public static List<string> GetFontNames()
    {
        List<string> fontNames = new List<string>();
        using (InstalledFontCollection installedFonts = new InstalledFontCollection())
        {
            foreach (var fontFamily in installedFonts.Families)
            {
                fontNames.Add(fontFamily.Name);
            }
        }
        return fontNames;
    }
}

