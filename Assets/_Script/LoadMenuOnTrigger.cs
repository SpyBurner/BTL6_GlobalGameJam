using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadMenuOnTrigger : MonoBehaviour
{
    public UIManager uiManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && uiManager)
        {
            uiManager.OpenUI();
        }
    }
}
