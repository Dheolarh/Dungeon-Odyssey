using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObjectsSystem : MonoBehaviour
{
    // Sprites related to Chest
    public Sprite closedChest;
    public Sprite[] openChest = new Sprite[2];
    public Sprite lootedChest;

    // Static access
    public static ObjectsSystem Instance;

    // Object systems
    public ChestSystem Chest;
    public FlagSystem Flag;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // Initialize systems
            Chest = new ChestSystem(this);
            Flag = new FlagSystem(this);
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
        GameStats.Instance.totalCoins += coin;
        Debug.Log($"Picked up {coin} coins. Total coins: {GameStats.Instance.totalCoins}");
    }

    private void OpenChestPowerUp()
    {
        float powerUp = Random.Range(3f, 10f);
        int chance = Random.Range(1, 3);

        switch (chance)
        {
            case 1:
                GameStats.Instance.totalHealth += powerUp;
                Debug.Log($"Picked up health. Total health: {GameStats.Instance.totalHealth}");
                break;
            case 2:
                GameStats.Instance.stamina += powerUp;
                Debug.Log($"Picked up stamina. Total stamina: {GameStats.Instance.stamina}");
                break;
            default:
                Debug.Log("No power-up found");
                break;
        }
    }

    // Expose sprites
    public Sprite GetClosedChest() => _system.closedChest;
    public Sprite GetOpenChest(int index) => _system.openChest[index];
    public Sprite GetLootedChest() => _system.lootedChest;
}

public class FlagSystem
{
    private readonly ObjectsSystem _system;

    public FlagSystem(ObjectsSystem system)
    {
        _system = system;
    }

    // Expose sprites
    public Sprite GetFlag() => _system.closedChest;
}