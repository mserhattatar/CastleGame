using UnityEngine;

public class PowerIconScript : MonoBehaviour
{
    private Transform _characterTransform;
    private bool hasMagnet;

    private void FixedUpdate()
    {
        transform.Rotate(Vector3.up, Time.deltaTime * 100);
        if (hasMagnet)
            transform.position = Vector3.MoveTowards(transform.position, _characterTransform.position + Vector3.up, Time.deltaTime * 6f);
    }

    public void SetVisibility(bool setActive)
    {
        gameObject.SetActive(setActive);

        if (hasMagnet && setActive == false)
            hasMagnet = false;
    }

    public void MagnetPower(Transform characterTransform)
    {
        _characterTransform = characterTransform;
        hasMagnet = true;
    }
}