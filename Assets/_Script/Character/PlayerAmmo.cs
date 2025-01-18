using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerAmmo : MonoBehaviour
{
    public float maxAir;
    public float currentAir;

    public float airDrainRate;

    public UnityEvent OutOfAirEvent = new UnityEvent();

    public int maxHarpoon;
    public int currentHarpoon;

    private void Start()
    {
        currentAir = maxAir;
        currentHarpoon = maxHarpoon;
        
        OutOfAirEvent.AddListener(OnOutOfAir);
    }

    private void Update()
    {
        UseAir(airDrainRate * Time.deltaTime);

        currentHarpoon = Mathf.Clamp(currentHarpoon, 0, maxHarpoon);

    }

    public void UseAir(float amount)
    {
        if (currentAir <= 0) return;

        currentAir -= amount;
        if (currentAir <= 0)
        {
            currentAir = 0;
            OutOfAirEvent.Invoke();
        }
    }

    private void OnOutOfAir()
    {
        //TODO
        Debug.Log("Out of air");
    }


}
