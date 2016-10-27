using UnityEngine;
using Maria;

public class RootBehaviour : MonoBehaviour
{
    private App _app = null;
    private Maria.Application _application = null;

    // Use this for initialization
    void Start()
    {
        transform.localPosition = Vector3.zero;
        transform.localScale = new Vector3(1, 1, 1);
    }

    // Update is called once per frame
    void Update()
    {

    }

    protected App InitApp()
    {
        if (_app == null)
        {
            _app = GameObject.Find("App").GetComponent<App>();
            if (_app == null) {
                Debug.Assert(false, "why ");
                return null;
            } else {
                _application = _app.Application;
                return _app;
            }
        }
        else
        {
            return _app;
        }
    }

    public App App
    {
        get
        {
            if (_app == null)
            {
                InitApp();
                return _app;
            }
            else
            {
                return _app;
            }
        }
        set
        {
            InitApp();
        }
    }

    public Maria.Application Application {
        get {
            if (_application != null) {
                return _application;
            } else {
                InitApp();
                return _application;
            }
        } 
        set {
            InitApp();
        }
    }
}
