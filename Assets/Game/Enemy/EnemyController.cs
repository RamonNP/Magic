using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float patrolDistance = 5f;
    [SerializeField] private float attackRange = 1.5f;
    [SerializeField] private float attackDelay = 2f;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private LayerMask platformLayer;
    [SerializeField] private EnemyAnimationController anim;
    [SerializeField] private float platformDetectionOffset = 0.5f;
    [SerializeField] private Transform _origemRaycastPlayerDetect;

    private EnemyStatus enemyStatus;
    private Rigidbody2D rb;
    private Vector2 patrolStartPos;
    private Vector2 patrolEndPos;
    private RaycastHit2D playerHit;
    private RaycastHit2D platformHit;
    [SerializeField] private bool isFacingRight = true;
    [SerializeField] private float timeSinceAttack;

    private float disableTimer = 0f; // Timer para desativar temporariamente o inimigo
    private bool disableEnemy = false; // Variável que indica se o inimigo está desativado


    public bool IsFacingRight { get => isFacingRight; set => isFacingRight = value; }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        enemyStatus = GetComponent<EnemyStatus>();
    }

    private void Start()
    {
        patrolStartPos = transform.position;
        patrolEndPos = patrolStartPos + Vector2.right * patrolDistance;
        anim = GetComponent<EnemyAnimationController>();
    }

    private void Update()
    {
        if (!enemyStatus.IsDie && !disableEnemy)
        {
            IAEnemy();
        }

        EnableEnemy();
    }

    private void EnableEnemy()
    {
        if (disableEnemy)
        {
            disableTimer += Time.deltaTime; // Adiciona tempo ao timer
            if (disableTimer >= 2f) // Se o timer for maior ou igual a 2 segundos
            {
                disableEnemy = false; // Desativa o inimigo
                disableTimer = 0f; // Reseta o timer
            }
        }
    }

    // Método para desativar temporariamente o inimigo
    public void DisableEnemy()
    {
        disableEnemy = true;
    }

    private void IAEnemy()
    {
        // Detect player with raycast
        playerHit = Physics2D.Raycast(_origemRaycastPlayerDetect.position, Vector2.right * (isFacingRight ? 1 : -1), attackRange, playerLayer);

        if (playerHit.collider != null && playerHit.collider.CompareTag("Player"))
        {
            // && playerHit.collider.CompareTag("Player")
            // o jogador foi detectado, faça algo aqui
            // Player detected, attack or move towards player
            if (timeSinceAttack >= attackDelay)
            {
                Debug.Log("ATAQUEI");
                Attack();
            }
            else
            {
                timeSinceAttack += Time.deltaTime;
                //MoveTowardsPlayer();
            }
        }
        else
        {
            //se voltou a patrulhar reset o ataque.
            timeSinceAttack = attackDelay;
            // Player not detected, patrol
            Patrol();
        }

        // Detect platform with raycast
        //platformHit = Physics2D.Raycast(transform.position, Vector2.down, 1.5f, platformLayer);
        // Detect platform with raycast
        Vector2 platformDetectionPos = transform.position + new Vector3((isFacingRight ? 1 : -1) * platformDetectionOffset, 0f, 0f);
        platformHit = Physics2D.Raycast(platformDetectionPos, Vector2.down, 1.5f, platformLayer);

        if (platformHit.collider == null)
        {
            // No platform detected, turn around
            isFacingRight = !isFacingRight;
            transform.localScale = new Vector3(isFacingRight ? 1 : -1, 1, 1);
        }

        // Debug raycasts
        Debug.DrawRay(_origemRaycastPlayerDetect.position, Vector2.right * (isFacingRight ? 1 : -1) * attackRange, Color.red);
        Debug.DrawRay(platformDetectionPos, Vector2.down * 1.5f, Color.green);
    }

    private void Patrol()
    {
        if (transform.position.x > patrolEndPos.x && isFacingRight)
        {
            isFacingRight = false;
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (transform.position.x < patrolStartPos.x && !isFacingRight)
        {
            isFacingRight = true;
            transform.localScale = new Vector3(1, 1, 1);
        }

        rb.velocity = new Vector2(isFacingRight ? moveSpeed : -moveSpeed, rb.velocity.y);
        anim.PlayerWalkkingEnemy();
    }

    private void MoveTowardsPlayer()
    {
        if (playerHit.point.x < transform.position.x && isFacingRight)
        {
            isFacingRight = false;
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (playerHit.point.x > transform.position.x && !isFacingRight)
        {
            isFacingRight = true;
            transform.localScale = new Vector3(1, 1, 1);
        }

        rb.velocity = new Vector2(isFacingRight ? moveSpeed : -moveSpeed, rb.velocity.y);
        anim.PlayerWalkkingEnemy();
    }

    private void Attack()
    {
        anim.PlayerAttackEnemy();
        timeSinceAttack = 0f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Player hit by attack, deal damage
            // Code here to deal damage to player
        }
    }
}
