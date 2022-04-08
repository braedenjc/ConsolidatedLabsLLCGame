using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private const float LOWESTMOVEMENTRATE = 10f;
    private const float UPPERSTRAFERATE = 100f;
    private const float MINROTATIONLIMIT = 10f;
    private const float MINROTATIONRATE = 10f;
    [Header("Movement Rate Modifiers")]
    [Range(LOWESTMOVEMENTRATE, UPPERSTRAFERATE)]
    [Min(LOWESTMOVEMENTRATE)]
    public float strafeRate = LOWESTMOVEMENTRATE;
    [Min(LOWESTMOVEMENTRATE)]
    public float speed = 10f;
    [Min(MINROTATIONRATE)]
    public float rotationRate = MINROTATIONRATE;

    [Min(MINROTATIONLIMIT)]
    public float rotationLimit = MINROTATIONLIMIT;

    [SerializeField]
    private Canvas gameOverCanvas;
    [SerializeField]
    private Rigidbody rb;
    
    [Space(10f)]
    [Header("Debug Flags")]
    [SerializeField]
    private bool movePlayer = false;
    private bool iAmAirborn = false;

    void FixedUpdate()
    {
        MoveJeep();
        CorrectRotation();
    }

    void MoveJeep(){
        //Basic movement code for moving left and right.
        Vector3 strafeVector = transform.position + (Vector3.right * Time.deltaTime * strafeRate * Input.GetAxis("Horizontal"));
        Vector3 forwardVector = transform.position + (Vector3.forward * speed * Time.deltaTime);
        Vector3 vectorCombination = new Vector3(strafeVector.x, transform.position.y, forwardVector.z);

        if(movePlayer){
            rb.MovePosition(vectorCombination);
        }
    }

    void CorrectRotation(){
        Quaternion currentRotation = transform.rotation;
        #if UNITY_EDITOR
            Debug.Log("Rotation of x is: " + currentRotation.eulerAngles.x);
        #endif
        if(iAmAirborn){
            if(currentRotation.eulerAngles.x > rotationLimit){
                Quaternion correctedRotation = Quaternion.Euler(
                    currentRotation.x - (Time.deltaTime * rotationRate),
                    currentRotation.y,
                    currentRotation.z
                );
                rb.MoveRotation(correctedRotation);
            }
            else if(currentRotation.x < -rotationLimit){
                Quaternion correctedRotation = Quaternion.Euler(
                    currentRotation.x + (Time.deltaTime * rotationRate),
                    currentRotation.y,
                    currentRotation.z
                );
                rb.MoveRotation(correctedRotation);
            }
        }
    }

    void OnCollisionEnter(Collision collision){
        bool tagIsObstacle = collision.gameObject.tag == "Obstacle";
        bool tagIsKillfield = collision.gameObject.tag == "killfield";
        iAmAirborn = false;
        if(tagIsObstacle || tagIsKillfield){
            movePlayer = false;
            gameOverCanvas.gameObject.SetActive(true);
            Destroy(this.gameObject);
        }

    }

    void OnCollisionExit(Collision collision){
        if(collision.contactCount == 0){
            iAmAirborn = true;
        }
    }
}
