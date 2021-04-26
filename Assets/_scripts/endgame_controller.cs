using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class endgame_controller : MonoBehaviour
{
    GameObject[] melee_enemies;
    // Update is called once per frame
    void Update()
    {
        melee_enemies = GameObject.FindGameObjectsWithTag("melee enemy");

        if (melee_enemies.Length == 0) {
            
            if (SceneManager.GetActiveScene().buildIndex == 1) SceneManager.LoadScene("win");
            else SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
        }

    }
}
