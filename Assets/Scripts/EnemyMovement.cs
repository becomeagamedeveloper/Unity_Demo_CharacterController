using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    [SerializeField]
    private float _speed;
    private Rigidbody2D _body;
    private bool _isFacingRight = false;

    // Use this for initialization
    void Start () {
        _body = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void FixedUpdate()
    {
        float y = _body.velocity.y;
        _body.velocity = new Vector2(_isFacingRight?_speed:-_speed, y);
    }

    //Funzione per cambiare la direzione del player
    private void Flip()
    {
        _isFacingRight = !_isFacingRight;

        //Sistema di coordinate locali del player
        Vector3 scale = transform.localScale;
        //Ribalta l'asse x
        scale.x *= -1;
        transform.localScale = scale;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "PlatformCollider":
                Flip();
                break;
            case "Projectile":
                Destroy(this.gameObject);
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.GetComponent<PlayerController>())
        {
            Destroy(collision.gameObject);
        }
    }
}
