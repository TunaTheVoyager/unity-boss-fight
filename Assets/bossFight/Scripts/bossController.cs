
using UnityEngine;

public class bossController : MonoBehaviour
{
    //Boss Attack
    [SerializeField] private Transform attackPoint;
    [SerializeField] private Vector2 boxSize;
    //[SerializeField] private Vector3 gizmosVector = new Vector3(1f, 1f, 1f);
    [SerializeField] private LayerMask playerLy;
    [SerializeField] private LayerMask summonRaiderLy;
    private LayerMask combinedLayerMask;
    [SerializeField] public float damage;
    private Collider2D[] targets;

    //Player Health Bar
    [SerializeField] private healthBar healthBar;
    [SerializeField] private float damageRatio;

    //Attack Point
    private SpriteRenderer bossSpriteRenderer;
    private Vector3 attackPointOffset;

    private void Awake()
    {
        bossSpriteRenderer = GetComponent<SpriteRenderer>();
        attackPointOffset = attackPoint.localPosition;
    }
    public void bossAttack()
    {
        combinedLayerMask = summonRaiderLy | playerLy;
        targets = Physics2D.OverlapBoxAll(attackPoint.transform.position, boxSize, combinedLayerMask);
        foreach (Collider2D target in targets)
        {
            if(target.CompareTag("Player"))
            {
                target.GetComponent<health>().takeDamage(damage);
                damageRatio = 0.25f;
                healthBar.healthBarImage.fillAmount -= damageRatio;
            }
            else if(target.CompareTag("Summon"))
            {
                target.GetComponent<health>().takeDamage(damage);
                damageRatio = 0.25f;
            }
          
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(attackPoint.transform.position,boxSize);
    }

    private void LateUpdate()
    {
        float x = Mathf.Abs(attackPointOffset.x) * (bossSpriteRenderer.flipX == true ? 1f : -1f);
        attackPoint.localPosition = new Vector3(
            x,
            attackPointOffset.y,
            attackPointOffset.z
        );
    }
}
