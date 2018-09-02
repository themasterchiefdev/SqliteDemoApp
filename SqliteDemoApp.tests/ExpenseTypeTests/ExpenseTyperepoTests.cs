using Moq;
using NUnit.Framework;
using Sqlite.Core.Entities;
using Sqlite.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SqliteDemoApp.tests.ExpenseTypeTests
{
    [TestFixture]
    public class ExpenseTypeRepoTests
    {
        private IExpenseTypeRepository _mockExpenseTypeRepository;

        /*
         * RajivY: I have setup the mock for the repository but it never hits its implementation class i.e. ExpenseTypeRepository
         *         I assume that I have mocked the Interface but never initialised its implementation
         */

        [Test]
        public void GetExpenses_GetsAllExpenses_ShouldReturnAListOfExpenses()
        {
            // Arrange
            var mockExpenseTypeRepo = new Mock<IExpenseTypeRepository>();

            // setup mock to return all the expenses
            mockExpenseTypeRepo.Setup(e => e.GetAllExpenses()).Returns(DummyExpenses);
            _mockExpenseTypeRepository = mockExpenseTypeRepo.Object;

            // Act
            var expenses = _mockExpenseTypeRepository.GetAllExpenses();
            var expectedExpensesCount = DummyExpenses().Count;

            //Assert
            Assert.AreEqual(expectedExpensesCount, expenses.Count());
        }

        private static List<ExpenseType> DummyExpenses()
        {
            return new List<ExpenseType>()
            {
                new ExpenseType()
                {
                    Id=Guid.NewGuid(),
                    Type = "Sample Type-1",
                    AddedOn = DateTime.Now,
                    LastModifiedOn = DateTime.Now
                },new ExpenseType()
                {
                    Id=Guid.NewGuid(),
                    Type = "Sample Type-2",
                    AddedOn = DateTime.Now,
                    LastModifiedOn = DateTime.Now
                }
            };
        }
    }
}