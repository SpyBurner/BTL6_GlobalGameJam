using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Harpoon : BaseWeapon
{
    private PlayerAmmo playerAmmo;

    private void Start()
    {
        playerAmmo = transform.parent.GetComponent<PlayerAmmo>();
    }

    public override void Shoot()
    {
        if (playerAmmo.currentHarpoon <= 0) return;

        playerAmmo.currentHarpoon -= 1;
        GameObject projectile = Instantiate(projectilePrefab, transform.position, transform.rotation);
        projectile.GetComponent<Harpoon_Projectile>().direction = transform.right;
    }
}
