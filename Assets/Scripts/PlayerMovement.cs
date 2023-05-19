using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Events;
using Text = TMPro.TextMeshProUGUI;
 


public class PlayerMovement : MonoBehaviour
{
    public bool isGrounded; 
    Rigidbody2D rb;
    public float jumpForce;
    private int collisionMask = 1 << 8 ;
    private float horizontal;
    private float speed = 8f;
    private bool isFacingRight = true;
    private bool isJumping;
    Animator anim;
    private bool isMoving;
    public int maxHealth = 100;
    public int currentHealth;
    public Healthbar healthbar;
    public bool playerIsClose;
    public Text levelCompleteText;
    public GameObject levelPanel;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    public int attackDamage = 20;
    public float attackRate = 2f;
    float nextAttackTime = 0f;
    public bool deathAnimationPlayed = false;
    public int count;
    public TextMeshProUGUI countText;
    public bool isGettingHit;
    public GameObject healthBarCanvas;
    public bool PlayerIsDead;

    public bool IsClose2;

    public GameObject DeathMenuCanvas;

    public UnityEvent PlayerDie;

    public bool IsClose;

    public GameObject PauseMenu;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        anim = GetComponent<Animator>();

        //HPSYSTEM
        currentHealth = maxHealth;
        healthbar.SetMaxHealth(maxHealth);

        count = 0;

        SetCountText();

        Time.timeScale = 1;

    }

    void SetCountText()
    {
        countText.text = "orbs collected : " + count.ToString();
    }
    
    void Update()
    {

        Scene scene = SceneManager.GetActiveScene();
        Debug.Log(scene.name);

        if (Input.GetKey(KeyCode.Escape))
        {
            PauseMenu.SetActive(true);
            Time.timeScale = 0;


        }


        //hoppa
        if (Input.GetKey("space") && isGrounded && !isJumping)
        {
            anim.SetTrigger("jump");
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isJumping = true;
            
        
        }

       if(IsClose == true && Input.GetKey(KeyCode.F) && count >= 4)
        {
            SceneManager.LoadScene(4);
            Debug.Log("DETTA FUNKAR OCKSÅ");
        }

       if(IsClose2 == true && Input.GetKey(KeyCode.F))
        {
            SceneManager.LoadScene(6);

        }
        
       //damage test
        if (Input.GetKey(KeyCode.Mouse1))
        {

            PlayerTakeDamage(20); 
        }
        //damage script
         
       

        //movement
        horizontal = Input.GetAxisRaw("Horizontal");
        Flip();


        // dö om man är för långt ner
        if (rb.transform.position.y < -50 && scene.name == "LEVEL 1+2")
        {
            
            gameObject.transform.localPosition = new Vector3(89, -19, 0); 
        }

        if (rb.transform.position.y < -15 && scene.name == "level 3")
        {
            die();
        }

        if (rb.transform.position.y < -15 && scene.name == "level 4")
        {
            die();
        }



            //animationer för att gå
            if (Mathf.Abs(horizontal) > 0.1f)
        {
            anim.SetBool("isWalking", true);
         
        }
        else
        {
            anim.SetBool("isWalking", false);
            
        }

        if(Time.time >= nextAttackTime)
        {
            //slå skript
            if (Input.GetKey(KeyCode.Mouse0))
            {
                anim.SetTrigger("attack");
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
    }
    //Så att man inte kan hoppa i luften

    

    public void PlayerTakeDamage(int Damage)
    {
        currentHealth -= Damage;
        healthbar.SetHealth(currentHealth);
        
        if (currentHealth >= 0)
        {
            anim.SetTrigger("hurt");

        }

        

        if (currentHealth <= 0)
        {
            die();

        }
    }

    public void PlayerHit()
    {
        isGettingHit = true;
    }
    private void FixedUpdate()
    {
        isGrounded = Physics2D.Raycast(rb.transform.position, rb.transform.up * -1.55f, 1.55f, ~collisionMask);
        Debug.DrawRay(rb.transform.position, rb.transform.up * -1.55f, Color.green, 3f);
        //Debug.Log(isGrounded);
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);


       
    }

    
    // spelaren tittar dit man går
    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale; 

        }

    }

    //om man kolliderar med något så hoppar man inte!
     void OnCollisionEnter2D(Collision2D col)
    {
        
            isJumping = false;

    }

    void Attack()
    {
       Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach(Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
            
        }


    }

    private void OnDrawGizmosSelected()
    {

        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

     void die()
    {
        anim.SetBool("isDead", true);

        this.enabled = false;

        Invoke(nameof(PauseTime), 2f);

        PlayerDie.Invoke();
       
    }

    private void PauseTime()
    {
        Time.timeScale = 0;
        DeathMenuCanvas.SetActive(true);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("collectable"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            currentHealth = currentHealth + 10;
            healthbar.SetHealth(currentHealth + 10);
            SetCountText();
        }

        if (other.gameObject.CompareTag("HealthResetPickUp"))
        {
            other.gameObject.SetActive(false);
            currentHealth = maxHealth;
            healthbar.SetMaxHealth(maxHealth);
        }

        if (other.gameObject.CompareTag("door"))
            
        {
            IsClose = true;
            Debug.Log("Funkar");
        }

        if (other.gameObject.CompareTag("door2"))
        {

            IsClose2 = true;
            

            
        }
    }



}
