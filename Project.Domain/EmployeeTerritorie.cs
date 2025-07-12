namespace Project.Domain
{
    public class EmployeeTerritorie
    {
        public int EmployeeID { get; set; }
        public Employee employee { get; set; }
        public int TerritoryID { get; set; }
        public Territorie territorie { get; set; }
    }
}
