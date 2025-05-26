using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private static LevelManager _instance;
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
    }
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        GameManager.Instance.AssignScenePortals();
        GameManager.Instance.LoadProgress();
        if (scene.name == "Level1")
        {
            StartCoroutine(SpawnPlayerAfterDelay(1f));
        }
        
        Debug.Log("Loaded " + scene.name);
    }
    
    IEnumerator SpawnPlayerAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Character.Instance.SpawnPlayerIntoScene();
    }

}
