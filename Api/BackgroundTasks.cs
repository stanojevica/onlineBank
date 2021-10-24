using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Api
{
    public class BackgroundTasks : BackgroundService
    {
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var nowDate = DateTime.Now.Date;
            var hours = DateTime.Now.Hour;
            var minutes = DateTime.Now.Minute;
            var sec = DateTime.Now.Second;
            var time = hours + ":" + minutes + ":" + sec;
            var wantedDate = "28";
            var wantedTime = "16:05:00";
            var date = nowDate.ToString();
            if(wantedDate == date && wantedTime == time)
            {
                Console.WriteLine("Molim te radi");
            }
            return null;

        }
    }
}
