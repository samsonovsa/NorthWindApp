using Microsoft.AspNetCore.Mvc;
using NorthWindApp.Models.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace NorthWindApp.ViewComponents
{
    public class BreadCrumbsViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            if (Request.Path.HasValue)
            {
                var breadCrumbs = CreateBreadCrumbs(Request.Path.Value);
                return View(null, breadCrumbs);
            }
            else
                return View();
        }

        private List<BreadCrumbViewModel> CreateBreadCrumbs(string path)
        {
            var breadCrumbs = new List<BreadCrumbViewModel>()
            {
                new BreadCrumbViewModel
                    {
                        Text = "Home",
                        Action = "Index",
                        Controller = "Home",
                        Active = true
                    }
            };

            var paths = path.Split("/");
            int indexOfPage;

            foreach (var item in paths)
            {
                if (string.IsNullOrEmpty(item) || int.TryParse(item, out indexOfPage))
                    continue;

                breadCrumbs.Add(
                    new BreadCrumbViewModel
                    {
                        Text = item,
                        Controller = item,
                        Action = "Index",
                        Active = true
                    });
            }

            breadCrumbs.Last().Active = false;

            return breadCrumbs;
        }
    }
}
