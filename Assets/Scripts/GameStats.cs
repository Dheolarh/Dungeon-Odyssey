using System;
using UnityEngine;

public class GameStats : MonoBehaviour
{
    public static GameStats Instance;
    public int totalCoins = 30;
    public float totalHealth = 16;
    public float stamina = 3;

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
