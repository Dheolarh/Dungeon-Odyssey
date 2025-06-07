using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private Transform _target;
    private SpriteRenderer _spriteRenderer;
    [Header("Target & Detection")]
    [SerializeField] private Transform mainCharacter;
    [SerializeField] private float detectionRange = .5f;
    [SerializeField] private float attackRange = .16f;
    [SerializeField] private float attackCooldown = 2f;

    [Header("Wandering")]
    [SerializeField] private float wanderRange = 1f;
    [SerializeField] private float wanderInterval = 2f;
    private float _wanderTimer;

    public bool isAttacking;


    private NavMeshAgent _agent;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false; 
        _agent.updateUpAxis = false;   
        if (mainCharacter == null)
        {
            GameObject playerObj = GameObject.FindWithTag("Player");
            if (playerObj != null)
            {
                mainCharacter = playerObj.transform;
            }
            else
            {
                Debug.LogWarning("Player not found. Make sure it has the 'Player' tag.");
            }
        }
        _wanderTimer = wanderInterval;
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, mainCharacter.position);
        Vector3 directionToPlayer = mainCharacter.position - transform.position;

        if (distanceToPlayer <= detectionRange)
        {
            if (directionToPlayer.x < 0)
            {
                switch (isAttacking)
                {
                    case true:
                        _spriteRenderer.flipX = true;
                        break;
                    case false:
                        _spriteRenderer.flipX = false;
                        break;
                }
            }
            else if (directionToPlayer.x > 0 && !isAttacking)
            {
                switch (isAttacking)
                {
                    case true:
                        _spriteRenderer.flipX = false;
                        break;
                    case false:
                        _spriteRenderer.flipX = true;
                        break;
                }
            }
            SetTarget(mainCharacter);
    
            if (distanceToPlayer <= attackRange)
            {
                Debug.Log("Enemy attacking the target!");
                // TODO: Add attack logic with cooldown
            }
        }
        else
        {
            Wander();
        }
    }

    public void SetTarget(Transform newTarget)
    {
        _target = newTarget;
        _agent.SetDestination(_target.position);
    }

    private void Wander()
    {
       _wanderTimer += Time.deltaTime;

        if (_wanderTimer >= wanderInterval)
        {
            Vector3 randomDestination = GetRandomNavMeshLocation(wanderRange);
            _agent.SetDestination(randomDestination);
            _wanderTimer = 0f;
        }
    }

    private Vector3 GetRandomNavMeshLocation(float range)
    {
        Vector2 randomDirection = Random.insideUnitCircle * range;
        Vector3 randomPosition = new Vector3(randomDirection.x, randomDirection.y, 0f) + transform.position;

        if (NavMesh.SamplePosition(randomPosition, out NavMeshHit hit, range, NavMesh.AllAreas))
        {
            return hit.position;
        }

        return transform.position;
    }
}
