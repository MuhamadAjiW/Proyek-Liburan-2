using System;
using UnityEngine;

public class DamagingHitbox : DamagingObject{
    void OnTriggerStay2D(Collider2D otherCollider){
        Debug.Log(string.Format("Collision in hitbox of {0} by {1}", transform.name, otherCollider.transform.name));
        if(otherCollider.transform.TryGetComponent<IDamageableEntity>(out var damageableEntity))
        
        if(damageableEntity.Damageable){
            damageableEntity.InflictDamage(Damage);
            InvokeOnDamage();

            if(otherCollider.transform.TryGetComponent<RigidObject>(out var rigidObject)) Knock(rigidObject.Rigidbody);
        }
    }
}