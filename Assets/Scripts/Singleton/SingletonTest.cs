using UnityEngine;

public class SingletonTest : MonoBehaviour
{
    // Método que inicia o singleton
    void Start()
    {
        GameManager.Instance.Print();
    }
}
