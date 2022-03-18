using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class LevelMovement : MonoBehaviour
{
    [Range(0, 30f)]
    public float scrollSpeed = 10f;
    public float stopMovement = 1;
    public GameObject level;

    void Update()
    {
        level.transform.position = new Vector3(level.transform.position.x, 
                                                level.transform.position.y, 
                                                level.transform.position.z + scrollSpeed * Time.deltaTime * -1 * stopMovement
                                            );
        #if UNITY_EDITOR
        Debug.Log("Z is at: " + level.transform.position.z);
        #endif
    }
}
