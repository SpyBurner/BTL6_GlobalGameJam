using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble_Gun : BaseWeapon
{
    public float airPerShot = 1;
    private PlayerAmmo playerAmmo;

    private void Start()
    {
        playerAmmo = transform.parent.GetComponent<PlayerAmmo>();
    }

    public override void Shoot()
    {
        if (playerAmmo.currentAir < airPerShot) return;

        playerAmmo.currentAir -= airPerShot;
        GameObject projectile = Instantiate(projectilePrefab, transform.position, transform.rotation);
        projectile.GetComponent<BubbleNode>().direction = transform.right;
    }
}
