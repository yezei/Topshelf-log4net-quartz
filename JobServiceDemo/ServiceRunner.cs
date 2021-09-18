﻿using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Text;
using Topshelf;

namespace JobServiceDemo
{
    public sealed class ServiceRunner : ServiceControl, ServiceSuspend
    {
        //调度器
        private readonly IScheduler scheduler;
        public ServiceRunner()
        {
            scheduler = StdSchedulerFactory.GetDefaultScheduler().GetAwaiter().GetResult();
        }
        //开始
        public bool Start(HostControl hostControl)
        {
            scheduler.Start();
            return true;
        }
        //停止
        public bool Stop(HostControl hostControl)
        {
            scheduler.Shutdown(false);
            return true;
        }
        //恢复所有
        public bool Continue(HostControl hostControl)
        {
            scheduler.ResumeAll();
            return true;
        }
        //暂停所有
        public bool Pause(HostControl hostControl)
        {
            scheduler.PauseAll();
            return true;
        }
    }
}
