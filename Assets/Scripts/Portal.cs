using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
  private GameManager _portalObject;
  private Collidables _portal;
  private GameObject _player;

  private void Start()
  {
    GameManager.Instance.RegisterPortal(gameObject);
    _portalObject = GameManager.Instance;;
    _portal = Collidables.Instance;
    _player = GameObject.FindGameObjectWithTag("Player");;
  }
  

  public void Teleport()
  {
    if (_portal.currentPortal == _portalObject.swordPortal)
    {
      Debug.Log("Teleporting to Sword Pickup");
      _player.transform.position = _portalObject.swordPickUp.transform.position + new Vector3(.3f, 0, 0);
        //new Vector3(-1.548f, -0.3219f, 0);
    }
    
    if (_portal.currentPortal == _portalObject.swordPickUp)
    {
      Debug.Log("Teleporting to Sword Portal");
      _player.transform.position = _portalObject.swordPortal.transform.position + new Vector3(-.3f, 0, 0);
        //new Vector3(5.481f, 0, 0);
    }
    
    if (_portal.currentPortal == _portalObject.portal1)
    {
      Debug.Log("Teleporting to Level 1");
      GameManager.Instance.SaveProgress();
      LoadScene("Level1");
    }
    if (_portal.currentPortal == _portalObject.swordPortal2)
    {
      Debug.Log("Teleporting to Sword Pickup 2");
      _player.transform.position = _portalObject.swordPickUp2.transform.position + new Vector3(-.3f, 0, 0);
        
    }
    
    if (_portal.currentPortal == _portalObject.swordPickUp2)
    {
      Debug.Log("Teleporting to Sword Portal 2");
      _player.transform.position = _portalObject.swordPortal2.transform.position + new Vector3(-.3f, 0, 0);
        
    }  
  }

  private void LoadScene(String sceneID)
  {
    SceneManager.LoadScene(sceneID);
    GameManager.Instance.LoadProgress();
  }
}