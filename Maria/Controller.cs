using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using Maria.Network;

namespace Maria {
    public class Controller {
        protected Context _ctx = null;
        protected bool _authtcp = false;
        protected bool _authudp = false;
        protected List<Actor> _actors = new List<Actor>();

        public Controller(Context ctx) {
            Debug.Assert(ctx != null);
            _ctx = ctx;
        }

        // Update is called once per frame
        internal virtual void Update(float delta) {
            foreach (var item in _actors) {
                item.Update(delta);
            }
        }

        public virtual void Enter() {
        }

        public virtual void Exit() {
        }

        public virtual void GateAuthCb(int code) {
            if (code == 200) {
                _authtcp = true;
            }
        }

        public virtual void GateDisconnect() {
            _authtcp = false;
            _ctx.GateAuth(null);
        }

        public virtual void UdpAuthCb(bool ok) {
            _authudp = ok;
        }

        public virtual void OnRecviveUdp(PackageSocketUdp.R r) {
        }

        public void Add(Actor item) {
            _actors.Add(item);
        }

        public bool Remove(Actor item) {
            return _actors.Remove(item);
        }
    }
}
