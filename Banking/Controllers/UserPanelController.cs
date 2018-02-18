using Banking.Entities;
using Banking.Infrastructure;
using Banking.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Banking.Controllers
{
    [Authorize]
    public class UserPanelController : Controller
    {
        private IRepository _repo;

        public UserPanelController(IRepository repo)
        {
            _repo = repo;
        }

        public ActionResult Index()
        {
            var id = User.Identity.GetUserId();
            var ba = _repo.GetUserBankAccount(id);

            var userBankAccount = new BankAccountViewModel()
            {
                AccountNumber = ba.AccountNumber,
                AvailableFunds = ba.AvailableFunds,
                Balance = ba.Balance,
                DebitLimit = ba.DebitLimit,
                Frozen = ba.Frozen,
                Locks = ba.Locks,
                OpeningDate = ba.OpeningDate,
                User = ba.User,
                UserId = ba.UserId
            };

            var userPayments = _repo.GetUserPayments(id).Select(p => new PaymentUserViewModel()
            {
                Amount = p.Amount,
                From = p.From,
                Id = p.Id,
                OperationType = p.OperationType,
                PaymentDate = p.PaymentDate,
                Title = p.Title,
                To = p.To,
                BalanceAfterOperation = p.BalanceAfterOperation
            });

            return View(new UserPanelViewModel()
            {
                BankAccount = userBankAccount,
                Payments = userPayments.Take(6)
            });
        }

        public ActionResult Payments()
        {
            var id = User.Identity.GetUserId();

            var userPayments = _repo.GetUserPayments(id).Select(p => new PaymentUserViewModel()
            {
                Amount = p.Amount,
                From = p.From,
                Id = p.Id,
                OperationType = p.OperationType,
                PaymentDate = p.PaymentDate,
                Title = p.Title,
                To = p.To,
                BalanceAfterOperation = p.BalanceAfterOperation
            });

            return View(userPayments);
        }


        public ActionResult NewPayment()
        {
            var id = User.Identity.GetUserId();
            var from = _repo.GetUserBankAccount(id);
            ViewBag.AccountNumber = from.AccountNumber.ToString().ToUpper();
            ViewBag.AvailableFunds = from.AvailableFunds;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NewPayment(NewPaymentViewModel model)
        {
            //var id = User.Identity.GetUserId();
            var id = "id";
            var from = _repo.GetUserBankAccount(id);
            var to = _repo.GetBankAccount(model.AccountNumber);

            if (ModelState.IsValid)
            {
                if (from.AvailableFunds > model.Amount)
                {
                    Payment p = new Payment()
                    {
                        Id = model.Id,
                        From = from,
                        To = to,
                        Amount = model.Amount * -1,
                        PaymentDate = model.PaymentDate,
                        Title = model.Title,
                        OperationType = TypeOfOperation.TransferToAccount,
                        BalanceAfterOperation = from.Balance - model.Amount
                    };

                    p.From.Balance -= model.Amount;
                    p.To.Balance += model.Amount;

                    _repo.Insert(p);
                    _repo.Save();
                    TempData["message"] = string.Format("Pomyślnie przelano pieniądze!");
                    return RedirectToAction("Index");
                }
                else
                {
                    //TempData["error"] = string.Format("Za mała ilość środków na koncie!");
                    //return RedirectToAction("Index");
                    throw new ArgumentOutOfRangeException("Za mało hasju");
                }
            }
            ModelState.AddModelError("", "Błąd");
            ViewBag.AccountNumber = from.AccountNumber.ToString().ToUpper();
            ViewBag.AvailableFunds = from.AvailableFunds;
            return View(model);
        }
    }
}