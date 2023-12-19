using System;
using UnityEngine;

public class Player : DamageableObject, IMovingEntity, IDamageableEntity{
    [SerializeField] private float walkSpeed = 10;
    [SerializeField] private float sprintSpeed = 25;
    [SerializeField] private int jumpLimit = 2;
    [SerializeField] private float jumpForce = 600;
    [SerializeField] private LayerMask ignoreWhileDashing;
    private float snapshotSpeed = 25;

    private PlayerMovement movement;
    private PlayerStance stance;
    private PlayerStateController stateController;
    
    public float SnapshotSpeed{
        set { snapshotSpeed = value; }
    }

    public int JumpLimit => jumpLimit;
    public int State => stateController.State;
    public float JumpForce => jumpForce;

    public float MaxSpeed => State switch{
        PlayerState.WALKING => walkSpeed,
        PlayerState.SPRINTING => sprintSpeed,
        PlayerState.JUMPING => snapshotSpeed,
        PlayerState.FALLING => snapshotSpeed,
        _ => 0,
    };

    private new void Awake(){
        base.Awake();
        movement = new PlayerMovement(this);
        stateController = new PlayerStateController(this);
        stance = new PlayerStance(this, ignoreWhileDashing);
        OnDeath += Death;
        stateController.OnDamageDelayOver += DamageCleared;
    }

    private void Death(){
        Debug.Log("Player is dead");
    }

    private void DamageCleared(){
        if(!Dead){
            SpriteRenderer.color = Color.white;
            Debug.Log("Player is no longer damaged");
        }
    }

    public override float InflictDamage(float damage){
        if(!Dead && !stateController.Damaged){
            SpriteRenderer.color = Color.red;

            Health -= damage;
            InvokeOnDamaged();
            if(Dead) InvokeOnDeath();
            Debug.Log(string.Format("Player remaining health: {0}", Health));
        }
        return Health;
    }

    void Update(){
        refresh();
        movement.Jump();
        stance.Execute();
    }

    void FixedUpdate(){
        // Util.PrintPlayerState(stateController);
        stateController.UpdateState();
        movement.Move();
    }

}
