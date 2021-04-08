using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Enemy_Behav : MonoBehaviour{

    Animator anim;
    Rigidbody2D rb;

    drop_behav drop;

    public float agroRadius = 10f;
    public float MeleeRadius = 2f;
    public Transform center;

    [SerializeField]
    float waitTime = 0f;

    [SerializeField] float atk_rate = 0.5f;
    float next_atk = 0f;

    public Transform target;

    [SerializeField]
    float mvSpeed = 5f;

    bool mv = false; 
    bool atking = false;

    public int maxHealth = 100;
    int currentHealth;

    public LayerMask PlayerLayer;

    [SerializeField]
    int attackDamage = 40;

    // Start is called before the first frame update
    void Start(){
        currentHealth = maxHealth; 
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        drop = GetComponent<drop_behav>();

    }

    // Update is called once per frame
    void Update(){

        float distance = Vector3.Distance(target.position, center.position);

        if (distance <= agroRadius && !atking){
            mv = true;
        }

        if (distance <= MeleeRadius){
            mv = false;
            anim.SetBool("mv", false);

            if( Time.time >= next_atk){
                StartCoroutine("Attack");  
                next_atk = Time.time + 1f/atk_rate;
            }
        }
    }

    private void FixedUpdate() {
        if(mv) Move_to_Player();
    }

    void Move_to_Player(){
        anim.SetBool("mv", true);
        rb.velocity = (Vector2) (target.position - center.position).normalized * mvSpeed;

        if (rb.velocity.x < -0.2f) transform.eulerAngles = new Vector3(0f, 180f, 0f);
        if (rb.velocity.x > 0.2f) transform.eulerAngles = new Vector3(0f, 0f, 0f);
    }

    IEnumerator Attack(){
        atking = true;
        rb.velocity = Vector2.zero;
        anim.SetTrigger("atk");
        atking = false;

        yield return new WaitForSeconds(waitTime/2);
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(center.position, MeleeRadius, PlayerLayer);

        foreach (Collider2D enemy in hitEnemies){

            Debug.Log("Hit " + enemy.name);
            if (enemy.tag == "Player"){ 
                enemy.GetComponent<Player_controller>().TakeDamage(attackDamage);
            }
        
        }
        yield return new WaitForSeconds(waitTime/2);
    }

    public void TakeDamage(int dmg){
        anim.SetTrigger("dmg");
        currentHealth -= dmg;
        if(currentHealth <= 0){
            Die();
        }
    }

    void Die(){
        rb.velocity = Vector2.zero;
        anim.SetTrigger("die");
        GetComponent<Collider2D>().enabled = false;
        GetComponent<Enemy_Behav>().enabled = false;
        drop.Drop();
        Destroy(this.gameObject, 2f);
    }


    private void OnDrawGizmos() {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(new Vector3(center.position.x, center.position.y, 0f), agroRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(new Vector3(center.position.x, center.position.y, 0f), MeleeRadius);
    }
}


