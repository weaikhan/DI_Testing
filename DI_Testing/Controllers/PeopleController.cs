using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using DI_Testing.Models;
using IRepo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelClass;

namespace DI_Testing.Controllers
{
    public class PeopleController : Controller
    {
        IPeopleInfo m_peopleInfo;
        public PeopleController(IPeopleInfo peopleInfo)
        {
            m_peopleInfo = peopleInfo;
        }
        public ActionResult Index()
        {
            PeopleViewModel peopleViewModel = new PeopleViewModel();

            //List<People> peoples = new List<People>();
            //People people1 = new People("Tan", "OUG", "012367");
            //People people2 = new People("Soo", "Subang", "123123");
            //peoples.Add(people1);
            //peoples.Add(people2);



            var peoples = m_peopleInfo.GetPeopleList().Tables[0].AsEnumerable()
            .Select(dataRow => new People
            {
                ID = dataRow.Field<Int32>("ID"),
                Name = dataRow.Field<string>("Name"),
                Address = dataRow.Field<string>("Address"),
                PhoneNo = dataRow.Field<string>("PhoneNo")
            }).ToList();

            peopleViewModel.peoples = peoples;

            return View(peopleViewModel);
        }

        // GET: PeopleController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PeopleController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PeopleController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PeopleController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PeopleController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PeopleController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PeopleController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
