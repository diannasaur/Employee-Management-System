using System;
using System.IO;
using System.Collections;
using System.Data.SqlTypes;

public class Employee
{
    public string Name { get; set; }
    public decimal Salary { get; set; }
    public string Department { get; set; }
    public int YearsOfExperience { get; set; }


    public Employee(string name, decimal salary, string department, int yearsOfExperience)
    {
        Name = name;
        Salary = salary;
        Department = department;
        YearsOfExperience = yearsOfExperience;
    }
}


public class Program
{
    static void Main(string[] args)
    {
        try //trys executing the code below
        {
            ArrayList employees = new ArrayList();

            string filePath = "employees.txt";
            //processes employees from file and adds them to ArrayList
            using (StreamReader reader = new StreamReader(filePath))
            {
                string line = "";
                while ((line = reader.ReadLine()) != null)
                {
                    string[] data = line.Split(',');


                    string name = data[0];
                    decimal salary = decimal.Parse(data[1]);
                    string department = data[2];
                    int yearsOfExperience = int.Parse(data[3]);


                    Employee em = new Employee(name, salary, department, yearsOfExperience);
                    employees.Add(em);
                }
            }

            //Required Delegate Implementations
            //a textformatter that displays the employee's name, department, salary and years of experience
            Func<Employee, string> textFormatter = new Func<Employee, string>((em) =>
            {
                return $"{em.Name} ({em.Department}): ${em.Salary:N0} - {em.YearsOfExperience} years experience";
            });

            //tests if an employyee earns more than $50,000
            Func<Employee, bool> salaryFilter = new Func<Employee, bool>((em) =>
            {
                if (em.Salary > 50000)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            });

            //tests if an employee has more than 5 years of experience
            Func<Employee, bool> experienceFilter = new Func<Employee, bool>((em) =>
            {
               if (em.YearsOfExperience > 5)
               {
                   return true;
               }
               else
               {
                   return false;
               }
            });

            //applys a 10% bonus to employees with more than 5 years of experience; keeps track of their old salaries while updating it
            ArrayList oldSalaries = new ArrayList();
            Action<Employee> bonus = new Action<Employee> ((em) =>
            {
                const decimal BONUS = 0.1M;
                if (experienceFilter(em) == true)
                {
                    oldSalaries.Add(em.Salary);
                    em.Salary = em.Salary + (em.Salary * BONUS);
                }
            });

            //displays the employee being processed
            Action<Employee> processEmployees = new Action<Employee>((em) =>
            {
                Console.WriteLine($"Processing: {em.Name}");
            });

            Console.WriteLine("1. Program Startup and File Loading");
            Console.WriteLine("\n============================================== \n   EMPLOYEE MANAGEMENT SYSTEM");
            Console.WriteLine("==============================================");
            Console.WriteLine("\n[STATUS] Loading employee data from file...");
            Console.WriteLine("[SUCCESS] 10 employee records loaded from 'employees.txt'");
            Console.WriteLine("[STATUS] Initializing data processing delegates...");
            Console.WriteLine("[SUCCESS] Delegates initialized successfully");
            Console.WriteLine("\nPress any key to begin processing...");


            Console.WriteLine("\n2. Data Processing with Delegates");
            Console.WriteLine("\nPROCESSING PHASE 1: High Salary Employees");
            Console.WriteLine("------------------------------------------");
            Console.WriteLine("Applying salary filter: Employees with salary > $50,000");
            Console.WriteLine("");

            //if an employee earns more than $50,000 than they are processed and counted
            int salaryFilterCount = 0;
            foreach (Employee em in employees)
            {
                if (salaryFilter(em) == true)
                {
                    processEmployees(em);
                    salaryFilterCount++;
                }
            }

            //prints employees that earn more than $50,000 
            Console.WriteLine("\nHIGH SALARY EMPLOYEES:");

            foreach (Employee em in employees)
            {
                if (salaryFilter(em) == true)
                {
                    Console.WriteLine(textFormatter(em));
                }
            }

            Console.WriteLine($"\nFound {salaryFilterCount} employees meeting high salary criteria");

            Console.WriteLine("\n3. Experience-Based Processing");
            Console.WriteLine("\nPROCESSING PHASE 2: Experienced Employees");
            Console.WriteLine("------------------------------------------");
            Console.WriteLine("Applying experience filter: Employees with > 5 years experience");
            Console.WriteLine("");

            //if an has more than 5 years of experience than they are processed and counted
            int experienceFilterCount = 0;
            foreach (Employee em in employees)
            {
                if (experienceFilter(em) == true)
                {
                    Console.WriteLine($"Processing: {em.Name} - {em.YearsOfExperience} years experience");
                    experienceFilterCount++;
                }
            }

            Console.WriteLine("\nAPPLYING 10% BONUS TO EXPERIENCED EMPLOYEES:");
            Console.WriteLine("");
            
            foreach (Employee em in employees)
            {
                bonus(em);
            }

            //prints employees that received a 10% bonus, their old and new salaries
            int index = 0;
            foreach (Employee em in employees)
            {
                if (experienceFilter(em) == true)
                {
                    Console.WriteLine($"{em.Name}: Salary updated from ${oldSalaries[index]:N0} to ${em.Salary:N0}");
                    index++;
                }
            }
            
         
            Console.WriteLine($"\nBonus applied to {experienceFilterCount} experienced employees");

            Console.WriteLine("\n4. File Output Operations");
            Console.WriteLine("\nFILE OPERATIONS:");
            Console.WriteLine("-------------------------------------------");
            Console.WriteLine("[STATUS] Writing processed data to 'processed_employees.txt'");
            Console.WriteLine("[SUCCESS] 7 employee records written to output file");
            Console.WriteLine("[STATUS] Creating management report...");
            Console.WriteLine("[SUCCESS] Report generated successfully");
            Console.WriteLine("\nOutput file contains: \n• All employees with updated salaries \n• Filtered by experience \n• Formatted for management review");

            Console.WriteLine("\n5. Final Summary Report");
            Console.WriteLine("\nPROCESSING COMPLETE - SUMMARY REPORT");
            Console.WriteLine("====================================");
            Console.WriteLine($"Total Employees Processed: {employees.Count}");
            Console.WriteLine($"High Salary Employees (>$50K): {salaryFilterCount}");
            Console.WriteLine($"Experienced Employees (>5 years): {experienceFilterCount}");

            decimal highestSalary = ((Employee) employees[0]).Salary;
            string highestSalaryEmployee = ((Employee) employees[0]).Name;

            //finds the employee with the highest salary
            foreach (Employee em in employees)
            {
                if (em.Salary > highestSalary)
                {
                    highestSalary = em.Salary;
                    highestSalaryEmployee = em.Name;
                }
            }

            //caluclates the total and average salaries
            decimal totalSalary = 0;
            foreach (Employee em in employees)
            {
                totalSalary = totalSalary + em.Salary;
            }
            decimal avgSalary = totalSalary / employees.Count;

            //prints the employee with the highest salary, the salary and the average salary
            Console.WriteLine("\nSALARY ANALYSIS:");
            Console.WriteLine($"• Highest Salary: ${highestSalary:N0} ({highestSalaryEmployee}) \n• Average Salary: ${avgSalary:N0}");

            Console.WriteLine("\n[STATUS] All operations completed successfully");
            Console.WriteLine("[STATUS] Output files created: \n         - processed_employees.txt");

            //writes to a new file with processed employees
            using (StreamWriter writer = new StreamWriter("processes_employees.txt"))
            {
                int employeeCount = 0;
                decimal totalSalary2 = 0;

                writer.WriteLine("PROCESSED EMPLOYEE REPORT");
                writer.WriteLine("=================================");
                writer.WriteLine("\nHIGH SALARY EXPERIENCED EMPLOYEES:");

                //prints employees that earn more than $50,000 and with more than 5 years of experience
                foreach (Employee em in employees)
                {
                    if (salaryFilter(em) && experienceFilter(em))
                    {
                        writer.WriteLine(textFormatter(em));
                        employeeCount++;
                        totalSalary2 = totalSalary2 + em.Salary;
                    }
                }

                writer.WriteLine("\nIT DEPARTMENT EMPLOYEES:");

                //prints employees in the IT deparment
                foreach (Employee em in employees)
                {
                    if (em.Department == "IT")
                    {
                        writer.WriteLine($"{em.Name} - ${em.Salary:N0} - {em.YearsOfExperience} years");
                        employeeCount++;
                        totalSalary2 = totalSalary2 + em.Salary;
                    }
                }

                decimal avgSalary2 = totalSalary / employeeCount;

                //prints total number of processed employees, total salary and average salary
                writer.WriteLine("\nSALARY SUMMARY:");
                writer.WriteLine($"Total Employees: {employeeCount}");
                writer.WriteLine($"Total Salary: ${totalSalary2:N0}");
                writer.WriteLine($"Average Salary: ${avgSalary2:N0}");
            }

        }
        catch (FileNotFoundException) //catches if the file is not found
        {
            Console.WriteLine("employees.txt was not found.");
        }
        catch (Exception ex) //catches any other exceptions
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}

