using UnityEngine;
using System.Collections;

public class Gate {

    public delegate void ConnectCallback(bool connected);
    public delegate void RecviveCallback(byte[] data, int start, int length);
    public delegate void DisconnectCallback(SocketError socketError, PackageSocketError packageSocketError);

    

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Command()
    {

    }
}
