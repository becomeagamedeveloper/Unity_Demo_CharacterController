using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [SerializeField]
    private float _maxSpeed;                //Massima velocità di movimento del character
    private float _movement;                //Input utente
    private bool _isFacingRight = true;     //Per controllare la direzione del giocatore

    private Rigidbody2D _body;

    private Animator _animator;             //Manager delle animazioni in unity

    [SerializeField]
    private GameObject _groundChecker;      
    [SerializeField]
    private LayerMask _whatIsGround;
    [SerializeField]
    private float _roundCheck = 0.2f;
    [SerializeField]
    private float _jumpForce = 200f;

    private bool _ground = false;


        
	// Use this for initialization
	void Start () {

        //Acquisisce una reference al manager delle animazioni in unity
        _animator = GetComponent<Animator>();

        _body = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {


        CheckHorizontalMovement();

        if (_ground && Input.GetAxis("Jump") != 0)
        {
            _body.AddForce(new Vector2(0, _jumpForce));
        }


    }

    private void CheckHorizontalMovement()
    {
        //Input dell'utente [-1, 1]
        _movement = Input.GetAxis("Horizontal");       

        //Se rivolti verso destra, ma movement negativo, cambia direzione
        if (_isFacingRight && _movement < 0)
            Flip();
        //Se rivolti verso sinistra, ma movement positivo, cambia direzione
        else if (!_isFacingRight && _movement > 0)
            Flip();

        //Imposta la condition nell'animator per cambiare animazione
        //Mathf.Abs() restituisce il valore assoluto
        _animator.SetFloat("Speed", Mathf.Abs(_movement));
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

    private void FixedUpdate()
    {
        float x = _movement * _maxSpeed;
        float y = _body.velocity.y;
        _body.velocity = new Vector2(x, y);

        //Partendo da _groundChecker.transform.position
        //Con un raggio pari a _roundCheck
        //Restituisce true se viene trovato almeno un collider il cui layer è contenuto in _whatIsGround
        _ground = Physics2D.OverlapCircle(_groundChecker.transform.position, _roundCheck, _whatIsGround);

        

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Il tag può essere impostato dall'inspector in unity editor
        if(collision.tag == "Gem")
        {
            //Distrugge l'elemento passato come argomento
            Destroy(collision.gameObject);
        }
    }

}
