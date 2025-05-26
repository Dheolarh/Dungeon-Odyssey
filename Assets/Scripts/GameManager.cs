using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Space]
    [Header("Portals")]
    public GameObject portal1;
    public GameObject portal2;
    public GameObject swordPickUp;
    public GameObject swordPickUp2;
    public GameObject swordPortal;
    public GameObject swordPortal2;
    public List<GameObject> allPortals = new List<GameObject>();
    
    
    [Space]
    [Header("Player Statistics")]
    public int totalCoins = 0;
    public int totalHealth = 50;
    public int experience = 1;
    public TextMeshProUGUI coinDisplay;
    public TextMeshProUGUI healthDisplay;
    public TextMeshProUGUI expDisplay;
    
    [Space]
    [Header("In-Game Event Notification")]
    public GameObject notifier;
    public TextMeshProUGUI notifierDisplay;
    public GameObject targetSprite;
    [SerializeField] private float duration;
    public float flowSpeedGameObjects;
    public float flowSpeedSprite;
    
    public void RegisterPortal(GameObject portal)
    {
        if (!allPortals.Contains(portal))
        {
            allPortals.Add(portal);
            Debug.Log("Registered portal: " + portal.name);
        }
        
        switch (portal.name)
        {
            case "Portal1":
                portal1 = portal;
                break;
            case "Portal2":
                portal2 = portal;
                break;
            case "SwordPickUp":
                swordPickUp = portal;
                break;
            case "SwordPortal":
                swordPortal = portal;
                break;
            case "SwordPickUp2":
                swordPickUp2 = portal;
                break;
            case "SwordPortal2":
                swordPortal2 = portal;
                break;
        }
    }

    private void Awake()
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

    private void Start()
    {
        PlayerPrefs.DeleteAll();
        coinDisplay.text = $"Coin: {totalCoins}";
        healthDisplay.text = $"Health: {totalHealth}";
        expDisplay.text = $"EXP: {experience}";

        notifierDisplay = notifier.GetComponentInChildren<TextMeshProUGUI>();
        notifierDisplay.text = "";
    }

    public void AssignScenePortals()
    {
        allPortals.RemoveAll(p => p == null);
        portal1 = allPortals.Find(p => p.name == "Portal1");
        portal2 = allPortals.Find(p => p.name == "Portal2");
        swordPickUp = allPortals.Find(p => p.name == "SwordPickUp");
        swordPickUp2 = allPortals.Find(p => p.name == "SwordPickUp2");
        swordPortal = allPortals.Find(p => p.name == "SwordPortal");
        swordPortal2 = allPortals.Find(p => p.name == "SwordPortal2");
    }

    public void SaveProgress()
    {
        string data = "";
        data += totalCoins.ToString() + "|";
        data += totalHealth.ToString() + "|";
        data += experience.ToString();
        
        PlayerPrefs.SetString("SaveProgress", data);
    }
    
    public void LoadProgress()
    {
        if (PlayerPrefs.HasKey("SaveProgress")) return;
        string[] data = PlayerPrefs.GetString("SaveProgress").Split("|");
        totalCoins = int.Parse(data[1]);
        totalHealth = int.Parse(data[2]);
        experience = int.Parse(data[3]);
    }

    public void NotifierSprite(String text, Color textColor, float flowSpeed)
    {
        notifierDisplay.color = textColor;
        notifierDisplay.text = text;
        notifierDisplay.transform.position = targetSprite.transform.position;
        notifier.transform.Translate(Vector3.up * Time.deltaTime * flowSpeed);
        StartCoroutine(FloatUpAndFade(flowSpeed));
    } 
    public void NotifierGameObject(GameObject target, String text, Color textColor, float flowSpeed)
    {
        notifierDisplay.color = textColor;
        notifierDisplay.text = text;
        notifierDisplay.transform.position = target.transform.position;
        StartCoroutine(FloatUpAndFade(flowSpeed));
    }
    
    private IEnumerator FloatUpAndFade(float speed)
    {
        float elapsed = 0f;

        Vector3 startPos = notifier.transform.position;
        Vector3 endPos   = startPos + Vector3.up * 2f;

        Color startCol = notifierDisplay.color;
        Color endCol   = new Color(startCol.r, startCol.g, startCol.b, 0f);

        while (elapsed < duration)
        {
            float t = elapsed / duration;

            notifier.transform.position = Vector3.Lerp(startPos, endPos, t);

            notifierDisplay.color = Color.Lerp(startCol, endCol, t);

            elapsed += Time.deltaTime * speed;
            yield return null;
        }

        notifierDisplay.color = startCol;
        notifierDisplay.text = "";
    }

}
