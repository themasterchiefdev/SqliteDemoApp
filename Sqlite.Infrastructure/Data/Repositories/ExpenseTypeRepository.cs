﻿using Microsoft.EntityFrameworkCore;
using Sqlite.Core.Entities;
using Sqlite.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sqlite.Infrastructure.Data.Repositories
{
    public class ExpenseTypeRepository : IExpenseTypeRepository
    {
        private bool _disposed = false;

        protected AppDbContext Context;

        public ExpenseTypeRepository(AppDbContext appDbContext)
        {
            Context = appDbContext;
        }

        /// <summary>
        /// Adds the Expenses
        /// </summary>
        /// <param name="expenseType"></param>
        public void AddExpenseType(ExpenseType expenseType)
        {
            Context.ExpenseTypes.Add(expenseType);
            SaveChangesToDb();
        }

        /// <summary>
        /// Edits Expense Type
        /// </summary>
        /// <param name="expenseType"></param>
        public void EditExpenseType(ExpenseType expenseType)
        {
            var getExpenseType = Context.ExpenseTypes.SingleOrDefault(e => e.Id == expenseType.Id);

            if (getExpenseType == null)
            {
                throw new InvalidOperationException("Expense type with the given Id doesn't exist.");
            }

            Context.Entry((expenseType)).State = EntityState.Modified;
            SaveChangesToDb();
        }

        /// <summary>
        /// Gets all the Expenses from the DB
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ExpenseType> GetAllExpense()
        {
            return Context.ExpenseTypes.ToList();
        }

        /// <summary>
        /// Get ExpenseType By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ExpenseType GetExpenseTypeById(Guid id)
        {
            return Context.ExpenseTypes.SingleOrDefault(e => e.Id == id);
        }

        /// <summary>
        /// Commits all the transactions to the database
        /// </summary>
        public void SaveChangesToDb()
        {
            Context.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    Context.Dispose();
                }
            }

            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
        }
    }
}