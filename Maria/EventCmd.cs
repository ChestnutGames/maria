using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Maria {
    public class EventCmd : Event {

        public static uint EVENT_LOGIN = 1;

        private uint _cmd;
        private GameObject _orgin;
        private Message _msg;

        public EventCmd(uint cmd) {
            _cmd = cmd;
        }

        public EventCmd(uint cmd, Message msg) {
            _cmd = cmd;
            _msg = msg;
        }

        public EventCmd(uint cmd, GameObject orgin, Message msg) {
            _cmd = cmd;
            _orgin = orgin;
            _msg = msg;
        }

        public uint Cmd { get { return _cmd; } }
        public GameObject Orgin { get { return _orgin; } }
        public Message Msg { get { return _msg; } }
    }
}
