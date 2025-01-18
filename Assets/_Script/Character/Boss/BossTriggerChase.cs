using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTriggerChase : MonoBehaviour
{
    public BossAI bossAI;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            bossAI.StartChase(other.gameObject);
        }
    }
}
