using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Maria {
    public class Actor : IDisposable {
        public delegate void RenderHandler();

        protected Context _ctx;
        protected Controller _controller;
        protected GameObject _go;

        public Actor(Context ctx, Controller controller)
            : this(ctx, controller, null) {
        }

        public Actor(Context ctx, Controller controller, GameObject go) {
            _ctx = ctx;
            _controller = controller;
            _go = go;
            _controller.Add(this);
        }

        public void Dispose() {
            _controller.Remove(this);
        }

        public GameObject Go { get { return _go; } set { _go = value; } }

        public virtual void Update(float delta) {
        }
    }
}
