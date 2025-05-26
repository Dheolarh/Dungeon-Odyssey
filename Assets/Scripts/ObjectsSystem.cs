using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObjectsSystem : MonoBehaviour
{
    // Sprites related to Chest
    public Sprite closedChest;
    public Sprite[] openChest = new Sprite[2];
    public Sprite lootedChest;
    
    //notifier element
    private String _notification;
    private Color _textColor;
    private float _textSpeed;

    // Static access
    public static ObjectsSystem Instance;

    // Object systems
    public ChestSystem Chest;
    public SwordSystem Flag;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // Initialize systems
            Chest = new ChestSystem(this);
            Flag = new SwordSystem(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}

public class ChestSystem
{
    private readonly ObjectsSystem _system;
    public Action[] ChestActions;

    public ChestSystem(ObjectsSystem system)
    {
        _system = system;
        ChestActions = new Action[] { OnOpenChestCoins, OpenChestPowerUp };
    }

    private void OnOpenChestCoins()
    {
        int coin = Random.Range(5, 50);
        GameManager.Instance.totalCoins += coin;
        GameManager.Instance.coinDisplay.text = $"Coins: {GameManager.Instance.totalCoins.ToString()}";
        GameManager.Instance.NotifierSprite($"+ {coin} coins", Color.yellow, GameManager.Instance.flowSpeedSprite);
    }

    private void OpenChestPowerUp()
    {
        int powerUp = Random.Range(3, 10);
        int chance = Random.Range(1, 3);

        switch (chance)
        {
            case 1:
                GameManager.Instance.totalHealth += powerUp;
                GameManager.Instance.healthDisplay.text = $"Health: {GameManager.Instance.totalHealth.ToString()}";
                GameManager.Instance.NotifierSprite($"+ {powerUp} lives", Color.green, GameManager.Instance.flowSpeedSprite);
                break;
            case 2:
                Color expColor = new Color(0.259f, 0.808f, 0.922f);
                GameManager.Instance.experience += powerUp;
                GameManager.Instance.expDisplay.text = $"exp: {GameManager.Instance.experience.ToString()}";
                GameManager.Instance.NotifierSprite($"+ {powerUp} exp", expColor, GameManager.Instance.flowSpeedSprite);
                break;
            default:
                GameManager.Instance.notifierDisplay.text =
                    $"Empty";
                break;
        }
    }

    // Expose sprites
    public Sprite GetClosedChest() => _system.closedChest;
    public Sprite GetOpenChest(int index) => _system.openChest[index];
    public Sprite GetLootedChest() => _system.lootedChest;
}

public class SwordSystem
{
    private readonly ObjectsSystem _system;

    public SwordSystem(ObjectsSystem system)
    {
        _system = system;
    }

    // Expose sprites
    public Sprite GetFlag() => _system.closedChest;
}

