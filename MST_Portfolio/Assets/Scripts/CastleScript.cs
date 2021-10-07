using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleScript : MonoBehaviour
{
    private int _castleGemCount;
    private SpawnManager _spawnManager;

    private void Start()
    {
        _spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
    }

    public void SentToCastle(int bagCount)
    {
        _castleGemCount += bagCount;
        _spawnManager.GenerateGem(bagCount);
    }
}