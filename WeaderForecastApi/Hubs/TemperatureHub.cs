using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using WeaderForecastApi.Models;

namespace WeaderForecastApi.Hubs
{
    public class TemperatureHub : Hub
    {
        public void BroadCastTemperature(Temperatures temperature)
        {
            Clients.All.receiveTemperature(temperature);
        }
    }
}