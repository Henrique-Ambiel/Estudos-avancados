using UnityEngine;

public class SingletonTest : MonoBehaviour
{
    // M�todo que inicia o singleton
    void Start()
    {
        GameManager.Instance.Print();
    }
}
