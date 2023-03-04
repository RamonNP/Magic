using UnityEngine;

public class FamiliarsController : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float distanceFromPlayer = 5f;
    [SerializeField] private float yOffset = 2f;
    [SerializeField] private float teleportDistance = 7f;
    [SerializeField] private float attackRange = 50f;

    private Vector3 lastPlayerPosition;
    private Vector3 smoothVelocity;
    [SerializeField] private bool attacking = false;
    private void Start()
    {
        //Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    private void Update()
    {
        FollowPlayer();
        //AtakingEnemyInRaycast();
    }

    private void AtakingEnemyInRaycast()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(playerTransform.position, attackRange);

        // Desenhar o círculo de colisão na posição atual do familiar
        Debug.DrawRay(playerTransform.position, Vector2.up * attackRange, Color.red);
        if (!attacking)
        {

            foreach (Collider2D collider in colliders)
            {
                if (collider.gameObject.CompareTag("Enemy"))
                {
                    Debug.Log(collider);
                    Attack(collider.transform);
                    break;
                }
            }
        }
    }

    private void FollowPlayer()
    {
        // verificar se o jogador foi setado
        if (playerTransform == null)
        {
            return;
        }

        // calcular a posição de destino do familiar
        Vector3 targetPosition = playerTransform.position - playerTransform.right * distanceFromPlayer;

        // verificar a direção do jogador
        if (playerTransform.transform.localScale.x < 0)
        {

            targetPosition -= -playerTransform.right * 2f * distanceFromPlayer;
            targetPosition.y = playerTransform.position.y + yOffset;
            // o jogador está se movendo para a direita, então o familiar deve ficar atrás dele
            //targetPosition += playerTransform.right * 2f * distanceFromPlayer;
        }
        else
        {
            targetPosition = playerTransform.position - playerTransform.right * distanceFromPlayer;
            targetPosition.y = playerTransform.position.y + yOffset;
        }

        // mover o familiar em direção ao jogador com suavidade
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref smoothVelocity, 0.1f, speed);

        // verificar se o familiar precisa mudar de direção
        if (transform.position.x > playerTransform.position.x && isFacingRight)
        {
            // virar para a esquerda
            Flip();
        }
        else if (transform.position.x < playerTransform.position.x && !isFacingRight)
        {
            // virar para a direita
            Flip();
        }
        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);
        if (distanceToPlayer > teleportDistance)
        {
            transform.position = targetPosition;
        }

        // atualizar a posição anterior do jogador
        lastPlayerPosition = playerTransform.position;
    }

    private bool isFacingRight = true;

    private void Flip()
    {
        // mudar a direção do familiar
        isFacingRight = !isFacingRight;

        // girar o familiar em 180 graus
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    private void Attack(Transform target)
    {
        //attacking = true;
        //animator.SetTrigger("Attack");

        Vector2 targetPosition = new Vector2(target.position.x, target.position.y + 2f);
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        if (transform.position.x > target.position.x && isFacingRight)
        {
            Flip();
        }
        else if (transform.position.x < target.position.x && !isFacingRight)
        {
            Flip();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

}
