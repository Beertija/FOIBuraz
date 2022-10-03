using FOIBuraz.Models;
using FOIBuraz.Services;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace FOIBuraz;

internal class MainLogic
{
    internal void FOIBurazApp(IConfiguration config)
    {
        Log.Information("Application has started succesfully!");

        List<Student> students = LoadStudents(config);
        if (!students.Any()) return;

        List<PairedStudents> pairedStudents = PairStudents(students);

        ConnectStudents(config, pairedStudents);

        Log.Information("Application has finished!");
    }

    private List<PairedStudents> PairStudents(List<Student> students)
    {
        List<PairedStudents> result = new List<PairedStudents>();
        List<Student> freshmanStudents = students.Where(x => x.Freshman == true).ToList();
        List<Student> olderStudents = students.Where(x => x.Freshman == false).ToList();

        int secondCounter = 0;
        for (int i = 0; i < freshmanStudents.Count; i++)
        {
            if (secondCounter < olderStudents.Count)
            {
                result.Add(new PairedStudents { FreshStudent = freshmanStudents[i], OlderStudent = olderStudents[secondCounter] });
                secondCounter++;
            }
            else
            {
                secondCounter = 0;
                result.Add(new PairedStudents { FreshStudent = freshmanStudents[i], OlderStudent = olderStudents[secondCounter] });
                secondCounter++;
            }
        }
        return result;
    }

    private List<Student> LoadStudents(IConfiguration config)
    {
        ExcelService excelService;
        List<Student> students = new List<Student>();
        try
        {
            excelService = new ExcelService(config);
            students = excelService.LoadDataFromExcel();
            excelService.Dispose();
        }
        catch (Exception e)
        {
            Log.Debug("Loading of students from an Excel file wasn't successful!");
            Log.Error(e.Message);
        }
        return students;
    }

    private void ConnectStudents(IConfiguration config, List<PairedStudents> pairedStudents)
    {
        EmailService EmailService;
        try
        {
            EmailService = new EmailService(config);
        }
        catch (Exception e)
        {
            Log.Error(e.Message);
            return;
        }

        foreach (var item in pairedStudents)
        {
            try
            {
                EmailService.GenerateEmails(item.FreshStudent, item.OlderStudent);
                Log.Information($"Generated emails for {item.FreshStudent.Name + " " + item.FreshStudent.LastName} - {item.OlderStudent.Name + " " + item.OlderStudent.LastName}");
            }
            catch (Exception e)
            {
                Log.Debug($"Emails for {item.FreshStudent.Name + " " + item.FreshStudent.LastName} - {item.OlderStudent.Name + " " + item.OlderStudent.LastName} haven't been sent!");
                Log.Error(e.Message);
            }
        }
        EmailService.Dispose();
    }
}