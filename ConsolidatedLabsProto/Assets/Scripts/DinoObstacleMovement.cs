using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
    using UnityEditor;
#endif

[ExecuteInEditMode]
public class DinoObstacleMovement : MonoBehaviour
{
    [SerializeField]
    private Rigidbody rb;
    [Header("Endpoints")]
    public float xLeftLimit = 0f;
    public float xRightLimit = 0f;
    
    [Space(10)]
    [Header("Movement")]
    public float speed = 0f;

    void FixedUpdate(){
        
        
    }
    
    #if UNITY_EDITOR
        void DrawLeftEndpointPath(){
            DrawPathLine(this.transform.position, xLeftLimit, Color.red);
        }

        void DrawRightEndpointPath(){
            DrawPathLine(this.transform.position, -xRightLimit, Color.magenta);
        }

        void DrawPathLine(Vector3 from, float distanceFromCenter, Color color){
            Vector3 endpoint = new Vector3
            (
                this.transform.position.x  + distanceFromCenter, 
                this.transform.position.y, 
                this.transform.position.z
            );
            Gizmos.color = color;
            Gizmos.DrawLine(this.transform.position, endpoint);

        }

        void OnDrawGizmosSelected(){
            DrawLeftEndpointPath();
            DrawRightEndpointPath();
        }       
    #endif

}
