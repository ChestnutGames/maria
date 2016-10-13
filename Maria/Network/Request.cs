using Bacon;
using Sproto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Maria.Network
{
    class Request
    {
        private Context _ctx;
        public Request(Context ctx)
        {
            _ctx = ctx;
        }

        public SprotoTypeBase role_info(uint session, SprotoTypeBase requestObj)
        {
            return null;
        }

        public SprotoTypeBase enter_room(uint session, SprotoTypeBase requestObj)
        {
            GameController controller = _ctx.Top() as GameController;
            return controller.OnEnterRoom(requestObj);
        }
    }
}
