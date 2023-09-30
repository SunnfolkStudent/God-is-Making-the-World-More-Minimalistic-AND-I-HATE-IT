using System;
using UnityEngine;

public class SnakeShoot : MonoBehaviour
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
         var clone = Instantiate(projectile, transform.position, Quaternion.identity);
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
