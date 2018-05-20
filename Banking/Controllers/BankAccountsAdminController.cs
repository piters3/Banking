using Banking.Entities;
using Banking.Infrastructure;
using Banking.Models;
using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Banking.Controllers
{
    [Authorize(Roles = "admin")]
    public class BankAccountsAdminController : Controller
    {
        private IRepository _repo;

        public BankAccountsAdminController(IRepository repo)
        {
            _repo = repo;
        }


        public ActionResult Index()
        {
            var accounts = _repo.GetBankAccounts().Select(a => new BankAccountViewModel()
            {
                AccountNumber = a.AccountNumber,
                AvailableFunds = a.AvailableFunds,
                Balance = a.Balance,
                DebitLimit = a.DebitLimit,
                Locks = a.Locks,
                OpeningDate = a.OpeningDate,
                Frozen = a.Frozen,
                User = a.User
            });
            return View(accounts);
        }


        public ActionResult Create()
        {
            ViewBag.UsersList = new SelectList(_repo.GetUsers(), "Id", "UserName");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BankAccountViewModel model)
        {
            if (ModelState.IsValid)
            {
                BankAccount ba = new BankAccount()
                {
                    AvailableFunds = model.AvailableFunds,
                    Balance = model.Balance,
                    DebitLimit = model.DebitLimit,
                    Frozen = model.Frozen,
                    Locks = model.Locks,
                    OpeningDate = model.OpeningDate,
                    UserId = model.UserId
                };

                _repo.Insert(ba);
                _repo.Save();
                TempData["message"] = string.Format("Konto bankowe zostało dodane!");
                return RedirectToAction("Index");
            }
            ViewBag.UsersList = new SelectList(_repo.GetUsers(), "Id", "UserName", model.UserId);
            ModelState.AddModelError("", "Popraw błędy formularza");
            return View(model);
        }


        public ActionResult Edit(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BankAccount ba = _repo.GetBankAccount(id);
            if (ba == null)
            {
                return HttpNotFound();
            }
            EditBankAccountViewModel model = new EditBankAccountViewModel()
            {
                AccountNumber = ba.AccountNumber,
                Balance = ba.Balance,
                DebitLimit = ba.DebitLimit,
                Frozen = ba.Frozen,
                Locks = ba.Locks
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditBankAccountViewModel model)
        {
            if (ModelState.IsValid)
            {
                BankAccount ba = _repo.GetBankAccount(model.AccountNumber);

                ba.Frozen = model.Frozen;
                ba.AccountNumber = model.AccountNumber;
                ba.Balance = model.Balance;
                ba.DebitLimit = model.DebitLimit;
                ba.Locks = model.Locks;

                _repo.Update(ba);
                _repo.Save();

                TempData["message"] = string.Format("Konto bankowe zostało zedytowane!");
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Błąd");
            return View(model);
        }


        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BankAccount ba = _repo.GetBankAccount(id);

            if (ba == null)
            {
                return HttpNotFound();
            }

            BankAccountViewModel model = new BankAccountViewModel()
            {
                AvailableFunds = ba.AvailableFunds,
                Balance = ba.Balance,
                DebitLimit = ba.DebitLimit,
                Frozen = ba.Frozen,
                Locks = ba.Locks,
                OpeningDate = ba.OpeningDate,
                AccountNumber = ba.AccountNumber,
                User = ba.User
            };

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BankAccount ba = _repo.GetBankAccount(id);
            if (ba == null)
            {
                return HttpNotFound();
            }
            _repo.Delete(ba);
            _repo.Save();
            TempData["message"] = string.Format("Konto bankowe zostało usunięte!");
            return RedirectToAction("Index");
        }
    }
}