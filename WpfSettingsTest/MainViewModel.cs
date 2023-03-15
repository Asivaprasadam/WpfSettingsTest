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

    #region BrushList
    public static HashSet<Brush> Brushes { get; set; } = GetBrushes();

    private static HashSet<Brush> GetBrushes()
    {
        HashSet<Brush> brushes = new HashSet<Brush>();
        brushes.Add(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF00B0FF")));
        brushes.Add(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFF5252")));
        brushes.Add(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFF4081")));
        brushes.Add(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFC51162")));
        brushes.Add(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF7C4DFF")));
        brushes.Add(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFF6D00")));
        brushes.Add(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFF9100")));
        brushes.Add(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFAB00")));
        brushes.Add(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF69F0AE")));
        brushes.Add(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF18FFFF")));
        brushes.Add(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF1DE9B6")));
        brushes.Add(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFEA00")));
        brushes.Add(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFEA80FC")));
        brushes.Add(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFE040FB")));
        brushes.Add(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFD500F9")));
        brushes.Add(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFAA00FF")));
        brushes.Add(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF00E676")));
        brushes.Add(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF64FFDA")));
        brushes.Add(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF1DE9B6")));
        brushes.Add(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF76FF03")));
        brushes.Add(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFD600")));
        brushes.Add(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFF6D00")));
        brushes.Add(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFF1744")));
        brushes.Add(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFDD2C00")));
        brushes.Add(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF2196F3")));
        brushes.Add(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF3F51B5")));
        brushes.Add(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF9C27B0")));
        brushes.Add(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFE91E63")));
        brushes.Add(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFF3D00")));
        brushes.Add(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFF5722")));
        brushes.Add(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFF8F00")));
        brushes.Add(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFE65100")));
        brushes.Add(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF4CAF50")));
        brushes.Add(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF00BCD4")));
        brushes.Add(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF607D8B")));
        brushes.Add(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF9E9E9E")));
        brushes.Add(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF6D4C41")));
        brushes.Add(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFC107")));
        brushes.Add(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF795548")));
        brushes.Add(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF8BC34A")));
        brushes.Add(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFF9800")));
        brushes.Add(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF9C27B0")));
        brushes.Add(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFD32F2F")));
        brushes.Add(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFC2185B")));
        brushes.Add(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF7B1FA2")));
        brushes.Add(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF673AB7")));
        brushes.Add(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF4A148C")));
        brushes.Add(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFE040FB")));
        brushes.Add(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFD500F9")));
        brushes.Add(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFAA00FF")));
        brushes.Add(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF1DE9B6")));

        return brushes;
    }
    #endregion BrushList

    #region BrushList
    public static HashSet<double> FontSizes { get; set; } = GetFontSizes();

    private static HashSet<double> GetFontSizes()
    {
        HashSet<double> fontSizes = new HashSet<double>();
        for (int i = 0; i < 96; i++)
        {
            if ((i % 2 == 0 && i % 3 == 0) || i % 4 == 0)
                _ = fontSizes.Add(i);
        }
        return fontSizes;
    }
    #endregion BrushList
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
