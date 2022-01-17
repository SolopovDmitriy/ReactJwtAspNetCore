using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication13.Entities;

namespace WebApplication13.Models
{
    public class EmployeeRepository
    {
        private readonly ClinicDBContext _dBContext;
        public EmployeeRepository(ClinicDBContext clinicDBContext)
        {
            _dBContext = clinicDBContext;
        }
        public Employee GetEmployeeById(int id)
        {
            return _dBContext.Employees.SingleOrDefault(emp => emp.Id == id);
        }
        public IQueryable<Employee> GetEmployees()
        {
            return _dBContext.Employees.OrderBy(x => x.Fio);
        }
    }
}
