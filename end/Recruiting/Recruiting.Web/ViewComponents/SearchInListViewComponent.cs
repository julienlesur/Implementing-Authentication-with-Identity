﻿using Microsoft.AspNetCore.Mvc;
using Recruiting.Web.Models.ViewModels;

namespace Recruiting.Web.ViewComponents
{
    public class SearchInListViewComponent : ViewComponent
    {

        public SearchInListViewComponent()
        {
        }

        public IViewComponentResult Invoke(string sort, string search,string controller, string action = "List")
        {
            SearchInListViewModel searchInList = new SearchInListViewModel
            {
                Controller = controller,
                Action = action,
                SearchText = search,
                CurrentSort = sort
            };
            return View(searchInList);
        }
    }
}
