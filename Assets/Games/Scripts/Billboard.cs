using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

using Photon.Pun;

public class Billboard : MonoBehaviour
{
    private Camera cam;
    [SerializeField] PhotonView pv;

    private void Start()
    {
        SetUsername(pv.Owner.NickName);
    }

    private void Update()
    {
        if (cam == null)
        {
            cam = FindObjectOfType<Camera>();
        }

        if (cam == null) return;

        transform.LookAt(cam.transform);
        transform.Rotate(Vector3.up * 180);
    }

    public void SetUsername(string name)
    {
        GetComponentInChildren<TextMeshProUGUI>().SetText(name);
    }
}
