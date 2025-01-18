using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Stat : MonoBehaviour, IDamagable
{
    public int maxHP;
    public int currentHP;
    public UnityEvent DeathEvent;

    public float speed;

    public int damage = 1;

    public bool isDead = false;

    public float invincibleTime = 0.5f;
    public bool isInvincible = false;

    public void Heal(int heal)
    {
        currentHP += heal;
        if (currentHP > maxHP)
        {
            currentHP = maxHP;
        }
    }

    public void TakeDamage(int damage)
    {
        if (isInvincible) return;

        currentHP -= damage;
        StartCoroutine(MakeInvincible());

        if (currentHP <= 0)
        {
            currentHP = 0;
            isDead = true;
            DeathEvent.Invoke();
        }
        else
        {
            // Not yet
        }
    }

    public void OnDeath()
    {
        //Destroy(gameObject);
    }

    public IEnumerator MakeInvincible()
    {
        isInvincible = true;
        yield return new WaitForSeconds(invincibleTime);
        isInvincible = false;
    }

    void Awake()
    {
        currentHP = maxHP;

        DeathEvent = new UnityEvent();

        DeathEvent.AddListener(OnDeath);
    }

}
