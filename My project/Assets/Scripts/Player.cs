using System;
using UnityEngine;

public sealed class Player : ITakeDamage
{
    public event Action OnIncreasedScore = null;
    public event Action OnPlayerDied = null;
    public event Action OnTakedDamage = null;

    public int health = 3;
    public uint Score { get; private set; } = 0;

    public void IncreaseScore(uint value)
    {
        Score += value;
        OnIncreasedScore?.Invoke();
    }

    public void TakeDamage(int damage)
    {
        if (health > 0)
        {
            health -= damage;
            OnTakedDamage?.Invoke();

            if (health <= 0)
            {
                Die();
            }
        }
    }

    private void Die()
    {
        Debug.Log("Player died!");
        OnPlayerDied?.Invoke();
    }
}