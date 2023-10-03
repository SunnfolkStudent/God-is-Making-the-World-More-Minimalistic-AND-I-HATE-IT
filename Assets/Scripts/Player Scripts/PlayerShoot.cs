using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public float timeBtwShots;
    public float startTimeBtwShots;

    public GameObject projectile;
    public Transform spawnPoint;

    private PlayerCubeMovement _playerCubeMovement;
    private InputCubeManager _inputCubeManager;

    private void Start()
    {
        timeBtwShots = startTimeBtwShots;
        _playerCubeMovement = GetComponent<PlayerCubeMovement>();
        _inputCubeManager = GetComponent<InputCubeManager>();
    }

    private void Update()
    {
        
        if (timeBtwShots <= 0 && _inputCubeManager.attackPressed)
        {
         
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
}