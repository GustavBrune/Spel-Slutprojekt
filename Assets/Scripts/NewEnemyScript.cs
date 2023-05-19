using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class NewEnemyScript : MonoBehaviour
{
    private Animator animator;
    public int maxHealth = 100;
    int EnemyCurrentHealth;
    public Healthbar healthbar;
    public GameObject healthBarCanvas;
    public GameObject[] collectables;
    public bool attackera;
    public List<Transform> points;
    public int nextID = 0;
    int idChangeValue = 1;
    public float speed = 2;
    private bool canAttack;
    public float attackCooldown;
    public UnityEvent playerDamage;
    public bool PlayerDead;

    void MoveToNextPoint()
    {
        Transform goalPoint = points[nextID];
        if (goalPoint.transform.position.x > transform.position.x)
            transform.localScale = new Vector3(6, 5, 1);
        else
            transform.localScale = new Vector3(-6, 5, 1);

        transform.position = Vector2.MoveTowards(transform.position, goalPoint.position, speed * Time.deltaTime);


        if (Vector2.Distance(transform.position, goalPoint.position) < 1f)
        {
            if (nextID == points.Count - 1)
                idChangeValue = -1;

            if (nextID == 0)
                idChangeValue = 1;

            nextID += idChangeValue;

        }
    }

    private void Update()
    {
        MoveToNextPoint();
        animator = GetComponent<Animator>();
        animator.SetBool("isMoving", true);

        if (attackera == true && canAttack && EnemyCurrentHealth >= 0 && !PlayerDead)
        {
            Debug.Log("ATTACK HWUNGE");

            Invoke(nameof(slå), 0f);
            //animator.SetBool("isAttacking", true);
        }


    }


    // det finns en bool för isMoving i animator. använd denna när npc ska gå Ismoving=true 
    //triggar animationen

    void Start()
    {
        EnemyCurrentHealth = maxHealth;

        healthbar.SetMaxHealth(maxHealth);

        canAttack = true;

    }


    public void StopAttacking()
    {
        PlayerDead = true;
    }


    public void TakeDamage(int Damage)
    {
        EnemyCurrentHealth -= Damage;
        healthbar.SetHealth(EnemyCurrentHealth);

        animator.SetTrigger("Hurt");

        if (EnemyCurrentHealth <= 0)
        {
            Die();

        }
    }

    void Die()
    {
        Debug.Log("Enemy died!");

        animator.SetBool("isDead", true);
        GetComponent<Collider2D>().enabled = false;
        healthBarCanvas.SetActive(false);

        collectable();

        this.enabled = false;
    }


    private void collectable()
    {
        for (int i = 0; i < collectables.Length; i++)
        {
            Instantiate(collectables[i], transform.position + new Vector3(0, 1, 0), Quaternion.identity);

        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            attackera = false;
            animator.SetBool("isAttacking", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && canAttack == true)
        {
            attackera = true;
            animator.SetBool("isAttacking", true);



        }
    }


    private void slå()
    {
        canAttack = false;

        Invoke(nameof(ResetAttack), 0.5f);

        playerDamage.Invoke();

        Debug.Log("spelare träffad");
    }

    //IEnumerator AttackCooldown()
    // {
    //yield return new WaitForSeconds(attackCooldown);
    //canAttack = true;
    //}

    private void ResetAttack()
    {
        canAttack = true;
    }

    private void flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }
}