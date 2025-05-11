using NUnit.Framework;
using UnityEngine;

[CreateAssetMenu(menuName = "Player Movements")]
public class characterStats : ScriptableObject
{
    [Header("Walk")]
    [UnityEngine.Range(1f, 100f)] public float walkSpeed = 12.5f;
    [Header("Dash")]
    [UnityEngine.Range(1f, 100f)] public float dashingPower = 24f;
    [UnityEngine.Range(0f, 10f)] public float dashingTime = 0.2f;
    [UnityEngine.Range(0f,10f)] public float dashingCoolDown = 1f;
}
