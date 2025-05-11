using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class cooldownsUI : MonoBehaviour
{
    [SerializeField]private characterController characterController;
    [SerializeField]private characterStats characterStats;
    [SerializeField]private Image dashImage;
    void Awake()
    {
        dashImage = GetComponent<Image>();
    }
    private void Start()
    {
        dashImage.fillAmount = 1f;
    }

    public IEnumerator wait()
    {
        float timer = 0f;
        dashImage.fillAmount = 0f;

        while (timer <= characterStats.dashingCoolDown)
        {
            timer += Time.deltaTime;
            dashImage.fillAmount = Mathf.Clamp01(timer / characterStats.dashingCoolDown);
            yield return null;
        }
    }
}
