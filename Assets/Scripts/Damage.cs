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
    private Health damageTarget = null;

	// Update is called once per frame
	void Update()
    {
        if (damageTarget)
        {
            timeCounter += Time.deltaTime;
            while (timeCounter >= secsPerTick)
            {
                if (damageTarget)
                    damageTarget.DealDamage(damagePerTick);

                timeCounter -= secsPerTick;
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
