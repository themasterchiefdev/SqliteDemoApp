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
        private bool _expenseTypeExists = false;

        public ExpenseTypeService(IExpenseTypeRepository repository)
        {
            _repository = repository;
        }

        public void CreateExpenseType(ExpenseType expenseType)
        {
            if (!ValidateExpenseType(expenseType))
            {
                _repository.AddExpenseType(expenseType);
            }
            else
            {
                throw new Exception();
            }
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
            var isValidExpenseType = _repository.GetExpenseTypeByName(expenseTypeToValidate.Type);
            if (isValidExpenseType != null)
            {
                return _expenseTypeExists = true;
            }
            else
            {
                return _expenseTypeExists = false;
            }
        }

        //TODO:Remove this method
        public bool DoesTheExpenseTypeExists(string expenseType)
        {
            var isValidExpenseType = _repository.GetExpenseTypeByName(expenseType);

            _expenseTypeExists = isValidExpenseType != null;

            return _expenseTypeExists;
        }
    }
}