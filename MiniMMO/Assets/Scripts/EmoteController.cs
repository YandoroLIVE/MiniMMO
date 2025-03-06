using Unity.Netcode;
using UnityEngine;
using System.Collections;

public class EmoteController : NetworkBehaviour
{
    public GameObject emotePrefab;
    private GameObject currentEmote;
    public float emoteDuration = 3f;

    private void Update()
    {
        if (IsOwner && Input.GetKeyDown(KeyCode.Alpha2))
        {
            DisplayEmoteServerRpc();
        }
    }
    [ServerRpc]
    private void DisplayEmoteServerRpc(ServerRpcParams rpcParams = default)
    {
        DisplayEmoteClientRpc();

        DestroyEmoteServerRpc();
    }

    [ClientRpc]
    private void DisplayEmoteClientRpc()
    {
        if (currentEmote == null)
        {
            currentEmote = Instantiate(emotePrefab, transform.position + new Vector3(0, 2, 0), Quaternion.identity);
            currentEmote.transform.SetParent(transform); 

            StartCoroutine(DestroyEmoteAfterDelay());
        }
    }

    private IEnumerator DestroyEmoteAfterDelay()
    {
        yield return new WaitForSeconds(emoteDuration);

        if (currentEmote != null)
        {
            Destroy(currentEmote);
        }
    }

    [ServerRpc]
    private void DestroyEmoteServerRpc(ServerRpcParams rpcParams = default)
    {
        DestroyEmoteClientRpc();
    }

    [ClientRpc]
    private void DestroyEmoteClientRpc()
    {
        if (currentEmote != null)
        {
            Destroy(currentEmote);
        }
    }
}
