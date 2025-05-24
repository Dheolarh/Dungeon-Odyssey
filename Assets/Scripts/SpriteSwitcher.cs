using System.Collections.Generic;
using UnityEngine;

public enum ObjectState
{
    Closed,
    OpenCoin,
    OpenPowerUp,
    Looted,
    Alive,
    Dead
}

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteSwitcher : MonoBehaviour
{
    public ObjectState currentState;
   

    private SpriteRenderer _spriteRenderer;
    private Dictionary<ObjectState, Sprite> chestSprites;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        chestSprites = new Dictionary<ObjectState, Sprite>
        {
            { ObjectState.Closed, ObjectsSystem.Instance.closedChest },
            { ObjectState.OpenCoin, ObjectsSystem.Instance.openChest[0]},
            { ObjectState.OpenPowerUp, ObjectsSystem.Instance.openChest[1]},
            { ObjectState.Looted, ObjectsSystem.Instance.lootedChest },
            
        };

        // Set initial state
        UpdateSprite(currentState);
    }

    public void SetState(ObjectState newState)
    {
        currentState = newState;
        UpdateSprite(newState);
    }

    private void UpdateSprite(ObjectState state)
    {
        if (chestSprites.TryGetValue(state, out Sprite newSprite))
        {
            _spriteRenderer.sprite = newSprite;
        }
        else
        {
            Debug.LogWarning($"Sprite for state {state} not found on {gameObject.name}");
        }
    }
}