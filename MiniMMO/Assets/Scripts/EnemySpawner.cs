using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Enemy enemy;
    void Start()
    {
        InvokeRepeating(nameof(SpawnEnemy), 2f, 6f);
    }

    // Update is called once per frame
    private void SpawnEnemy()
    {
        Enemy instantiatedEnemy = Instantiate(enemy);
        instantiatedEnemy.GetComponent<NetworkObject>().Spawn();
    }
}
