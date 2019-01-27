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
            if ((damageTarget.gameObject.tag == "Enemy" && transform.tag == "PlayerDamage") || (transform.tag == "Enemy" && damageTarget.gameObject.tag == "Player"))
            {
                timeCounter += Time.deltaTime;
                while (timeCounter >= secsPerTick)
                {
                    if (numTicks > 0 && ticksOfDamage >= numTicks)
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
	}

    private void OnCollisionEnter(Collision collision)
    {
        print(collision.transform.name);
        Health h = collision.gameObject.GetComponent<Health>();
        if (h != null)
        {
            damageTarget = h;
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (this.gameObject.tag == "PlayerDamage" && collision.gameObject.tag == "Enemy")
        {
            print(collision.name + "trig");
            Health h = collision.gameObject.GetComponent<Health>();
            if (h)
            {
                damageTarget = h;
            }
        }
    }
}
