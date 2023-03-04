using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIaController : MonoBehaviour
{
    public float speed;
    public float distance;
    public Transform PlayerTransform;
    public bool ground = true;
    public LayerMask groundLayer;
    public bool facinRight = true;
    public bool attack = true;
    bool isRight = true;
    public Transform playerCheck;
    public Transform groundCheck;
    private bool isAttack;
    public Transform enemyFlip;
    [SerializeField] private EnemyAnimationController _enemyAnimationController;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        EnemyAttackPlayer();
        if (!isAttack)
        {
            EnemyPatrulha();
        }
    }

    private void EnemyPatrulha()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        RaycastHit2D ground = Physics2D.Raycast(groundCheck.position, Vector2.down, distance);
        //ground = Physics2D.Linecast(groundCheck.position, transform.position, groundLayer);
        if (ground.collider == false)
        {
            if (isRight == true)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                isRight = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
                isRight = true;

            }
        }
        if (ground == false)
        {
            speed *= 1;
        }

        if (speed > 0 && facinRight)
        {
            Flip();
        }
        else if (speed < 0 && facinRight)
        {
            Flip();
        }
        void Flip()
        {
            facinRight = !facinRight;
            Vector3 Scale = enemyFlip.localScale;
            Scale.x *= -1;
            enemyFlip.localScale = Scale;
        }
    }
    private void EnemyAttackPlayer()
    {
        // Execute um Raycast a partir da posição do inimigo em direção ao jogador
        //RaycastHit2D hit = Physics2D.Raycast(transform.position, playerCheck.position - transform.position, 10, LayerMask.GetMask("Player"));
        RaycastHit2D hit = Physics2D.Raycast(transform.position, playerCheck.position - transform.position, 10);
        Debug.DrawRay(transform.position, playerCheck.position - transform.position, Color.red);

        Debug.Log("Hit " + hit.collider.gameObject.name + hit.collider.tag);
        // Se o Raycast atingir o jogador, chame o método PlayerAttackEnemy() do EnemyAnimationController
        if (hit.collider != null && hit.collider.CompareTag("Player"))
        {
            isAttack = true;
            _enemyAnimationController.PlayerAttackEnemy();
        } else
        {
            isAttack = false;
        }
        
    }
}
