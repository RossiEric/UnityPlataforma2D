using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerDamage : MonoBehaviour
{
    public int KnockBackMagnitude;
    public int KnockBackMagnitudeOnDamage;
    public int Maxhealth;
    public int health;
    public GameObject deathEffect;
    public bool Hurt;
    FlashSprites Efect;
    public Animator anime;
    public bool Morto;

    public float TimeCantTakeDamage;

    public AudioSource DieEffecty;

    // UI
    public BarraVida BarraVida;
    public Text ValorVida;

    private Rigidbody2D m_Rigidbody2D;



    // Start is called before the first frame update
    void Start()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        BarraVida.BarInicialAjuste(Maxhealth, health);
        ValorVida.text = health.ToString() + "/" + Maxhealth.ToString();
        Efect = GameObject.Find("MainGame").GetComponent<FlashSprites>();
    }

    // Update is called once per frame
    void Update()
    {
        anime.SetBool("hurt", Hurt);
    }

    public void TakeDamage(int damage)
    {
        if (!Hurt)
        {
            Debug.Log("player TakeDamage");

            StartCoroutine(damageTimer());

            #region Damage calc
            BarraVida.BarDamage(Maxhealth, damage);
            health -= damage;
            if (health < 0)
            {
                health = 0;
            }
            ValorVida.text = health.ToString() + "/" + Maxhealth.ToString();
            #endregion

            if (health <= 0)
            {
                Die();
            }
            else
            {
                SpriteRenderer[] sprites = GetComponentsInChildren<SpriteRenderer>();
                StartCoroutine(Efect.FlashSpritesActive(sprites, 5, 0.05f));
                //Hurt = false;
            }
        }        
    }


    private IEnumerator damageTimer()
    {
        if (!Morto)
        {
            KnockBackOnDamage(transform);
            Hurt = true;
            yield return new WaitForSeconds(TimeCantTakeDamage);
            Hurt = false;
        }
    }

    private IEnumerator Resetgame()
    {
        yield return new WaitForSeconds(0.9f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void HealDamage(int heal)
    {

        Debug.Log(heal);

        if (Maxhealth >= (health + heal) )
        {
            health += heal;
        }
        else
        {
            health = Maxhealth;
        }

        BarraVida.BarInicialAjuste(Maxhealth, health);
        ValorVida.text = health.ToString() + "/" + Maxhealth.ToString();

    }

    public void Die()
    {
        if (!Morto)
        {
            DieEffecty.Play();
            Morto = true;
            GetComponent<Renderer>().enabled = false;
            Instantiate(deathEffect, transform.position, Quaternion.identity);
            Time.timeScale -= 0.5f;
            StartCoroutine(Resetgame());
            //Destroy(gameObject);
            //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

    }

    public void KnockBack(Transform transformEnemy)
    {
        var force = transform.position - transformEnemy.transform.position;

        force.Normalize();
        GetComponent<Rigidbody2D>().AddForce(force * KnockBackMagnitude);
    }

    public void KnockBackOnDamage(Transform transformEnemy)
    {
        m_Rigidbody2D.velocity = Vector2.zero;

        if (transformEnemy.localScale.x < 0)
        {
            m_Rigidbody2D.velocity = Vector2.right * KnockBackMagnitudeOnDamage;
        }
        else
        {
            m_Rigidbody2D.velocity = Vector2.left * KnockBackMagnitudeOnDamage;
        }
    }

}
