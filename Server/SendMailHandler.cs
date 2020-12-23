using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Mail;

namespace Server
{
    class SendMailHandler
    {
        public static void SendEmail()
        {
            //cria uma mensagem
            MailMessage mail = new MailMessage();

            //define os endereços
            mail.From = new MailAddress("matheus.jimenez@hotmail.com");
            mail.To.Add("matheus.jimenez@hotmail.com.br");

            //define o conteúdo
            mail.Subject = "Este é um simples ,muito simples email";
            mail.Body = "Este é o corpo do email";

            //envia a mensagem
            SmtpClient smtp = new SmtpClient("smtp.gmail.com");
            smtp.Send(mail);
        }
    }


}
