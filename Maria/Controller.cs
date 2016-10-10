using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace Maria
{
    public class Controller
    {
        protected Context _ctx = null;
        protected GameObject _scene = null;

        public Controller(Context ctx)
        {
            Debug.Assert(ctx != null);
            _ctx = ctx;
        }

        // Use this for initialization
        internal void start()
        {

        }

        // Update is called once per frame
        internal virtual void Update(float delta)
        {

        }

        protected void LoadScene(string name)
        {
            SceneManager.LoadSceneAsync(name);
            SceneManager.activeSceneChanged += ActiveSceneChanged;
            SceneManager.sceneLoaded += SceneLoaded;
        }

        public GameObject InitScene()
        {
            if (_scene == null)
            {
                _scene = GameObject.Find("Root");
                Debug.Assert(_scene != null);
                var com = _scene.GetComponent<RootBehaviour>();
                com.OnEnter(_ctx, this);
            }
            
            return _scene;
        }

        public virtual void Enter()
        {

        }

        public virtual void Exit()
        {

        }

        public virtual void Run()
        {

        }

        public virtual void OnDisconnect()
        {

        }

        public virtual void ActiveSceneChanged(Scene from, Scene to)
        {
        }

        public virtual void SceneLoaded (Scene scene, LoadSceneMode sm)
        {
        }
    }
}
