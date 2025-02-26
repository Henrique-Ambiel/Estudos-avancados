using UnityEngine;

public abstract class IPersistentSingleton<T> : MonoBehaviour where T : MonoBehaviour //Classe genérica que herda de MonoBehaviour
{
    private static T _uniqueInstance = null; //Instância única da classe

#if UNITY_EDITOR
    private static bool _isQuitting = false;
#endif

    //Método que procura o primeiro objeto do tipo T
    public static T Instance
    {
        get
        {
#if UNITY_EDITOR
            if (_isQuitting)
            {
                Debug.LogWarning("[" + typeof(T).Name + "] A aplicação está fechando, retornando nulo");
                return null;
            }
#endif
            //Verifica se a instância é nula
            if (_uniqueInstance == null)
            {
                _uniqueInstance = FindFirstObjectByType<T>();
                if (_uniqueInstance == null) //Se não encontrou uma instânica, cria um novo objeto 
                {
                    GameObject singletonPrefab = Resources.Load<GameObject>(typeof(T).Name);
                    if (singletonPrefab != null) //Se encontrou o prefab, instancia o objeto
                    {
                        GameObject singletonObject = Instantiate<GameObject>(singletonPrefab);
                        if (singletonObject != null) //Se conseguiu instanciar o objeto, atribui a instância
                        {
                            _uniqueInstance = singletonObject.GetComponent<T>();
                        }
                    }
                    if (_uniqueInstance == null) //Se não conseguiu instanciar o objeto, cria um novo objeto
                    {
                        _uniqueInstance = new GameObject(typeof(T).Name).AddComponent<T>();
                    }
                }
            }

            return _uniqueInstance; //Retorna a instância
        }
        private set //Atribui um valor a instância
        {
            if (_uniqueInstance == null) //Se a instância é nula, atribui o valor, se não, destroi o objeto
            {
                _uniqueInstance = value;
                DontDestroyOnLoad(_uniqueInstance.gameObject);
            }
            else if (_uniqueInstance != value)
            {
#if UNITY_EDITOR //Se estiver no editor, exibe um erro
                Debug.LogError("[" + typeof(T).Name + "] Tentou criar uma segunda instância");
#endif
                DestroyImmediate(value.gameObject);
            }
        }

    }

    //Método que procura o primeiro objeto do tipo T
    protected virtual void Awake()
    {
        Instance = this as T; //Atribui a instância ao objeto atual
    }


    protected void OnDestroy() //Método que destroi a instância
    {
        if (_uniqueInstance == this)
        {
            _uniqueInstance = null;
        }
    }


    protected void OnApplicationQuit() //Método que é chamado quando a aplicação é fechada
    {
#if UNITY_EDITOR
        _isQuitting = true;
#endif
    }
}