using UnityEngine;
using TMPro;

public class EnemyNameLabelController : MonoBehaviour
{
    [SerializeField] private Transform enemyTransform; // Transform do inimigo que o label seguir�
    [SerializeField] private TextMeshProUGUI nameLabel; // Componente TextMeshProUGUI que exibir� o nome e a vida do inimigo
    [SerializeField] private float _gapLabelY; 
    private Camera mainCamera;

    private void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
    }

    private void LateUpdate()
    {
        if (enemyTransform != null)
        {
            // Atualiza a posi��o do label para seguir o inimigo
            //transform.position = enemyTransform.position + new Vector3(0, 1.5f, 0);
            Vector3 screenPosition = mainCamera.WorldToScreenPoint(enemyTransform.position+ new Vector3(0, _gapLabelY, 0));
            transform.position = screenPosition;

            //Debug.Log(enemyTransform.position + new Vector3(0, _gapLabelY, 0));
            // Atualiza o texto do label com o nome e a vida do inimigo
            EnemyStatus enemy = enemyTransform.GetComponent<EnemyStatus>();
            nameLabel.text = enemy.Enemy + " HP: " + enemy.Attribute.LifeCurrent + "/" + enemy.Attribute.LifeMax;
        }
        else
        {
            // Se o inimigo for destru�do, destr�i tamb�m o label
            Destroy(gameObject);
        }
    }
}
