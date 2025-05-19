using UnityEngine;

public class raiderWeapon : MonoBehaviour
{
    [SerializeField] private Transform shotPoint;
    [SerializeField] private float damage = 50.0f;
    [SerializeField] private float raycastDistance = 2.3f;
    private Animator raiderAnimator;

    private void Awake()
    {
        raiderAnimator = GetComponent<Animator>();
    }
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            raiderAnimator.SetTrigger("attacking");
            raiderShoot();
        }
    }

    public void raiderShoot()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(shotPoint.position, shotPoint.right, raycastDistance);
        if (hitInfo.collider != null && hitInfo.collider.CompareTag("Boss"))
        {
            health enemy = hitInfo.transform.GetComponent<health>();
            if (enemy != null)
            {
                enemy.takeDamage(damage);
            }
        }
    }

    private void OnDrawGizmos()
    {
       Gizmos.color = Color.yellow;
       Vector3 endOfTheLine = new Vector3(shotPoint.position.x + 2.3f,shotPoint.position.y,shotPoint.position.z);
       Vector3[] points = { shotPoint.position, endOfTheLine };
       Gizmos.DrawLineStrip(points, false);
    }

}
