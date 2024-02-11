using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lexington.Model
{
    internal class ChatText
    {
        public string Text { get; set; } = string.Empty;
        public int Rate { get; set; } = 100;

        public int LiveTime { get; set; } = 5;

    }
}
