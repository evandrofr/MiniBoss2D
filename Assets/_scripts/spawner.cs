using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour
{

    public GameObject[] inimigos;
    
    GameObject player;
    
    [SerializeField]
    int level = 1;

    // [SerializeField]
    // int number_goblins;

    // [SerializeField]
    // int number_skelletons;

    // [SerializeField]
    // int number_mushrooms;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        SpawnEnemy();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    float getRandom_outside_player (string axis){
        if (axis == "x")
        {
            return Random.Range(-6, 6);
        }

        if (axis == "y")
        {
            return Random.Range(-3, 3);
        }

        return 0;
    
    }

    void SpawnEnemy(){
       
        for (int i = 0; i < Random.Range(1, 4)+level-1; i++)
        {
            Instantiate(inimigos[0], new Vector3(getRandom_outside_player("x"), getRandom_outside_player("y"), 0f), Quaternion.identity, transform);
        }

        for (int i = 0; i < Random.Range(1, 3)+level-1; i++)
        {
            Instantiate(inimigos[1], new Vector3(getRandom_outside_player("x"), getRandom_outside_player("y"), 0f), Quaternion.identity, transform);
            Instantiate(inimigos[3], new Vector3(getRandom_outside_player("x"), getRandom_outside_player("y"), 0f), Quaternion.identity, transform);
        }

        Instantiate(inimigos[2], new Vector3(getRandom_outside_player("x"), getRandom_outside_player("y"), 0f), Quaternion.identity, transform);

        foreach (Transform child in transform)
        {
            child.GetComponent<Enemy_Behav>().target = player.transform;
        }
    }

}
