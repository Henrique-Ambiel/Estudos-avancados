using UnityEngine;

public abstract class IPersistentSingleton<T> : MonoBehaviour where T : MonoBehaviour //Classe gen�rica que herda de MonoBehaviour
{
    private static T _uniqueInstance = null; //Inst�ncia �nica da classe

#if UNITY_EDITOR
    private static bool _isQuitting = false;
#endif

    //M�todo que procura o primeiro objeto do tipo T
    public static T Instance
    {
        get
        {
#if UNITY_EDITOR
            if (_isQuitting)
            {
                Debug.LogWarning("[" + typeof(T).Name + "] A aplica��o est� fechando, retornando nulo");
                return null;
            }
#endif
            //Verifica se a inst�ncia � nula
            if (_uniqueInstance == null)
            {
                _uniqueInstance = FindFirstObjectByType<T>();
                if (_uniqueInstance == null) //Se n�o encontrou uma inst�nica, cria um novo objeto 
                {
                    GameObject singletonPrefab = Resources.Load<GameObject>(typeof(T).Name);
                    if (singletonPrefab != null) //Se encontrou o prefab, instancia o objeto
                    {
                        GameObject singletonObject = Instantiate<GameObject>(singletonPrefab);
                        if (singletonObject != null) //Se conseguiu instanciar o objeto, atribui a inst�ncia
                        {
                            _uniqueInstance = singletonObject.GetComponent<T>();
                        }
                    }
                    if (_uniqueInstance == null) //Se n�o conseguiu instanciar o objeto, cria um novo objeto
                    {
                        _uniqueInstance = new GameObject(typeof(T).Name).AddComponent<T>();
                    }
                }
            }

            return _uniqueInstance; //Retorna a inst�ncia
        }
        private set //Atribui um valor a inst�ncia
        {
            if (_uniqueInstance == null) //Se a inst�ncia � nula, atribui o valor, se n�o, destroi o objeto
            {
                _uniqueInstance = value;
                DontDestroyOnLoad(_uniqueInstance.gameObject);
            }
            else if (_uniqueInstance != value)
            {
#if UNITY_EDITOR //Se estiver no editor, exibe um erro
                Debug.LogError("[" + typeof(T).Name + "] Tentou criar uma segunda inst�ncia");
#endif
                DestroyImmediate(value.gameObject);
            }
        }

    }

    //M�todo que procura o primeiro objeto do tipo T
    protected virtual void Awake()
    {
        Instance = this as T; //Atribui a inst�ncia ao objeto atual
    }


    protected void OnDestroy() //M�todo que destroi a inst�ncia
    {
        if (_uniqueInstance == this)
        {
            _uniqueInstance = null;
        }
    }


    protected void OnApplicationQuit() //M�todo que � chamado quando a aplica��o � fechada
    {
#if UNITY_EDITOR
        _isQuitting = true;
#endif
    }
}