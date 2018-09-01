namespace Sqlite.Core.Entities
{
    public class Expense
    {
        public int Id { get; set; }

        public string VendorName { get; set; }

        public ExpenseType ExpenseType { get; set; }
    }
}