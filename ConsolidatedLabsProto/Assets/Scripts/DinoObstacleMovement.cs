using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
    using UnityEditor;
#endif

[ExecuteAlways]
public class DinoObstacleMovement : MonoBehaviour
{
    public enum MovementModes{
        HORIZONTAL,
        TOWARDPLAYER,
        CHASEPLAYER,
        PAUSED,
        DISCO
    }

    [SerializeField]
    private Rigidbody rb;
    [Header("Endpoints")]
    public float xLeftLimit = 0f;
    public float xRightLimit = 0f;
    
    [Space(10)]
    [Header("Movement")]
    public float speed = 0f;
    public MovementModes currentMovement;
    [SerializeField]
    private bool iAmMovingLeft = true;
    private float xStart;

    void Start(){
        xStart = this.transform.position.x; //record this dino's starting position for later comparisons.
    }

    void Update(){
        if(currentMovement == MovementModes.HORIZONTAL){
            MoveDinoHorizontal();
        }
        else if (currentMovement == MovementModes.TOWARDPLAYER){
            MoveDinoTowardPlayer();
        }
        else if (currentMovement == MovementModes.CHASEPLAYER){
            ChasePlayer();
        }
        else if (currentMovement == MovementModes.DISCO){
            DinoDisco();
        }
    }

    void DinoDisco(){
        float yEulerAngle = (1 - (transform.rotation.y) * (Time.deltaTime * speed));
        Vector3 angle = new Vector3(transform.eulerAngles.x, yEulerAngle, transform.eulerAngles.z);
        transform.eulerAngles += angle;
        
    }

    void MoveDinoTowardPlayer(){
        Quaternion dinoRotationTowardPlayer = Quaternion.Euler(0, -180,0);
        this.transform.rotation = dinoRotationTowardPlayer;
        Vector3 forwardMotion = new Vector3(
            transform.position.x,
            transform.position.y,
            transform.position.z - (speed * Time.deltaTime)
        );
        rb.MovePosition(forwardMotion);
    }

    void MoveDinoHorizontal(){
        int moveLeftModifier;

        //set a modifier based on which direction we need to go. This will also determine which way we face as we move.
        if(iAmMovingLeft){
            moveLeftModifier = -1;
        }
        else{
            moveLeftModifier = 1;
        }
       
        Vector3 forwardMotion = new Vector3(
            transform.position.x + (speed * Time.deltaTime * moveLeftModifier),
            transform.position.y,
            transform.position.z 
        );

        Quaternion dinoRotation = Quaternion.Euler(0, 90 * moveLeftModifier, 0);
        transform.rotation = dinoRotation;
        rb.MovePosition(forwardMotion);
        
        //check to see if we are going to start moving a different direction on the next frame.
        if(transform.position.x <= xStart - xLeftLimit){
            iAmMovingLeft = false;
        }
        else if (transform.position.x >= xStart + xRightLimit){
            iAmMovingLeft = true;
        }
    }

    void ChasePlayer(){
        Vector3 forwardMotion = new Vector3(
            transform.position.x,
            transform.position.y,
            transform.position.z + (speed * Time.deltaTime)
        );

        Quaternion dinoRotation = Quaternion.Euler(0, 0, 0);
        transform.rotation = dinoRotation;
        rb.MovePosition(forwardMotion);
    }

    
    #region Editor only functions
    #if UNITY_EDITOR
        void DrawLeftEndpointPath(){
            DrawPathLine(this.transform.position, xLeftLimit, Color.red);
        }

        void DrawRightEndpointPath(){
            DrawPathLine(this.transform.position, xRightLimit, Color.magenta);
        }

        void DrawPathLine(Vector3 from, float distanceFromCenter, Color color){
            Vector3 endpoint = new Vector3
            (
                xStart  + distanceFromCenter, 
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
    #endregion

}
