using Banking.Entities;
using Banking.Infrastructure;
using Banking.Models;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
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
                var from = _repo.GetBankAccount(model.From.AccountNumber);
                var to = _repo.GetBankAccount(model.To.AccountNumber);

                Payment p = new Payment()
                {
                    Id = model.Id,
                    From = from,
                    To = to,
                    Amount = model.Amount,
                    PaymentDate = model.PaymentDate,
                    Title = model.Title,
                    OperationType = model.OperationType,
                };

                if (p.OperationType == TypeOfOperation.PaymentToATM)
                {
                    p.Title = "Wpłata";
                    p.To.Balance += model.Amount;//razy 2 ??     
                    p.From.Balance += model.Amount;//razy 2 ?? 
                    p.BalanceAfterOperation = p.To.Balance;
                    _repo.Insert(p);
                    _repo.Save();
                    TempData["message"] = string.Format("Wpłacono pieniądze do bankomatu!");
                    return RedirectToAction("Index");
                }
                else if (p.OperationType == TypeOfOperation.WithdrawalFromATM)
                {
                    if (p.From.AvailableFunds > model.Amount)
                    {
                        p.Title = "Wypłata";
                        p.Amount *= -1;
                        p.To.Balance -= model.Amount;
                        p.From.Balance -= model.Amount;
                        p.BalanceAfterOperation = p.To.Balance;
                        _repo.Insert(p);
                        _repo.Save();
                        TempData["message"] = string.Format("Pieniądze zostały wyciągnięte z bankomatu!");
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["error"] = string.Format("Za mała ilość środków na koncie!");
                        return RedirectToAction("Index");
                        throw new ArgumentOutOfRangeException("Za mało hasju");
                    }
                }
                else if (p.OperationType == TypeOfOperation.TransferToAccount || p.OperationType == TypeOfOperation.CardPayment)
                {
                    if (p.From.AvailableFunds > model.Amount)
                    {
                        p.Amount *= -1;
                        p.From.Balance -= model.Amount;
                        p.To.Balance += model.Amount;
                        p.BalanceAfterOperation = p.To.Balance;
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


        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Payment payment = await _repo.GetPaymentAsync(id);

            if (payment == null)
            {
                return HttpNotFound();
            }
            return View(payment);
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