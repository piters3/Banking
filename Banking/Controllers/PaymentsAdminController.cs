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
    public class PaymentsAdminController : Controller
    {
        private IRepository _repo;

        public PaymentsAdminController(IRepository repo)
        {
            _repo = repo;
        }

        public ActionResult Index()
        {
            var payments = _repo.GetPayments().Select(p => new PaymentAdminViewModel()
            {
                Amount = p.Amount,
                From = p.From,
                Id = p.Id,
                OperationType = p.OperationType,
                PaymentDate = p.PaymentDate,
                Title = p.Title,
                To = p.To
            });
            return View(payments);
        }


        public ActionResult Create()
        {
            ViewBag.BankAccountsList = new SelectList(_repo.GetBankAccounts(), "AccountNumber", "User.UserName");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PaymentAdminViewModel model)
        {
            if (ModelState.IsValid)
            {
                Payment p = new Payment()
                {
                    Id = model.Id,
                    From = _repo.GetBankAccount(model.From.AccountNumber),
                    To = _repo.GetBankAccount(model.To.AccountNumber),
                    Amount = model.Amount,
                    PaymentDate = model.PaymentDate,
                    Title = model.Title,
                    OperationType = model.OperationType,
                };

                if (p.From.AvailableFunds > model.Amount)
                {
                    p.From.Balance -= model.Amount;
                    p.To.Balance += model.Amount;
                    _repo.Insert(p);
                    _repo.Save();
                    TempData["message"] = string.Format("Płatność została dodana!");
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["error"] = string.Format("Za mała ilość środków na koncie!");
                    return RedirectToAction("Index");
                    throw new ArgumentOutOfRangeException("Za mało hasju");
                }           
            }
            ViewBag.BankAccountsList = new SelectList(_repo.GetBankAccounts(), "AccountNumber", "User.UserName");
            ModelState.AddModelError("", "Błąd");
            return View(model);
        }


        public ActionResult Edit(int id)
        {
            Payment p = _repo.GetPayment(id);
            if (p == null)
            {
                return HttpNotFound();
            }
            PaymentAdminViewModel model = new PaymentAdminViewModel()
            {
                Id = p.Id,
                From = p.From,
                To = p.To,
                Amount = p.Amount,
                PaymentDate = p.PaymentDate,
                Title = p.Title,
                OperationType = p.OperationType,
            };
            ViewBag.BankAccountsList = new SelectList(_repo.GetBankAccounts(), "AccountNumber", "User.UserName");

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PaymentAdminViewModel model)
        {
            if (ModelState.IsValid)
            {
                Payment p = new Payment()
                {
                    Id = model.Id,
                    From = model.From,
                    To = model.To,
                    Amount = model.Amount,
                    PaymentDate = model.PaymentDate,
                    Title = model.Title,
                    OperationType = model.OperationType,
                };

                _repo.Update(p);
                _repo.Save();

                TempData["message"] = string.Format("Płatność została zedytowana!");
                return RedirectToAction("Index");
            }
            ViewBag.BankAccountsList = new SelectList(_repo.GetBankAccounts(), "AccountNumber", "User.UserName");
            ModelState.AddModelError("", "Błąd");
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            Payment p = _repo.GetPayment(id);
            if (p == null)
            {
                return HttpNotFound();
            }
            _repo.Delete(p);
            _repo.Save();
            TempData["message"] = string.Format("Płatność została usunięta!");
            return RedirectToAction("Index");
        }
    }
}