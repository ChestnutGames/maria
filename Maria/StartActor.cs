using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Maria {
    class StartActor : Actor {

        public StartActor(Context ctx, Controller controller) : base(ctx, controller) {
            EventListenerCmd listener1 = new EventListenerCmd(Bacon.MyEventCmd.EVENT_SETUP_STARTROOT, SetupStartRoot);
            _ctx.EventDispatcher.AddCmdEventListener(listener1);
        }

        private void SetupStartRoot(EventCmd e) {
            _go = e.Orgin;
            _ctx.Countdown("startcontroller", 2, CountdownCb);

            //byte[] buffer = new byte[4] { 1, 2, 3, 4 };
            
            //Rudp.Rudp u = new Rudp.Rudp(1, 5);
            //u.Send(buffer);
            //u.Update(null, 1);

        }

        private void CountdownCb() {
            _ctx.Push("login");
        }
    }
}
