using Bacon;
using Sproto;

namespace Maria.Network
{
    class Request
    {
        private Context _ctx;
        public Request(Context ctx)
        {
            _ctx = ctx;
        }

        public SprotoTypeBase handshake(uint session, SprotoTypeBase requestObj)
        {
            S2cSprotoType.handshake.response responseObj = new S2cSprotoType.handshake.response();
            responseObj.errorcode = Errorcode.SUCCESS;
            return responseObj;
        }

        public SprotoTypeBase born(uint session, SprotoTypeBase requestObj)
        {
            GameController controller = _ctx.Top() as GameController;
            return controller.OnBorn(requestObj);
        }

        public SprotoTypeBase leave(uint session, SprotoTypeBase requestObj)
        {
            GameController controller = _ctx.Top() as GameController;
            return controller.OnLeave(requestObj);
        }
    }
}
