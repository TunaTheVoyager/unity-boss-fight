using UnityEngine;

public class bossController : MonoBehaviour
{
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float radius;
    [SerializeField] private Vector3 vector3 = new Vector3(1f, 1f, 1f);
    [SerializeField] private LayerMask playerLy;
    [SerializeField] public float damage;

    [SerializeField] private healthBar healthBar;
    private float damageRatio;

    private SpriteRenderer bossSpriteRenderer;
    private Vector3 attackPointOffset;

    private void Awake()
    {
        bossSpriteRenderer = GetComponent<SpriteRenderer>();
        attackPointOffset = attackPoint.localPosition;
    }
    public void characterAttack()
    {
        Collider2D[] player = Physics2D.OverlapCircleAll(attackPoint.transform.position, radius, playerLy);

        foreach (Collider2D colliderPlayer in player)
        {
            Debug.Log("Hit");
            colliderPlayer.GetComponent<health>().takeDamage(damage);
            damageRatio = 0.25f;
            healthBar.healthBarImage.fillAmount -= damageRatio;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(attackPoint.transform.position,vector3);
    }

    private void LateUpdate()
    {
        // AttackPoint’i her frame, doðru tarafa taþý:
        float x = Mathf.Abs(attackPointOffset.x) * (bossSpriteRenderer.flipX == true ? 1f : -1f);
        attackPoint.localPosition = new Vector3(
            x,
            attackPointOffset.y,
            attackPointOffset.z
        );
    }
}
