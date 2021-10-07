using UnityEngine;

public class PowerIconRotateScript : MonoBehaviour
{
    private void LateUpdate()
    {
        transform.Rotate(Vector3.up, Time.deltaTime * 100);
    }
}
