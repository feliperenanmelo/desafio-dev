using Polly.Extensions.Http;
using Polly.Retry;
using System.Net.Http;
using System;
using Polly;

namespace Bycoders.DesafioDev.App.Extensions
{
    public static class PollyExtensions
    {
        public static AsyncRetryPolicy<HttpResponseMessage> EsperarTentar()
        {
            var retentarChamada = HttpPolicyExtensions
                .HandleTransientHttpError()
                .WaitAndRetryAsync(new[]
                {
                    TimeSpan.FromSeconds(3),
                    TimeSpan.FromSeconds(5),
                    TimeSpan.FromSeconds(10)
                }, (outcome, timespan, retryCount, context) =>
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine($"Tentando pela {retryCount} vez");
                    Console.ForegroundColor = ConsoleColor.White;
                });

            return retentarChamada;
        }
    }
}
