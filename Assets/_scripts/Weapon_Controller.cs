using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Controller : MonoBehaviour{

    public int weaponDamege = 30;
    
    Animator anim;

    public SpriteRenderer sprite;

    public bool mv = false;

    public Transform attackPoint;

    public float attackRange = 0.5f;
    public float attackRate = 1.5f;
    private float lastAttackTimestamp = 0.0f;

    public LayerMask enemyLayers;

    // Start is called before the first frame update
    void Start() {
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
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

            if (enemy.tag == "melee enemy"){ 
                enemy.GetComponent<Enemy_Behav>().TakeDamage(weaponDamege);
            }
        
        }
    }


    void OnDrawGizmos(){
        if(attackPoint == null) return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
