using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Maria {
    class StartActor : Actor {

        public StartActor(Context ctx, Controller controller) : base(ctx, controller) {
            EventListenerCmd listener1 = new EventListenerCmd(Bacon.MyEventCmd.EVENT_STARTSCENE_ENTER, OnEnter);
            _ctx.EventDispatcher.AddCmdEventListener(listener1);
        }

        public void OnEnter(EventCmd e) {
            _go = e.Orgin;
            _ctx.Countdown("startcontroller", 2, CountdownCb);
        }

        public void CountdownCb() {
            _ctx.Push("login");
        }

    }
}
