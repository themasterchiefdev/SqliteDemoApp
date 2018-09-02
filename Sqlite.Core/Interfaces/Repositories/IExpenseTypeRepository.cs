using Sqlite.Core.Entities;
using System;
using System.Collections.Generic;

namespace Sqlite.Core.Interfaces.Repositories
{
    public interface IExpenseTypeRepository : IDisposable
    {
        void AddExpenseType(ExpenseType expenseType);

        void EditExpenseType(ExpenseType expenseType);

        void RemoveExpenseType(Guid id);

        IEnumerable<ExpenseType> GetAllExpenses();

        ExpenseType GetExpenseTypeByName(string expenseTypeName);

        void SaveChangesToDb();
    }
}