using System;
using UnityEngine;

public class DamageableObject : RigidObject, IDamageableEntity
{
    [SerializeField] protected float baseHealth = 100;
    private float health;

    public float Health {
        get => health;
        set => health = value > 0? value : 0;
    }
    public bool Dead => health <= 0;
    public event Action OnDeath;
    public event Action OnDamaged;

    protected new void Awake(){
        base.Awake();
        health = baseHealth;
    }
    protected void InvokeOnDeath(){
        OnDeath?.Invoke();
    }
    protected void InvokeOnDamaged(){
        OnDamaged?.Invoke();
    }

    // Overrideables
    public virtual bool Damageable => !Dead;
    public virtual float InflictDamage(float damage){
        Health -= damage;
        InvokeOnDamaged();
        if(Dead) InvokeOnDeath();
        Debug.Log(string.Format("{0} remaining health: {1}", name, Health));

        return Health;
    }
}