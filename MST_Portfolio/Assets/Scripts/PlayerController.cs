using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody _playerRb;
    [SerializeField] private float speed;
    [SerializeField] private float turnSpeed;

    public int playerBag;

    private void Start()
    {
        _playerRb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        float forwardInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");
        
        transform.Translate(Vector3.forward * Time.deltaTime * speed * forwardInput);
        transform.Rotate(Vector3.up, Time.deltaTime * turnSpeed * horizontalInput);
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Gem"))
        {
            other.gameObject.SetActive(false);
            playerBag++;
        }
    }

    private void OnCollisionStay(Collision other)
    {
        if (playerBag > 0 && other.gameObject.CompareTag("PlayerCastle"))
        {
            other.gameObject.GetComponent<CastleScript>().SentToCastle(playerBag);
            playerBag = 0;
        }
    }
}