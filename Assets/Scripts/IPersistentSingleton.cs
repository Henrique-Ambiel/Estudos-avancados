using UnityEngine;

public abstract class IPersistentSingleton<T> : MonoBehaviour where T :MonoBehaviour //Classe gen�rica que herda de MonoBehaviour
{
   private static T _uniqueInstance = null; //Inst�ncia �nica da classe
   
   //M�todo que procura o primeiro objeto do tipo T
   public static T Instance 
   {
        get 
        {
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
                        if(singletonObject != null) //Se conseguiu instanciar o objeto, atribui a inst�ncia
                        {
                            _uniqueInstance = singletonObject.GetComponent<T>();
                        }
                    }
                    if(_uniqueInstance == null) //Se n�o conseguiu instanciar o objeto, cria um novo objeto
                    {
                        _uniqueInstance = new GameObject(typeof(T).Name).AddComponent<T>();
                    }
                }
            }

            return _uniqueInstance; //Retorna a inst�ncia
        }
        private set //Atribui um valor a inst�ncia
        {
            _uniqueInstance = value;
        }

    }
}