using System;
using System.IO;
using Topshelf;

namespace JobServiceDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            log4net.Config.XmlConfigurator.ConfigureAndWatch(new FileInfo(AppDomain.CurrentDomain.BaseDirectory + "log4net.config"));
            HostFactory.Run(x =>
            {
                x.UseLog4Net();

                x.Service<ServiceRunner>(s => {
                    s.ConstructUsing(name => new ServiceRunner());
                    s.WhenStarted((tc, hc) => tc.Start(hc));
                    s.WhenStopped((tc, hc) => tc.Stop(hc));
                    s.WhenContinued((tc, hc) => tc.Continue(hc));
                    s.WhenPaused((tc, hc) => tc.Pause(hc));
                });

                x.RunAsLocalService();
                x.StartAutomaticallyDelayed();

                x.SetDescription("用于系统定时任务计划执行");
                x.SetDisplayName("JobService");
                x.SetServiceName("JobService");

                x.EnablePauseAndContinue();
            });
        }
    }
}
