using UnityEngine;

public class ConfigManager : MonoBehaviour
{
    private static ConfigManager instance;

    // Defina aqui as configurações que deseja armazenar
    public float MediatingPowerOfItems = 3.0f;
    public bool enableMusic = true;
    public bool enableSFX = true;
    

    private void Awake()
    {
        // Verifica se a instância já existe, e destrói a nova instância criada caso já exista uma
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // Configura a instância como sendo esta classe, e faz com que ela não seja destruída ao trocar de cena
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Retorna a instância da classe ConfigManager, criando uma caso ela ainda não exista
    public static ConfigManager GetInstance()
    {
        if (instance == null)
        {
            instance = new GameObject("ConfigManager").AddComponent<ConfigManager>();
        }

        return instance;
    }
}
