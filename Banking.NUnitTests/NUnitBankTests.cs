using Banking.Controllers;
using Banking.Entities;
using Banking.Infrastructure;
using Banking.Models;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Web.Mvc;

namespace Banking.NUnitTests
{
    [TestFixture]
    public class NUnitBankTests
    {
        private IRepository _repository;
        private HomeController _homeController;
        private BankAccountsAdminController _bankController;
        private UserPanelController _userPanelController;
        private PanelAdminController _panelAdminController;


        [SetUp]
        protected void SetUp()
        {
            _repository = Substitute.For<IRepository>();
            _homeController = new HomeController();
            _bankController = new BankAccountsAdminController(_repository);
            _userPanelController = new UserPanelController(_repository);
            _panelAdminController = new PanelAdminController();
        }


        [Test]
        public void BankAccountsAdmin_Details_Returns_BankAccountViewModel()
        {
            var bankAccountToDisplay = new BankAccount(Guid.NewGuid(), 1000);
            _repository.GetBankAccount(Arg.Any<Guid>()).Returns(bankAccountToDisplay);

            var viewResult = _bankController.Details(bankAccountToDisplay.AccountNumber) as ViewResult;
            var model = viewResult.Model as BankAccountViewModel;

            Assert.That(1000, Is.EqualTo(model.Balance));
            Assert.AreEqual(1000, model.Balance);
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
        public void PanelAdmin_Index_Returns_Instance_Of_View_Result()
        {
            var viewResult = _panelAdminController.Index() as ViewResult;

            Assert.That(viewResult, Is.InstanceOf<ViewResult>());
            Assert.That(viewResult, Is.InstanceOf(typeof(ViewResult)));
            Assert.IsInstanceOf(typeof(ViewResult), viewResult);
            Assert.IsInstanceOf<ViewResult>(viewResult);
            Assert.IsAssignableFrom(typeof(ViewResult), viewResult);
            Assert.IsAssignableFrom<ViewResult>(viewResult);
        }


        [Test]
        [Ignore("Pomijam")]
        public void Ignore()
        {
            int a = 2, b = 3, correctResult = 5;

            int result = a + b;

            Assert.AreEqual(result, correctResult);
        }


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

    }
}
