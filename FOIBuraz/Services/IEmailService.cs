using FOIBuraz.Models;

namespace FOIBuraz.Services;

public interface IEmailService
{
    void GenerateEmails(Student freshmanStudent, Student olderStudent);
}