using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private Spawner[] spawners;

    private void Start()
    {
        spawners = FindObjectsOfType<Spawner>();
    }

    public Vector3 GetPosition()
    {
        return spawners[Random.Range(0, spawners.Length)].transform.position;
    }
}
