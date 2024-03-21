
using NKDAgility.WorkSimulator.Connectors.DataContracts;
using System.Collections.ObjectModel;

namespace NKDAgility.WorkSimulator.Connectors
{
    public interface IConnectorDataSource
    {
       public ReadOnlyCollection<Team> GetTeams();
    }
}