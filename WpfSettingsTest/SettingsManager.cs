﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Configuration;
using System.Reflection;

namespace WpfSettingsTest;

public class SettingsManager
{
    private readonly ApplicationSettingsBase _settings;

    public SettingsManager(ApplicationSettingsBase settings)
    {
        _settings = settings;
    }

    public void Load()
    {
        _settings.Reload();

        foreach (SettingsProperty property in _settings.Properties)
        {
            if (property.IsReadOnly)
            {
                continue;
            }

            var value = _settings[property.Name];

            if (value == null)
            {
                continue;
            }

            var propertyInfo = _settings.GetType().GetProperty(property.Name);
            if (propertyInfo != null && propertyInfo.CanWrite)
            {
                propertyInfo.SetValue(_settings, value, null);
            }
        }
    }

    public void Save(string propertyName = null)
    {
        if (propertyName != null)
        {
            var property = _settings.Properties[propertyName];
            var value = _settings[property.Name];
            Properties.Settings.Default[propertyName] = value;
        }
        else
        {
            foreach (SettingsProperty property in _settings.Properties)
            {
                if (property.IsReadOnly)
                {
                    continue;
                }

                var value = _settings[property.Name];

                if (value != null)
                {
                    Properties.Settings.Default[property.Name] = value;
                }
            }
        }

        Properties.Settings.Default.Save();
    }

    public void Reset(string propertyName = null)
    {
        if (propertyName != null)
        {
            var property = _settings.Properties[propertyName];
            var defaultValueAttribute = (DefaultValueAttribute)property.Attributes[typeof(DefaultValueAttribute)];
            var defaultValue = defaultValueAttribute?.Value ?? property.PropertyType.GetDefaultValue();

            _settings[property.Name] = defaultValue;
            Properties.Settings.Default[propertyName] = defaultValue;
        }
        else
        {
            Properties.Settings.Default.Reset();

            foreach (SettingsProperty property in _settings.Properties)
            {
                if (property.IsReadOnly)
                {
                    continue;
                }

                var defaultValueAttribute = (DefaultValueAttribute)property.Attributes[typeof(DefaultValueAttribute)];
                var defaultValue = defaultValueAttribute?.Value ?? property.PropertyType.GetDefaultValue();

                _settings[property.Name] = defaultValue;
            }
        }
    }

    public T GetSetting<T>(string propertyName)
    {
        var property = _settings.Properties[propertyName];
        return (T)_settings[property.Name];
    }

    public void SetSetting<T>(string propertyName, T value)
    {
        var property = _settings.Properties[propertyName];
        _settings[property.Name] = value;
    }

}

public static class TypeExtensions
{
    public static object GetDefaultValue(this Type type)
    {
        return type.IsValueType ? Activator.CreateInstance(type) : null;
    }
}