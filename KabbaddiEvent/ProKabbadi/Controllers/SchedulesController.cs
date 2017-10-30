using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using KabbaddiEvent;
using ProKabbadi.Models;

namespace ProKabbadi.Controllers
{
    public class SchedulesController : Controller
    {
        private ProKabbadiContext db = new ProKabbadiContext();
               
        // GET: Schedules
        public ActionResult Index()
        {
            var schedules = db.Schedules.Include(s => s.PlayGround).Include(s => s.TeamA).Include(s => s.TeamB);
            return View("Index", schedules.ToList());
        }

        // GET: Schedules/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Schedule schedule = db.Schedules.Find(id);
            if (schedule == null)
            {
                return HttpNotFound();
            }
            return View(schedule);
        }

        [HttpGet]
        public ActionResult GererateScheduleForKabbaddi()
        {
            GererateSchedule();
            return RedirectToAction("Index");
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private void GererateSchedule()
        {
            ClearSchedule();

            var schedules = GenerateTeamCombination();

            var totalTeams = db.Events.FirstOrDefault().NumberOfTeams;
            var matchesPerTeam = (totalTeams - 1) * 2;
            var totalDays = (matchesPerTeam * 2) * 10;

            var matches = GenerateEmptySchedule(totalDays);
           

            foreach (var item in schedules)
            {
                for (int i = 0; i <= totalDays; i++)
                {
                    var steams = GetTeams(schedules, matches[i].Day);

                    bool teamMatchScheduled = steams.Contains(item.TeamA.TeamName) || steams.Contains(item.TeamB.TeamName);

                    if (!teamMatchScheduled && null == matches[i].TeamA)
                    {
                        item.Day = matches[i].Day;
                        item.IsFirst = matches[i].IsFirst;
                        matches[i].TeamA = item.TeamA;
                        break;
                    }
                }
            }

            schedules = schedules.OrderBy(t => t.Day).ToList();
            foreach (var item in schedules)
            {
                db.Schedules.Add(item);
            }
            db.SaveChanges();
        }

        private void ClearSchedule()
        {
            var all = from c in db.Schedules select c;
            db.Schedules.RemoveRange(all);
            db.SaveChanges();
        }
        private List<string> GetTeams(List<Schedule> matches, DateTime currentDay)
        {
            List<string> teams = new List<string>();
            var scheduledTeams = from match in matches
                                 where (match.Day.Equals(currentDay) || match.Day.Equals(currentDay.AddDays(1)) || match.Day.Equals(currentDay.AddDays(-1)))
                                 select match;
            if (null != scheduledTeams)
            {
                teams = ((from team in scheduledTeams
                              select team.TeamA.TeamName).Union(from team in scheduledTeams
                                                                select team.TeamB.TeamName)).ToList();
            }
            return teams;
        }

        private List<Schedule> GenerateTeamCombination()
        {
            var teams = new List<Team>();
            var schedules = new List<Schedule>();
            foreach (var item in db.Teams.ToList())
            {
                teams.Add(item);
            }


            var matchStartDate = DateTime.MinValue;
            foreach (var item in db.Teams.ToList())
            {
                teams.Remove(item);
                if (null != teams && teams.Any())
                {
                    foreach (var item1 in teams)
                    {
                        schedules.Add(new KabbaddiEvent.Schedule()
                        {
                            TeamA = item,
                            TeamB = item1,
                            PlayGround = item.HomeGround,
                            Day = matchStartDate,
                            IsFirst = true
                        });
                    }
                }
            }

            teams = new List<Team>();
            foreach (var item in db.Teams.ToList())
            {
                teams.Add(item);
            }

            foreach (var item in db.Teams.ToList())
            {
                teams.Remove(item);
                if (null != teams && teams.Any())
                {
                    foreach (var item1 in teams)
                    {
                        schedules.Add(new KabbaddiEvent.Schedule()
                        {
                            TeamA = item,
                            TeamB = item1,
                            PlayGround = item.HomeGround,
                            Day = matchStartDate,
                            IsFirst = false
                        });
                    }
                }
            }
            return schedules;
        }

        private List<Schedule> GenerateEmptySchedule(int totalDays)
        {
            var startDate = db.Events.FirstOrDefault().StartDate;
            List<Schedule> matches = new List<Schedule>();
            for (int i = 0; i <= totalDays; i = i + 2)
            {
                int addDays = i / 2;

                var match = new Schedule();
                match.Day = startDate.AddDays(addDays);
                match.IsFirst = true;
                matches.Add(match);

                match = new Schedule();
                match.Day = startDate.AddDays(addDays);
                match.IsFirst = false;
                matches.Add(match);
            }

            return matches;
        }
    }
}
