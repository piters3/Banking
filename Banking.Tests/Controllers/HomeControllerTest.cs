using Banking.Controllers;
using Banking.Entities;
using Banking.Infrastructure;
using Banking.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System;
using System.Web.Mvc;

namespace Banking.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        /*   [TestMethod]
           public void BankAccountsAdmin_Details_Returns_BankAccountViewModel()
           {
               // Arrange
               var repo = Substitute.For<IRepository>();
               var bankAccount = new BankAccount
               {
                   AccountNumber = Guid.NewGuid(),
                   Balance = 1000
               };
               repo.GetBankAccount(Arg.Any<Guid>()).Returns(bankAccount);
               BankAccountsAdminController controller = new BankAccountsAdminController(repo);

               // Act
               var result = controller.Details(bankAccount.AccountNumber) as ViewResult;
               var model = result.ViewData.Model as BankAccountViewModel;

               // Assert
               Assert.AreEqual(1000, model.Balance);
           }


           //[TestMethod]
           //public void BankAccountsAdmin_Edit_Add_Model_Error()
           //{

           //}

           [TestMethod]
           public void Home_Index_Is_Not_Null()
           {
               // Arrange
               HomeController controller = new HomeController();

               // Act
               var result = controller.Index() as ViewResult;

               // Assert
               Assert.IsNotNull(result);
           }


           [TestMethod]
           [ExpectedException(typeof(ArgumentOutOfRangeException))]
           public void NewPayment_Exception()
           {
               // Arrange
               var repo = Substitute.For<IRepository>();
               var bankAccount = new BankAccount
               {
                   AccountNumber = Guid.NewGuid(),
                   AvailableFunds = 1000
               };
               var model = new NewPaymentViewModel()
               {
                   Amount = 2000
               };

               repo.GetBankAccount(Arg.Any<Guid>()).Returns(bankAccount);
               repo.GetUserBankAccount(Arg.Any<string>()).Returns(bankAccount);
               UserPanelController controller = new UserPanelController(repo);

               // Act
               var result = controller.NewPayment(model) as ViewResult;
           }


           [DataTestMethod]
           [DataRow(1000)]
           [DataRow(2000)]
           [DataRow(3000)]
           public void NewPayment_Multi(int amount)
           {
               // Arrange
               var repo = Substitute.For<IRepository>();
               var from = new BankAccount(Guid.NewGuid(), 10000);
               var to = new BankAccount(Guid.NewGuid(), 0);
               var model = new NewPaymentViewModel() { Amount = amount };

               repo.GetUserBankAccount(Arg.Any<string>()).Returns(from);
               repo.GetBankAccount(Arg.Any<Guid>()).Returns(to);

               UserPanelController controller = new UserPanelController(repo);

               // Act
               var result = controller.NewPayment(model) as ViewResult;

               // Assert
               Assert.AreEqual(to.Balance, amount);
           }

           [TestMethod]
           public void PanelAdmin_InstanceOf()
           {
               // Arrange
               PanelAdminController controller = new PanelAdminController();

               // Act
               var result = controller.Index() as ViewResult;

               // Assert
               Assert.IsInstanceOfType(result, typeof(ViewResult));
           }

           [TestMethod]
           [Ignore]
           public void Ignore()
           {
               // Arrange
               PanelAdminController controller = new PanelAdminController();

               // Act
               var result = controller.Index() as ViewResult;

               // Assert
               Assert.IsInstanceOfType(result, typeof(ViewResult));
           }

           [TestMethod]
           public void Contains()
           {
               // Arrange
               var repo = Substitute.For<IRepository>();
               BankAccountsAdminController controller = new BankAccountsAdminController(repo);
               BankAccountViewModel model = new BankAccountViewModel()
               {
                   AccountNumber = Guid.NewGuid(),
                   OpeningDate = new DateTime(2018,01,01)
               };

               // Act
               var result = controller.Create(model) as ViewResult;

               // Assert
               StringAssert.Contains(controller.TempData["message"].ToString(), "Konto bankowe");
           }

       }*/
    }
}
