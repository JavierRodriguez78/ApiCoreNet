using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ApiGTT.Models;
using Microsoft.EntityFrameworkCore;

internal class TimedHostedService : IHostedService, IDisposable
{
    private readonly ILogger _logger;
    private Timer _timer;

    public TimedHostedService(ILogger<TimedHostedService> logger)
    {
        _logger = logger;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Timed Background Service is starting.");
 
   
    
    

    



        _timer = new Timer(DoWork, null, TimeSpan.Zero, 
            TimeSpan.FromSeconds(20));

        return Task.CompletedTask;
    }

    private void DoWork(object state)
    { 
        var optionsBuild = new DbContextOptionsBuilder<AppDBContext>();
        
        optionsBuild.UseNpgsql("Host=localhost;Port=5432;Username=postgres;Password=example;Database=ApiGtt;");

        
        using (var context = new AppDBContext(optionsBuild.Options)){
         long Id = 1;
         context.Users.Load();
         foreach(var user in context.Users.Local){
            Console.WriteLine(user.username);
         }
         
    }
        _logger.LogInformation("Timed Background Service is working.");
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Timed Background Service is stopping.");

        _timer?.Change(Timeout.Infinite, 0);

        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _timer?.Dispose();
    }
}