using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Reset : MonoBehaviour
{
       // Update is called once per frame
    private void Start() {
        Destroy(GameObject.FindGameObjectWithTag("Player"));
        Destroy(GameObject.FindGameObjectWithTag("audio"));
    }

    void Update() {

        if(Input.GetKeyDown(KeyCode.Escape)) SceneManager.LoadScene("SampleScene");
    }
}
