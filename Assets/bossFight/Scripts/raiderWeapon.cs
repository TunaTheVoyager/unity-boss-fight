using System.Collections;
using UnityEngine;

public class raiderWeapon : MonoBehaviour
{
    [SerializeField] private Transform shotPoint;
    [SerializeField] private float damage = 50.0f;
    [SerializeField] private float raycastDistance = 2.3f;
    private Animator raiderAnimator;
    public int shotgunAmmo = 2;
    aggroSystem aggroSystem;

    private void Awake()
    {
        raiderAnimator = GetComponent<Animator>();
        aggroSystem = GetComponent<aggroSystem>();
    }
    void Update()
    {

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
                aggroSystem.attackNumber++;
                shotgunAmmo -= 1;
                if(shotgunAmmo == 0)
                {
                    StartCoroutine(shotgunReload());
                }
            }
        }
    }

    private IEnumerator shotgunReload()
    {
        AnimatorStateInfo animatorStateInfo = raiderAnimator.GetCurrentAnimatorStateInfo(0);
        raiderAnimator.SetInteger("ammo", 0);
        yield return new WaitForSeconds(2f);
        shotgunAmmo = 2;
        raiderAnimator.SetInteger("ammo", 2);
    }

    private void OnDrawGizmos()
    {
       Gizmos.color = Color.yellow;
        if (raiderController.raiderFlip)
        {
            Vector3 endOfTheLine = new Vector3(shotPoint.position.x + -2.8f, shotPoint.position.y, shotPoint.position.z);
            Vector3[] points = { shotPoint.position, endOfTheLine };
            Gizmos.DrawLineStrip(points, false);
        }
       else
        {
            Vector3 endOfTheLine = new Vector3(shotPoint.position.x + 2.8f, shotPoint.position.y, shotPoint.position.z);
            Vector3[] points = { shotPoint.position, endOfTheLine };
            Gizmos.DrawLineStrip(points, false);
        }
    }

}
