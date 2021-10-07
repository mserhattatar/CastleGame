using UnityEngine;
using UnityEngine.Serialization;

public class CastleScript : MonoBehaviour
{
    private SpawnManager _spawnManager;
    private CastlesManager _castlesManager;
    [SerializeField] private bool isPlayer;
    [FormerlySerializedAs("addGemParticleSystem")] [SerializeField] private ParticleSystem addPowerParticleSystem;

    private void Start()
    {
        _spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
        _castlesManager = GameObject.Find("Castles Manager").GetComponent<CastlesManager>();
    }

    public void SentToCastle(int bagCount)
    {
        _spawnManager.GeneratePowerIcon(bagCount);
        _castlesManager.AddPower(isPlayer, bagCount);
        addPowerParticleSystem.Play();
    }
}