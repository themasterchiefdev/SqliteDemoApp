using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using Sqlite.Core.Entities;
using Sqlite.Core.Interfaces.Repositories;
using Sqlite.Infrastructure.Data;
using Sqlite.Infrastructure.Services.ExpenseTypeService;
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
         *         I assume that I have mocked the Interface but never initialized its implementation
         */

        [Test]
        public void GetExpenses_GetAllExpenses_ShouldReturnAllExpenses()
        {
            // arrange
            // Setup Repository
            var mockExpenseTypeRepo = new Mock<IExpenseTypeRepository>();
            mockExpenseTypeRepo.Setup(e => e.GetAllExpenses()).Returns(DummyExpenses);
            _mockExpenseTypeRepository = mockExpenseTypeRepo.Object;

            // Setup DbContext to use inMemory SQLite database
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlite("DataSource=:memory:")
                .Options;

            // Create the schema in the database
            using (var context = new AppDbContext(options))
            {
                context.Database.EnsureCreated();
            }

            // Act
            // Insert seed data into the database using one instance of the context
            using (var context = new AppDbContext(options))
            {
                context.ExpenseTypes.Add(new ExpenseType() { Type = "Groceries", });
                context.ExpenseTypes.Add(new ExpenseType() { Type = "Internet", });
                context.SaveChanges();
            }

            // Assert
            // Use a clean instance of the context to run the test
            using (new AppDbContext(options))
            {
                var service = new ExpenseTypeService(_mockExpenseTypeRepository);
                var result = service.GetAllExpenses();
                Assert.AreEqual(2, result.Count());
            }
        }

        [Test]
        public void GetExpensesById_GetsExpensesBasedOnId_ShouldReturnExpenseById()
        {
            // arrange
            // Setup Repository
            var mockExpenseTypeRepo = new Mock<IExpenseTypeRepository>();
            mockExpenseTypeRepo.Setup(e => e.GetExpenseTypeByName(It.IsAny<string>()))
                .Returns((string s) => DummyExpenses().Find(x => x.Type == s));
            _mockExpenseTypeRepository = mockExpenseTypeRepo.Object;

            // Setup DbContext to use inMemory SQLite database
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlite("DataSource=:memory:")
                .Options;

            // Create the schema in the database
            using (var context = new AppDbContext(options))
            {
                context.Database.EnsureCreated();
            }

            // Act
            // Insert seed data into the database using one instance of the context
            using (var context = new AppDbContext(options))
            {
                var dummyExpenses = DummyExpenses();
                foreach (var dummyExpenseType in dummyExpenses)
                {
                    context.ExpenseTypes.Add(dummyExpenseType);
                }

                context.SaveChanges();
            }

            // Assert
            // Use a clean instance of the context to run the test
            using (new AppDbContext(options))
            {
                var service = new ExpenseTypeService(_mockExpenseTypeRepository);
                var result = service.GetExpenseTypeByName("Internet");
                Assert.AreEqual("Internet", result.Type);
            }
        }

        [Test]
        public void GetExpensesById_GetsExpensesBasedOnId_ShouldThrowAnError()
        {
            // arrange
            // Setup Repository
            var mockExpenseTypeRepo = new Mock<IExpenseTypeRepository>();
            mockExpenseTypeRepo.Setup(e => e.GetExpenseTypeByName(It.IsAny<string>()))
                .Throws<InvalidOperationException>();
            _mockExpenseTypeRepository = mockExpenseTypeRepo.Object;

            // Setup DbContext to use inMemory SQLite database
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlite("DataSource=:memory:")
                .Options;

            // Create the schema in the database
            using (var context = new AppDbContext(options))
            {
                context.Database.EnsureCreated();
            }

            // Act
            // Insert seed data into the database using one instance of the context
            using (var context = new AppDbContext(options))
            {
                var dummyExpenses = DummyExpenses();
                foreach (var dummyExpenseType in dummyExpenses)
                {
                    context.ExpenseTypes.Add(dummyExpenseType);
                }

                context.SaveChanges();
            }

            // Assert
            // Use a clean instance of the context to run the test
            using (new AppDbContext(options))
            {
                var service = new ExpenseTypeService(_mockExpenseTypeRepository);
                Assert.Throws<InvalidOperationException>(() => service.GetExpenseTypeByName("Intern"));
            }
        }

        private static List<ExpenseType> DummyExpenses()
        {
            return new List<ExpenseType>()
            {
                new ExpenseType()
                {
                    Id=Guid.NewGuid(),
                    Type = "Groceries",
                    AddedOn = DateTime.Now,
                    LastModifiedOn = DateTime.Now
                },new ExpenseType()
                {
                    Id=Guid.NewGuid(),
                    Type = "Internet",
                    AddedOn = DateTime.Now,
                    LastModifiedOn = DateTime.Now
                }
            };
        }
    }
}