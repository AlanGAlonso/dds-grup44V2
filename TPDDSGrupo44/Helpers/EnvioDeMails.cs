using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using TPDDSGrupo44.DataModels;
using TPDDSGrupo44.Models;

namespace TPDDSGrupo44.Helpers
{
    public class EnvioDeMails
    {
        public EnvioDeMails()
        {
        }
        
        public void enviarMail(TimeSpan cantSegDemora, int tipoDeMail)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress("dds44utnviernes@gmail.com");

                using (var db = new BuscAR())
                {
                    List<Usuario> admins = db.Usuarios.Where(u => u.rol.nombre == "Administrador").ToList();
                    foreach (Usuario admin in admins)
                    {
                        mail.To.Add(admin.email);
                    }
                }
                    

                switch (tipoDeMail)
                {
                    case 0:
                        mail.Subject = "WARNING: Búsqueda lenta";
                        mail.Body = "Alguien realizó una búsqueda que tomó "+ cantSegDemora.Seconds + " segundos";
                        break;
                    case 1:
                        mail.Subject = "ERROR: Fallo la aplicacion";
                        mail.Body = "Se Notifica al Administrador que hubo un error. Revisar LOGS";
                        break;
                }
                
                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("dds44utnviernes@gmail.com", "carodds44");
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);
                Console.WriteLine("mail Send");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }


        }

    }
}