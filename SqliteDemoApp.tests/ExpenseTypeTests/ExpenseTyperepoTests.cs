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
         * RajivY: https://docs.microsoft.com/en-us/ef/core/miscellaneous/testing/sqlite
         */

        #region Dummy ExpensesList

        private static List<ExpenseType> DummyExpenses()
        {
            return new List<ExpenseType>()
            {
                new ExpenseType()
                {
                    Id=Guid.Parse("0df4ec64-6c4c-4085-8dac-0ee98943d8a9"),
                    Type = "Groceries",
                    AddedOn = DateTime.Now,
                    LastModifiedOn = DateTime.Now
                },new ExpenseType()
                {
                    Id=Guid.Parse("144a9c6a-135c-4535-8c6e-13adff61b24a"),
                    Type = "Internet",
                    AddedOn = DateTime.Now,
                    LastModifiedOn = DateTime.Now
                }
            };
        }

        #endregion Dummy ExpensesList

        #region GetAllExpensesTypes

        [Test]
        public void GetExpensesTypes_GetsAllTheExpenses_ShouldReturnAllExpenses()
        {
            // arrange
            // Setup-list
            var types = DummyExpenses();

            // Setup Repository
            var mockExpenseTypeRepo = new Mock<IExpenseTypeRepository>();

            mockExpenseTypeRepo.Setup(e => e.AddExpenseType(It.IsAny<ExpenseType>()))
                .Callback((ExpenseType expenseType) => types.Add(expenseType));

            mockExpenseTypeRepo.Setup(e => e.GetAllExpenses()).Returns(types);

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

            // Act and Assert
            // Use a clean instance of the context to run the test
            using (new AppDbContext(options))
            {
                var service = new ExpenseTypeService(_mockExpenseTypeRepository);
                var result = service.GetAllExpenses();
                Assert.AreEqual(2, result.Count());
            }
        }

        #endregion GetAllExpensesTypes

        #region GetExpensesTypeByName

        [Test]
        [TestCase("Internet", "Internet")]
        [TestCase("Groceries", "Groceries")]
        public void GetExpenseTypeById_GetsExpensesBasedOnType_ShouldReturnExpenseTypeByName(string expenseTypeName, string expectedTypeName)
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
                context.Database.EnsureDeleted();
            }

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
                var result = service.GetExpenseTypeByName(expenseTypeName);
                Assert.AreEqual(expectedTypeName, result.Type);
            }
        }

        [Test]
        public void GetExpensesTypeByName_PassInvalidExpenseType_ShouldThrowAnInvalidOperationException()
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
                context.Database.EnsureDeleted();
            }

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

        #endregion GetExpensesTypeByName

        #region Create ExpenseType

        [Test]
        public void CreateExpenseType_AddANewExpenseType_ShouldAddAnExpense()
        {
            // arrange

            // Setup-list
            var types = DummyExpenses();

            // Setup Repository
            var mockExpenseTypeRepo = new Mock<IExpenseTypeRepository>();

            mockExpenseTypeRepo.Setup(e => e.AddExpenseType(It.IsAny<ExpenseType>()))
                .Callback((ExpenseType expenseType) => types.Add(expenseType));

            mockExpenseTypeRepo.Setup(e => e.GetAllExpenses()).Returns(types);

            _mockExpenseTypeRepository = mockExpenseTypeRepo.Object;

            // Setup DbContext to use inMemory SQLite database
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlite("DataSource=:memory:")
                .Options;

            // Create the schema in the database
            using (var context = new AppDbContext(options))
            {
                //  context.Database.EnsureDeleted();

                context.Database.EnsureCreated();
            }

            //// Assert
            //// Use a clean instance of the context to run the test
            using (var context = new AppDbContext(options))
            {
                var service = new ExpenseTypeService(_mockExpenseTypeRepository);
                service.CreateExpenseType(new ExpenseType()
                {
                    Id = Guid.NewGuid(),
                    Type = "Rajivs",
                    AddedOn = DateTime.Now,
                    LastModifiedOn = DateTime.Now
                });
                context.SaveChanges();

                var result = _mockExpenseTypeRepository.GetAllExpenses().Count();
                var newRecord = types.Find(t => t.Type == "Rajivs");

                // Assert
                Assert.Multiple(() =>
                {
                    Assert.AreEqual(3, result);
                    Assert.AreEqual("Rajivs", newRecord.Type);
                });
            }
        }

        [Test]
        [Ignore("No working as expected")]
        public void CreateExpenseType_AddAnExistingExpenseType_ShouldThrowException()
        {
            //Arrange
            // Setup-list
            var types = DummyExpenses();

            // Setup Repository
            var mockExpenseTypeRepo = new Mock<IExpenseTypeRepository>();

            mockExpenseTypeRepo.Setup(e => e.AddExpenseType(It.IsAny<ExpenseType>()))
                .Callback((ExpenseType expenseType) => types.Add(expenseType));

            mockExpenseTypeRepo.Setup(e => e.GetAllExpenses()).Returns(types);

            _mockExpenseTypeRepository = mockExpenseTypeRepo.Object;

            // Setup DbContext to use inMemory SQLite database
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlite("DataSource=:memory:")
                .Options;

            // Create the schema in the database
            using (var context = new AppDbContext(options))
            {
                //context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
            }

            //// Assert
            //// Use a clean instance of the context to run the test
            using (var context = new AppDbContext(options))
            {
                var service = new ExpenseTypeService(_mockExpenseTypeRepository);

                Assert.Throws<Exception>(() => service.CreateExpenseType(new ExpenseType()
                {
                    Id = Guid.NewGuid(),
                    Type = "Groceries",
                    AddedOn = DateTime.Now,
                    LastModifiedOn = DateTime.Now
                }));
            }
        }

        #endregion Create ExpenseType

        #region Edit Expense Types

        [Test]
        public void EditExpenseType_EditAnExistingExpenseType_ShouldUpdateAnExistingExpenseType()
        {
            // Arrange
            var types = DummyExpenses();

            // set-up Repository
            var mockExpenseTypeRepo = new Mock<IExpenseTypeRepository>();

            // get expense type to update
            mockExpenseTypeRepo.Setup(e => e.GetExpenseTypeByName(It.IsAny<string>()))
                .Returns((string s) => types.Find(x => x.Type == s));

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
            using (var context = new AppDbContext(options))
            {
                var service = new ExpenseTypeService(_mockExpenseTypeRepository);
                var et = service.GetExpenseTypeByName("Groceries");
                et.Type = "Power";
                service.EditExpenseType(et);
                Assert.AreEqual("Power", service.GetExpenseTypeByName("Power").Type);
            }
        }

        [Test]
        public void EditExpenseType_EditATypeWhichDoesntExist_ShouldReturnAnException()
        {
            // Arrange
            var types = DummyExpenses();

            // set-up Repository
            var mockExpenseTypeRepo = new Mock<IExpenseTypeRepository>();

            // get expense type to update
            mockExpenseTypeRepo.Setup(e => e.GetExpenseTypeByName(It.IsAny<string>()))
                .Returns((string s) => types.Find(x => x.Type == s));

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
            using (var context = new AppDbContext(options))
            {
                var service = new ExpenseTypeService(_mockExpenseTypeRepository);
                var et = new ExpenseType()
                {
                    Type = "Power"
                };

                Assert.Throws<Exception>(() => service.EditExpenseType(et));
            }
        }

        #endregion Edit Expense Types

        #region Remove Expense Type

        [Test]
        [TestCase("Groceries")]
        [TestCase("Internet")]
        public void RemoveExpenseType_RemovestheSpecifiedExpenType_ShouldRemoveExpenseTypeFromList(string typeName)
        {
            // Arrange
            var types = DummyExpenses();

            // set up repos
            // set-up Repository
            var mockExpenseTypeRepo = new Mock<IExpenseTypeRepository>();

            // get expense type to delete
            mockExpenseTypeRepo.Setup(e => e.GetExpenseTypeByName(It.IsAny<string>()))
                 .Returns((string s) => types.Find(x => x.Type == s));

            // To Remove item based on the given GUID.
            // Setup mock for remove item based on GUID
            // In the callback, 1st Find the item based on the Id and then remove it from the list
            mockExpenseTypeRepo.Setup(r => r.RemoveExpenseType(It.IsAny<Guid>()))
                .Callback((Guid id) => types.Remove(types.Find(x => x.Id == id)));

            // get all expenses
            mockExpenseTypeRepo.Setup(x => x.GetAllExpenses()).Returns(types);

            _mockExpenseTypeRepository = mockExpenseTypeRepo.Object;

            // set up db context
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
            using (var context = new AppDbContext(options))
            {
                var service = new ExpenseTypeService(_mockExpenseTypeRepository);

                var item = service.GetExpenseTypeByName(typeName);

                service.RemoveExpenseType(item.Id);

                Assert.AreEqual(1, types.Count);
            }
        }

        [Test]
        [Ignore("Bug, need to fix this later")]
        public void RemoveExpenseType_ProvideInvalidExpenseType_ShouldThrowAnException()
        {
            // Arrange
            var types = DummyExpenses();

            // set up repos
            // set-up Repository
            var mockExpenseTypeRepo = new Mock<IExpenseTypeRepository>();

            // get expense type to delete
            //mockExpenseTypeRepo.Setup(e => e.GetExpenseTypeByName(It.IsAny<string>()))
            //    .Returns((string s) => types.Find(x => x.Type == s));

            mockExpenseTypeRepo.Setup(x => x.Find(It.IsAny<Guid>()))
                .Returns((Guid id) => types.Find(x => x.Id == id));

            // To Remove item based on the given GUID.
            // Setup mock for remove item based on GUID
            // In the callback, 1st Find the item based on the Id and then remove it from the list
            mockExpenseTypeRepo.Setup(r => r.RemoveExpenseType(It.IsAny<Guid>()))
                .Callback((Guid id) => types.Remove(types.Find(x => x.Id == id)));

            // get all expenses
            mockExpenseTypeRepo.Setup(x => x.GetAllExpenses()).Returns(types);

            _mockExpenseTypeRepository = mockExpenseTypeRepo.Object;

            // set up db context
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
            using (var context = new AppDbContext(options))
            {
                var service = new ExpenseTypeService(_mockExpenseTypeRepository);

                var item = Guid.Parse("0df4ec64-6c4c-4085-8dac-0ee98943d8a7");

                //service.RemoveExpenseType(item.Id);

                Assert.Throws<InvalidOperationException>(() => service.RemoveExpenseType(item));
            }
        }

        #endregion Remove Expense Type
    }
}