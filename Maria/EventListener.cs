using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Maria {
    public class EventListener {

        protected bool _enable;

        public EventListener() {
            _enable = true;
        }
         
        public bool Enable { get { return _enable; } set { _enable = value; } }

    }
}
