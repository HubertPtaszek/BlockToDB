using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Infrastructure
{
    public class DxAutocompleteParams
    {
        public string SearchValue { get; set; }
        public int? Skip { get; set; }
        public int? Take { get; set; }
    }
}
