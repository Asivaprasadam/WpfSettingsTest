using System;
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
    public static SettingsManager Instance { get; private set; } = new SettingsManager(Properties.Settings.Default);

    private SettingsManager(ApplicationSettingsBase settings)
    {
        _settings = settings;
    }

    public void Load()
    {
        _settings.Reload();

        foreach (SettingsProperty property in _settings.Properties)
        {
            if (property.IsReadOnly)
                continue;

            var value = _settings[property.Name];

            if (value == null)
                continue;

            var propertyInfo = _settings.GetType().GetProperty(property.Name);
            if (propertyInfo != null && propertyInfo.CanWrite)
                propertyInfo.SetValue(_settings, value, null);
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
                    continue;

                var value = _settings[property.Name];

                if (value != null)
                    Properties.Settings.Default[property.Name] = value;
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
                    continue;

                var defaultValueAttribute = (DefaultValueAttribute)property.Attributes[typeof(DefaultValueAttribute)];
                var defaultValue = defaultValueAttribute?.Value ?? property.PropertyType.GetDefaultValue();

                _settings[property.Name] = defaultValue;
            }
        }
    }

    public object GetPropertyValue(string propertyName)
    {
        var property = _settings.Properties[propertyName];
        return _settings[property.Name];
    }

    public void SetPropertyValue(string propertyName, object value)
    {
        var propertyInfo = _settings.GetType().GetProperty(propertyName);
        if (propertyInfo != null && propertyInfo.CanWrite)
            propertyInfo.SetValue(_settings, Convert.ChangeType(value, propertyInfo.PropertyType), null);
    }

    public List<string> GetProperties()
    {
        List<string> properties = new List<string>();
        foreach (SettingsProperty property in _settings.Properties)
            properties.Add(property.Name);
        return properties;
    }
}

public static class TypeExtensions
{
    public static object? GetDefaultValue(this Type type) => type.IsValueType ? Activator.CreateInstance(type) : null;
}
