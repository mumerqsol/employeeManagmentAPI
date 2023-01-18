using EmployeeManagement.Model.Account;
using EmployeeManagement.Model.EmployeeHiring;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Model
{
    public class HCMContext : DbContext
    {

        public HCMContext(DbContextOptions<HCMContext> options) : base(options)
        {

        }

        public DbSet<UserMaster> usermst { get; set; }
        public DbSet<Employee> empmaster { get; set; }
        public DbSet<Designation> designation { get; set; }
        public DbSet<EmpDesignation> empdesignation { get; set; }
        public DbSet<MenuMaster> UserMenu { get; set; }
        public DbSet<EmpChart> empv_empchart { get; set; }


        //Create Multiple Employee Saving Model
        public async Task<EmployeeMaster> InserEmployeeMaster(EmployeeMaster employee)
        {
            if (employee != null)
            {

                Employee employee1 = new Employee()
                {

                    empid = employee.empid,
                    empname = employee.empname,
                    dob = employee.dob,
                    jod = employee.jod,
                    nicno = employee.nicno,
                    isactive = employee.isactive,
                    cnfmdate = employee.cnfmdate,
                    imagepath = employee.imagepath
                };

                if (employee.empid == 0)
                {
                    await this.empmaster.AddAsync(employee1);
                    await this.SaveChangesAsync();
                    employee.empid = employee1.empid;
                }
                else
                {

                    this.Entry(employee1).State = EntityState.Modified;
                    await this.SaveChangesAsync();
                }



                EmpDesignation designation = new EmpDesignation()
                {
                    id = employee.empDesignation.id,
                    empid = employee.empid,
                    desigid = employee.empDesignation.desigid,
                    validfrom = employee.empDesignation.validfrom,
                    validto = employee.empDesignation.validto,
                    isactive = employee.empDesignation.isactive
                };

                if (employee.empDesignation.id == 0)
                {
                    await this.empdesignation.AddAsync(designation);
                    await this.SaveChangesAsync();

                    employee.empDesignation.id = designation.id;
                    employee.empDesignation.empid = designation.empid;
                }
                else
                {
                    this.Entry(designation).State = EntityState.Modified;
                    await this.SaveChangesAsync();

                }
            }

            return employee;
        }
        public async Task<EmployeeMaster> GetEmployeeMaster(int empid)
        {
            EmployeeMaster empMaster = null;
            if(empid > 0)
            {
                empMaster = new EmployeeMaster();

                Employee employee = await this.empmaster.FindAsync(empid);
                if (employee != null)
                {

                    empMaster.empid = employee.empid;
                    empMaster.empname = employee.empname;
                    empMaster.dob = employee.dob;
                    empMaster.jod = employee.jod;
                    empMaster.nicno = employee.nicno;
                    empMaster.isactive = employee.isactive;
                    empMaster.cnfmdate = employee.cnfmdate;
                    empMaster.imagepath = employee.imagepath;

                }
                EmpDesignation empDesignation = await this.empdesignation.Where(o => o.empid == empid).Select(t => t).FirstOrDefaultAsync();//.ToListAsync();
                if (empDesignation != null)
                {

                    empMaster.empDesignation = empDesignation;

                }

            }

            return empMaster;
        }
    }
}
