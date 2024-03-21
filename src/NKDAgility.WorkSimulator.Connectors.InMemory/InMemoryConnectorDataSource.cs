using NKDAgility.WorkSimulator.Connectors.DataContracts;
using System.Collections.ObjectModel;

namespace NKDAgility.WorkSimulator.Connectors.InMemory
{
    public class InMemoryConnectorDataSource : IConnectorDataSource
    {
        private ReadOnlyCollection<Team> _teams;

        public ReadOnlyCollection<Team> GetTeams()
        {
            if (_teams == null)
            {
                var newteams = new List<Team>();
                for (int i = 0; i < 5; i++)
                {
                    int teamID = i + 1;
                    var t = new Team() { Name = $"Team {teamID}", UpdateIntervalSeconds = teamID };
                    for (int j = 0; j < 0; j++)
                    {
                        int memberID = j + 1;
                        t.TeamMembers.Add(new TeamMember() { Team = t, Name = $"{t.Name} Member {memberID}" });
                    }
                    newteams.Add(t);
                }
                _teams = new ReadOnlyCollection<Team>(newteams);
            }
            return _teams;
        }
    }
}
