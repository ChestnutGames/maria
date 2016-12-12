﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using Maria.Network;
using System;

namespace Maria  {
    public class Controller : INetwork {
        protected Context _ctx = null;
        protected bool _authtcp = false;
        protected bool _authudp = false;
        protected List<Actor> _actors = new List<Actor>();

        public Controller(Context ctx) {
            Debug.Assert(ctx != null);
            _ctx = ctx;
        }

        // Update is called once per frame
        public virtual void Update(float delta) {
            foreach (var item in _actors) {
                item.Update(delta);
            }
        }

        public void Add(Actor item) {
            _actors.Add(item);
        }

        public bool Remove(Actor item) {
            return _actors.Remove(item);
        }

        public virtual void Enter() {
        }

        public virtual void Exit() {
        }

        public void GateAuthed(int code) {
            if (code == 200) {
                _authtcp = true;
            }
        }

        public void GateDisconnected() {
            _authtcp = false;
        }

        public void UdpAuthed(uint session) {
            _authudp = true;
            throw new NotImplementedException();
        }
    }
}
