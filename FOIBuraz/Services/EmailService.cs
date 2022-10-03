using FOIBuraz.Models;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;

namespace FOIBuraz.Services;

public sealed class EmailService : IEmailService, IDisposable
{
    public EmailService(IConfiguration config)
    {
        try
        {
            MainEmail mainEmail = config.GetRequiredSection("MainEmail").Get<MainEmail>();
            _smtpClient = config.GetRequiredSection("Smtp").Get<SmtpClient>();
            _smtpClient.Credentials = new NetworkCredential(mainEmail.Email, mainEmail.Key);
            _senderMailAdress = mainEmail.Email;
        }
        catch (InvalidOperationException)
        {
            throw;
        }

        if (_smtpClient is null) throw new FileNotFoundException("Configuration of EmailService wasn't successful!");
    }

    private string _senderMailAdress;

    private SmtpClient _smtpClient;

    /// <summary>
    /// Generating emails and then sending them as a HTML template.
    /// </summary>
    /// <param name="freshmanStudent">Mali buraz</param>
    /// <param name="olderStudent">Veliki buraz</param>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="InvalidOperationException"></exception>
    /// <exception cref="ObjectDisposedException"></exception>
    /// <exception cref="SmtpException"></exception>
    /// <exception cref="SmtpFailedRecipientException"></exception>
    /// <exception cref="SmtpFailedRecipientsException"></exception>
    public void GenerateEmails(Student freshmanStudent, Student olderStudent)
    {
        try
        {
            var mails = new List<MailMessage>();
            mails.Add(GenerateMailTemplate(freshmanStudent, olderStudent, freshmanStudent.Email));
            mails.Add(GenerateMailTemplate(freshmanStudent, olderStudent, olderStudent.Email));
            mails.ForEach(x => _smtpClient.Send(x));
        }
        catch (Exception)
        {
            throw;
        }
    }

    private MailMessage GenerateMailTemplate(Student freshmanStudent, Student olderStudent, string reciever)
        => new MailMessage(new MailAddress(_senderMailAdress), new MailAddress(reciever))
        {
            Subject = $"FOI BURAZ - Matched pair {freshmanStudent.Name + " " + freshmanStudent.LastName} - {olderStudent.Name + " " + olderStudent.LastName}",
            IsBodyHtml = true,
            Body = $"<html><body>"
                      + $"<h2>FOI BURAZ app</h2>"
                      + $"<p>Ovaj mail služi kao poveznica dvaju studenata te način djeljenja osnovnih informacija.</p>"
                      + $"Mali buraz: <b>{freshmanStudent.Name + " " + freshmanStudent.LastName}</b><br>"
                      + $"Email: <b>{freshmanStudent.Email}</b><br>"
                      + $"Broj mobitela: <b>{freshmanStudent.MobileNumber ??= "-"}</b><br><br>"
                      + $"Veliki buraz: <b>{olderStudent.Name + " " + olderStudent.LastName}</b><br>"
                      + $"Email: <b>{olderStudent.Email}</b><br>"
                      + $"Broj mobitela: <b>{olderStudent.MobileNumber ??= "-"}</b><br><br>"
                      + $"Molimo Vas da ne odgovarate na ovaj mail, upite šaljite na mail: foiburaz@foi.hr</b><br><br>"
                      + $"<p>S poštovanjem,<br>"
                      + $"FOI BURAZ app, Studentski zbor FOI</p>"
                      + $"</body></html>"
        };

    public void Dispose()
    {
        _senderMailAdress = null;
        _smtpClient.Dispose();
    }
}