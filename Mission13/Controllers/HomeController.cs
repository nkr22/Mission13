using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Mission13.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Mission13.Controllers
{
    public class HomeController : Controller
    {
        private IBowlersRepository _repo { get; set; }
        public HomeController(IBowlersRepository temp)
        {
            _repo = temp;
        }

        public IActionResult Index(string bowlerTeam)
        {
            var blah = _repo.Bowlers
                .Include(b => b.Team)
                .Where(b => b.Team.TeamName == bowlerTeam || bowlerTeam == null)
                .OrderBy(b => b.BowlerLastName)
                .ToList();
            return View(blah);
        }

        [HttpGet]
        public IActionResult AddBowler()
        {
            ViewBag.Teams = _repo.Teams.ToList();

            return View("AddBowler");
        }

        [HttpPost]
        public IActionResult AddBowler(Bowler b)
        {

            if (ModelState.IsValid)
            {
                _repo.CreateBowler(b);
                return RedirectToAction("Index");
            }
            else
            {
                return View(b);
            }
        }


        [HttpGet]
        public IActionResult Edit(int bowlerid)
        {
            ViewBag.Teams = _repo.Teams.ToList();            

            var bowler = _repo.Bowlers.Single(x => x.BowlerID == bowlerid);
            ViewBag.Team= _repo.Teams.Single(x => x.TeamID==bowler.TeamID);

            return View("AddBowler", bowler);

        }

        [HttpPost]
        public IActionResult Edit(Bowler b)
        {
            if (ModelState.IsValid)
            {
                _repo.SaveBowler(b);

                return RedirectToAction("Index");
            }
            else
            {
                return View("AddBowler", b);
            }

        }

        public IActionResult Delete(Bowler b)
        {
            _repo.DeleteBowler(b);

            return RedirectToAction("Index");
        }

    }
}
