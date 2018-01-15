using System;
using System.Collections.Generic;
using System.Text;

namespace ElectronicSign
{
    class Message
    {
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public bool SingleLine { get; set; }
        public bool Scrolling { get; set; }
        public string ScrollingSpeed { get; set; }
        public string DisplayTime { get; set; }
    }
}
