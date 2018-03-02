using Banking.Controllers;
using Banking.Entities;
using Banking.Infrastructure;
using Banking.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Banking.MSTests
{
    [TestClass]
    public class MSBankTests
    {
        private IRepository _repository;
        private HomeController _homeController;
        private BankAccountsAdminController _bankController;
        private UserPanelController _userPanelController;
        private PaymentsAdminController _paymentsAdminController;


        [TestInitialize()]
        public void Initialize()
        {
            _repository = Substitute.For<IRepository>();
            _homeController = new HomeController();
            _bankController = new BankAccountsAdminController(_repository);
            _userPanelController = new UserPanelController(_repository);
            _paymentsAdminController = new PaymentsAdminController(_repository);
        }


        [TestMethod]
        public void BankAccountsAdmin_Details_Returns_HttpStatusCode_BadRequest_On_Null_Id()
        {
            _repository.GetBankAccount(Arg.Any<Guid>()).Returns((BankAccount)null);

            var httpStatusCodeResult = _bankController.Details(null) as HttpStatusCodeResult;

            Assert.AreEqual(400, httpStatusCodeResult.StatusCode);
        }


        [TestMethod]
        public void Home_Index_Is_Not_Null()
        {
            var viewResult = _homeController.Index() as ViewResult;

            Assert.IsNotNull(viewResult);
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void UserPanel_NewPayment_Throw_Exception_If_Not_Enough_Money()
        {
            var senderBankAccount = new BankAccount(Guid.NewGuid(), 1000);
            var recipientBankAccount = new BankAccount(Guid.NewGuid(), 1000);
            var paymentModel = new NewPaymentViewModel() { Amount = 2000 };
            _repository.GetBankAccount(Arg.Any<Guid>()).Returns(senderBankAccount);
            _repository.GetUserBankAccount(Arg.Any<string>()).Returns(recipientBankAccount);

            var viewResult = _userPanelController.NewPayment(paymentModel) as ViewResult;
        }


        [DataTestMethod]
        [DataRow(1000)]
        [DataRow(2000)]
        [DataRow(3000)]
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
            Assert.AreEqual(senderBankAccount.Balance, senderInitialBalance - amount);
        }


        [TestMethod]
        public void Home_Index_Returns_Instance_Of_View_Result()
        {
            var viewResult = _homeController.Index() as ViewResult;

            Assert.IsInstanceOfType(viewResult, typeof(ViewResult));
        }


        //[TestMethod]
        //[Ignore]
        //public void Ignore()
        //{
        //    int a = 2, b = 3, correctResult = 5;

        //    int result = a + b;

        //    Assert.AreEqual(result, correctResult);
        //}


        [TestMethod]
        public void BankAccountsAdmin_Create_Returns_TempData_Message()
        {
            BankAccountViewModel model = new BankAccountViewModel()
            {
                AccountNumber = Guid.NewGuid(),
                OpeningDate = new DateTime(2018, 01, 01)
            };

            var viewResult = _bankController.Create(model) as ViewResult;

            StringAssert.Contains(_bankController.TempData["message"].ToString(), "Konto bankowe");
        }


        [TestMethod]
        public async Task PaymentsAdmin_Details_Async()
        {
            var paymentToDisplay = new Payment() { Id = 1, Amount = 100, Title = "Tytuł przelewu" };
            _repository.GetPaymentAsync(Arg.Any<int>()).Returns((paymentToDisplay));

            var viewResult = await _paymentsAdminController.Details(paymentToDisplay.Id) as ViewResult;
            var model = viewResult.Model as Payment;

            Assert.AreEqual(100, model.Amount);
            Assert.IsNotNull(viewResult);
        }


        [TestMethod]
        public void All_Bank_Account_In_List_Are_Not_Null()
        {
            List<BankAccount> bankList = new List<BankAccount>();

            for (int i = 0; i < 100000; i++)
            {
                BankAccount ba = new BankAccount(Guid.NewGuid(), 1000);
                bankList.Add(ba);
            }         

            CollectionAssert.AllItemsAreNotNull(bankList);
        }


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