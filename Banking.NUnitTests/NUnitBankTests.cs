using Banking.Controllers;
using Banking.Entities;
using Banking.Infrastructure;
using Banking.Models;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Banking.NUnitTests
{
    [TestFixture]
    public class NUnitBankTests
    {
        private IRepository _repository;
        private HomeController _homeController;
        private BankAccountsAdminController _bankController;
        private UserPanelController _userPanelController;
        private PaymentsAdminController _paymentsAdminController;


        [SetUp]
        protected void SetUp()
        {
            _repository = Substitute.For<IRepository>();
            _homeController = new HomeController();
            _bankController = new BankAccountsAdminController(_repository);
            _userPanelController = new UserPanelController(_repository);
            _paymentsAdminController = new PaymentsAdminController(_repository);
        }


        [Test]
        public void BankAccountsAdmin_Details_Returns_HttpStatusCode_BadRequest_On_Null_Id()
        {
            _repository.GetBankAccount(Arg.Any<Guid>()).Returns((BankAccount)null);

            var httpStatusCodeResult = _bankController.Details(null) as HttpStatusCodeResult;

            Assert.AreEqual(400, httpStatusCodeResult.StatusCode);
        }


        [Test]
        public void Home_Index_Is_Not_Null()
        {
            var viewResult = _homeController.Index() as ViewResult;

            Assert.That(viewResult, Is.Not.Null);
            Assert.NotNull(viewResult);
            Assert.IsNotNull(viewResult);
        }


        [Test]
        public void UserPanel_NewPayment_Throw_Exception_If_Not_Enough_Money()
        {
            var senderBankAccount = new BankAccount(Guid.NewGuid(), 1000);
            var recipientBankAccount = new BankAccount(Guid.NewGuid(), 1000);
            var paymentModel = new NewPaymentViewModel() { Amount = 2000 };
            _repository.GetBankAccount(Arg.Any<Guid>()).Returns(senderBankAccount);
            _repository.GetUserBankAccount(Arg.Any<string>()).Returns(recipientBankAccount);

            Assert.Throws<ArgumentOutOfRangeException>(() => _userPanelController.NewPayment(paymentModel)); 
            Assert.Catch<ArgumentOutOfRangeException>(()=> _userPanelController.NewPayment(paymentModel));
            Assert.That(() => _userPanelController.NewPayment(paymentModel), Throws.TypeOf<ArgumentOutOfRangeException>());
        }


        [TestCase(1000)]
        [TestCase(2000)]
        [TestCase(3000)]
        public void UserPanel_NewPayment_Changes_Bank_Accounts_Balance(int amount)
        {
            int senderInitialBalance = 10000;
            int recipientInitialBalance = 9999;
            var senderBankAccount = new BankAccount(Guid.NewGuid(), senderInitialBalance);
            var recipientBankAccount = new BankAccount(Guid.NewGuid(), recipientInitialBalance);
            var model = new NewPaymentViewModel() { Amount = amount };
            _repository.GetUserBankAccount(Arg.Any<string>()).Returns(senderBankAccount);
            _repository.GetBankAccount(Arg.Any<Guid>()).Returns(recipientBankAccount);

            var viewResult = _userPanelController.NewPayment(model) as ViewResult;

            Assert.AreEqual(recipientBankAccount.Balance, recipientInitialBalance + amount);
            Assert.That(recipientBankAccount.Balance, Is.EqualTo(recipientInitialBalance + amount));
            Assert.AreEqual(senderBankAccount.Balance, senderInitialBalance - amount);
            Assert.That(senderBankAccount.Balance, Is.EqualTo(senderInitialBalance - amount));
        }


        [Test]
        public void Home_Index_Returns_Instance_Of_View_Result()
        {
            var viewResult = _homeController.Index() as ViewResult;

            Assert.That(viewResult, Is.InstanceOf<ViewResult>());
            Assert.That(viewResult, Is.InstanceOf(typeof(ViewResult)));
            Assert.IsInstanceOf(typeof(ViewResult), viewResult);
            Assert.IsInstanceOf<ViewResult>(viewResult);
            Assert.IsAssignableFrom(typeof(ViewResult), viewResult);
            Assert.IsAssignableFrom<ViewResult>(viewResult);
        }


        //[Test]
        //[Ignore("Pomijam")]
        //public void Ignore()
        //{
        //    int a = 2, b = 3, correctResult = 5;

        //    int result = a + b;

        //    Assert.AreEqual(result, correctResult);
        //}


        [Test]
        public void BankAccountsAdmin_Create_Returns_TempData_Message()
        {
            BankAccountViewModel model = new BankAccountViewModel()
            {
                AccountNumber = Guid.NewGuid(),
                OpeningDate = new DateTime(2018, 01, 01)
            };

            var viewResult = _bankController.Create(model) as ViewResult;

            StringAssert.Contains("Konto bankowe", _bankController.TempData["message"].ToString());
            Assert.That(_bankController.TempData["message"].ToString(), Contains.Substring("Konto bankowe"));
            Assert.That(_bankController.TempData["message"].ToString(), Does.Contain("Konto bankowe"));
        }


        [Test]
        public async Task PaymentsAdmin_Details_Returns_Payment_For_Given_Id_Async()
        {
            var paymentToDisplay = new Payment() { Id = 1, Amount = 100, Title = "Tytuł przelewu" };
            _repository.GetPaymentAsync(Arg.Any<int>()).Returns((paymentToDisplay));

            var viewResult = await _paymentsAdminController.Details(paymentToDisplay.Id) as ViewResult;
            var model = viewResult.Model as Payment;

            Assert.AreEqual(100, model.Amount);
            Assert.IsNotNull(viewResult);
        }


        //[Test]
        //public void All_Bank_Account_In_List_Are_Not_Null()
        //{
        //    List<BankAccount> bankList = new List<BankAccount>();

        //    for (int i = 0; i < 100000; i++)
        //    {
        //        BankAccount ba = new BankAccount(Guid.NewGuid(), 1000);
        //        bankList.Add(ba);
        //    }

        //    CollectionAssert.AllItemsAreNotNull(bankList);
        //}


        //private void FakeLoggedInUser()
        //{
        //    var validPrincipal = new ClaimsPrincipal(
        //        new[]
        //        {
        //            new ClaimsIdentity(
        //            new[] {new Claim(ClaimTypes.NameIdentifier, "FakeUserId")})
        //        });

        //    var context = Substitute.For<HttpContextBase>();
        //    var request = Substitute.For<HttpRequestBase>();
        //    context.User.Returns(validPrincipal);
        //    context.Request.Returns(request);

        //    _userPanelController.ControllerContext = new ControllerContext(context, new RouteData(), _userPanelController);
        //}
    }
}
