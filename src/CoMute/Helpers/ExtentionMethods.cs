using CoMute.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoMute.Helpers
{
  public static class ExtentionMethods
  {
    public static User WithoutPassword(this User user)
    {
      user.Password = null;
      return user;
    }

    public static T GetPropertyValue<T>(object obj, string propName)
    {
      return (T)obj.GetType().GetProperty(propName).GetValue(obj, null);
    }

    public static bool ComparePropertyValues<T>(T oldObj, T newObj, string propName)
    {
      var oldValue = oldObj.GetType().GetProperty(propName).GetValue(oldObj, null);
      var newValue = newObj.GetType().GetProperty(propName).GetValue(newObj, null);

      return oldValue == newValue;
    }
  }
}
