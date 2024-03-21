using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NKDAgility.WorkSimulator.Connectors.DataContracts;
using System.Xml.Linq;

namespace NKDAgility.WorkSimulator.Workers
{
    public class ProjectManagerWorker : BackgroundService
    {
        private readonly ILogger<ProjectManagerWorker> _logger;
        private Team _team;

        public ProjectManagerWorker(Team team, ILogger<ProjectManagerWorker> logger)
        {
            _logger = logger;
            _team = team;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
                while (!stoppingToken.IsCancellationRequested)
                {
                var delayInterval = _team.UpdateIntervalSeconds * 86400000;
                    if (_logger.IsEnabled(LogLevel.Information))
                    {
                        _logger.LogInformation("ProjectManagerWorker[{_name}]START [At:{time}]", _team.Name, DateTimeOffset.Now);
                    }


                _logger.LogInformation("ProjectManagerWorker[{_name}]COMPLETE [Next:{time}]", _team.Name, DateTimeOffset.Now.AddMilliseconds(delayInterval));
                await Task.Delay(delayInterval, stoppingToken);
                }
                _logger.LogInformation("Stopping");
        }
    }
}


