using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastlesManager : MonoBehaviour
{
    [SerializeField] private ParticleSystem enemyParticle;
    [SerializeField] private ParticleSystem playerParticle;
    private int _enemyPower;
    private int _playerPower;


    public void AddPower(bool isPlayer, int addPower)
    {
        if (isPlayer)
            _playerPower += addPower;
        else
            _enemyPower += addPower;
    }

    private void ChangePower()
    {
        //maximum power speed 7 orta nokta
        
    }
}