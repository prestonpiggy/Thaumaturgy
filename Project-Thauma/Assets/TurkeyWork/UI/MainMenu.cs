using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : Bolt.GlobalEventListener {

    UdpKit.UdpEndPoint endpoint = UdpKit.UdpEndPoint.Parse ("127.0.0.1:27000");

    public void HostGame () {
        BoltLauncher.StartServer (endpoint);
    }

    public void JoinGame () {
        BoltLauncher.StartClient ();
    }

    public void ExitGame () {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit ();
#endif
    }
}
