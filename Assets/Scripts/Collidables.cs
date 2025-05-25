using System;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class Collidables : MonoBehaviour
{
    public static Collidables Instance;
    private BoxCollider2D _obj;
    public GameObject currentPortal;
    [SerializeField] private HashSet<GameObject> hitObjects;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        hitObjects = new HashSet<GameObject>();
        _obj = GetComponent<BoxCollider2D>();
    }
    
    public void OnCollisionEnter2D(Collision2D other)
    {
        GameObject target = other.gameObject;
        var spriteSwitcher = target.GetComponent<SpriteSwitcher>();

        if (hitObjects.Contains(target))
        {
            Debug.Log("Already collected " + target.name);
            return;
        }

        switch (target.tag)
        {
            case "Chest":
                HandleChestCollision(target, spriteSwitcher);
                hitObjects.Add(target);
                break;
            case "Enemy":
                break;
            case "Portals":
                currentPortal = target.gameObject;
                Portal portalScript = target.GetComponent<Portal>();
                portalScript.Teleport();
                hitObjects.Remove(target);
                break;
            case "Sword":
                Destroy(target);
                if (target.gameObject.name == "BasicSword")
                    GameManager.Instance.notifierDisplay.text = "Equipped Iron Sword!";
                if (target.gameObject.name == "SoulSword")
                    GameManager.Instance.notifierDisplay.text = "Equipped Soul Sword!";
                StartCoroutine(ResetNotifierDislay());
                break;
        }

        
    }
    
    IEnumerator ResetNotifierDislay()
    {
        yield return new WaitForSeconds(4);
        GameManager.Instance.notifierDisplay.text = "";
    }

    public void OnCollisionExit2D(Collision2D other)
    {
        GameObject target = other.gameObject;
        var spriteSwitcher = target.GetComponent<SpriteSwitcher>();
        if (spriteSwitcher == null) return;
        switch (target.tag)
        {
            case "Chest":
                if (spriteSwitcher != null && 
                    (spriteSwitcher.currentState == ObjectState.OpenCoin || spriteSwitcher.currentState == ObjectState.OpenPowerUp))
                    spriteSwitcher.SetState(ObjectState.Looted);
                break;
        }
    }
    
    private void HandleChestCollision(GameObject chest, SpriteSwitcher switcher)
    {
        int random = UnityEngine.Random.Range(0, ObjectsSystem.Instance.Chest.ChestActions.Length);
        switcher.SetState(random == 0 ? ObjectState.OpenCoin : ObjectState.OpenPowerUp);
        ObjectsSystem.Instance.Chest.ChestActions[random]();
    }

}