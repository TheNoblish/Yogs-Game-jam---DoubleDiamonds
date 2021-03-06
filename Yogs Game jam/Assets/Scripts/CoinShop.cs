﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinShop : MonoBehaviour
{
    public GameObject coinCounter;
    public GameObject coinShopPanel;
    Text coinCounterText;
    public Text coinShopCounterText;
    bool shopOpen;
    public int coins;

    GameObject player;

    public Sprite[] sprites;

    public static bool snowballsUnlocked;
    int throwSpeedLevel;
    public Image throwSpeedLevels;
    int movementSpeedLevel;
    public Image movementSpeedLevels;
    int jumpHeightLevel;
    public Image jumpHeightLevels;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        coinCounterText = coinCounter.GetComponent<Text>();
        //coinShopCounterText = GameObject.Find("CoinShopCounter").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("tab") && !shopOpen)
        {
            coinShopPanel.SetActive(true);
            coinShopCounterText.text = "Coins: " + coins.ToString();
            shopOpen = true;
            coinCounter.SetActive(false);
        } else if (Input.GetKeyDown("tab") && shopOpen)
        {
            closeShop();
        }
    }

    public void closeShop()
    {
        coinCounter.SetActive(true);
        coinCounterText.text = "Coins: " + coins.ToString();
        shopOpen = false;
        coinShopPanel.SetActive(false);
    }

    public int getCoins()
    {
        return coins;
    }

    public void addCoin(int coin)
    {
        coins = coins + coin;
        coinCounterText.text = "Coins: " + coins.ToString();
    }

    public void upgradeSnowballs()
    {
        if (!snowballsUnlocked && coins >= 1)
        {
            snowballsUnlocked = true;
            coins = coins - 1;
            coinShopCounterText.text = "Coins: " + coins.ToString();
            coinCounterText.text = "Coins: " + coins.ToString();
            //
        }
    }

    public void upgradeThrowSpeed()
    {

        if (throwSpeedLevel < 4 && coins >= 1)
        {
            switch (throwSpeedLevel)
            {
                case 0:
                    player.GetComponent<throwStuff>().setThrowForce();
                    throwSpeedLevels.sprite = sprites[0];
                    break;
                case 1:
                    player.GetComponent<throwStuff>().setThrowForce();
                    throwSpeedLevels.sprite = sprites[1];
                    break;
                case 2:
                    player.GetComponent<throwStuff>().setThrowForce();
                    throwSpeedLevels.sprite = sprites[2];
                    break;
                case 3:
                    player.GetComponent<throwStuff>().setThrowForce();
                    throwSpeedLevels.sprite = sprites[3];
                    break;
                default:
                    break;
            }

                coins = coins - 1;
                coinShopCounterText.text = "Coins: " + coins.ToString();
                coinCounterText.text = "Coins: " + coins.ToString();

            if (throwSpeedLevel <= 3)
            {
                throwSpeedLevel = throwSpeedLevel + 1;
            }
        }

    }

    public void upgradeMovementSpeed()
    {

        if (movementSpeedLevel < 4 && coins >= 1)
        {
            switch (movementSpeedLevel)
            {
                case 0:
                    player.GetComponent<PlayerController>().setMovementSpeed(0.5f);
                    movementSpeedLevels.sprite = sprites[0];
                    break;
                case 1:
                    player.GetComponent<PlayerController>().setMovementSpeed(0.5f);
                    movementSpeedLevels.sprite = sprites[1];
                    break;
                case 2:
                    player.GetComponent<PlayerController>().setMovementSpeed(0.5f);
                    movementSpeedLevels.sprite = sprites[2];
                    break;
                case 3:
                    player.GetComponent<PlayerController>().setMovementSpeed(0.5f);
                    movementSpeedLevels.sprite = sprites[3];
                    break;
                default:
                    break;
            }

                coins = coins - 1;
                coinShopCounterText.text = "Coins: " + coins.ToString();
                coinCounterText.text = "Coins: " + coins.ToString();

            if (movementSpeedLevel <= 3)
            {
                movementSpeedLevel = movementSpeedLevel + 1;
            }
        }
    }

    public void upgradeJumpHeight()
    {

        if (jumpHeightLevel < 4 && coins >= 1)
        {
            switch (jumpHeightLevel)
            {
                case 0:
                    player.GetComponent<PlayerController>().setJumpHeight(0.25f);
                    jumpHeightLevels.sprite = sprites[0];
                    break;
                case 1:
                    player.GetComponent<PlayerController>().setJumpHeight(0.25f);
                    jumpHeightLevels.sprite = sprites[1];
                    break;
                case 2:
                    player.GetComponent<PlayerController>().setJumpHeight(0.25f);
                    jumpHeightLevels.sprite = sprites[2];
                    break;
                case 3:
                    player.GetComponent<PlayerController>().setJumpHeight(0.25f);
                    jumpHeightLevels.sprite = sprites[3];
                    break;
                default:
                    break;
            }

                coins = coins - 1;
                coinShopCounterText.text = "Coins: " + coins.ToString();
                coinCounterText.text = "Coins: " + coins.ToString();

            if (jumpHeightLevel <= 3)
            {
                jumpHeightLevel = jumpHeightLevel + 1;
            }
        }
    }
}
