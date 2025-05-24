using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
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
        StartCoroutine(DelayedPlayerSpawn(scene.name));
    }

    Vector3 SpawnPointForScene(string sceneName)
    {
        if (sceneName == "Level1") return GameManager.Instance.portal2.transform.position;
        if (sceneName == "Level2") return GameManager.Instance.swordPickUp.transform.position;
        return Vector3.zero;
    }
    
    IEnumerator DelayedPlayerSpawn(string sceneName)
    {
        yield return null; // wait one frame so all Awake/Start methods run

        GameManager.Instance.AssignScenePortals();

        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            player.transform.position = SpawnPointForScene(sceneName);
        }
        else
        {
            Debug.LogWarning("Player not found in scene.");
        }
    }

}
