using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private float _speed = 12f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Constantly moves laser on the y axis
        transform.Translate(Vector3.up * _speed * Time.deltaTime);

        //Destroys the laser prefab gameObject when it moves out of bounds
        if (transform.position.y > 8 || transform.position.y < -8 || transform.position.x < -12 || transform.position.x > 12)
        {
            if(transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            Destroy(this.gameObject);
        }
    }
}
