using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private const float LOWESTMOVEMENTRATE = 10f;
    private const float UPPERSTRAFERATE = 100f;
    [Header("Movement Rate Modifiers")]
    [Range(LOWESTMOVEMENTRATE, UPPERSTRAFERATE)]
    [Min(LOWESTMOVEMENTRATE)]
    public float strafeRate = LOWESTMOVEMENTRATE;
    [Min(LOWESTMOVEMENTRATE)]
    public float speed = 10f;

    
    [SerializeField]
    private Canvas gameOverCanvas;
    [SerializeField]
    private Rigidbody rb;
    
    [Space(10f)]
    [Header("Debug Flags")]
    [SerializeField]
    private bool movePlayer = false;

    void FixedUpdate()
    {
        //Basic movement code for moving left and right.
        Vector3 strafeVector = transform.position + (Vector3.right * Time.deltaTime * strafeRate * Input.GetAxis("Horizontal"));
        Vector3 forwardVector = transform.position + (Vector3.forward * speed * Time.deltaTime);
        Vector3 vectorCombination = new Vector3(strafeVector.x, transform.position.y, forwardVector.z);

        if(movePlayer){
            rb.MovePosition(vectorCombination);
        }
    }

    void OnCollisionEnter(Collision collision){
        bool tagIsObstacle = collision.gameObject.tag == "Obstacle";
        bool tagIsKillfield = collision.gameObject.tag == "killfield";
        if(tagIsObstacle || tagIsKillfield){
            movePlayer = false;
            gameOverCanvas.gameObject.SetActive(true);
            Destroy(this.gameObject);
        }

    }
}
