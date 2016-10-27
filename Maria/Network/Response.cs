using Bacon;
using Sproto;
using System;
using System.Text;
using UnityEngine;

namespace Maria.Network {
    class Response {
        private Context _ctx;
        public Response(Context ctx) {
            _ctx = ctx;
        }

        public void handshake(uint session, SprotoTypeBase responseObj) {
            InitController controller = _ctx.GetController<InitController>("init");
            controller.Handshake(responseObj);
        }

        // 进入房间这个协议
        public void join(uint session, SprotoTypeBase responseObj) {
            GameController controller = _ctx.Top() as GameController;
            controller.Join(responseObj);
        }

        public void born(uint session, SprotoTypeBase responseObj) {
            GameController ctr = _ctx.Top() as GameController;
            ctr.Born(responseObj);
        }

        public void opcode(uint session, SprotoTypeBase responseObj) {
            GameController ctr = _ctx.Top() as GameController;
            ctr.OpCode(responseObj);
        }
    }
}
