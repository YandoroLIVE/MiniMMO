using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class SkinSwapper : NetworkBehaviour
{
    public NetworkVariable<Color> objectColor = new NetworkVariable<Color>(new Color(1, 1, 1));

    void Update()
    {
        if (IsOwner)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                ChangeColor(new Color(Random.value, Random.value, Random.value));
            }
        }
        GetComponent<Renderer>().material.color = objectColor.Value;
    }

    public void ChangeColor(Color newColor)
    {
        if (IsServer)
        {
            objectColor.Value = newColor;
        }
        else
        {
            ChangeColorServerRpc(newColor);
        }
    }

    [ServerRpc]
    void ChangeColorServerRpc (Color newColor)
    {
        objectColor.Value = newColor;
    }
}
