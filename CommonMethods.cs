using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KITEPlacement
{
    public class CommonMethods
    {
        #region Method to Create Array list based on the given objects
        public static ArrayList CreateArrayList(object[] value)
        {
            ArrayList _allist = new ArrayList();
            for (int i = 0; i < value.Length; i++)
            {
                _allist.Add(value[i]);
            }

            return _allist;
        }
        #endregion Method to Create Array list based on the given objects
    }
}
