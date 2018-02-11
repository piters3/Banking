using Banking.Entities;
using Banking.Infrastructure;
using Banking.Models;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Banking.Controllers
{
    [Authorize(Roles = "admin")]
    public class PaymentsAdminController : Controller
    {
        private IRepository<Payment> _repo;
        private IRepository<BankAccount> _bankAccountsRepo;

        public PaymentsAdminController(IRepository<Payment> repo, IRepository<BankAccount> bankAccountsRepo)
        {
            _repo = repo;
            _bankAccountsRepo = bankAccountsRepo;
        }

        public ActionResult Index()
        {
            var payments = _repo.GetAll().Select(p => new PaymentAdminViewModel()
            {
                Account = p.Account,
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
            ViewBag.BankAccountsList = new SelectList(_bankAccountsRepo.GetAll(), "AccountNumber", "User.UserName");
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
                    From = model.From,
                    To = model.To,
                    Amount = model.Amount,
                    PaymentDate = model.PaymentDate,
                    Title = model.Title,
                    OperationType = model.OperationType,
                    Account = model.Account
                };

                //p.Account.Balance += model.Amount;


                _repo.Insert(p);
                _repo.Save();
                TempData["message"] = string.Format("Płatność została dodana!");
                return RedirectToAction("Index");
            }
            ViewBag.BankAccountsList = new SelectList(_bankAccountsRepo.GetAll(), "AccountNumber", "User.UserName");
            ModelState.AddModelError("", "Błąd");
            return View(model);
        }


        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payment p = _repo.GetById(id);
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
                Account = p.Account
            };
            ViewBag.BankAccountsList = new SelectList(_bankAccountsRepo.GetAll(), "AccountNumber", "User.UserName");

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
                    Account = model.Account
                };

                _repo.Update(p);
                _repo.Save();

                TempData["message"] = string.Format("Płatność została zedytowana!");
                return RedirectToAction("Index");
            }
            ViewBag.BankAccountsList = new SelectList(_bankAccountsRepo.GetAll(), "AccountNumber", "User.UserName");
            ModelState.AddModelError("", "Błąd");
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payment p = _repo.GetById(id);
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