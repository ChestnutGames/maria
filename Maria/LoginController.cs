using Bacon;
using UnityEngine;

namespace Maria
{
    class LoginController : Controller
    {
        private string _server;
        private string _username;
        private string _password;

        private LoginActor _actor;

        public LoginController(Context ctx) : base(ctx)
        {
            _actor = new LoginActor(_ctx, this);

            EventListenerCmd listener1 = new EventListenerCmd(EventCmd.EVENT_LOGIN, Login);
            _ctx.EventDispatcher.AddCmdEventListener(listener1);
        }

        public override void Enter()
        {
            base.Enter();
            SMActor actor = ((AppContext)_ctx).SMActor;
            actor.LoadScene("login");
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void AuthGateCB(int code)
        {
            base.AuthGateCB(code);
            if (code == 200)
            {
                _ctx.Push("game");
            }
        }

        public override void OnDisconnect()
        {
            base.OnDisconnect();
            _ctx.AuthGate(null);
        }

        public void Auth(string server, string username, string password)
        {
            _server = server;
            _username = username;
            _password = password;
            _ctx.AuthLogin(server, username, password, null);
        }

        public void Login(EventCmd e) {
            string str = "login controller login.";
            Debug.Log(str);
            
            Message msg = e.Msg;
            string server = msg["server"].ToString();
            string username = msg["username"].ToString();
            string password = msg["password"].ToString();
            Auth(server, username, password);
        }
    }
}
