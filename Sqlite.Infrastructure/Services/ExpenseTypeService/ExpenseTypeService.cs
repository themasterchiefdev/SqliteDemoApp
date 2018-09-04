using Sqlite.Core.Entities;
using Sqlite.Core.Interfaces.Repositories;
using Sqlite.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sqlite.Infrastructure.Services.ExpenseTypeService
{
    /// <summary>
    /// This class acts as a glue between ExpenseTypeRepository and the webui.
    /// </summary>
    public class ExpenseTypeService : IExpenseTypeService
    {
        private readonly IExpenseTypeRepository _repository;

        public ExpenseTypeService(IExpenseTypeRepository repository)
        {
            _repository = repository;
        }

        public void CreateExpenseType(ExpenseType expenseType)
        {
            throw new NotImplementedException();
        }

        public void EditExpenseType(ExpenseType expenseType)
        {
            throw new NotImplementedException();
        }

        public void RemoveExpenseType(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ExpenseType> GetAllExpenses()
        {
            return _repository.GetAllExpenses().ToList();
        }

        public ExpenseType GetExpenseTypeByName(string expenseTypeName)
        {
            var expenseType = _repository.GetExpenseTypeByName(expenseTypeName);
            if (expenseType == null)
            {
                throw new InvalidOperationException();
            }
            return expenseType;
        }

        public bool ValidateExpenseType(ExpenseType expenseTypeToValidate)
        {
            throw new NotImplementedException();
        }
    }
}