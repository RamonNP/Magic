using UnityEngine;

public class ConfigManager : MonoBehaviour
{
    private static ConfigManager instance;

    // Defina aqui as configura��es que deseja armazenar
    public float MediatingPowerOfItems = 3.0f;
    public bool enableMusic = true;
    public bool enableSFX = true;
    

    private void Awake()
    {
        // Verifica se a inst�ncia j� existe, e destr�i a nova inst�ncia criada caso j� exista uma
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // Configura a inst�ncia como sendo esta classe, e faz com que ela n�o seja destru�da ao trocar de cena
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Retorna a inst�ncia da classe ConfigManager, criando uma caso ela ainda n�o exista
    public static ConfigManager GetInstance()
    {
        if (instance == null)
        {
            instance = new GameObject("ConfigManager").AddComponent<ConfigManager>();
        }

        return instance;
    }
}
