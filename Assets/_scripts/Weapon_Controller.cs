using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Controller : MonoBehaviour{
    int weaponDamege = 30;
    Animator anim;

    public bool mv = false;

    public Transform attackPoint;

    public float attackRange = 0.5f;
    public float attackRate = 2f;
    private float lastAttackTimestamp = 0.0f;

    public LayerMask enemyLayers;

    // Start is called before the first frame update
    void Start() {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)){
            Attack();
        }

        anim.SetBool("mv", mv);
        
    }

    void Attack(){

        if (Time.time - lastAttackTimestamp < 1/attackRate) return;
        lastAttackTimestamp = Time.time;

        anim.SetTrigger("atk");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies){
            Debug.Log("Hit " + enemy.name);
            enemy.GetComponent<Skelleron_Behav>().TakeDamage(weaponDamege);
        }
    }


    void OnDrawGizmos(){
        if(attackPoint == null) return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
