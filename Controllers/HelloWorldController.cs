using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using my_first_mvc.Models;
using static my_first_mvc.Models.Dbmanager;

namespace my_first_mvc.Controllers
{
    public class HelloWorldController : Controller
    {
        // GET: HelloWorldController

        //進到HelloWorld/index的事件
        [HttpGet]
        public ActionResult Index()
        {
            Dbmanager dbmanager = new Dbmanager();
            List<Dbmanager.Account> accounts = dbmanager.GetAccounts("物料");
            ViewBag.accounts = accounts;
            return View();
        }


        //進到CreateAccount的事件
        [HttpGet]
        public ActionResult CreateAccount()
        {
            return View();
        }

        //CreateAccount按下新增後的事件
        [HttpPost]
        public ActionResult CreateAccount(Dbmanager.Account account)
        {
            Dbmanager dbmanager = new Dbmanager();
            try
            {
                dbmanager.newAccount(account);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return RedirectToAction("index");
        }

        //抓取edit的id內的修改資料 丟到以下的post事件
        [HttpGet]
        public ActionResult EditAccount(string ID)
        {
            Dbmanager dbmanager = new Dbmanager();
            Account account = dbmanager.getAccount(ID);
            return View(account);
        }

        //到資料庫修改數據
        [HttpPost]
        public ActionResult EditAccount(Dbmanager.Account account)
        {
            Dbmanager dbmanager = new Dbmanager();
            dbmanager.EditAccount(account);
            return RedirectToAction("index");
        }

        //刪除資料
        public ActionResult DelAccount(string ID)
        {
            Dbmanager dbmanager = new Dbmanager();
            dbmanager.DelAccount(ID);
            return RedirectToAction("index");
        }

        // GET: HelloWorldController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: HelloWorldController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HelloWorldController/Create
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

        // GET: HelloWorldController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: HelloWorldController/Edit/5
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

        // GET: HelloWorldController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: HelloWorldController/Delete/5
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
