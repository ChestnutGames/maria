using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Maria {
    public class Event {
        protected enum Type {
            NONE,
            CMD,
            CUSTOM,
        }

        protected Type _type = Type.NONE;
    }
}
