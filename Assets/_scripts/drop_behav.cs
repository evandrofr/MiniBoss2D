using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class drop_behav : MonoBehaviour
{

    public GameObject weapon;

    public void Drop(){
        

        Instantiate(weapon, transform.position, Quaternion.identity);
            
        
    }
}
