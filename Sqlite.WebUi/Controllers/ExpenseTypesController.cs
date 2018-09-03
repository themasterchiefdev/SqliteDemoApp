using Microsoft.AspNetCore.Mvc;
using Sqlite.Core.Entities;
using Sqlite.Core.Interfaces.Services;
using System.Collections.Generic;
using System.Linq;

namespace Sqlite.WebUi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class ExpenseTypesController : ControllerBase
    {
        private readonly IExpenseTypeService _expenseTypeService;

        public ExpenseTypesController(IExpenseTypeService expenseTypeService)
        {
            _expenseTypeService = expenseTypeService;
        }

        // GET api/expensetypes
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ExpenseType))]
        public ActionResult<IEnumerable<ExpenseType>> GetAllExpenses()
        {
            var expenseTypeList = _expenseTypeService.GetAllExpenses().ToList();
            return Ok(expenseTypeList);
        }

        //// GET: api/expensetypes/GetExpenseTypeByName/5
        //[HttpGet("{expenseTypeName}")]
        //[ProducesResponseType(200, Type = typeof(ExpenseType))]
        //[ProducesResponseType(404)]
        //public ActionResult<ExpenseType> GetExpenseTypeById(string expenseTypeName)
        //{
        //    var expenseType = _expenseTypeService.GetExpenseTypeByName(expenseTypeName);

        //    if (expenseType == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(expenseType);
        //}

        //// POST: api/expensetypes/addexpensetype
        //[HttpPost]
        //[ValidateModel]
        //[Route("CreateExpenseType")]
        //public IActionResult AddExpenseType([FromBody] ExpenseType expenseType)
        //{
        //    var type = new ExpenseType()
        //    {
        //        Id = Guid.NewGuid(),
        //        Type = expenseType.Type,
        //        AddedOn = DateTime.Now,
        //        LastModifiedOn = DateTime.Now
        //    };
        //    var expenseTypeAlreadyExists = _expenseTypeService.GetExpenseTypeByName(expenseType.Type);
        //    if (expenseTypeAlreadyExists != null)
        //    {
        //        return BadRequest("Expense Type already exists");
        //    }

        //    //_expenseTypeService.AddExpenseType(type);
        //    //_expenseTypeService.SaveChangesToDb();

        //    return Ok(type);
        //}

        //// DELETE: api/expensetypes/deleteexpensetype/5
        //[HttpDelete]
        //[Route("deleteexpensetype/{expenseTypeName}")]
        //public IActionResult Delete(string expenseTypeName)
        //{
        //    var expenseTypeInDb = _expenseTypeService.GetExpenseTypeByName(expenseTypeName);

        //    if (expenseTypeInDb == null)
        //    {
        //        return NotFound();
        //    }
        //    _expenseTypeService.RemoveExpenseType(expenseTypeInDb.Id);

        //    //_expenseTypeService.SaveChangesToDb();
        //    return NoContent();
        //}
    }
}