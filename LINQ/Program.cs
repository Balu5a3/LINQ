using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQ
{
    public class Employee {
        public string Id { get; set; }
        public string Name { get; set; }
        public string DeptId { get; set; }
        public string Dept { get; set; }
        public int Amount { get; set; }
    }

    public class Dept
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string DeptId { get; set; }
    }


    public class Salary
    {
        public string Id { get; set; }
        public int Amount { get; set; }
        public string DeptId { get; set; }
    }

    public class newL{
        public string Dept { get; set; }
        public int Amount { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {

            List<Employee> L1 = new List<Employee>() {
                new Employee { Id = "1", Name="Surya",DeptId = "1"},
                new Employee { Id = "2", Name = "Surya" ,DeptId = "1"},
                new Employee { Id = "3", Name = "Vikram",DeptId = "2" },
                new Employee { Id = "4", Name = "Chak",DeptId = "2" },
                new Employee { Id = "5", Name = "Gowrav",DeptId = "3" },
                new Employee { Id = "6", Name = "Sathvik",DeptId = "" }
            };

            List<Dept> D1 = new List<Dept>() {
                new Dept { Id = "1" , Name = "Maths"},
                new Dept { Id = "2" , Name = "English"},
                new Dept { Id = "3" , Name = "Social"},
            };


            var innerjoin = (from a in L1
                             join b in D1 on a.DeptId equals b.Id
                             select new { aa = a, bb = b }).ToList();


            var leftjoin = (from a in L1
                            join b in D1 on a.DeptId equals b.Id into temp
                            from b in temp.DefaultIfEmpty()
                            where b == null
                            select a).ToList();


            var rightjoin = (from a in D1
                             join b in L1 on a.Id equals b.DeptId into temp
                             from c in temp.DefaultIfEmpty()
                             where c == null
                             select new {
                                 aa = a,
                                 bb = c
                             }).ToList();



            var groupjoin = (from a in L1
                             join b in D1 on a.DeptId equals b.Id into j1
                             from b in j1.DefaultIfEmpty()
                             where b == null
                             group a by a.DeptId into g
                             select new
                             {
                                 a = g.Key,
                                 b = g
                             }).ToList();

            
            List<Employee> empList = new List<Employee>() {
                new Employee { Id="1", Name = "Surya", Dept= "Direction", DeptId="1", Amount = 0},
                new Employee { Id="2", Name = "Teja", Dept= "Direction", DeptId="1", Amount = 0},
                new Employee { Id="3", Name = "Ravi", Dept= "Direction", DeptId="1", Amount = 0},
                new Employee { Id="4", Name = "Gourav", Dept= "Production", DeptId="2", Amount = 0}
            };
            
            List<Salary> salList = new List<Salary>() {
                new Salary { Id="1", Amount = 1000, DeptId="1"},
                new Salary { Id="2", Amount = 1000, DeptId="1"},
                new Salary { Id="3", Amount = 5000, DeptId="2"}
            };

            //under trail to get second highest salary name from another table through join
            var lll = (from emp in empList
                       join sal in salList on emp.DeptId equals sal.DeptId into j1
                       from sal in j1.DefaultIfEmpty()
                       group sal by sal.Amount into g
                       select new {
                           Sa = g.Key,
                           Liss = g.ToList()
                       }).OrderByDescending(x => x.Sa).Skip(1).ToList();


            var llle = (from emp in empList
                        join sal in salList on emp.DeptId equals sal.DeptId
                        group sal by emp.Dept into g
                        select new {
                            Name = g.Key,
                            Value = g.Sum(z=>z.Amount)
                        }).ToList();


            var ddk = (from emp in empList
                       group emp by emp.Dept into g
                       orderby g.Key descending
                       select new {
                           Name = g.Key,
                           Amount = g.Sum(x=>x.Amount)
                       }).ToList();
            

            var lle = (from empll in empList
                       group empll by empll.Dept into g
                       select new { 
                           name = g.Key,
                           valuea = g.Sum(x=>x.Amount)
                       }).ToList();


            List<newL> newll = (from emmp in empList
                        join sal in salList on emmp.Id equals sal.Id into j1
                        from sal in j1.DefaultIfEmpty()
                        select new newL
                        {
                            Dept = emmp.Name,
                            Amount = sal == null ? 0 : sal.Amount
                        }).ToList();

            string aa = string.Empty;
            
        }
    }
}
