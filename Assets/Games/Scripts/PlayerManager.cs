using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using System.IO;

public class PlayerManager : MonoBehaviour
{
    private PhotonView pv;
    private PlayerHealth player;
    private Spawner[] spawners;
    private Spawner spawner;

    private void Awake()
    {
        pv = GetComponent<PhotonView>();
        spawners = FindObjectsOfType<Spawner>();
        spawner = spawners[Random.Range(0, spawners.Length)];
    }

    private void Start()
    {
        if (pv.IsMine)
        {
            CreateController();
        }
    }

    public void Die()
    {
        PhotonNetwork.Destroy(player.gameObject);
        CreateController();
    }

    private void CreateController()
    {
        player = PhotonNetwork.Instantiate(Path.Combine("Prefabs", "Player"), Vector3.zero, Quaternion.identity).GetComponent<PlayerHealth>();
        player.OnDied.AddListener(Die);
    }
}
