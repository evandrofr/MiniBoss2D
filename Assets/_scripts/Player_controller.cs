using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Player_controller : MonoBehaviour{
    
    Rigidbody2D rb;
    Animator anim;

    float horizontalMove = 0f;
    float verticalMove = 0f;

    public Weapon_Controller weapon;

    float MvSpeed = 5f;

    public int maxHealth = 100;
    int currentHealth;

    public health_bar_behav health_Bar;


    // Start is called before the first frame update
    void Start(){
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        currentHealth = maxHealth;
        health_Bar.SetMaxHealth(currentHealth);
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update(){
        horizontalMove = Input.GetAxisRaw("Horizontal");
        verticalMove = Input.GetAxisRaw("Vertical");

        if (horizontalMove > 0.1 || horizontalMove < -0.1 || verticalMove > 0.1 || verticalMove < -0.1) {
            anim.SetBool("walking", true);
            weapon.mv = true;
        }
        else {
            anim.SetBool("walking", false);
            weapon.mv = false;
        }
    
    }

    void FixedUpdate(){
        Move();
    }

    void Move(){
        if (horizontalMove < -0.2f) {
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }

        if (horizontalMove > 0.2f) {
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
        }

        rb.velocity = new Vector2(horizontalMove, verticalMove).normalized * MvSpeed;
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
        // rb.velocity = Vector2.zero;
        anim.SetTrigger("die");
        SceneManager.LoadScene("gameover");
    }

    private void OnTriggerEnter2D(Collider2D other) {
        
        if (other.tag == "drop"){

            weapon_stats weaponStats = other.GetComponent<weapon_stats>();
            
            weapon.weaponDamege = weaponStats.dmg;
            weapon.attackRate = weaponStats.attackRate;
            weapon.sprite.sprite = weaponStats.sprite;


            weapon.attackSFX = weaponStats.attackSFX;

            Destroy(other.gameObject);


        }
    }
}
