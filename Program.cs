using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace birthday_mail
{
    public class employeeData
    {
        public int EmpId { get; set; }
        public string EmpName { get; set; }
        public string Email { get; set; }
        public Int64 PhNo { get; set; }
        public string EmpBDate { get; set; }
        [DisplayName("Upload File")]
        public string img_name { get; set; }
        //public HttpPostedFileBase img_file { get; set; }
    }
    internal class birthdayMail
    {
        static void Main(string[] args)
        {
            {
                //Data Source = LAPTOP - VAN8158V\SERVER11; Initial Catalog = birthday; Persist Security Info = True; User ID = sa; Password = ***********; Encrypt = True; Trust Server Certificate = True
                SqlConnection conn = new SqlConnection("Data Source=LAPTOP-VAN8158V\\SERVER11;Initial Catalog=birthday;Persist Security Info=True;User ID=sa;Password=Server11;");
                conn.Open();
                string date1 = DateTime.Today.ToString("yyyy-MM-dd");
                string qry = "select * from employeeData where EmpBDate=@date1";
                SqlCommand cmd = new SqlCommand(qry, conn);
               
                //Console.WriteLine(date1);
                cmd.Parameters.Add(new SqlParameter("@date1",date1));
                SqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                
                employeeData emp = new employeeData
                {
                    EmpId = Convert.ToInt32(dr["EmpId"]),
                    EmpName = dr["EmpName"].ToString(),
                    Email = dr["Email"].ToString(),
                    PhNo = Convert.ToInt64(dr["PhNo"]),
                    EmpBDate = dr["EmpBDate"].ToString(),
                    img_name = dr["img_name"].ToString()
                };
                cmd.Parameters.Add(new SqlParameter("@EmpId", emp.EmpId));
                cmd.Parameters.Add(new SqlParameter("@EmpName", emp.EmpName));
                cmd.Parameters.Add(new SqlParameter("@Email", emp.Email));
                cmd.Parameters.Add(new SqlParameter("@PhNo", emp.PhNo));
                cmd.Parameters.Add(new SqlParameter("@EmpBDate", emp.EmpBDate));
                cmd.Parameters.Add(new SqlParameter("@img_name", emp.img_name));

                string removepath = "C:/Users/ISHITA/source/repos/OneMoreShot/1/";
                string current_path = emp.img_name.Replace(removepath, "https://raw.githubusercontent.com/Ishita25-sys/OneMoreShot/master/1/");
                    //var bdate = emp.EmpBDate;
                    if (date1 == emp.EmpBDate)
                    {
                        string fromMail = "ishu.252502@gmail.com";
                        string fromPassword = "ibkg cjbs dpzr sfjz";

                        MailMessage msg = new MailMessage();
                        msg.From = new MailAddress(fromMail);
                        msg.Subject = "Birthday Wishes...";
                        msg.To.Add(new MailAddress(emp.Email));
                        msg.Body = $@"
                            <!DOCTYPE html>
                            <html lang='en'>
                            <head>
                                <meta charset='UTF-8'>
                                <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                                <title>Birthday Post Template</title>
                                <link rel='stylesheet' href='style.css'>
                                
                            </head>
                            <body>
                                <div class='birthday-post'>
                                    <img src='https://raw.githubusercontent.com/Ishita25-sys/One-More-Shot/main/Birthday%20poster.png' alt='Birthday Background' class='background-image'>
                                    <div class=""employee-photo-container"">
                                        <img src='{current_path}' alt='Employee Photo' class='employee-photo'>
                                    </div>
                                    <div class='employee-info'>
                                        <h1 class='employee-name'>{emp.EmpName}</h1>
                                    </div>
                                </div>
                            </body>
                            </html>
                            ";
                        msg.IsBodyHtml = true;

                        var smtpClient = new SmtpClient("smtp.gmail.com")
                        {
                            Port = 587,
                            Credentials = new NetworkCredential(fromMail, fromPassword),
                            EnableSsl = true,
                        };
                        smtpClient.Send(msg);
                    }
            }
        }
    }
}
