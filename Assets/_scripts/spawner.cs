using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour
{

    public GameObject[] inimigos;
    public GameObject player;

    // [SerializeField]
    // int number_goblins;

    // [SerializeField]
    // int number_skelletons;

    // [SerializeField]
    // int number_mushrooms;
    
    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemy();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnEnemy(){
        for (int i = 0; i < Random.Range(1, 4); i++)
        {
            Instantiate(inimigos[0], new Vector3(Random.Range(-6, 6), Random.Range(-3, 3), 0f), Quaternion.identity, transform);
        }

        for (int i = 0; i < Random.Range(1, 2); i++)
        {
            Instantiate(inimigos[1], new Vector3(Random.Range(-6, 6), Random.Range(-3, 3), 0f), Quaternion.identity, transform);
        }

        Instantiate(inimigos[2], new Vector3(Random.Range(-6, 6), Random.Range(-3, 3), 0f), Quaternion.identity, transform);

        foreach (Transform child in transform)
        {
            child.GetComponent<Enemy_Behav>().target = player.transform;
        }
    }
}
