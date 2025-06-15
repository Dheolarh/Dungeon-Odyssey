using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private Transform _target;
    private SpriteRenderer _spriteRenderer;
    [Header("Target & Detection")]
    [SerializeField] private Transform mainCharacter;
    [SerializeField] private float detectionRange = .5f;
    [SerializeField] private float attackRange = .5f;
    [SerializeField] private float attackCooldown = 2f;

    [Header("Wandering")]
    [SerializeField] private float wanderRange = 1f;
    [SerializeField] private float wanderInterval = 2f;
    private float _wanderTimer;
    
    [Header("Stats")]
    public int health = 100;

    public bool isAttacking;


    private NavMeshAgent _agent;


    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _agent = GetComponent<NavMeshAgent>();
        InitAgent();
        AssignCharacter();
        _wanderTimer = wanderInterval;
    }

    private void AssignCharacter()
    {
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
    }
    private void InitAgent()
    {
        _agent.updateRotation = false; 
        _agent.updateUpAxis = false; 
        _agent.speed = 0.5f;
        _agent.acceleration = 2f;
        _agent.angularSpeed = 0f; 
        _agent.stoppingDistance = 0.2f; 
        _agent.autoBraking = false;
        _agent.isStopped = false; 
    }

    private void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
        if(gameObject != null) Coordination();
    }

    private void Coordination()
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
            else if (directionToPlayer.x > 0)
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
                TryAttack();
            }
        }
        else
        {
            Wander();
        }
    }

    public void SetTarget(Transform newTarget)
    {
        if (newTarget == null) return;
        _target = newTarget;
        _agent.speed = Character.Instance.moveSpeed /2;
        _agent.acceleration = 4f;
        _agent.SetDestination(_target.position);
    }
    
    
    private float _lastAttackTime;
    private void TryAttack()
    {
        if (Time.time - _lastAttackTime >= attackCooldown)
        {
            Debug.Log("Enemy attacking the player!");
            _lastAttackTime = Time.time;
            
        }
    }
    
    public void TakeDamage(int hit)
    {
        int damage = hit;
        string swordName = Collidables.Instance._swordName;
    
        switch (swordName)
        {
            case "BasicSword":
                health -= damage * 2;
                break;
            case "SoulSword":
                health -= damage * 4;
                break;
            default:
                health -= damage;
                break;
        }
        Debug.Log($"Attacked with {swordName}, enemy health = {health}");
    }


    private void Wander()
    {
        _agent.speed = 0.5f;
        _agent.acceleration = 2f;
        _wanderTimer += Time.deltaTime;
        if (_wanderTimer >= wanderInterval && !_agent.pathPending && _agent.remainingDistance <= _agent.stoppingDistance)
        {
            Vector3 randomDestination = GetRandomNavMeshLocation(wanderRange);
            _agent.SetDestination(randomDestination);
            _wanderTimer = 0f;
        }
        if (_agent.velocity.x < 0)
            _spriteRenderer.flipX = false;
        else if (_agent.velocity.x > 0)
            _spriteRenderer.flipX = true;
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
