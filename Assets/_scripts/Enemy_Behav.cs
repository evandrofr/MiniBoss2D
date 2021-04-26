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

    public health_bar_behav health_Bar;

    public AudioClip attackSFX;

    float SpaceBetween = 1.5f;

    GameObject[] Other_Enemies;

    // Start is called before the first frame update
    void Start(){
        currentHealth = maxHealth; 
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        drop = GetComponent<drop_behav>();
        health_Bar.SetMaxHealth(currentHealth);

    }

    // Update is called once per frame
    void Update(){
        
        Other_Enemies = GameObject.FindGameObjectsWithTag("melee enemy");

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

    void Move_to_Player() {
        anim.SetBool("mv", true);
        avoid_other_enemies();
        rb.velocity = (Vector2) (target.position - center.position).normalized * mvSpeed;

        if (rb.velocity.x < -0.2f) transform.eulerAngles = new Vector3(0f, 180f, 0f);
        if (rb.velocity.x > 0.2f) transform.eulerAngles = new Vector3(0f, 0f, 0f);
    }

    void avoid_other_enemies() {
        foreach (GameObject obj in Other_Enemies)
        {
            if (obj != gameObject && obj != null)
            {
                float distance_to_enemy = Vector3.Distance(obj.transform.position, center.position);
                
                if (distance_to_enemy <= SpaceBetween)
                {
                    Vector3 direction = center.position - obj.transform.position;
                    transform.Translate(direction * Time.deltaTime * mvSpeed);
                }
            }
        }
    }

    IEnumerator Attack(){
        atking = true;
        rb.velocity = Vector2.zero;
        anim.SetTrigger("atk");
        AudioManager.PlaySFX(attackSFX);
        atking = false;
        yield return new WaitForSeconds(waitTime);

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(center.position, MeleeRadius, PlayerLayer);

        foreach (Collider2D enemy in hitEnemies){

            if (enemy.tag == "Player"){ 
                enemy.GetComponent<Player_controller>().TakeDamage(attackDamage);
            }
        
        }
    }

    public void TakeDamage(int dmg){
        anim.SetTrigger("dmg");
        currentHealth -= dmg;
        health_Bar.SetHealth(currentHealth);
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


