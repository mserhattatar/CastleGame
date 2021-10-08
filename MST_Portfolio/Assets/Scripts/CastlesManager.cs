using System;
using UnityEngine;

public class CastlesManager : MonoBehaviour
{
    private ComponentContainer MyComponent;

    private SpawnManager _spawnManager;
    
    [SerializeField] private ParticleSystem enemyParticle;
    [SerializeField] private ParticleSystem enemyAddPowerIconParticle;
    [SerializeField] private ParticleSystem playerParticle;
    [SerializeField] private ParticleSystem playerAddPowerIconParticle;
    
    private const float MaxPower = 26;
    private float _enemyPower;
    private float _playerPower;
    public int levelPower;

    public void Initialize(ComponentContainer componentContainer)
    {
        MyComponent = componentContainer;
    }
    private void Start()
    {
        _spawnManager = MyComponent.GetComponent("SpawnManager") as SpawnManager;
    }

    public void AddPower(bool isPlayer, int addPower)
    {
        if (isPlayer)
        {
            _playerPower += addPower;
            playerAddPowerIconParticle.Play();
        }
        else
        {
            _enemyPower += addPower;
            enemyAddPowerIconParticle.Play();
        }
        _spawnManager.GeneratePowerIcon(addPower);
           
        ChangePower();
    }

    private void ChangePower()
    {
        var enemyParticleMain = enemyParticle.main;
        enemyParticleMain.startSpeed = _enemyPower / levelPower;

        var playerParticleMain = playerParticle.main;
        playerParticleMain.startSpeed = _playerPower / levelPower;

        IsThereAWinner();
    }

    private void IsThereAWinner()
    {
        if (_enemyPower / levelPower >= MaxPower)
        {
        }
        else if (_playerPower / levelPower >= MaxPower)
        {
        }
    }
}