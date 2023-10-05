using UnityEngine;

public class SnakeCubeShoot : MonoBehaviour
{
   public float timeBtwShots;
   public float startTimeBtwShots;

   public GameObject projectile;
   public Transform spawnPoint;

   private SnakeCubePatrol _snakeCubePatrol;
   
   
   private void Start()
   {
      timeBtwShots = startTimeBtwShots;
      _snakeCubePatrol = GetComponent<SnakeCubePatrol>();
   }

   private void Update()
   {

      if (timeBtwShots <= 1)
      {
         _snakeCubePatrol.isShooting = true;
      }

      if (timeBtwShots >= 2.5f && timeBtwShots <= 3)
      {
         _snakeCubePatrol.isShooting = false;
      }
      
      if (timeBtwShots <= 0)
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
