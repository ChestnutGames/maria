using UnityEngine;
using System.Collections;
using Maria;

public class RootBehaviour : MonoBehaviour
{
    private App _app = null;
    private Context _ctx = null;
    private Controller _controller = null;

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

    public void OnEnter(Context ctx, Controller ctr)
    {
        _ctx = ctx;
        _controller = ctr;
    }

    protected App InitApp()
    {
        if (_app == null)
        {
            _app = GameObject.Find("App").GetComponent<App>();
            Debug.Assert(_app != null);
            return _app;
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

    public Context Context
    {
        get
        {
            if (_ctx == null)
            {
                App app = InitApp();
                return app.AppContext;
            }
            else
            {
                return _ctx;
            }
        }
    }

    public Controller Controller
    {
        get
        {
            
            if (_controller == null)
            {
                return this.Context.GetCurController();
            }
            else
            {
                return _controller;
            }
        }
    }
}
