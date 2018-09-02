using Sqlite.Core.Entities;
using System;
using System.Collections.Generic;

namespace Sqlite.Core.Interfaces.Repositories
{
    public interface IExpenseTypeRepository : IDisposable
    {
        void AddExpenseType(ExpenseType expenseType);

        void EditExpenseType(ExpenseType expenseType);

        IEnumerable<ExpenseType> GetAllExpense();

        ExpenseType GetExpenseTypeById(Guid id);

        void SaveChangesToDb();
    }
}