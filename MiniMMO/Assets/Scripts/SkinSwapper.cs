using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class SkinSwapper : NetworkBehaviour
{
    public NetworkVariable<Color> cubeColor = new NetworkVariable<Color>(new Color(1, 1, 1));

    private Renderer cubeRenderer;


    private void Start()
    {
        cubeRenderer = GetComponentInChildren<Renderer>();

        if (cubeRenderer != null)
        {
            cubeRenderer.material.color = cubeColor.Value;
        }
    }
    void Update()
    {
        if (IsOwner)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                ChangeColor(new Color(Random.value, Random.value, Random.value));
            }
        }
        
        if (cubeRenderer != null)
        {
            cubeRenderer.material.color = cubeColor.Value;
        }
    }

    public void ChangeColor(Color newColor)
    {
        if (IsServer)
        {
            cubeColor.Value = newColor;
        }
        else
        {
            ChangeColorServerRpc(newColor);
        }
    }

    [ServerRpc]
    void ChangeColorServerRpc (Color newColor)
    {
        cubeColor.Value = newColor;
    }
}
