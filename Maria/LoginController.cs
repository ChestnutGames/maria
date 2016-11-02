using Bacon;
using UnityEngine;

namespace Maria {
    class LoginController : Controller {
        private string _server;
        private string _username;
        private string _password;

        private LoginActor _loginActor;

        public LoginController(Context ctx) : base(ctx) {
            _loginActor = new LoginActor(_ctx, this);
        }

        public override void Enter() {
            base.Enter();
            SMActor actor = ((AppContext)_ctx).SMActor;
            actor.LoadScene("login");
        }

        public override void Exit() {
            base.Exit();
        }

        public void AuthLogin(string server, string username, string password) {
            _server = server;
            _username = username;
            _password = password;
            _ctx.AuthLogin(server, username, password);
        }

        public override void AuthGateCB(int code) {
            base.AuthGateCB(code);
            if (code == 200) {
                _ctx.Push("game");
            } else {
                _loginActor.EnableCommitOk();
            }
        }

        public override void AuthGateOnDisconnect() {
            base.AuthGateOnDisconnect();
        }

    }
}
