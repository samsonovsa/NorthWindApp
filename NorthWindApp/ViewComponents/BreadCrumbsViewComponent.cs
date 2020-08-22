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

            var paths = path.Split("/").Where(p=> !string.IsNullOrEmpty(p)).ToList();

            if(paths.Count>0)
                breadCrumbs.Add(
                    new BreadCrumbViewModel
                    {
                        Text = paths[0],
                        Controller = paths[0],
                        Action = "Index",
                        Active = true
                    });

            if (paths.Count > 1)
                breadCrumbs.Add(
                new BreadCrumbViewModel
                {
                    Text = paths[1],
                    Controller = paths[0],
                    Action = paths[1],
                    Active = false
                });

            breadCrumbs.Last().Active = false;

            return breadCrumbs;
        }
    }
}
