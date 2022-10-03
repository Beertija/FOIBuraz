using FOIBuraz.Models;

namespace FOIBuraz.Services;

public interface IExcelService
{
    List<Student> LoadDataFromExcel();
}