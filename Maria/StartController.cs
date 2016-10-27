using UnityEngine;

namespace Maria
{
    public class StartController : Controller
    {
        private StartActor _actor = null;

        public StartController(Context ctx) : base(ctx)
        {
            _actor = new StartActor(_ctx, this);
            
        }

        public override void Enter()
        {
        }

        public override void Exit()
        {
        }
    }
}
