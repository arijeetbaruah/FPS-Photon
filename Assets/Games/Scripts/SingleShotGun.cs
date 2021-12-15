using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using TMPro;

public class SingleShotGun : Gun
{
    [SerializeField] private Camera cam;
    [SerializeField] private TextMeshProUGUI ammoUI;

    private PhotonView pv;
    private Animator animator;
    private bool isShooting;

    private void Awake()
    {
        pv = GetComponent<PhotonView>();
        animator = GetComponent<Animator>();

        ammo = ((GunInfo)item).maxAmmo;
    }

    public override void Use()
    {
        Shoot();
    }

    public void UpdateAmmo()
    {
        ammoUI.SetText($"Ammo: {ammo}/{((GunInfo)item).maxAmmo}");
    }

    public void Shoot()
    {
        if (isShooting) return;
        if (ammo <= 0)
        {
            ammo = 0;
            UpdateAmmo();
            return;
        }

        isShooting = true;
        Ray ray = cam.ViewportPointToRay(new Vector2(0.5f, 0.5f));
        ray.origin = cam.transform.position;
        animator.SetTrigger("Shoot");

        --ammo;
        UpdateAmmo();

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            hit.collider.gameObject.GetComponent<IDamageble>()?.TakeDamage(((GunInfo) item).damage);
            pv.RPC("RPC_Shoot", RpcTarget.All, hit.point, hit.normal);
        }
    }

    public void ShootAnimationReset()
    {
        isShooting = false;
    }

    [PunRPC]
    public void RPC_Shoot(Vector3 hitPosition, Vector3 hitNormal)
    {
        Collider[] colliders = Physics.OverlapSphere(hitPosition, 0.3f);
        if (colliders.Length != 0)
        {
            GameObject bulletImpactObj = Instantiate(bulletImpactPrefab, hitPosition + hitNormal * 0.001f, Quaternion.LookRotation(hitNormal, Vector3.up) * bulletImpactPrefab.transform.rotation);
            Destroy(bulletImpactObj, 10);
            bulletImpactObj.transform.SetParent(colliders[0].transform);
        }
    }

    public override void Reload()
    {
        ammo = ((GunInfo)item).maxAmmo;
        animator.SetTrigger("Reload");
        UpdateAmmo();
    }

    public override void UpdateUI()
    {
        UpdateAmmo();
    }
}
