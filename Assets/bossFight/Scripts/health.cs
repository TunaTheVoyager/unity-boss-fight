using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class health : MonoBehaviour
{
    [SerializeField] public float healthAmount;
    private GameObject boss;
    private AnimatorStateInfo bossStateInfo;
    private Animator Animator;
    private SpriteRenderer SpriteRenderer;
    private Color currentColor;
    private Color targetColor = Color.red;

    public bool isItDead = false;

    public static bool takeDamageAnim = false;
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
            takeDamageAnim = true;
            Animator.SetBool("isTakeDamage",true);
        }
        else if(CompareTag("Boss"))
        {
            Animator.SetTrigger("isTakeDamage");
        }
        else if(CompareTag("Summon"))
        {
            Animator.SetTrigger("takeDamage");
        }
        if (healthAmount <= 0f && isItDead == false)
        {
            if (gameObject.CompareTag("Player"))
            {
                Animator.SetBool("isDie", true);
                gameObject.GetComponent<characterController>().enabled = false; //Controller devre dýþý
                gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static; //Hareketi Durdur
                Invoke("DisableCollider", 1f);
                boss.GetComponent<Animator>().SetBool("playerDied", true);
                isItDead = true;
                Invoke("loadScene", 5f);
            }
            else if (gameObject.CompareTag("Boss"))
            {
                Animator.SetBool("isDeath", true);
                gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static; //Hareketi Durdur
                Transform fireball = transform.Find("burningArea");
                Destroy(fireball.gameObject);
                Invoke("DisableCollider", 2f);
                isItDead = true;
                Invoke("loadScene", 5f);
            }
            else if (gameObject.CompareTag("Summon"))
            {
                Animator.SetBool("dead",true);
                GetComponent<raiderController>().enabled = false;
                Invoke("DisableCollider", 1f);
                isItDead = true;
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

    private void loadScene()
    {
        SceneManager.LoadScene("bossFight"); //Temporary Method!!!!
    }

}
