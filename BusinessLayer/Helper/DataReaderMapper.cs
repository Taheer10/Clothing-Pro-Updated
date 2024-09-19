using System.Data.Common;
using System.Reflection;

namespace  ClothingPro.BusinessLayer.Helper;
public class DataReaderMapper
{
    // int part is column indices (ordinals)
    public static List<T> ConvertToList<T>(DbDataReader dr)
    {
        List<T> list = new List<T>();
        T obj = default(T);
        obj = Activator.CreateInstance<T>();

        dr.Read();
        if (dr.HasRows)
        {               
            foreach (PropertyInfo prop in obj.GetType().GetProperties())
            {
                if (!object.Equals(dr[prop.Name], DBNull.Value))
                {
                    prop.SetValue(obj, dr[prop.Name], null);
                }
            }
        }
        list.Add(obj);

        return list;
    }
}
