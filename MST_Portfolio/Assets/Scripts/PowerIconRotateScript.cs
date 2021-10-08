using UnityEngine;
using UnityEngine.Profiling;

public class PowerIconRotateScript : MonoBehaviour
{
    private void FixedUpdate()
    {
        Profiler.BeginSample("PowerIconRotateScript  FixedUpdate  SERHAT");
        Debug.Log("This code is being profiled");
        transform.Rotate(Vector3.up, Time.deltaTime * 100);
        Profiler.EndSample();
       
    }
}
