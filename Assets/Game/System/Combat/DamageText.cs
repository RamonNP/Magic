using TMPro;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMesh;
    [SerializeField] private float duration = 1.0f;
    [SerializeField] private float verticalSpeedMultiplier = 1.0f;
    [SerializeField] private float speed = 1.0f;
    [SerializeField] private bool enable = false;
    [SerializeField] private Vector3 offset = new Vector3(0, 1, 0);
    [SerializeField] private Color _textColor;

    //private Transform _folowPosition;
    private float startTime;
    private Vector3 startPosition;
    private void Start()
    {
        // Define a cor da sombra como preto
        textMesh.faceColor = Color.white;
        textMesh.outlineWidth = 1f;
        textMesh.outlineColor = Color.black;
    }

    public void ShowDamageText(int damageAmount, Transform tranformPosition)
    {
        //_folowPosition = tranformPosition;
        textMesh.text = "-" + damageAmount.ToString();
        enable = true;
        startTime = Time.time;
        startPosition = tranformPosition.position;
        startPosition += new Vector3(0,3,0);
        Destroy(gameObject, duration);
    }

    void Update()
    {
        if (enable)
        {
            float timeElapsed = Time.time - startTime;
            float t = Mathf.Clamp01(timeElapsed / duration);

            Vector3 position = transform.position;
            position = startPosition + offset * t;
            position.y += speed * t * verticalSpeedMultiplier;

            // adiciona a posição do player ao texto
            //position += _folowPosition.position;

            // converte a posição 3D em 2D para exibir corretamente na tela
            Vector2 screenPosition = Camera.main.WorldToScreenPoint(position);


            // atualiza a posição do objeto na tela
            transform.position = position;
            textMesh.color = _textColor;
        }
    }
}
