using Microsoft.Extensions.DependencyInjection;
using NKDAgility.WorkSimulator.Connectors;
using NKDAgility.WorkSimulator.Connectors.DataContracts;
using NKDAgility.WorkSimulator.Connectors.InMemory;
using NKDAgility.WorkSimulator.Workers;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;
using System;
using System.Reflection;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = Host.CreateApplicationBuilder(args);
        builder.Logging.ClearProviders();
        string outputTemplate = "[{Timestamp:HH:mm:ss} {Level:u3}] [" + GetVersionTextForLog() + "] {Message:lj}{NewLine}{Exception}";
        builder.Services.AddSerilog(lc => lc
                .WriteTo.Console(theme: AnsiConsoleTheme.Code, outputTemplate: outputTemplate)
                .Enrich.FromLogContext()
                .Enrich.WithMachineName()
                );

        TeamConnector tc = new TeamConnector(new InMemoryConnectorDataSource());

        foreach (Team team in tc.GetTeams())
        {
            builder.Services.AddSingleton<IHostedService, ProjectManagerWorker>(serviceProvider => new ProjectManagerWorker(team, serviceProvider.GetService<ILogger<ProjectManagerWorker>>()));
            foreach (TeamMember member in team.TeamMembers)
            {
                builder.Services.AddSingleton<IHostedService, TeamMemberWorker>(serviceProvider => new TeamMemberWorker(member, serviceProvider.GetService<ILogger<TeamMemberWorker>>()));
            }
        }

        var host = builder.Build();
        host.Run();
    }


    public static string GetVersionTextForLog()
    {
        Version runningVersion = GetRunningVersion();
        string debugVersion = (string.IsNullOrEmpty(ThisAssembly.Git.BaseTag) ? "v" + runningVersion + "-local" : ThisAssembly.Git.BaseTag + "-" + ThisAssembly.Git.Commits + "-local");
        string textVersion = ((runningVersion.Major > 0) ? "v" + runningVersion : debugVersion);
        return textVersion;
    }

    public static Version GetRunningVersion()
    {
        Version assver = Assembly.GetEntryAssembly()?.GetName().Version;
        if (assver == null)
        {
            return new Version("0.0.0");
        }
        return new Version(assver.Major, assver.Minor, assver.Build);
    }
}