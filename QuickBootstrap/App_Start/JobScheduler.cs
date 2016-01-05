﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using Quartz;
using Quartz.Impl;

namespace QuickBootstrap.App_Start
{
    public class JobScheduler
    {
        public static void Start()
        {
            IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();
            Debug.WriteLine("default Scheduler is:" + scheduler.SchedulerName);
            scheduler.Start();

            IJobDetail job = JobBuilder.Create<PerformanceExportJob>().Build();

            ITrigger trigger = TriggerBuilder.Create()
                .WithDailyTimeIntervalSchedule
                  (s =>
                     s.WithIntervalInHours(24)
                    .OnEveryDay()
                    .StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(0, 0))
                  )
                .Build();

            ITrigger trigger1= TriggerBuilder.Create()
                    .WithIdentity("trigger1", "group1")
                    .StartNow()
                    .WithSimpleSchedule(x => x
                        .WithIntervalInSeconds(10)
                        .RepeatForever())
                    .Build();

            scheduler.ScheduleJob(job, trigger);
        }

        public static void Stop()
        {
            var scheduler = StdSchedulerFactory.GetDefaultScheduler();
            Debug.WriteLine("default Scheduler is:" + scheduler.SchedulerName);
            scheduler.Shutdown(true);
        }

    }
}