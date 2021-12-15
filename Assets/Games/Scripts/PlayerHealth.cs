using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour, IDamageble
{
    [SerializeField] private float maxHealth = 100;
    [SerializeField] private PlayerHPUI HPUI;

    private PhotonView pv;
    public float currentHealth;
    public UnityEvent OnDied;

    private void Start()
    {
        currentHealth = maxHealth;

        pv = GetComponent<PhotonView>();
        if (!pv.IsMine)
        {
            HPUI.gameObject.SetActive(false);
        }
        else
        {
            HPUI.gameObject.SetActive(true);
            HPUI.MaxValue = maxHealth;
            HPUI.SetValue(currentHealth);
        }
    }

    private void Update()
    {
        if (transform.position.y < -50)
        {
            OnDied.Invoke();
        }
    }

    public void TakeDamage(float dmg)
    {
        pv.RPC("RPC_TakeDamage", RpcTarget.All, dmg);
    }

    [PunRPC]
    public void RPC_TakeDamage(float dmg)
    {
        if (!pv.IsMine) return;
        currentHealth = Mathf.Max(0, currentHealth - dmg);
        HPUI.SetValue(currentHealth);
        if (currentHealth == 0)
        {
            OnDied.Invoke();
        }
    }
}
