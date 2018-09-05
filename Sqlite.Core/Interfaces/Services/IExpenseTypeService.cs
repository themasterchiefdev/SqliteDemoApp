using Sqlite.Core.Entities;
using System;
using System.Collections.Generic;

namespace Sqlite.Core.Interfaces.Services
{
    // ref: https://stackoverflow.com/questions/5049363/difference-between-repository-and-service-layer
    // ref: https://docs.microsoft.com/en-us/aspnet/mvc/overview/older-versions-1/models-data/validating-with-a-service-layer-cs
    public interface IExpenseTypeService
    {
        void CreateExpenseType(ExpenseType expenseType);

        void EditExpenseType(ExpenseType expenseType);

        void RemoveExpenseType(Guid id);

        IEnumerable<ExpenseType> GetAllExpenses();

        ExpenseType GetExpenseTypeByName(string expenseTypeName);

        bool ValidateExpenseType(ExpenseType expenseTypeToValidate);

        bool DoesTheExpenseTypeExists(string expenseType);

        bool Find(Guid id);
    }
}