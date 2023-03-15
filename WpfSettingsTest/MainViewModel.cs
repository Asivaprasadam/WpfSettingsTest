using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Drawing.Text;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows;
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

        // Init from settings
        FontSize = (double)SettingsManager.Instance.GetPropertyValue(nameof(FontSize));
        TextFont = (string)SettingsManager.Instance.GetPropertyValue(nameof(TextFont));
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

    #region Property List
    private Brush? _background;
    public Brush? Background
    {
        get => _background;
        set => SetProperty(ref _background, value);
    }

    private string _textFont;
    public string TextFont
    {
        get => _textFont;
        set => SetProperty(ref _textFont, value);
    }

    private double _fontSize;
    public double FontSize
    {
        get => _fontSize;
        set => SetProperty(ref _fontSize, value);
    }

    public string UserConfigPath => ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal).FilePath;

    #endregion  Property List

    #region INotifyPropertyChanged implementation
    public event PropertyChangedEventHandler? PropertyChanged;
    //public event PropertyChangedEventHandler? SettingsChanged;

    private void OnSettingsChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (sender == null) return;

        Type type = sender.GetType();
        PropertyInfo propertyInfo = type.GetProperty(e.PropertyName);

        if (propertyInfo == null) return;

        var value = propertyInfo.GetValue(sender, null);
        SettingsManager.Instance.SetPropertyValue(e.PropertyName, value);
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

    internal void Save()
    {
        SettingsManager.Instance.Save(nameof(MainViewModel.FontSize));
        SettingsManager.Instance.Save(nameof(MainViewModel.TextFont));
        SettingsManager.Instance.SetPropertyValue(nameof(MainViewModel.FontSize), FontSize);
        SettingsManager.Instance.SetPropertyValue(nameof(MainViewModel.TextFont), TextFont);
        _ = MessageBox.Show(messageBoxText: "Settings Saved!");

    }

    internal void Restore()
    {
        SettingsManager.Instance.Reset(nameof(MainViewModel.FontSize));
        SettingsManager.Instance.Reset(nameof(MainViewModel.TextFont));

        FontSize = (double)SettingsManager.Instance.GetPropertyValue(nameof(FontSize));
        TextFont = (string)SettingsManager.Instance.GetPropertyValue(nameof(TextFont));
        _ = MessageBox.Show(messageBoxText: "Settings Restored!");
    }
    #endregion
}
