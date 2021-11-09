using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombScript : MonoBehaviour
{
    [SerializeField] private GameObject bombParticleEffect;
   public void SetVisibility(bool setActive)
    {
        gameObject.SetActive(setActive);
        if (!setActive)
        {

        }
    }
}
