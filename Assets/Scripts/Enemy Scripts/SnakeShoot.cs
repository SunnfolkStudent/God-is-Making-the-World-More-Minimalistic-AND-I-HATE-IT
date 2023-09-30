using System;
using UnityEngine;

public class SnakeShoot : MonoBehaviour
{
   public float timeBtwShots;
   public float startTimeBtwShots;

   public GameObject projectile;
   public Transform spawnPoint;

   private SnakePatrol _snakePatrol;

   private void Start()
   {
      timeBtwShots = startTimeBtwShots;
      _snakePatrol = GetComponent<SnakePatrol>();
   }

   private void Update()
   {

      if (timeBtwShots <= 1) { _snakePatrol.isShooting = true; }
      if (timeBtwShots >= 2.5f && timeBtwShots <= 3) { _snakePatrol.isShooting = false; }
      
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
