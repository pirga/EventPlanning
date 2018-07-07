using AutoMapper;
using EventPlanning.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EventPlanning.Controllers
{
    public class EventsController : Controller
    {
        public ActionResult Index()
        {
            using (var db = new ApplicationDbContext())
            {
                var allCurUser = GetAllEventCurUser().ToList();
                var allEvents = db.Events.ToList();
                var allEventsId = allEvents.Select(x => x.Id).ToList();
                var allEventViewModel = Mapper.Map<List<EventViewModel>>(allEvents);

                if (allCurUser != null)
                    if (allCurUser.Count() != 0)
                    {
                        for (int i = 0; i < allCurUser.Count; i++)
                        {
                            var id = allEventsId.IndexOf(allCurUser[i].Id);
                            if (id > -1)
                            {
                                allEventViewModel[id].StateFollow = 1;
                            }
                        }
                    }
                return View(allEventViewModel);
            }
        }

        [Authorize(Roles ="user, admin")]
        [HttpPost]
        public ActionResult RegisterEvent(int idEvent)
        {
            var curUser = GetCurUser();

            if (curUser == null)
            {
                return RedirectToAction("Login", "Account");
            }

            using (var db = new ApplicationDbContext())
            {
                var myEvent = db.Events.Find(idEvent);
                if (myEvent.MaxCountSubscribers - 1 > -1)
                {
                    db.RegisterEvents.Add(new RegisterEvent { IdEvent = idEvent, IdUser = curUser.Id });
                    myEvent.MaxCountSubscribers -= 1;
                    db.Events.AddOrUpdate(myEvent);
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "user, admin")]
        [HttpPost]
        public ActionResult CancelEvent(int idEvent)
        {
            var curUser = GetCurUser();

            if (curUser == null)
            {
                return RedirectToAction("Login", "Account");
            }

            using (var db = new ApplicationDbContext())
            {
                var registerEvent = db.RegisterEvents.First(x => x.IdEvent == idEvent && x.IdUser == curUser.Id);
                var myEvent = db.Events.Find(idEvent);
                if (registerEvent == null)
                {

                }
                else
                {
                    myEvent.MaxCountSubscribers += 1;
                }
                db.RegisterEvents.Remove(registerEvent);
                db.Events.AddOrUpdate(myEvent);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }
        
        public IEnumerable<MyEvent> GetAllEventCurUser()
        {
            var curUser = GetCurUser();
            if (curUser == null)
            {
                return new List<MyEvent>();
            }

            using (var db = new ApplicationDbContext())
            {
                var registerEvents = db.RegisterEvents.Where(x => x.IdUser == curUser.Id).ToList();
                var events = (from myEvent in db.Events.ToList()
                              from regEvent in registerEvents
                              where myEvent.Id == regEvent.IdEvent
                              select myEvent).ToList();
                return events;
            }
        }

        [Authorize(Roles = "user, admin")]
        public ActionResult GetEventsUser()
        {
            var curUser = GetCurUser();
            if (curUser == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var events = Mapper.Map<IEnumerable<EventViewModel>>(GetAllEventCurUser());
            return View(events);
        }

        public string GetDateEvent(int idEvent)
        {
            using (var db = new ApplicationDbContext())
            {
                var dateEvent = db.Events.Find(idEvent).DateEvent;
                var day = dateEvent.Day < 10 ? "0" + dateEvent.Day.ToString() : dateEvent.Day.ToString();
                var month = dateEvent.Month < 10 ? "0" + dateEvent.Month.ToString() : dateEvent.Month.ToString();
                var year = dateEvent.Year;
                var hour = dateEvent.Hour < 10 ? "0" + dateEvent.Hour.ToString() : dateEvent.Hour.ToString();
                var minute = dateEvent.Minute < 10 ? "0" + dateEvent.Minute.ToString() : dateEvent.Minute.ToString();
                var second = dateEvent.Second < 10 ? "0" + dateEvent.Second.ToString() : dateEvent.Second.ToString();
                var normalDateEvent = $"{year}-{month}-{day}T{hour}:{minute}";
                return normalDateEvent;
            }
        }

        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult Create(MyEventViewModel item)
        {
            using (var db = new ApplicationDbContext())
            {
                db.Events.Add(Mapper.Map<MyEvent>(item.MyEvent));
                db.SaveChanges();
                var idEvent = db.Events.ToList().Last().Id;
                return RedirectToAction("Edit", new { id = idEvent });
            }
        }

        public ActionResult Details(int id)
        {
            using (var db = new ApplicationDbContext())
            {
                var myEvent = db.Events.Find(id);
                if (myEvent == null)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    var EventProperties = db.PropertiesEvent.Where(x => x.IdEvent == myEvent.Id).ToList();
                    var model = new MyEventViewModel() { MyEvent = Mapper.Map<EventViewModel>(myEvent), MyEventsProperties = EventProperties };
                    return View(model);
                }
            }
        }

        [Authorize(Roles = "admin")]
        public ActionResult Edit(int id)
        {
            using (var db = new ApplicationDbContext())
            {
                var myEvent = db.Events.Find(id);
                if (myEvent == null)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    var EventProperties = db.PropertiesEvent.Where(x => x.IdEvent == myEvent.Id).ToList();
                    var model = new MyEventViewModel() { MyEvent = Mapper.Map<EventViewModel>(myEvent), MyEventsProperties = EventProperties };
                    return View(model);
                }
            }
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult Edit(MyEventViewModel item)
        {
            using (var db = new ApplicationDbContext())
            {
                var myEvent = db.Events.Find(item.MyEvent.Id);
                if (myEvent != null)
                {
                    db.Events.AddOrUpdate(Mapper.Map<MyEvent>(item.MyEvent));
                    var tt = db.PropertiesEvent.Where(x => x.IdEvent == myEvent.Id).ToList();
                    db.PropertiesEvent.RemoveRange(tt);
                    db.SaveChanges();
                    if (item.Key != null && item.Value != null)
                    {
                        for (int i = 0; i < item.Key.Count; i++)
                        {
                            db.PropertiesEvent.Add(new MyEventProperty { IdEvent = myEvent.Id, Key = item.Key[i], Value = item.Value[i] });
                        }
                    }
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
        }

        [Authorize(Roles = "admin")]
        public ActionResult Delete(int id)
        {
            using (var db = new ApplicationDbContext())
            {
                var myEvent = db.Events.Find(id);
                if (myEvent != null)
                {
                    return View(myEvent);
                }
                return RedirectToAction("Index");
            }
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult Delete(MyEvent item)
        {
            using (var db = new ApplicationDbContext())
            {
                var myEvent = db.Events.Find(item.Id);
                if (myEvent != null)
                {
                    db.Events.Remove(myEvent);
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
        }

        public ApplicationUser GetCurUser()
        {

            if (User.Identity.IsAuthenticated)
            {
                using (var db = new ApplicationDbContext())
                {
                    var user = db.Users.Find(User.Identity.GetUserId());
                    return user;
                }
            }
            else
            {
                return null;
            }
        }
    }
}