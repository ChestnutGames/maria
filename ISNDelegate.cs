using UnityEngine;
using System.Collections;
using System.Net.Sockets;

public interface ISNDelegate {

    void OnConnected(SNSocket s);

    void OnMessage(SNSocket s, byte[] buffer);

    void OnError(SNSocket s, SocketError errorCode, string msg);

    void OnClose(SNSocket s);
}
