using UnityEngine;

public class CastlesManager : MonoBehaviour
{
    [SerializeField] private ParticleSystem enemyParticle;
    [SerializeField] private ParticleSystem playerParticle;
    private float _enemyPower;
    private float _playerPower;
    private const float MaxPower = 26;
    public int levelPower;


    public void AddPower(bool isPlayer, int addPower)
    {
        if (isPlayer)
            _playerPower += addPower;
        else
            _enemyPower += addPower;
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