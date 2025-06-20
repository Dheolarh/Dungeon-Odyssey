using System;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class Collidables : MonoBehaviour
{
    private Enemy _currentEnemy;
    public Enemy GetCurrentEnemy() => _currentEnemy;
    private Character _character;
    public static Collidables Instance;
    private BoxCollider2D _obj;
    public GameObject currentPortal;
    [SerializeField] private HashSet<GameObject> hitObjects;

    public bool withEnemey;
    
    //notifier element
    private GameObject _target;
    private String _notification;
    private Color _textColor;
    private float _textSpeed;
    
    //collections
    public string _swordName;
    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        _character = Character.Instance;
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
                GameManager.Instance.targetSprite = target;
                HandleChestCollision(target, spriteSwitcher);
                hitObjects.Add(target);
                break;
            case "Enemy":
                withEnemey = true;
                _currentEnemy = other.gameObject.GetComponent<Enemy>();
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
                {
                    _swordName = "BasicSword";
                    GameManager.Instance.NotifierGameObject(target, "Equipped Basic Sword", Color.green, GameManager.Instance.flowSpeedGameObjects); 
                }

                if (target.gameObject.name == "SoulSword")
                {
                    _swordName = "SoulSword";
                    GameManager.Instance.NotifierGameObject(target, "Equipped Soul Sword", Color.black, GameManager.Instance.flowSpeedGameObjects);;
                }
                break;
        }

        
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
            case "Enemy":
                withEnemey = false;
                _currentEnemy = null;
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