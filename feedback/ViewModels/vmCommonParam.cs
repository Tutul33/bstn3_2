using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace feedback.ViewModels
{
    public class vmCommonParam
    {
        public int? pageNumber { get; set; }
        public int? pageSize { get; set; }
        public string search  { get; set; }
        public bool IsPaging { get; set; }
        public int id { get; set; }
        public int UserId { get; set; }
        public bool opinion { get; set; }
    }
}
