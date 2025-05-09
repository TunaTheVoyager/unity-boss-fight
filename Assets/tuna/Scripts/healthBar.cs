using UnityEngine;
using UnityEngine.UI;

public class healthBar : MonoBehaviour
{
    public Image healthBarImage;
    [SerializeField]private bossController bossController;

    void Awake()
    {
        healthBarImage = GetComponent<Image>();
    }
    private void Start()
    {
        healthBarImage.fillAmount = 1f;
    }

    void Update()
    {
    }
}
