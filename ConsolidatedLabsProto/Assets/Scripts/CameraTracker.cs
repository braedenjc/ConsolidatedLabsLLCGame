using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class CameraTracker : MonoBehaviour
{
    public GameObject vehicle;
    [Range(0f, 10f)]
    public float distanceBehindVehicle = -7f;
    
    void Update(){
        transform.position = new Vector3(transform.position.x, transform.position.y, vehicle.transform.position.z - distanceBehindVehicle);
    }
}
