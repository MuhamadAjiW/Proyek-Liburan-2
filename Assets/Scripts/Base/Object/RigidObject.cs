using UnityEngine;

public class RigidObject : MonoBehaviour{
    private Rigidbody2D rigidBody;
    new private Collider2D collider;
    private SpriteRenderer sprite;

    private bool grounded = false;

    public bool Grounded => grounded;
    public Rigidbody2D Rigidbody => rigidBody;
    public Collider2D Collider => collider;
    public SpriteRenderer SpriteRenderer => sprite;
    public Vector2 Position => transform.position;

    public void Awake(){
        rigidBody = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        collider = GetComponent<Collider2D>();
    }


    private void OnCollisionEnter2D(Collision2D collision){
        if (collision.gameObject.CompareTag(EnvironmentConfig.GROUND_TAG) && !grounded){
            grounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision){
        if (collision.gameObject.CompareTag(EnvironmentConfig.GROUND_TAG) && grounded){
            grounded = false;
        }
    }

    protected void refresh(){
        Rigidbody.AddForce(Vector2.zero);
    }
}