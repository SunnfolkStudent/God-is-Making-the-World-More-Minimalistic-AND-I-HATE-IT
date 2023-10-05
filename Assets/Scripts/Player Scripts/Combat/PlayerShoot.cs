using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public float timeBtwShots;
    public float startTimeBtwShots;

    public GameObject projectile;
    public Transform spawnPoint;
    
    private InputCubeManager _inputCubeManager;
    [SerializeField] private PlayerProjektile _projektile;

    public bool canAttack = false;

    private Vector2 _shootDirection;

    private void Start()
    {
        timeBtwShots = startTimeBtwShots;
        _inputCubeManager = GetComponent<InputCubeManager>();
        //_projektile = GetComponent<Projektile>();
    }

    private void Update()
    {
        
        if (timeBtwShots <= 0 && _inputCubeManager.attackPressed && canAttack)
        {
            ShootDirection();

            var clone = Instantiate(projectile, spawnPoint.position, Quaternion.identity);
            clone.GetComponent<Projektile>();
            Destroy(clone, 5f);
            timeBtwShots = startTimeBtwShots;
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }
    }

    private void ShootDirection()
    {
        
        if ( _inputCubeManager.shootDirection.x != 0 || _inputCubeManager.shootDirection.y * 10 != 0)
        {
            _projektile.speed = _inputCubeManager.shootDirection.x * 10;
            _projektile.arc = _inputCubeManager.shootDirection.y * 10;
        }
        
    }
}