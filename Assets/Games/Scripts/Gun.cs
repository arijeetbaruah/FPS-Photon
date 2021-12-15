using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Gun : Item
{
    public abstract override void Use();
    public abstract override void UpdateUI();
    public abstract void Reload();

    public GameObject bulletImpactPrefab;
    public int ammo;
}
