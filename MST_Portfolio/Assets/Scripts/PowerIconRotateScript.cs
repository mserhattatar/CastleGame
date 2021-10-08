using UnityEngine;
using UnityEngine.Profiling;

public class PowerIconRotateScript : MonoBehaviour
{
    private void FixedUpdate()
    {
       transform.Rotate(Vector3.up, Time.deltaTime * 100);
    }
}
