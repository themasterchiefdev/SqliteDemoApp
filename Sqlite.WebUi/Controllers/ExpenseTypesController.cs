using Microsoft.AspNetCore.Mvc;
using Sqlite.Core.Entities;
using Sqlite.Core.Interfaces.Repositories;
using Sqlite.WebUi.Validation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sqlite.WebUi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class ExpenseTypesController : ControllerBase
    {
        private readonly IExpenseTypeRepository _expenseTypeRepository;

        public ExpenseTypesController(IExpenseTypeRepository expenseTypeRepository)
        {
            _expenseTypeRepository = expenseTypeRepository;
        }

        // GET api/expensetypes
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ExpenseType))]
        public ActionResult<IEnumerable<ExpenseType>> GetAllExpenses()
        {
            var expenseTypeList = _expenseTypeRepository.GetAllExpenses().ToList();
            return Ok(expenseTypeList);
        }

        // GET: api/expensetypes/GetExpenseTypeById/5
        [HttpGet("{expenseTypeName}")]
        [ProducesResponseType(200, Type = typeof(ExpenseType))]
        [ProducesResponseType(404)]
        public ActionResult<ExpenseType> GetExpenseTypeById(string expenseTypeName)
        {
            var expenseType = _expenseTypeRepository.GetExpenseTypeById(expenseTypeName);

            if (expenseType == null)
            {
                return NotFound();
            }

            return Ok(expenseType);
        }

        // POST: api/expensetypes/addexpensetype
        [HttpPost]
        [ValidateModel]
        [Route("AddExpenseType")]
        public IActionResult AddExpenseType([FromBody] ExpenseType expenseType)
        {
            var type = new ExpenseType()
            {
                Id = Guid.NewGuid(),
                Type = expenseType.Type,
                AddedOn = DateTime.Now,
                LastModifiedOn = DateTime.Now
            };
            var expenseTypeAlreadyExists = _expenseTypeRepository.GetExpenseTypeById(expenseType.Type);
            if (expenseTypeAlreadyExists != null)
            {
                return BadRequest("Expense Type already exists");
            }

            _expenseTypeRepository.AddExpenseType(type);
            _expenseTypeRepository.SaveChangesToDb();

            return Ok(type);
        }

        // DELETE: api/expensetypes/deleteexpensetype/5
        [HttpDelete]
        [Route("deleteexpensetype/{expenseTypeName}")]
        public IActionResult Delete(string expenseTypeName)
        {
            var expenseTypeInDb = _expenseTypeRepository.GetExpenseTypeById(expenseTypeName);

            if (expenseTypeInDb == null)
            {
                return NotFound();
            }
            _expenseTypeRepository.RemoveExpenseType(expenseTypeInDb.Id);
            _expenseTypeRepository.SaveChangesToDb();
            return NoContent();
        }
    }
}