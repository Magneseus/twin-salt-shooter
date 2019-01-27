using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int curHealth;
    public int maxHealth;
    public bool IsDead { get { return curHealth <= 0; } }

    private System.Action OnDeathCallback = null;

    // Use this for initialization
    void Start()
    {

    }

    public void DealDamage(int damage)
    {
        curHealth -= damage;
        
        if (curHealth <= 0)
        {
            curHealth = 0;
            if (OnDeathCallback != null)
                OnDeathCallback();
        }
    }

    public void SetOnDeathCallback(System.Action callback)
    {
        OnDeathCallback = callback;
    }
}
