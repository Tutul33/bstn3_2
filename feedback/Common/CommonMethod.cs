using feedback.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace feedback.Common
{
    public static class CommonMethod
    {
        public static int Skip(vmCommonParam cmncls)
        {
            int skipnumber = 0;
            if (cmncls.pageNumber > 0)
            {
                skipnumber = ((int)cmncls.pageNumber - 1) * (int)cmncls.pageSize;
            }
            return skipnumber;
        }
    }
}
