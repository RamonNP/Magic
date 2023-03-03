using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolAI2 : MonoBehaviour
{
    public Transform enemyTransform;
    public Transform groundCheck;
    public Transform playerCheck;
    public float moveSpeed = 2f;
    public float raycastDistance = 0.5f;
    public float attackRange = 0.5f;

    private Rigidbody2D enemyRb;
    [SerializeField] private Animator enemyAnimator;
    private Vector2 currentDirection = Vector2.right;
    private Vector2[] directions = { Vector2.left, Vector2.right };
    private bool isAttacking = false;
    private bool isCoroutineRunning = false;
    [SerializeField]
    private Transform alvo;
    [SerializeField]
    private float velocidadeMovimento;
    [SerializeField]
    private Rigidbody2D rigidbodyy;




    void Start()
    {
        enemyRb = transform.GetComponent<Rigidbody2D>();
    }
    
    void FixedUpdate()
    {
        Vector2 posicaoAlvo = this.alvo.position;
        Vector2 posicaoAtual = this.transform.position;
        Vector2 direcao = posicaoAlvo = posicaoAtual;
        direcao = direcao.normalized;
        this.rigidbodyy.velocity = (this.velocidadeMovimento * direcao);
        Vector2 targetPosition = new Vector2(enemyTransform.position.x, enemyRb.position.y);

        if (!isAttacking)
        {
            RaycastHit2D groundHit = Physics2D.Raycast(groundCheck.position, currentDirection, raycastDistance);
            Debug.DrawRay(groundCheck.position, currentDirection * raycastDistance, Color.green);

            if (!groundHit.collider)
            {
                FlipDirection();
            }

            RaycastHit2D playerHit = Physics2D.Raycast(playerCheck.position, currentDirection, attackRange);
            Debug.DrawRay(playerCheck.position, currentDirection * attackRange, Color.red);

            if (playerHit.collider)
            {
                enemyAnimator.Play("Attacking");
                isAttacking = true;

            }

            else
            {
                enemyRb.velocity = currentDirection * moveSpeed;

                if (Mathf.Abs(enemyRb.position.x - targetPosition.x) < 0.1f)
                {
                    FlipDirection();
                }
            }
        }
        else //estou atacando 
        {
            //Debug.Log("estou atacando");
            enemyRb.velocity = Vector2.zero;
            if (!isCoroutineRunning)
            {
                StartCoroutine(EndAttackIEnumerator());
            }


        }
        
    }
    IEnumerator EndAttackIEnumerator()
    {
        isCoroutineRunning = true;
        yield return new WaitForSeconds(3f);
        isAttacking = false;
        isCoroutineRunning = false;
    }


    void FlipDirection()
    {
        currentDirection = currentDirection == Vector2.left ? Vector2.right : Vector2.left;
        enemyTransform.localScale = new Vector3(currentDirection.x, 1, 1);
    }
    void EndAttack()
    {
        isAttacking = false;
    }
}
