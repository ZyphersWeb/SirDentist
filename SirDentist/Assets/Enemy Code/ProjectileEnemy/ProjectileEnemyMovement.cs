using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileEnemyMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    private GameObject player;
    private GameObject[] ground;

    public bool shouldRun;
    
    private Rigidbody2D rb;
    private bool shouldMove = true;
    private bool grounded = true;
    private Animator animator;
    private Collider2D enemyCollider;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        enemyCollider = GetComponent<Collider2D>();
        player = GameObject.FindWithTag("Player");
        ground = GameObject.FindGameObjectsWithTag("Ground");
    }

    // Update is called once per frame
    void Update()
    {
        touchingGrass();
        shouldRunfunc();

        //Debug.Log("What: " + grounded);
        if (shouldMove && grounded && shouldRun)
        {
            TowardsPlayer();
        }
        else
        {
            standIdle();
        }
            
    }

    private void shouldRunfunc(){
        float dist_to_player= Vector3.Distance(player.transform.position,transform.position);
        if(dist_to_player<20){
            shouldRun = true;
        }
        else{
            shouldRun = false;
        }
    }

    void TowardsPlayer()
    {
        if (Mathf.Round(player.transform.position.x*10f) - Mathf.Round(transform.position.x*10f) < 0)
        {
            StartCoroutine(moveRight());
            animator.SetBool("alert", true);
           
        }
        else if (Mathf.Round(player.transform.position.x*10f) - Mathf.Round(transform.position.x*10f) > 0)
        {
            StartCoroutine(moveLeft());
            animator.SetBool("alert", true);
        }

    }

    void standIdle()
    {
        //StandIdle Animation
    }

    IEnumerator moveLeft()
    {
        yield return new WaitForSeconds(0.5f);


        if (rb.velocity == new Vector2(0, 0) || rb.velocity.x > 0)
        {
            Vector2 movement = new Vector2(-1 * moveSpeed, rb.velocity.y);
            rb.velocity = movement;
        }
        else if (rb.velocity.x > -5)
        {
            Vector2 movement = new Vector2(-1 * 0.5f * moveSpeed, 0);
            rb.velocity += movement;
        }
    }

    IEnumerator moveRight()
    {
        yield return new WaitForSeconds(0.5f);

        if (rb.velocity == new Vector2(0, 0) || rb.velocity.x < 0)
        {
            Vector2 movement = new Vector2(1 * moveSpeed, rb.velocity.y);
            rb.velocity = movement;
        }
        else if(rb.velocity.x < 5)
        {
            Vector2 movement = new Vector2(1 * 0.5f * moveSpeed, 0);
            rb.velocity += movement;
        }
        
    }

    IEnumerator stopMoving()
    {
        shouldMove = false;
        rb.velocity = new Vector2(0, 0);
        yield return new WaitForSeconds(1f);
        shouldMove = true;

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        //Debug.Log("In contact with " + (other.gameObject.tag));
        if (other.gameObject.tag == "Player")
        {
            StartCoroutine(stopMoving());
        }
        else if(other.gameObject.tag == "Flail")
        {
            StartCoroutine(stopMoving());
        }

        
    }

    private void touchingGrass()
    {
        //if (other.gameObject.tag == ("Ground"))

        ground = GameObject.FindGameObjectsWithTag("Ground");
        Collider2D groundCollider;
             
        foreach (GameObject floors in ground)
        {

            groundCollider = floors.GetComponent<Collider2D>();

            if (groundCollider != null)
            {
                if (enemyCollider.IsTouching(groundCollider))
                {
                    //Debug.Log("Grounded true");
                    grounded = true;
                }
            }
        }
        
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        //Debug.Log("No longer in contact with " + (collision.gameObject.tag));
        if (collision.gameObject.tag == ("Ground"))
        {
            //Debug.Log("Grounded false");
            grounded = false;
        }
    }
}
