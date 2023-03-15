using System;
using System.Collections.Generic;
using System.Configuration;
using System.ComponentModel;
using System.Reflection;

namespace WpfSettingsTest;

public class SettingsManager3<T>
{
    private readonly T _settings;

    public SettingsManager3(T settings)
    {
        _settings = settings;
    }

    public void Load()
    {
        Default.Reload();

        foreach (var property in typeof(T).GetProperties())
        {
            if (property.CanWrite && Default.Properties[property.Name] != null)
            {
                var value = Default[property.Name];
                property.SetValue(_settings, value);
            }
        }
    }

    public void Save(string propertyName = null)
    {
        if (propertyName != null)
        {
            var property = typeof(T).GetProperty(propertyName);
            var value = property.GetValue(_settings);
            Default[propertyName] = value;
        }
        else
        {
            foreach (var property in typeof(T).GetProperties())
            {
                if (property.CanRead && property.CanWrite)
                {
                    var value = property.GetValue(_settings);
                    Default[property.Name] = value;
                }
            }
        }

        Default.Save();
    }

    public void Reset(string propertyName = null)
    {
        if (propertyName != null)
        {
            var property = typeof(T).GetProperty(propertyName);
            var defaultValueAttribute = property.GetCustomAttribute<DefaultValueAttribute>();
            var defaultValue = defaultValueAttribute?.Value ?? property.PropertyType.GetDefaultValue();

            property.SetValue(_settings, defaultValue);
            Default[propertyName] = defaultValue;
        }
        else
        {
            Default.Reset();

            foreach (var property in typeof(T).GetProperties())
            {
                if (property.CanWrite)
                {
                    var defaultValueAttribute = property.GetCustomAttribute<DefaultValueAttribute>();
                    var defaultValue = defaultValueAttribute?.Value ?? property.PropertyType.GetDefaultValue();

                    property.SetValue(_settings, defaultValue);
                }
            }
        }
    }

    private static ApplicationSettingsBase Default => typeof(T).GetProperty("Default")?.GetValue(null) as ApplicationSettingsBase;

}
