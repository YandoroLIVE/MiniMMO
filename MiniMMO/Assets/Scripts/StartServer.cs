using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class StartServer : MonoBehaviour
{
    public void Start()
    {
        if (NetworkManager.Singleton != null)
        {
            NetworkManager.Singleton.StartServer();
        }
    }
}
