using Bacon;
using Sproto;
using System;
using System.Text;
using UnityEngine;

namespace Maria.Network
{
    class Response
    {
        private Context _ctx;
        public Response(Context ctx)
        {
            _ctx = ctx;
        }

        public void handshake(uint session, SprotoTypeBase responseObj)
        {
            C2sSprotoType.handshake.response o = responseObj as C2sSprotoType.handshake.response;
            Debug.Log(string.Format("handshake {0}", o.errorcode));
        }

        // 进入房间这个协议
        public void join(uint session, SprotoTypeBase responseObj)
        {
            GameController controller = _ctx.Top() as GameController;
            controller.Join(responseObj);
        }

        public void born(uint session, SprotoTypeBase responseObj)
        {
            GameController ctr = _ctx.Top() as GameController;
            ctr.Born(responseObj);
        }
    }
}
