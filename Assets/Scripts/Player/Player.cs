using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Player Variables
    [SerializeField]
    private float _speed = 9.5f;

    private int _lives = 3;
    private SpawnManager _spawnManager;

    //Laser Variables
    [SerializeField]
    private GameObject _laserPrefab, _tripleShotPrefab;

    [SerializeField]
    private float _fireRate = 0.2f;
    private float _nextFire = 0.0f;

    //Power Up Variables
    [SerializeField]
    private bool _isTripleShotActive = false;

    [SerializeField]
    private float _speedBoostMultiplier = 2;

    [SerializeField]
    private bool _isShieldActive = false;
    [SerializeField]
    private GameObject _playerShield;

    [SerializeField]
    private int _score;

    private UI_Manager _uiManager;

    [SerializeField]
    private GameObject _rightEngineDamaged, _leftEngineDamaged;

    [SerializeField]
    private GameObject _explosion;

    [SerializeField]
    private AudioSource _laserShot;

    [SerializeField]
    private AudioSource _pickupSound;


    private void Start()
    {
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UI_Manager>();

        if (_spawnManager == null)
        {
            Debug.LogError("The Spawn_Manager is NULL");
        }
        if(_uiManager == null)
        {
            Debug.LogError("The UI Manager is null");
        }
    }

    // Update is called once per frame
    void Update()
    {
        MouseControl();

        CalculateMovement();
    }

    public void Damage()
    {
        if (_isShieldActive)
        {
            _isShieldActive = false;
            _playerShield.SetActive(false);
            return;
        }

        _lives--;
        _uiManager.UpdateLives(_lives);

        switch (_lives)
        {
            case 0:
                Instantiate(_explosion, transform.position, Quaternion.identity);
                _spawnManager.OnPlayerDeath();
                Destroy(this.gameObject);
                break;
            case 1:
                _leftEngineDamaged.SetActive(true);
                break;
            case 2:
                _rightEngineDamaged.SetActive(true);
                break;
        }
    }

    void MouseControl()
    {
        //find the position of the mouse
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 0;
        Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);

        //find the distance of the mouse relative to the player on the x and y axis
        mousePos.x = mousePos.x - objectPos.x;
        mousePos.y = mousePos.y - objectPos.y;

        //determines the angle from the player to the mouse cursor
        float angle = Mathf.Atan2(mousePos.y, mousePos.x);
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, (angle * Mathf.Rad2Deg) + 270));

        //spawn gameobject if left mouse is pressed
        if (Input.GetKey(KeyCode.Mouse0) && Time.time > _nextFire)
        {
            FireLaser(angle);
        }
    }

    void FireLaser(float angle)
    {
        _nextFire = Time.time + _fireRate;

        if (_isTripleShotActive)
        {
            Instantiate(_tripleShotPrefab, transform.position, Quaternion.Euler(new Vector3(0,0, (angle* Mathf.Rad2Deg) + 270)));
        }
        else
        {
            Instantiate(_laserPrefab, transform.position, Quaternion.Euler(new Vector3(0, 0, (angle * Mathf.Rad2Deg) + 270)));
        }

        _laserShot.Play();
    }

    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInupt = Input.GetAxis("Vertical");

        //Players current position
        Vector3 Pos = transform.position;

        Vector3 direction = new Vector3(horizontalInput, verticalInupt, 0);

        //restricts player bounds on the x and y axis
        transform.position = new Vector3(Mathf.Clamp(Pos.x, -9.5f, 9.5f), Mathf.Clamp(Pos.y, -5, 5f), 0);

        

        //Controls speed and direction of movement
        transform.position += Vector3.right * horizontalInput * _speed * Time.deltaTime;
        transform.position += Vector3.up * verticalInupt * _speed * Time.deltaTime;
    }

    public void TripleShotActive()
    {
        _pickupSound.Play();
        _isTripleShotActive = true;
        StartCoroutine(TripleShotPowerDown());
    }

    IEnumerator TripleShotPowerDown()
    {
        yield return new WaitForSeconds(5);
        _isTripleShotActive = false;
    }

    public void SpeedBoostActive()
    {
        _pickupSound.Play();
        _speed = _speed * _speedBoostMultiplier;
        StartCoroutine(SpeedBoostPowerDown());
    }

    IEnumerator SpeedBoostPowerDown()
    {
        yield return new WaitForSeconds(5);
        _speed = _speed / _speedBoostMultiplier;
    }

    public void ShieldActive()
    {
        _pickupSound.Play();
        _isShieldActive = true;
        _playerShield.SetActive(true);
    }

    public void UpdateScore()
    {
        _score += 10;
        _uiManager.NewScore(_score);
    }
}
