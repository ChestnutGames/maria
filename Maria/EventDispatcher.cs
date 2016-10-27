using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Maria {
    public class EventDispatcher {

        protected Context _ctx;
        protected List<EventListener> _listers;
        protected Dictionary<string, EventListenerCustom> _dic;
        protected Dictionary<uint, EventListenerCmd> _cmdDic;

        public EventDispatcher(Context ctx) {
            _ctx = ctx;
            _listers = new List<EventListener>();
            _cmdDic = new Dictionary<uint, EventListenerCmd>();
        }

        public void AddEventListener(EventListener listener) {

        }

        public void AddCmdEventListener(EventListenerCmd listener) {
            uint cmd = listener.Cmd;
            _cmdDic[cmd] = listener;
        }

        public EventListenerCustom AddCustomEventListener(string eventName, EventListenerCustom.OnEventCustomHandler callback) {
            EventListenerCustom listener = new EventListenerCustom(eventName, callback);
            _dic[eventName] = listener;
            return listener;
        }

        public void DispatchCmdEvent(Command cmd) {
            EventCmd e = new EventCmd(cmd.Cmd, cmd.Orgin, cmd.Msg);
            if (_cmdDic.ContainsKey(cmd.Cmd)) {
                EventListenerCmd listener = _cmdDic[cmd.Cmd];
                if (listener != null) {
                    listener.Handler(e);
                }
            }
        }

        public void DispatchEvent(Event e) {
        }

        public void DispatchCustomEvent(string eventName, object o) {
        }

    }
}
