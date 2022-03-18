using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Range(20f, 1000f)]
    [Min(20f)]
    public float strafeRate = 20f;
    public float xRotationThreshold = 10f;
    [Min(1f)]
    public float xRotationCorrectionRate = 1f;
    [Min(10f)]
    public float speed = 10f;
    public LevelManager levelManager;
    public Canvas gameOverCanvas;
    public Rigidbody rb;

    [SerializeField]
    [Header("Debug Flags")]
    private bool movePlayer = false;
    
    void FixedUpdate()
    {
        //Basic movement code for moving left and right.
        Vector3 strafeVector = this.transform.position + (Vector3.right * Time.deltaTime * strafeRate * Input.GetAxis("Horizontal"));
        Vector3 forwardVector = this.transform.position + (Vector3.forward * speed * Time.deltaTime);
        Vector3 vectorCombination = new Vector3(strafeVector.x, this.transform.position.y, forwardVector.z);

        if(movePlayer){
            rb.MovePosition(vectorCombination);
        }

    }

    void OnCollisionEnter(Collision collision){
        if(levelManager is LevelManager &&
            collision.gameObject.tag == "Obstacle"){
            movePlayer = false;
            gameOverCanvas.gameObject.SetActive(true);
        }
    }
}
