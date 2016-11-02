using Bacon;
using Maria;
using UnityEngine;
using System.Collections.Generic;

public class App : MonoBehaviour {

    public RootBehaviour _root = null;
    private Maria.Application _app = null;
    private Queue<Actor.RenderHandler> _renderQueue = new Queue<Actor.RenderHandler>();

    // Use this for initialization
    void Start() {
        DontDestroyOnLoad(this);
        _app = new Maria.Application(this);
        var com = _root.GetComponent<StartBehaviour>();
        com.SetupStartRoot();
    }

    // Update is called once per frame
    void Update() {
        lock (_renderQueue) {
            while (_renderQueue.Count > 0) {
                Actor.RenderHandler handler = _renderQueue.Dequeue();
                handler();
            }
        }
    }

    void OnApplicationFocus(bool isFocus) {
        if (_app != null) {
            _app.OnApplicationFocus(isFocus);
        }
    }

    void OnApplicationPause(bool isPause) {
        if (_app != null) {
            _app.OnApplicationPause(isPause);
        }
    }

    void OnApplicationQuit() {
        if (_app != null) {
            _app.OnApplicationQuit();
        }
    }

    public void Enqueue(Command cmd) {
        _app.Enqueue(cmd);
    }

    public void EnqueueRenderQueue(Actor.RenderHandler handler) {
        lock (_renderQueue) {
            _renderQueue.Enqueue(handler);
        }
    }

}
