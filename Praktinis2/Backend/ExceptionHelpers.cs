using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praktinis2
{
    static class ExceptionHelpers 
    {

        /// Kur , Line , Detection , Error
        public static int LineNumber(this Exception ex)
        {
            int n;
            int i = ex.StackTrace.LastIndexOf(" ");
            if (i > -1)
            {
                string s = ex.StackTrace.Substring(i + 1);
                if (int.TryParse(s, out n))
                    return n;
            }
            return -1;
        }
    }

}
