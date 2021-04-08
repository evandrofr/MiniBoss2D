using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pause_controller : MonoBehaviour
{

    [SerializeField] GameObject pauseScreen;
    bool pause = false;

    // Start is called before the first frame update
    void Start()
    {
        pauseScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape)) {
            pause = !pause;

            if (pause) {
                Time.timeScale = 0f;
                pauseScreen.SetActive(true);
            }
            else {
                Time.timeScale = 1f;
                pauseScreen.SetActive(false);
            }
        }
    }

    public void Retornar(){
        pause = false;
    }
}
