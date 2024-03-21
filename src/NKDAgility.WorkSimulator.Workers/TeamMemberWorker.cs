using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NKDAgility.WorkSimulator.Connectors.DataContracts;

namespace NKDAgility.WorkSimulator.Workers
{
    public class TeamMemberWorker : BackgroundService
    {
        private readonly ILogger<TeamMemberWorker> _logger;
        private TeamMember _member;

        public TeamMemberWorker(TeamMember member, ILogger<TeamMemberWorker> logger)
        {
            _logger = logger;
            _member = member;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                if (_logger.IsEnabled(LogLevel.Information))
                {
                    _logger.LogInformation("TeamMemberWorker ({_name}) running at: {time}", _member.Name, DateTimeOffset.Now);
                }



                await Task.Delay(_member.Team.UpdateIntervalSeconds * 2000, stoppingToken);
            }
            _logger.LogInformation("Stopping");
        }
    }
}


