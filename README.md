# Employee Management System

A C# console program that reads employee data from a file, processes it using delegates to filter employees by salary and experience, applies a bonus to eligible employees, and generates a summarized report — both to the console and to an output file.

## Features

- **Reads employee records** from `employees.txt` (CSV format)
- **Filters employees using delegates:**
  - `salaryFilter` — employees earning more than $50,000
  - `experienceFilter` — employees with more than 5 years of experience
- **Applies a 10% bonus** (via an `Action<Employee>` delegate) to employees with more than 5 years of experience, while preserving their original salary for reporting
- **Formats employee details** for display via a `textFormatter` delegate
- **Prints a step-by-step processing log** to the console (file loading, filtering phases, bonus application, salary analysis)
- **Writes a summary report** to an output file, including:
  - High-salary, experienced employees
  - IT department employees
  - Total employee count, total salary, and average salary

## Input File Format

`employees.txt` is a comma-separated file with **no header row**, one employee per line:

```
Name,Salary,Department,YearsOfExperience
```

Example:

```
Jane Doe,55000,IT,7
John Smith,42000,Sales,3
Alice Chen,68000,IT,9
```

## Processing Logic

| Step | Rule |
|---|---|
| High salary filter | `Salary > 50000` |
| Experience filter | `YearsOfExperience > 5` |
| Bonus | Employees with `YearsOfExperience > 5` receive a **10% salary increase** |

## Output

- **Console:** a formatted, phase-by-phase report (file loading status, filtered employee lists, bonus updates, and a final salary analysis showing highest and average salary).
- **File:** a summary report written to `processes_employees.txt` It includes:
  - High-salary **and** experienced employees
  - IT department employees
  - Total employees processed, total salary, and average salary

## Error Handling

- If `employees.txt` is missing, the program catches `FileNotFoundException` and prints a friendly message instead of crashing.
- Other exceptions are caught and their message is printed to the console.
