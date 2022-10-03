using FOIBuraz.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Office.Interop.Excel;

namespace FOIBuraz.Services;

public sealed class ExcelService : IExcelService, IDisposable
{
    public ExcelService(IConfiguration config)
    {
        string excelFilePath;
        try
        {
            excelFilePath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName, config["ExcelFileName"]);
            Application _excel = new Application();
            _workbook = _excel.Workbooks.Open(excelFilePath);
        }
        catch (Exception)
        {
            throw;
        }

        if (_workbook is null) throw new FileNotFoundException($"There is no document with the path of: {excelFilePath}");

        _worksheet = _workbook.Worksheets[1];

        if (_worksheet is null) throw new FileNotFoundException("There is no sheet");
    }

    private Workbook _workbook;
    private Worksheet _worksheet;

    public List<Student> LoadDataFromExcel()
    {
        List<Student> data = new List<Student>();
        int row = 2;
        int column = 2;

        while (string.IsNullOrWhiteSpace(_worksheet.Cells[row, column].Value?.ToString()) == false)
        {
            data.Add(new Student
            {
                Freshman = _worksheet.Cells[row, column].Value?.ToString().Equals("Velikog buraza") ? false : true,
                Name = _worksheet.Cells[row, column + 1].Value,
                LastName = _worksheet.Cells[row, column + 2].Value,
                Email = _worksheet.Cells[row, column + 3].Value,
                MobileNumber = _worksheet.Cells[row, column + 4].Value?.ToString()
            });
            row++;
        }
        return data;
    }

    public void Dispose()
    {
        _worksheet = null;
        _workbook.Close();
    }
}