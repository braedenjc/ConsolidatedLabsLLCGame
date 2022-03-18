using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public LevelMovement levelMovement;
    public void StopMovement(){
        if(levelMovement is LevelMovement){
            levelMovement.stopMovement = 0;
        }
    }
}
