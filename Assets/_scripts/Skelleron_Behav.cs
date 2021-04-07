using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Skelleron_Behav : MonoBehaviour{

    Animator anim;
    Rigidbody2D rb;

    public float agroRadius = 10f;
    public float MeleeRadius = 2f;
    public Transform center;

    [SerializeField] float atk_rate = 0.5f;
    float next_atk = 0f;


    public Transform target;

    [SerializeField]
    float mvSpeed = 5f;

    bool mv = false; 
    bool atking = false;

    public int maxHealth = 100;
    int currentHealth;

    // Start is called before the first frame update
    void Start(){
        currentHealth = maxHealth; 
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

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
        rb.velocity = (target.position - center.position).normalized * mvSpeed;

        if (rb.velocity.x < -0.2f) transform.eulerAngles = new Vector3(0f, 180f, 0f);
        if (rb.velocity.x > 0.2f) transform.eulerAngles = new Vector3(0f, 0f, 0f);
    }

    IEnumerator Attack(){
        atking = true;
        rb.velocity = Vector2.zero;
        anim.SetTrigger("atk");
        yield return new WaitForSeconds(1.15f);
        atking = false;
    }

    public void TakeDamage(int dmg){
        anim.SetTrigger("dmg");
        currentHealth -= dmg;
        if(currentHealth <= 0){
            Die();
        }
    }

    void Die(){
        GetComponent<Collider2D>().enabled = false;
        Debug.Log("Enemy dies!");
        anim.SetTrigger("die");
        GetComponent<Skelleron_Behav>().enabled = false;
    }


    private void OnDrawGizmos() {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(new Vector3(center.position.x, center.position.y, 0f), agroRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(new Vector3(center.position.x, center.position.y, 0f), MeleeRadius);
    }
}


