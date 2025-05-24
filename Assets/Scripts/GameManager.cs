using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    //Portals
    public GameObject portal1;
    public GameObject portal2;
    public GameObject swordPickUp;
    public GameObject swordPickUp2;
    public GameObject swordPortal;
    public GameObject swordPortal2;

    public List<GameObject> allPortals = new List<GameObject>();

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

    public void AssignScenePortals()
    {
        portal1 = allPortals.Find(p => p.name == "Portal1");
        portal2 = allPortals.Find(p => p.name == "Portal2");
        swordPickUp = allPortals.Find(p => p.name == "SwordPickUp");
        swordPickUp2 = allPortals.Find(p => p.name == "SwordPickUp2");
        swordPortal = allPortals.Find(p => p.name == "SwordPortal");
        swordPortal2 = allPortals.Find(p => p.name == "SwordPortal2");
    }

    public void SaveProgress()
    {
        Debug.Log("Progress Saved");
    }
    
    public void LoadProgress()
    {
        Debug.Log("Progress Loaded");
    }
}
