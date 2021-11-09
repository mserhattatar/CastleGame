using System;
using UnityEngine;
using System.Collections;

public class MagnetPowerScript : MonoBehaviour
{
    private Transform _characterTransform;

    private void Start()
    {
        GameManager.ReloadLevelHandler += ReloadMagnetPowerScript;
    }

    private void FixedUpdate()
    {
        if (gameObject.activeInHierarchy)
        {
            Transform transform1;
            (transform1 = transform).Rotate(Vector3.up, Time.deltaTime * 200f);
            transform1.position = _characterTransform.position;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PowerIcon"))
        {
            other.GetComponent<PowerIconScript>().MagnetPower(_characterTransform);
        }
    }

    public void SetMagnetPowerTransform(Transform characterTransform)
    {
        _characterTransform = characterTransform;
        gameObject.SetActive(true);
        StartCoroutine(MagnetCountDownRoutine());
    }

    private IEnumerator MagnetCountDownRoutine()
    {
        var second = UnityEngine.Random.Range(7, 15);
        yield return new WaitForSeconds(second);
        gameObject.SetActive(false);
    }

    private void ReloadMagnetPowerScript(int levelNumber, int powerIconAmount, int magnetPowerIconAmount)
    {
        gameObject.SetActive(false);
    }
}