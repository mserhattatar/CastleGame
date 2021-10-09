using System;
using UnityEngine;

public class CastlesManager : MonoBehaviour
{
    private ComponentContainer myComponent;

    private SpawnManager _spawnManager;
    private GameManager _gameManager;

    [SerializeField] private ParticleSystem enemyParticle;
    [SerializeField] private ParticleSystem enemyAddPowerIconParticle;
    [SerializeField] private ParticleSystem playerParticle;
    [SerializeField] private ParticleSystem playerAddPowerIconParticle;

    private const float MaxPower = 50;
    private float _enemyPower;
    private float _playerPower;
    public int levelPower;

    public void Initialize(ComponentContainer componentContainer)
    {
        myComponent = componentContainer;
    }

    private void Start()
    {
        _spawnManager = myComponent.GetComponent("SpawnManager") as SpawnManager;
        _gameManager = myComponent.GetComponent("GameManager") as GameManager;
        
        GameManager.ReloadLevelHandler += ReloadCastlesManager;
    }

    public void AddPower(bool isPlayer, float addPower, bool checkHit = true)
    {
        if (isPlayer)
        {
            _playerPower += addPower;
            if (checkHit)
                playerAddPowerIconParticle.Play();
            var playerParticleMain = playerParticle.main;
            playerParticleMain.startSpeed = MathPower(isPlayer);
        }
        else
        {
            _enemyPower += addPower;
            if (checkHit)
                enemyAddPowerIconParticle.Play();
            var enemyParticleMain = enemyParticle.main;
            enemyParticleMain.startSpeed = MathPower(isPlayer);
        }

        if (checkHit && MathPower(!isPlayer) + MathPower(isPlayer) >= MaxPower)
        {
            var diff = ((MathPower(!isPlayer) + MathPower(isPlayer)) - MaxPower);
            AddPower(!isPlayer, -diff, false);
        }

        _spawnManager.GeneratePowerIcon((int)addPower);
        IsThereAWinner();
    }

    private float MathPower(bool isPlayer)
    {
        float power;
        if (isPlayer)
            power = _playerPower / levelPower;
        else
            power = _enemyPower / levelPower;

        if (power > MaxPower)
            power = MaxPower;

        return power;
    }


    private void IsThereAWinner()
    {
        if (_enemyPower / levelPower >= MaxPower)
        {
            Debug.LogError("Enemy Kazandı");
        }
        else if (_playerPower / levelPower >= MaxPower)
        {
            Debug.LogWarning("player Kazandı");
        }
    }

    private void ReloadCastlesManager()
    {
        _enemyPower = 0;
        _playerPower = 0;
        levelPower = _gameManager.GetLevelNumber();
        AddPower(true, _playerPower, false);
        AddPower(false, _enemyPower, false);
    }
}