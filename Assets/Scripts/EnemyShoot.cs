using System;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
   public float timeBtwShots;
   public float startTimeBtwShots;

   public GameObject projectile;

   private void Start()
   {
      timeBtwShots = startTimeBtwShots;
   }

   private void Update()
   {
      if (timeBtwShots <= 0)
      {
         Instantiate(projectile, transform.position, Quaternion.identity);
         timeBtwShots = startTimeBtwShots;
      }
      else
      {
         timeBtwShots -= Time.deltaTime;
      }
   }
}
