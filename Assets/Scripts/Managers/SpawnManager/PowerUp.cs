using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField]
    private float _speed = 1.5f;

    //Power Up IDs
    // 0 = Triple Shot
    // 1 = Speed
    // 2 = Shield
    [SerializeField]
    private int _powerUpId;
    
    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(0, -1, 0) * _speed * Time.deltaTime);

        if(transform.position.y < -8)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player player = collision.GetComponent<Player>();

            if(player != null)
            {
                switch (_powerUpId)
                {
                    case 0:
                        player.TripleShotActive();
                        break;
                    case 1:
                        player.SpeedBoostActive();
                        break;
                    case 2:
                        player.ShieldActive();
                        break;
                    default:
                        Debug.Log("Default Case");
                        break;
                }
            }

            Destroy(this.gameObject);
        }
    }
}
