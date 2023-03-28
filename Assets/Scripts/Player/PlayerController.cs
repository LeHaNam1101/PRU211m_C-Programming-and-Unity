using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D rb;
    private Animator ani;
    public GameObject swordHitbox;
    Collider2D swordCollider;
    public Transform characterTransform;
    public Transform swordTransform;
    private int playerMaxHealth = 100;
    private int currentHealth = 100;
    public float speed = 5f;
    private GameObject player;
    //moveW

    private float left_right;
    private float up_down;
    private bool isfacingRight = true;
    
    public bool isAttacking = false;
    //attack
    public float attackRange;
    public int attackDamage;
    public Transform attackPoint;
    public LayerMask enemyLayers;
    //speedUp
    public float speedBoost = 10f;
    private float speedUpTime ;
    public float SpeedUpTime = 1f;
    bool speedOnce = false;
    ////skill1
    public GameObject skillPrefab;
    public Button button;
    public float showDuration = 5f;
    public float cooldownDuration = 20f;

    private bool isCooldown = false;
    //Skill2
    public GameObject skillPrefab2;
    public Button button2;
    public float showDuration2 = 5f;
    public float cooldownDuration2 = 10f;

    private bool isCooldown2 = false;
    //Skill1
    public void ShowSkill1()
    {
        if (!isCooldown)
        {
            skillPrefab.SetActive(true);
            Invoke("HideSkill1", showDuration);
            isCooldown = true;
            button.interactable = false;
            Invoke("EndCooldown", cooldownDuration);
        }
    }
    //Skill2
    public void ShowSkill2()
    {
        if (!isCooldown2)
        {
            skillPrefab2.SetActive(true);
            Invoke("HideSkill2", showDuration2);
            isCooldown2 = true;
            button2.interactable = false;
            Invoke("EndCooldown2", cooldownDuration2);
        }
    }

    private void HideSkill1()
    {
        skillPrefab.SetActive(false);
    }
    private void HideSkill2()
    {
        skillPrefab2.SetActive(false);
    }
    private void EndCooldown()
    {
        isCooldown = false;
        button.interactable = true;
    }
    private void EndCooldown2()
    {
        isCooldown2 = false;
        button2.interactable = true;
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();

        skillPrefab.SetActive(false);
        skillPrefab2.SetActive(false);
        swordCollider = swordHitbox.GetComponent<Collider2D>();
        characterTransform = player.transform;
        swordTransform = transform;

    }

    // Update is called once per frame
    void Update()

    {
        if (characterTransform.localScale.x > 0)
        {
            // nếu nhân vật quay sang phải, xoay gameobject kiếm về bên phải
            swordTransform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            // nếu nhân vật quay sang trái, xoay gameobject kiếm về bên trái
            swordTransform.localScale = new Vector3(-1, 1, 1);
        }
        //move
        if (!isAttacking)
        {
            left_right = Input.GetAxis("Horizontal");
            up_down = Input.GetAxis("Vertical");
            rb.velocity = new Vector2(left_right * speed, rb.velocity.y);
            rb.velocity = new Vector2(rb.velocity.x, up_down * speed);
            
        }
        //animation
        flip();
        ani.SetFloat("move", Mathf.Abs(left_right));
        AnimatorStateInfo stateInfo = ani.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName("Attack") && stateInfo.normalizedTime >= 1f)
        {

            ani.SetTrigger("Idle");
        }

        //attack
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ani.SetTrigger("attack");
            isAttacking = true;
            rb.velocity = Vector2.zero;
            //Attack();
        }

        //SpeedUp
        if(Input.GetKeyDown(KeyCode.Z) && speedUpTime <= 0)
        {
            speed += speedBoost;
            speedUpTime = SpeedUpTime;
            speedOnce = true;

        }
        if(speedUpTime<= 0 && speedOnce == true)
        {
            speed -= speedBoost;
            speedOnce = false;
        }
        else
        {
            speedUpTime -= Time.deltaTime;
        }
        //Skill1
        if (Input.GetKeyDown(KeyCode.X))
        {
            ShowSkill1();
        }
        //Skill2
        if (Input.GetKeyDown(KeyCode.C))
        {
            GameObject skill = Instantiate(skillPrefab2);
            skill.transform.SetParent(transform);
            skill.transform.localPosition = Vector3.zero;
            skill.transform.rotation = Quaternion.Euler(0, 0, transform.eulerAngles.z);
            ShowSkill2();
        }
    }
    

    void flip()
    {
        
        if(isfacingRight && left_right <0 || !isfacingRight && left_right > 0)
        {
            isfacingRight = !isfacingRight;
            
            Vector3 scale = transform.localScale;
            scale.x = scale.x * -1;
            transform.localScale = scale;
            
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ICollectible collectible = collision.GetComponent<ICollectible>();
        if (collectible != null)
        {
            currentHealth +=  10;
            currentHealth = Mathf.Clamp(currentHealth, 0, playerMaxHealth);
            collectible.Collect();
        }
    }

    public void EndAttack()
    {
        isAttacking = false;
    }
    //void Attack()
    //{
    //    Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

    //    foreach (Collider2D enemy in hitEnemies)
    //    {
    //        enemy.GetComponent<EnemyController>().TakeDamage(attackDamage);
    //    }
    //}
}
