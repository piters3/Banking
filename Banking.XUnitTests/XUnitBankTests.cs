using Banking.Controllers;
using Banking.Entities;
using Banking.Infrastructure;
using Banking.Models;
using NSubstitute;
using System;
using System.Web.Mvc;
using Xunit;

namespace Banking.XUnitFacts
{
    public class XUnitBankFacts
    {
        private IRepository _repository;
        private HomeController _homeController;
        private BankAccountsAdminController _bankController;
        private UserPanelController _userPanelController;
        private PanelAdminController _panelAdminController;


        public XUnitBankFacts()
        {
            _repository = Substitute.For<IRepository>();
            _homeController = new HomeController();
            _bankController = new BankAccountsAdminController(_repository);
            _userPanelController = new UserPanelController(_repository);
            _panelAdminController = new PanelAdminController();
        }


        [Fact]
        public void BankAccountsAdmin_Details_Returns_BankAccountViewModel()
        {
            var bankAccountToDisplay = new BankAccount(Guid.NewGuid(), 1000);
            _repository.GetBankAccount(Arg.Any<Guid>()).Returns(bankAccountToDisplay);

            var viewResult = _bankController.Details(bankAccountToDisplay.AccountNumber) as ViewResult;
            var model = viewResult.Model as BankAccountViewModel;

            Assert.Equal(1000, model.Balance);
            Assert.True(model.Balance.Equals(1000));
        }


        [Fact]
        public void Home_Index_Is_Not_Null()
        {
            var viewResult = _homeController.Index() as ViewResult;

            Assert.NotNull(viewResult);
        }


        [Fact]
        public void UserPanel_NewPayment_Throw_Exception_If_Not_Enough_Money()
        {
            var senderBankAccount = new BankAccount(Guid.NewGuid(), 1000);
            var recipientBankAccount = new BankAccount(Guid.NewGuid(), 1000);
            var paymentModel = new NewPaymentViewModel() { Amount = 2000 };
            _repository.GetBankAccount(Arg.Any<Guid>()).Returns(senderBankAccount);
            _repository.GetUserBankAccount(Arg.Any<string>()).Returns(recipientBankAccount);

            Assert.Throws<ArgumentOutOfRangeException>(() => _userPanelController.NewPayment(paymentModel));
            var exception = Record.Exception(() => _userPanelController.NewPayment(paymentModel));
            Assert.IsType<ArgumentOutOfRangeException>(exception);
            Assert.IsAssignableFrom<ArgumentOutOfRangeException>(exception);      
        }


        [Theory]
        [InlineData(1000)]
        [InlineData(2000)]
        [InlineData(3000)]
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

            Assert.Equal(recipientBankAccount.Balance, recipientInitialBalance + amount);
            Assert.Equal(senderBankAccount.Balance, senderInitialBalance - amount);
        }


        [Fact]
        public void PanelAdmin_Index_Returns_Instance_Of_View_Result()
        {
            var viewResult = _panelAdminController.Index() as ViewResult;

            Assert.IsType<ViewResult>(viewResult);
            Assert.IsAssignableFrom<ViewResult>(viewResult);
        }


        [Fact(Skip = "Pomijam")]
        public void Ignore()
        {
            int a = 2, b = 3, correctResult = 5;

            int result = a + b;

            Assert.Equal(result, correctResult);
        }


        [Fact]
        public void BankAccountsAdmin_Create_Returns_TempData_Message()
        {
            BankAccountViewModel model = new BankAccountViewModel()
            {
                AccountNumber = Guid.NewGuid(),
                OpeningDate = new DateTime(2018, 01, 01)
            };

            var viewResult = _bankController.Create(model) as ViewResult;

            Assert.Contains("Konto bankowe", _bankController.TempData["message"].ToString());
        }
    }
}
