using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Text;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Automation.Provider;
using System.Windows.Media;

namespace WpfSettingsTest;

public class MainViewModel : INotifyPropertyChanged
{
    public static MainViewModel Instance { get; set; } = new MainViewModel();

    public MainViewModel()
    {
        FontsList = GetFontNames();
        PropertyChanged += OnSettingsChanged;
    }

    #region Static Lists
    #region FontsList
    public static HashSet<string> FontsList { get; set; } = GetFontNames();

    private static HashSet<string> GetFontNames()
    {
        HashSet<string> fontNames = new HashSet<string>();
        using (InstalledFontCollection installedFonts = new InstalledFontCollection())
        {
            foreach (var fontFamily in installedFonts.Families)
            {
                fontNames.Add(fontFamily.Name);
            }
        }
        return fontNames;
    }
    #endregion FontsList

    #region FontSizes
    public static HashSet<double> FontSizes { get; set; } = GetFontSizes();

    private static HashSet<double> GetFontSizes()
    {
        HashSet<double> fontSizes = new HashSet<double>();
        for (int i = 0; i <= 96; i++)
        {
            if ((i % 2 == 0 && i % 3 == 0) || i % 4 == 0)
                _ = fontSizes.Add(i);
        }
        return fontSizes;
    }
    #endregion FontSizes
    #endregion Static Lists

    #region FontsList
    private Brush? _background;
    public Brush? Background
    {
        get => _background;
        set => SetProperty(ref _background, value);
    }
    #endregion FontsList

    #region INotifyPropertyChanged implementation
    public event PropertyChangedEventHandler? PropertyChanged;
    //public event PropertyChangedEventHandler? SettingsChanged;

    private void OnSettingsChanged(object? sender, PropertyChangedEventArgs e)
    {
        //SettingsChanged?.Invoke(this, e);
        Console.WriteLine(e.PropertyName);
    }

    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }


    protected virtual bool SetProperty<T>(ref T member, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(member, value)) return false;
        member = value;
        OnPropertyChanged(propertyName ?? throw new ArgumentNullException(nameof(propertyName)));
        return true;
    }
    #endregion
}
