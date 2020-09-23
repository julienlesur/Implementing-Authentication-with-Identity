using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recruiting.Web.Models.ViewModels
{
    public class CheckBoxesGroupViewModel
    {
        public List<string> Elements { get; set; }
        public List<string> SelectedElements { get; set; }
        public string PropertyName { get; set; }
        public string PropertyChangedName { get; set; }
        public string GroupLegend { get; set; }
    }
}
