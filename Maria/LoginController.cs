using Bacon;

namespace Maria
{
    class LoginController : Controller
    {
        private string _server;
        private string _username;
        private string _password;

        public LoginController(Context ctx) : base(ctx)
        {
        }

        public override void Enter()
        {
            base.Enter();
            LoadScene("login");
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void Run()
        {
            _ctx.Push("login");
        }

        public override void AuthGateCB(int code)
        {
            base.AuthGateCB(code);
            if (code == 200)
            {
                RunGame();
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

        private void RunGame()
        {
            GameController ctr = _ctx.GetController<GameController>("game") as GameController;
            ctr.Run();
        }

    }
}
