﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Maria;

public class LoginPanelBehaviour : MonoBehaviour {


    public RootBehaviour _root = null;
    public GameObject _uiroot = null;
    public GameObject _usernmIF = null;
    public GameObject _passwdIF = null;

    private Actor _actor;
    private string _server = null;
    private string _username = null;
    private string _password = null;
    private bool _commit = false;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public string GetUsername()
    {
        return _usernmIF.GetComponent<InputField>().text;
    }

    public string GetPassword()
    {
        return _passwdIF.GetComponent<InputField>().text;
    }

    public void OnLoginCommit()
    {
        if (!_commit)
        {
            _commit = true;
            _username = GetUsername();
            _password = GetPassword();
            if (_username.Length < 4)
            {
                Debug.Log("you should have more lenth.");
                return;
            }
            if (_password.Length < 3)
            {
                return;
            }
            _server = "sample";

            Maria.Message msg = new Message();
            msg["username"] = _username;
            msg["password"] = _password;
            msg["server"] = _server;

            Maria.Command cmd = new Command(Bacon.MyEventCmd.EVENT_LOGIN, gameObject, msg);
            _root.App.Application.Enqueue(cmd);

        }
    }
}