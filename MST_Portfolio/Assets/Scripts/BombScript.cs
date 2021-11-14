using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombScript : MonoBehaviour
{
    [SerializeField] private ParticleSystem bombParticleEffect;
   public void SetVisibility(bool setActive)
    {
        if (!setActive)
        {
            Instantiate(bombParticleEffect, transform.position + (Vector3.up * 0.7f) , Quaternion.Euler(0,0,0));
        }
        gameObject.SetActive(setActive);       
    }
}
