using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lexington.Model
{
    public class Expedition: ResoureCount
    {
        public string ExpeditionId { get; set; } = string.Empty;

        public string ExpeditionName { get; set; } = string.Empty;
        public string ExpeditionTime { get; set; } = string.Empty;

    }
}
