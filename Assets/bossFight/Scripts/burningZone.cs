using UnityEngine;

public class burningZone : MonoBehaviour
{
    [Header("Damage Stats")]
    [SerializeField] private float damagePerSecond = 0.1f;
    private float damageThisFrame;
    private health objectHealth;
    [SerializeField]private healthBar healthBar;
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if(!collision.CompareTag("Player") || !collision.CompareTag("Summon")) return;
        if(collision.CompareTag("Summon"))
        {
            objectHealth = collision.GetComponent<health>();
            objectHealth.healthAmount -= damagePerSecond;
            if (objectHealth.healthAmount <= 0)
            {
                objectHealth.takeDamage(damageThisFrame);
            }
        }
        else if(collision.CompareTag("Player"))
        {
            objectHealth = collision.GetComponent<health>();
            objectHealth.healthAmount -= damagePerSecond;
            healthBar.healthBarImage.fillAmount -= damagePerSecond / 100;
            if (objectHealth.healthAmount <= 0)
            {
                objectHealth.takeDamage(damageThisFrame);
            }
        }

        //Invoke("wait", 2f);
    }
    
    //public void wait()
    //{
    //    objectHealth.healthAmount = damagePerSecond;
    //    healthBar.healthBarImage.fillAmount -= damagePerSecond / 100;
    //}
}
