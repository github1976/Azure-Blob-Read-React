using Microsoft.AspNetCore.Mvc;

namespace EmployeesBackEndApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly ILogger<EmployeeController> _logger;

        private List<Employee> employees;

        public EmployeeController(ILogger<EmployeeController> logger)
        {
            _logger = logger;
            employees = PopulateEmployees();

        }

        [HttpGet(Name = "GetEmployees")]
        public IEnumerable<Employee> Get()
        {
            return employees;
        }

        [HttpDelete("{id}", Name = "DeleteEmployee")]
        public int Delete([FromRoute] int id)
        {
            var employee = employees.FirstOrDefault(x => x.id == id);
            if (employee != null) employees.Remove(employee);
            return id;
        }

        [HttpPost]
        public int AddEmployee([FromBody] Employee employee)
        {
            employees.Add(employee);
            return employee.id;
        }

        List<Employee> PopulateEmployees()
        {
            return new List<Employee>
            {
                new Employee
                {
                    id = 100,
                    name = "Munib Butt",
                    age = 52,
                    counter = 1
                },
                new Employee
                {
                    id = 200,
                    name = "John Doe",
                    age = 25,
                    counter = 2
                },
                new Employee
                {
                    id = 300,
                    name = "Mike Doe",
                    age = 30,
                    counter = 3
                },
                new Employee
                {
                    id = 400,
                    name = "John Smith",
                    age = 35,
                    counter = 4
                }
            };
        }

    }

    public class Employee
    {
        public int id { get; set; }
        public string? name { get; set; }
        public int age { get; set; }
        public int counter { get; set; }
    }

}
