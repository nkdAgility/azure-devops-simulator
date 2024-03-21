using NKDAgility.WorkSimulator.Connectors.DataContracts;
using System.Collections.ObjectModel;

namespace NKDAgility.WorkSimulator.Connectors
{
    public class TeamConnector
    {
        private IConnectorDataSource _connectorDataSource;

        public TeamConnector(IConnectorDataSource dataSource)
        {
            _connectorDataSource = dataSource;
        }

        public ReadOnlyCollection<Team> GetTeams()
        {
           return _connectorDataSource.GetTeams();
        }
    }
}
