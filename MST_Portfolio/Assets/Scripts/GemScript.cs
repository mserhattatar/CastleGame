using UnityEngine;

public class GemScript : MonoBehaviour
{
    private void LateUpdate()
    {
        transform.Rotate(Vector3.up, Time.deltaTime * 100);
    }
}
