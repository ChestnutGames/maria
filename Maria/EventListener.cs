using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Maria {
    public class EventListener {

        public bool _enable;
        
        public EventListener() {
            _enable = false;
        }
         
        public bool Enable { get { return _enable; } set { _enable = value; } }


    }
}
