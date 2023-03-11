using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DistanceFightingController : MonoBehaviour
{
    [SerializeField] private List<GameObject> _arrowPrefabObjects;
    [SerializeField] private Transform _arrowSpawnPoint;
    [SerializeField] private Transform _playerSpownPointFlip;
    [SerializeField] private AnimatorPlayerEvent _magicEvent;
    [SerializeField] private PlayerStatusController _playerStatusController;
    [SerializeField] private float _curveShoot = 0.2f;
    [SerializeField] private float _curveHighShoot = 0.2f;
    public Action<EnemyController, Arrow> OnplayerShotArrowByAnimation;

    public PlayerStatusController PlayerStatusController { get => _playerStatusController; set => _playerStatusController = value; }

    private void OnEnable()
    {
        _magicEvent.OnplayerShotArrowByAnimation += LaunchShoot;
    }

    public void LaunchShoot(AnimatorEnum animatorEnum)
    {
        if(animatorEnum.Equals(AnimatorEnum.HighShot))
        {
            HighShot();
        }        
        if(animatorEnum.Equals(AnimatorEnum.Shooting))
        {
            ShootArrow();
        }
    }

    public void ShootArrow()
    {
        // Cria uma instância do game object da flecha na posição atual do objeto e com sua rotação
        GameObject arrowInstance = Instantiate(GetArrowObjectByType(), _arrowSpawnPoint.position, _arrowSpawnPoint.rotation);
        // Obtém a referência para a classe Arrow do game object instanciado
        Arrow arrowScript = arrowInstance.GetComponent<Arrow>();
        arrowScript.OnArrowCollision += ArrowCollision;

        Rigidbody2D rb = arrowScript.GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Dynamic;

        // Aplica uma força inicial para disparar a flecha
        Vector2 direction = new Vector2(_playerSpownPointFlip.localScale.x, _curveShoot);
        rb.AddForce(direction * arrowScript.Speed, ForceMode2D.Impulse);
        //rb.AddForce(transform.up, ForceMode2D.Impulse);

    }
    public void HighShot()
    {
        // Cria uma instância do game object da flecha na posição atual do objeto e com sua rotação
        GameObject arrowInstance = Instantiate(GetArrowObjectByType(), _arrowSpawnPoint.position, _arrowSpawnPoint.rotation);
        // Obtém a referência para a classe Arrow do game object instanciado
        Arrow arrowScript = arrowInstance.GetComponent<Arrow>();
        arrowScript.OnArrowCollision += ArrowCollision;

        Rigidbody2D rb = arrowScript.GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Dynamic;

        // Aplica uma força inicial para disparar a flecha
        Vector2 direction = new Vector2(_playerSpownPointFlip.localScale.x, _curveHighShoot);
        rb.AddForce(direction * arrowScript.Speed, ForceMode2D.Impulse);
        rb.AddForce(transform.up, ForceMode2D.Impulse);

    }
    public void ArrowCollision(Collider2D enemy, Arrow _arrowType)
    {
        OnplayerShotArrowByAnimation?.Invoke(enemy.gameObject.GetComponent<EnemyController>(), _arrowType);
    }


        // Método que recebe um tipo de flecha (ArrowType) e retorna um GameObject com esse tipo de flecha
    public GameObject GetArrowObjectByType()
    {
        ArrowType arrowType = _playerStatusController.EquipedArrow.ArrowType;
        // Usa o método FirstOrDefault() da classe Enumerable para encontrar o primeiro GameObject com o tipo de flecha correspondente
        GameObject arrowObject = _arrowPrefabObjects.FirstOrDefault(obj =>
        {
            Arrow arrowComponent = obj.GetComponent<Arrow>();
            return arrowComponent.ArrowType == arrowType;
        });

        // Retorna o GameObject encontrado (ou null se nenhum foi encontrado)
        return arrowObject;
    }


}
