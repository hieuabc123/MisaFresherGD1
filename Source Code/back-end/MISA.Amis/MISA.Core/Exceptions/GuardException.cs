using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Exceptions
{
    public class GuardException:Exception
    {
        public GuardException(string msg, object data = null):base(msg)
        {
            var MsgError = new
            {
                Msg = msg,
                FileNotValid = data
            };
            this.Data.Add("Error", MsgError);
        }
    }
}
