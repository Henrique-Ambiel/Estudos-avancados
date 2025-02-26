using UnityEngine;

public class GameManager : IPersistentSingleton<GameManager> //Classe que herda de IPersistentSingleton
{
    //Imprime no console a mesnagem caso o singleton funcione
    public void Print()
    {
        Debug.Log("Singleton Funciona!");
    }
}
