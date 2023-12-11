using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMelee : MonoBehaviour
{
    public float swingCooldown = 1.0f;
    public Collider2D swordCollider;
    public Animator animator;
    private bool canSwing = true;

    void Start()
    {
        // Ensure the collider is initially disabled
        swordCollider.enabled = false;
    }

    void Update()
    {
        if (Input.GetButtonDown("Swing"))
        {
            SwingSword();
            StartCoroutine(SwingCooldown());
        }
    }

    void SwingSword()
    {
        // Enable the collider when swinging the sword
        animator.SetBool("Swing", true);
        swordCollider.enabled = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the sword collider has entered a collider with the "Enemy" tag
        if (other.gameObject.tag == "Enemy")
        {
            // Perform actions when the sword collides with an enemy
            Debug.Log("Sword hit an enemy!");

            //Check if Eye Enemy
            EyeDamage EyeDamage = other.gameObject.GetComponent<EyeDamage>();
            if(EyeDamage != null){
                EyeDamage.meleeDamage(10);
                Debug.Log("hit");
            }
            else{
                ToothDamage toothDamage = other.gameObject.GetComponent<ToothDamage>();
                if(toothDamage != null){
                    toothDamage.meleeDamage(10);
                    Debug.Log("hit2");
                }
                else{
                    EnemyDamage rangedDamage = other.gameObject.GetComponent<EnemyDamage>();
                    if(rangedDamage != null){
                        rangedDamage.meleeDamage(10);
                        Debug.Log("hit3");
                    }
                }
            }
        }
        if(other.gameObject.tag == "BossLeg"){
            BossMainScript bossDamage = GameObject.FindGameObjectWithTag("BossObject").GetComponent<BossMainScript>();
            bossDamage.meleeDamage(10);
            other.GetComponent<BossFootScript>().freeze();
        }
        if(other.gameObject.tag == "BreakWall")
        {
            other.GetComponent<BreakableWall>().hitByWeapon();
        }
    }

    IEnumerator SwingCooldown()
    {
        canSwing = false;
        

        // Wait for the specified cooldown duration
        yield return new WaitForSeconds(swingCooldown);

        // Disable the collider after the cooldown
        animator.SetBool("Swing", false);
        swordCollider.enabled = false;

        canSwing = true;
    }
}

/*
    bool attack = false;

    public Collider2D attackbox;
    // Start is called before the first frame update
    void Start()
    {
        attackbox = gameObject.GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z)){
            StartCoroutine(swing());
        }
    }

    void OnCollisionStay2D(Collision2D other) 
    { 
        Debug.Log("Whatttt");
        if (other.gameObject.tag == "Enemy") 
        { 
            Debug.Log("Hit");
            EnemyDamage enemyDamage = other.gameObject.GetComponent<EnemyDamage>();
            enemyDamage.meleeDamage(10);
        } 
    }

    public IEnumerator swing(){
        attack = true;
        yield return new WaitForSeconds(0.2f);
        attack = false;
    }
*/