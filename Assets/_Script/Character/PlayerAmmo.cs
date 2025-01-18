using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerAmmo : MonoBehaviour
{
    public float maxAir;
    public float currentAir;

    public float airDrainRate;

    public UnityEvent OutOfAirEvent;

    public int maxHarpoon;
    public int currentHarpoon;

    private void Start()
    {
        currentAir = maxAir;
        currentHarpoon = maxHarpoon;

        OutOfAirEvent = new UnityEvent();
        
        OutOfAirEvent.AddListener(OnOutOfAir);
    }

    private void Update()
    {
        UseAir(airDrainRate * Time.deltaTime);
    }

    public void UseAir(float amount)
    {
        currentAir -= amount;
        if (currentAir <= 0)
        {
            OutOfAirEvent.Invoke();
        }
    }

    private void OnOutOfAir()
    {
        //TODO
        Debug.Log("Out of air");
    }


}
