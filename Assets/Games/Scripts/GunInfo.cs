using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Gun", menuName = "FPS/Gun Info", order = 0)]
public class GunInfo : ItemInfo
{
    public float damage;
    public int maxAmmo;
}
