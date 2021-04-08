using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menu_controller : MonoBehaviour
{

    [SerializeField] GameObject MenuScreen;
    
    private void Start() {
        Time.timeScale = 0f;
    }

    public void StartGame(){
        Time.timeScale = 1f;
        MenuScreen.SetActive(false);
    }
}
