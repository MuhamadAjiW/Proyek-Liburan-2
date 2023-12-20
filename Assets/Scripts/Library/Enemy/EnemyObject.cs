using System;
using UnityEngine;

public abstract class EnemyObject : DamageableObject, IDamageableEntity{
    protected new void Awake(){
        base.Awake();
        Health = baseHealth * EnemyConfig.GLOBAL_HEALTH_MULTIPLIER;
    }
}