using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class endgame_controller : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        GameObject[] melee_enemies = GameObject.FindGameObjectsWithTag("melee enemy");

        if (melee_enemies.Length == 0) SceneManager.LoadScene("win");

    }
}
