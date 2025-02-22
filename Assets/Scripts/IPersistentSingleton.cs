using UnityEngine;

public abstract class IPersistentSingleton<T> : MonoBehaviour where T :MonoBehaviour //Classe genérica que herda de MonoBehaviour
{
   private static T _uniqueInstance = null; //Instância única da classe
   
   //Método que procura o primeiro objeto do tipo T
   public static T Instance 
   {
        get 
        {
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
                        if(singletonObject != null) //Se conseguiu instanciar o objeto, atribui a instância
                        {
                            _uniqueInstance = singletonObject.GetComponent<T>();
                        }
                    }
                    if(_uniqueInstance == null) //Se não conseguiu instanciar o objeto, cria um novo objeto
                    {
                        _uniqueInstance = new GameObject(typeof(T).Name).AddComponent<T>();
                    }
                }
            }

            return _uniqueInstance; //Retorna a instância
        }
        private set //Atribui um valor a instância
        {
            _uniqueInstance = value;
        }

    }
}