using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public float timeBtwShots;
    public float startTimeBtwShots;

    public GameObject projectile;
    public Transform spawnPoint;
    
    private InputCubeManager _inputCubeManager;
    [SerializeField] private Projektile _projektile;

    private void Start()
    {
        timeBtwShots = startTimeBtwShots;
        _inputCubeManager = GetComponent<InputCubeManager>();
        //_projektile = GetComponent<Projektile>();
    }

    private void Update()
    {
        
        if (timeBtwShots <= 0 && _inputCubeManager.attackPressed)
        {
            ShootDirection();
            
            var clone = Instantiate(projectile, spawnPoint.position, Quaternion.identity);
            clone.GetComponent<Projektile>().shootDirection = transform.localScale.x;
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
        _projektile.speed = _inputCubeManager.shootDirection.x * 10;
        _projektile.arc = _inputCubeManager.shootDirection.y * 10;
    }
}