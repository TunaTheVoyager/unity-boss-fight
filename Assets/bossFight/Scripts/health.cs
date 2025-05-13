using System.Collections;
using UnityEditor;
using UnityEngine;

public class health : MonoBehaviour
{
    [SerializeField] public float healthAmount;
    private GameObject boss;
    private AnimatorStateInfo bossStateInfo;
    private Animator Animator;
    private SpriteRenderer SpriteRenderer;
    private Color currentColor;
    private Color targetColor = Color.red;

    //private Collider2D targetCollider;

    [Range(0f, 1f)]
    [SerializeField] private float redStep = 0.1f;

    void Start()
    {
        //GameObject = GetComponent<GameObject>();
        SpriteRenderer = GetComponent<SpriteRenderer>();
        Animator = GetComponent<Animator>();
        currentColor = SpriteRenderer.color;
        boss = GameObject.FindGameObjectWithTag("Boss");
    }
    public void takeDamage(float damage)
    {
        healthAmount -= damage;
        currentColor = Color.Lerp(currentColor, targetColor, redStep);
        SpriteRenderer.color = currentColor;

        if(CompareTag("Player"))
        {
            Animator.SetBool("isTakeDamage",true);
        }
        else
        {
            Animator.SetTrigger("isTakeDamage");
        }
        if (healthAmount <= 0f)
        {
            if (gameObject.CompareTag("Player"))
            {
                Animator.SetBool("isDie",true);
                gameObject.GetComponent<characterController>().enabled = false; //Controller devre dýþý
                gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static; //Hareketi Durdur
                Invoke("DisableCollider", 1f);
                //StartCoroutine(waitForBossIdleAnim());
                boss.GetComponent<Animator>().SetBool("playerDied", true);
            }
            else
            {
                Animator.SetBool("isDeath", true);
                gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static; //Hareketi Durdur
                Transform fireball = transform.Find("burningArea");
                Destroy( fireball.gameObject );
                Invoke("DisableCollider", 2f);
            }
        }
    }

    public void EndTakeDamageAnim()
    {
        if (CompareTag("Player"))
           Animator.SetBool("isTakeDamage", false);
    }

    public void DisableCollider()
    {
          gameObject.GetComponent<Collider2D>().isTrigger = true;
    }

}
