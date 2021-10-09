using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public delegate void GameManagerDelegate();

    public static GameManagerDelegate ResetLevel;
    public static GameManagerDelegate NextLevel;

    private ComponentContainer MyComponent;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void Initialize(ComponentContainer componentContainer)
    {
        MyComponent = componentContainer;
    }
}