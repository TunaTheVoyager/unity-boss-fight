using UnityEngine;

public class bossController : MonoBehaviour
{
    //Boss Attack
    [SerializeField] private Transform attackPoint;
    [SerializeField] private Vector2 boxSize;
    //[SerializeField] private Vector3 gizmosVector = new Vector3(1f, 1f, 1f);
    [SerializeField] private LayerMask playerLy;
    [SerializeField] public float damage;
    private Collider2D[] player;

    //Player Health Bar
    [SerializeField] private healthBar healthBar;
    private float damageRatio;

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
        player = Physics2D.OverlapBoxAll(attackPoint.transform.position, boxSize, playerLy);

        foreach (Collider2D colliderPlayer in player)
        {
            colliderPlayer.GetComponent<health>().takeDamage(damage);
            damageRatio = 0.25f;
            healthBar.healthBarImage.fillAmount -= damageRatio;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(attackPoint.transform.position,boxSize);
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
