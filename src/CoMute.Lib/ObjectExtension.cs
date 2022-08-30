namespace CoMute.Lib
{
    //  these extension methods are used to convert between the database object and DTO objects
    static class ObjectExtension
    {
        public static void CopyPropertiesFrom(this object target, object source)
        {
            var sourceProperties = source.GetType().GetProperties();
            var targetProperties = target.GetType().GetProperties();

            foreach (var fromProperty in sourceProperties)
            {
                foreach (var toProperty in targetProperties)
                {
                    if (fromProperty.Name == toProperty.Name && fromProperty.PropertyType == toProperty.PropertyType)
                    {
                        toProperty.SetValue(target, fromProperty.GetValue(source));
                        break;
                    }
                }
            }
        }

        public static T CopyPropertiesTo<T>(this object source) where T : new()
        {
            var target = new T();
            target.CopyPropertiesFrom(source);
            return target;
        }
    }
}