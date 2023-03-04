using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldMagic : MonoBehaviour
{
    public float duration = 5f;
    public float force = 500f;
    [SerializeField] private Transform _playerSkin;
    [SerializeField] private Color _colorShield;
    [SerializeField] private Sprite _sprite;

    private void Start()
    {

    }

    public void ActivateShield()
    {
        // Criar o objeto de escudo
        GameObject shield = new GameObject("Shield");
        SpriteRenderer shieldRenderer = shield.AddComponent<SpriteRenderer>();
        shieldRenderer.color = _colorShield;
        shieldRenderer.sprite = _sprite;

        // Colocar o escudo na frente do personagem
        shield.transform.position = transform.position + transform.right * 0.5f;

        // Definir a escala do escudo
        shield.transform.localScale = new Vector3(0.5f, 0.5f, 1f);

        // Destruir o escudo após a duração especificada
        Destroy(shield, duration);

        // Aplicar força no personagem na direção oposta
        //Rigidbody2D rb = _playerSkin.GetComponent<Rigidbody2D>();
        //rb.AddForce(-transform.right * force);
    }
}
