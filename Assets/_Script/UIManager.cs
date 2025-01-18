using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Stat playerStat;
    public PlayerAmmo ammo;
    public TMP_Text priceAir, priceHarp, priceSpd, priceHP, priceAirRate, money, debt;
    public GameManager gameManager;
    public Canvas UI;
    public PlayerControl player;
    public Transform spawn;

    [Space]
    public Canvas gameplayCanvas;
    public Text HPText, AirText, AmmoText;


    void Start()
    {
        player.gameObject.SetActive(false);
        UI.enabled = true;
        gameplayCanvas.enabled = false;

        ammo.OutOfAirEvent.AddListener(() => OpenUI(true));
    }

    public void OpenUI(bool discardFish = false)
    {
        if (discardFish)
        {
            Debug.Log("OpenUI called with OutOfAir");
        }
        //Sell fish
        BubbleNode bubbleNode = player.GetComponentInChildren<BubbleNode>();
        bubbleNode = bubbleNode.nextNode;

        int totalMoney = 0;

        while (bubbleNode)
        {
            FishStat fishStat = bubbleNode.GetComponentInChildren<FishStat>();

            if (fishStat && !discardFish)
            {
                totalMoney += fishStat.price;
            }

            BubbleNode nextNode = bubbleNode.nextNode;

            for (int i = 0; i < bubbleNode.transform.childCount; i++)
            {
                Destroy(bubbleNode.transform.GetChild(i).gameObject);
            }

            Destroy(bubbleNode.gameObject);

            bubbleNode = nextNode;
        }

        gameManager.Money += totalMoney;

        //Open UI
        player.gameObject.SetActive(false);
        UI.enabled = true;
        gameplayCanvas.enabled = false;
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

        HPText.text = playerStat.currentHP + "/" + playerStat.maxHP;
        AirText.text = ammo.currentAir.ToString("F2") + "/" + ammo.maxAir;
        AmmoText.text = ammo.currentHarpoon + "/" + ammo.maxHarpoon;

    }
    public void upAir()
    {
        if (gameManager.Money < gameManager.airPrice) return;
        ammo.maxAir += (ammo.maxAir * 20/100);
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

        SceneManager.LoadScene("EndGame");
    }
    public void Play()
    {
        playerStat.currentHP = playerStat.maxHP;
        ammo.currentAir = ammo.maxAir;
        ammo.currentHarpoon = ammo.maxHarpoon;

        UI.enabled = false;
        gameplayCanvas.enabled = true;

        player.gameObject.SetActive(true);
        player.GetComponent<Transform>().position = spawn.position;
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        player.ResetMovement();
    }
}
