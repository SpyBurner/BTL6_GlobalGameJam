using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseWeapon: MonoBehaviour
{
    public float fireRate;
    public float damage;
    public KeyCode shootKey = KeyCode.Mouse0;
    public KeyCode inventoryKey = KeyCode.Alpha1;

    public bool isOnCooldown = false;

    public GameObject projectilePrefab;

    private void OnEnable()
    {
        isOnCooldown = false;
    }

    public abstract void Shoot();
    public IEnumerator Cooldown()
    {
        isOnCooldown = true;
        yield return new WaitForSeconds(1/fireRate);
        isOnCooldown = false;
    }

    public void Update()
    {
        if (Input.GetKey((KeyCode)shootKey) && !isOnCooldown)
        {
            Shoot();
            StartCoroutine(Cooldown());
        }

        if (Input.GetKey((KeyCode)inventoryKey))
        {
            Debug.Log("Switching to weapon: " + gameObject.name);
        }
    }
}
