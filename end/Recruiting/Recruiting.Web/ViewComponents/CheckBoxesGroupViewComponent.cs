using Microsoft.AspNetCore.Mvc;
using Recruiting.Web.Models.ViewModels;
using System.Collections.Generic;

namespace Recruiting.Web.ViewComponents
{
    public class CheckBoxesGroupViewComponent : ViewComponent
    {

        public IViewComponentResult Invoke(List<string> elements,
                                            List<string> selectedElements,
                                            string propertyName,
                                            string propertyChangedName,
                                            string groupLegend)
        {
            CheckBoxesGroupViewModel vm = new CheckBoxesGroupViewModel
            {
                Elements = elements,
                SelectedElements = selectedElements,
                PropertyName = propertyName,
                PropertyChangedName = propertyChangedName,
                GroupLegend = groupLegend
            };
            return View(vm);
        }
    }
}
