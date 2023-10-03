using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public float timeBtwShots;
    public float startTimeBtwShots;

    public GameObject projectile;
    public Transform spawnPoint;

    private PlayerCubeMovement _playerCubeMovement;
    private InputManager _inputManager;

    private void Start()
    {
        timeBtwShots = startTimeBtwShots;
        _playerCubeMovement.GetComponent<PlayerCubeMovement>();
        _inputManager.GetComponent<InputManager>();
    }

    private void Update()
    {

        
        if (timeBtwShots <= 0 && _inputManager.attackPressed)
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

