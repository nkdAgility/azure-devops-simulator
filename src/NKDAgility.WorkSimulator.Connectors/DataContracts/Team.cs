using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NKDAgility.WorkSimulator.Connectors.DataContracts
{
    public class Team
    {
        public string Name { get; set; }
        public int UpdateIntervalSeconds { get; set; }
        public List<TeamMember> TeamMembers { get; set; }

        public Team()
        {
            TeamMembers = new List<TeamMember>();
        }
    }
}
