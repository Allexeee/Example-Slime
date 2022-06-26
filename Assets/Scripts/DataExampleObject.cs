using System;
using Newtonsoft.Json.Linq;

namespace PixeyeGames.ExampleSlime
{
  public class DataExampleObject : JObject
  {
    public override JToken this[object key]
    {
      get
      {
        if (!(key is string propertyName))
        {
          throw new Exception();
        }

        return this[propertyName];
      }
      set
      {
        if (!(key is string propertyName))
        {
          throw new Exception();
        }

        this[propertyName] = value;
      }
    }

    public new JToken this[string propertyName]
    {
      get
      {
        JProperty property = Property(propertyName, StringComparison.Ordinal);

        if (property == null)
        {
          property = new JProperty(propertyName, new DataExampleObject());
          AddProp(property);
          OnPropertyChanged(propertyName);
          return GetProp(property);
        }

        return GetProp(property);
      }
      set
      {
        JProperty property = Property(propertyName, StringComparison.Ordinal);
        if (property != null)
        {
          SetProp(property, value);
        }
        else
        {
          property = new JProperty(propertyName, value);

          AddProp(property);
        }

        OnPropertyChanged(propertyName);
      }
    }

    void SetProp(JProperty property, JToken value)
    {
      var propertyName = property.Name;
      var path         = Path.IsNullOrEmpty() ? $"{propertyName}.Set" : $"{Path}.{propertyName}.Set";
      if (IoC.TryGet(path, IoC.Args.Write(Root, value), out ICommand command))
      {
        property.Value = value;
        command.Execute();
      }
      else
        property.Value = value;
    }

    void AddProp(JProperty property)
    {
      var propertyName = property.Name;
      var value        = property.Value;
      var path         = Path.IsNullOrEmpty() ? $"{propertyName}.Set" : $"{Path}.{propertyName}.Set";
      if (IoC.TryGet(path, IoC.Args.Write(Root, value), out ICommand command))
      {
        Add(property);
        command.Execute();
      }
      else
        Add(property);
    }

    JToken GetProp(JProperty property)
    {
      var propertyName = property.Name;
      var path         = Path.IsNullOrEmpty() ? $"{propertyName}.Get" : $"{Path}.{propertyName}.Get";
      if (IoC.TryGet(path, IoC.Args.Write(property), out JToken result))
        return result;
      else
        return property.Value;
    }
  }
}