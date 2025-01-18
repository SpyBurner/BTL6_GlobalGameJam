using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public interface IDamagable
{
    public void TakeDamage(int damage);
    public void Heal(int heal);
    public void OnDeath();

    IEnumerator MakeInvincible();

}
