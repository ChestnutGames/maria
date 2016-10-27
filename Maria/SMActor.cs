using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Maria {
    public class SMActor : Actor {

        private string _name = string.Empty;

        public SMActor(Context ctx, Controller controller) : base(ctx, controller) {
        }

        public void LoadScene(string name) {
            _name = name;
            _ctx.EnqueueRenderQueue(RenderOnLoadScene);
        }

        public void ActiveSceneChanged(Scene from, Scene to) {
        }

        public void SceneLoaded(Scene scene, LoadSceneMode sm) {
        }

        public void RenderOnLoadScene() {
            Debug.Assert(_name.Length > 0);
            SceneManager.LoadSceneAsync(_name);
            SceneManager.activeSceneChanged += ActiveSceneChanged;
            SceneManager.sceneLoaded += SceneLoaded;
            //_ctx.UnregisterActor(this);
        }
    }
}
