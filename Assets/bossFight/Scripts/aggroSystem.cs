using System;
using UnityEngine;
using UnityEngine.Rendering;

public class aggroSystem : MonoBehaviour
{
    [Header("Variables")]
    [SerializeField] public float aggroScore = 0;
    [SerializeField] private float attackPoint = 0;
    public int attackNumber;

    //void Start()
    //{
        
    //}

    void Update()
    {
        if (CompareTag("Player"))
        {
            aggroScore = attackNumber * attackPoint;
        }
        else if(CompareTag("Summon"))
        {
            aggroScore = attackNumber * attackPoint;
        }
    }
}
