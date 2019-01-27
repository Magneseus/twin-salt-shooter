using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Damage : MonoBehaviour
{
    public int damagePerTick = 1;
    public float secsPerTick = 0.5f;
    public int numTicks = 1;

    private float timeCounter = 0f;
    private int ticksOfDamage = 0;
    private Health damageTarget = null;
    private System.Action OnDamageFinishedCallback = null;


    public void SetOnDamageFinishedCallback(System.Action callback)
    {
        OnDamageFinishedCallback = callback;
    }

    // Update is called once per frame
    void Update()
    {
        if (damageTarget)
        {
            timeCounter += Time.deltaTime;
            while (timeCounter >= secsPerTick)
            {
                if (ticksOfDamage >= numTicks)
                {
                    damageTarget = null;

                    if (OnDamageFinishedCallback != null)
                        OnDamageFinishedCallback();
                }

                if (damageTarget)
                    damageTarget.DealDamage(damagePerTick);

                timeCounter -= secsPerTick;
                ticksOfDamage++;
            }
        }
	}

    private void OnCollisionEnter(Collision collision)
    {
        Health h = collision.gameObject.GetComponent<Health>();
        if (h)
        {
            damageTarget = h;
        }
    }
}
