using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Attributes
{
    /// <summary>
    ///     - Check Null value
    /// </summary>
    /// Created By: NTHIEU (28/05/2021)
    [AttributeUsage(AttributeTargets.Property)]
    public class Required : Attribute
    {
        public string Msg;
        public Required(string msg)
        {
            Msg = msg;
        }
    }
    /// <summary>
    ///     - Check Duplicate data in DataBase
    /// </summary>
    /// Created By: NTHIEU (28/05/2021)
    [AttributeUsage(AttributeTargets.Property)]
    public class CheckDuplicateDB : Attribute
    {
        public string Msg;
        public CheckDuplicateDB(string msg)
        {
            Msg = msg;
        }
    }

    /// <summary>
    ///     - Check format date
    /// </summary>
    /// Created By: NTHIEU (28/05/2021)
    [AttributeUsage(AttributeTargets.Property)]
    public class FormatDate : Attribute
    {
    }

    /// <summary>
    ///     - Check value data availble in database
    /// </summary>
    /// Created By: NTHIEU (28/05/2021)
    [AttributeUsage(AttributeTargets.Property)]
    public class CheckAvailable : Attribute
    {
        public string Msg;
        public CheckAvailable(string msg)
        {
            Msg = msg;
        }
    }
}