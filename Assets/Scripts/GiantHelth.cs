using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiantHelth : MonoBehaviour, IHealth
{
    [SerializeField]
    private int startingHealth = 1000;

    private int currentHealth;

    public event Action<float> OnHPPctChanged = delegate { };
    public event Action OnDied = delegate { };

    public int count = 0;
    private float start_time, elapsed_time;

    private void Start()
    {
        currentHealth = startingHealth;
        start_time = Time.deltaTime;
    }

    public float CurrentHpPct
    {
        get { return (float)currentHealth / (float)startingHealth; }
    }

    public void TakeDamage(int amount)
    {
        if (amount <= 0)
        {
            throw new ArgumentOutOfRangeException("How do you take negative damage? ... are you trying to heal them?" + amount);

        }

       
            currentHealth -= amount;

        OnHPPctChanged(CurrentHpPct);


        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void FixedUpdate()
    {
        elapsed_time += Time.deltaTime;
        Debug.Log("elapsed time is " + elapsed_time);
        if (elapsed_time > 30.0f)
        {
            currentHealth = startingHealth;
            OnHPPctChanged(CurrentHpPct);
            elapsed_time = 0f;
            Debug.Log("health restored!");
        }
    }

    private void Die()
    {
        OnDied();
        GameObject.Destroy(this.gameObject);
    }
}
