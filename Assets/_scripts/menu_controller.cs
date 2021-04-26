using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menu_controller : MonoBehaviour
{

    [SerializeField] GameObject MenuScreen;

    pause_controller pauseWindow;

    private void Start() {
        Time.timeScale = 0f;
        GetComponent<pause_controller>().inGame = false;

    }

    public void StartGame(){
        Time.timeScale = 1f;
        MenuScreen.SetActive(false);
        GetComponent<pause_controller>().inGame = true;

    }
}
