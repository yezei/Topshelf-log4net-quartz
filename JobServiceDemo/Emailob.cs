using log4net;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailServices.Common
{
    [PersistJobDataAfterExecution]//保存执行状态
    [DisallowConcurrentExecution]//不允许并发执行
    public class Emailob : IJob
    {
        //日志
        private static readonly ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        async Task IJob.Execute(IJobExecutionContext context)
        {
            logger.Debug("开始发送邮件");
            try
            {
                await Task.Run(() => SendEmail());
            }
            catch (Exception ex)
            {
                await Task.Run(() => logger.Error(ex.Message));
                throw ex;
            }
            logger.Debug("邮件发送结束");
        }

        public void SendEmail()
        {
            System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();
            client.Host = "smtp.163.com";
            client.UseDefaultCredentials = true;
            client.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
            client.Credentials = new System.Net.NetworkCredential("huishanmail@163.com", "huishan0731");
            System.Net.Mail.MailMessage Message = new System.Net.Mail.MailMessage();
            Message.From = new System.Net.Mail.MailAddress("huishanmail@163.com");
            Message.To.Add("599067663@qq.com");
            Message.Subject = "测试标体";
            Message.Body = "测试邮件体";
            Message.SubjectEncoding = System.Text.Encoding.UTF8;
            Message.BodyEncoding = System.Text.Encoding.UTF8;
            Message.Priority = System.Net.Mail.MailPriority.High;
            Message.IsBodyHtml = true;
            client.Send(Message);
        }
    }
}
