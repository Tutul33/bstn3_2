using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace feedback.ViewModels
{
    public static class dataUtilities
    {
        public const String conString = @"Server=TUTULPC\TUTUL;Database=FeedBack;Trusted_Connection=True;";
        public const string SpGetPostData = "[dbo].[SpGetPostData]";
    }
}
