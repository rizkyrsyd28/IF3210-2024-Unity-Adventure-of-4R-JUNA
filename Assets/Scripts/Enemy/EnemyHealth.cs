using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] public float maxHealth;
    [HideInInspector] public float health;
    [HideInInspector] public ParticleSystem hitParticle;
    [SerializeField] public HealthBar healthBar;

    [HideInInspector] public bool isDead = false;
    void Awake()
    {
        hitParticle = GetComponentInChildren<ParticleSystem>();
        health = maxHealth;
        healthBar.UpdateHealthBar(maxHealth, health);
    }

    public void TakeDamage(float damage) 
    {
        Debug.Log("Hit");
        health -= damage;

        hitParticle.Play();
        GameStateManager.Instance.UpdateOnHitTarget();

        healthBar.UpdateHealthBar(maxHealth, health);

        if (health <= 0) Death();
    }

    public virtual void Death()
    {
        if(isDead) return;
        isDead = true;

        Animator animator = GetComponent<Animator>();

        GetComponent<NavMeshAgent>().enabled = false;

        animator.SetTrigger("Dead");
            
        GetComponent<OrbManager>().DropRandomOrb();
        Debug.Log("ENEMYDEAD:" + gameObject.name);
        if (!gameObject.CompareTag("Raja"))
        {
            GetComponent<OrbManager>().DropRandomOrb();
            if(gameObject.CompareTag("Kroco")) 
            {
                Debug.Log("KrocoKill");
                GameStateManager.Instance.UpdateKrocoKill(); 
            } else if(gameObject.CompareTag("KepalaKroco")) 
            { 
                GameStateManager.Instance.UpdateKepalaKrocoKill();
            } else if(gameObject.CompareTag("Jendral")) 
            { 
                GameStateManager.Instance.UpdateJendralKill(); 
            }
        } else {
            GameStateManager.Instance.UpdateRajaKill();
        }

        Debug.Log("Death");

        Destroy(gameObject, 2.5f);
    } 

}
