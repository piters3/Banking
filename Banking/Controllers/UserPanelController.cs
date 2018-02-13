using Banking.Infrastructure;
using Banking.Models;
using Microsoft.AspNet.Identity;
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
                To = p.To
            });


            return View(new UserPanelViewModel() {
                BankAccount = userBankAccount,
                Payments = userPayments
            });
        }
    }
}