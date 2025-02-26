using UnityEngine;

public static class BootStrapper
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)] //Esse método é chamado antes de qualquer cena ser carregada
    static void onBeforeSceneLoad()
    {
        GameManager gameManager = GameManager.Instance;
    }
}