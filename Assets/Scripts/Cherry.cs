using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cherry : MonoBehaviour {

    [SerializeField]
    private float _projectileSpeed;

    public bool _isFacingRight = true;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (_isFacingRight)
        {
            transform.Translate(Vector2.right * _projectileSpeed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector2.left * _projectileSpeed * Time.deltaTime);
        }
        
	}


    //Funzione per cambiare la direzione del player
    public void Flip()
    {
        _isFacingRight = !_isFacingRight;

        //Sistema di coordinate locali del player
        Vector3 scale = transform.localScale;
        //Ribalta l'asse x
        scale.x *= -1;
        transform.localScale = scale;

    }
}
