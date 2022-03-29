using Microsoft.AspNetCore.Mvc;
using Mission13.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mission13.Components
{
    public class TeamsViewComponent: ViewComponent 
    {
        private IBowlersRepository _repo { get; set; }
        public TeamsViewComponent(IBowlersRepository temp)
        {
            _repo = temp;
        }

        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedType = RouteData?.Values["bowlerTeam"];
            var teams = _repo.Teams
                .Select(x => x.TeamName)
                .Distinct()
                .OrderBy(x => x);
            return View(teams);
        }
    }
}
