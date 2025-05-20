using UnityEngine;

public class raiderController : MonoBehaviour
{
    private Animator raiderAnimator;
    private Transform bossPosition;
    private Transform raiderPosition;
    public static bool raiderFlip;

    void Start()
    {
        raiderAnimator = GetComponent<Animator>();
        bossPosition = GameObject.FindGameObjectWithTag("Boss").transform;
        raiderPosition = GetComponent<Transform>();
    }

    void Update()
    {
        if (bossPosition.position.x < raiderPosition.position.x)
        {
            raiderPosition.rotation = Quaternion.Euler(0, 180, 0);
            raiderFlip = true;
        }
        else if (bossPosition.position.x > raiderPosition.position.x)
        {
            raiderPosition.rotation = Quaternion.Euler(0, 0, 0);
            raiderFlip = false;
        }
    }
}
