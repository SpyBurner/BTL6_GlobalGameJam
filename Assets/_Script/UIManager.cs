using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Stat playerStat;
    public PlayerAmmo ammo;
    public TMP_Text priceAir, priceHarp, priceSpd, priceHP, priceAirRate, money, debt;
    public GameManager gameManager;
    public Canvas UI;
    public PlayerControl player;
    void Start()
    {
        player.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        priceAir.text = gameManager.airPrice.ToString();
        priceHarp.text = gameManager.harpPrice.ToString();
        priceSpd.text = gameManager.spdPrice.ToString();
        priceHP.text = gameManager.hpPrice.ToString();
        priceAirRate.text = gameManager.airRatePrice.ToString();
        money.text = gameManager.Money.ToString() + '$';
        debt.text = gameManager.debt.ToString() + '$';
    }
    public void upAir()
    {
        if (gameManager.Money < gameManager.airPrice) return;
        ammo.maxAir += (ammo.maxAir * 10/100);
        gameManager.Money -= gameManager.airPrice;
        gameManager.airPrice += 2;
    }
    public void upSpeed()
    {
        if (gameManager.Money < gameManager.spdPrice) return;
        playerStat.speed = playerStat.speed * 110 / 100;
        gameManager.Money -= gameManager.spdPrice;
        gameManager.spdPrice *= 2;
    }
    public void upHarpoon()
    {
        if (gameManager.Money < gameManager.harpPrice) return;
        ammo.maxHarpoon += 1;
        gameManager.Money -= gameManager.harpPrice;
        gameManager.harpPrice *= 2;
    }
    public void upArmor()
    {
        if (gameManager.Money < gameManager.hpPrice) return;
        playerStat.maxHP = playerStat.maxHP * 110 / 100;
        gameManager.Money -= gameManager.hpPrice;
        gameManager.hpPrice *= 2;
    }
    public void upAirDrain()
    {
        if (gameManager.Money < gameManager.airRatePrice) return;
        ammo.airDrainRate = ammo.airDrainRate * 90 / 100;
        gameManager.Money -= gameManager.airRatePrice;
        gameManager.airRatePrice *= 2;
    }
    public void payDebt()
    {
        if (gameManager.Money < gameManager.debt) return;
        gameManager.Money -= gameManager.debt;
        gameManager.debt = 0;
    }
    public void Play()
    {
        playerStat.currentHP = playerStat.maxHP;
        ammo.currentAir = ammo.maxAir;
        ammo.currentHarpoon = ammo.maxHarpoon;
        UI.enabled = false;
        player.enabled = true;
    }
}
