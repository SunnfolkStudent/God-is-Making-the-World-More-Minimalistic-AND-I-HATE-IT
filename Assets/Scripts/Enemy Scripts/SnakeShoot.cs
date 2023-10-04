using UnityEngine;

public class SnakeShoot : MonoBehaviour
{
   public float timeBtwShots;
   public float startTimeBtwShots;

   public GameObject projectile;
   public Transform spawnPoint;

   private SnakePatrol _snakePatrol;

   private Animator animator;
   
   private void Start()
   {
      timeBtwShots = startTimeBtwShots;
      _snakePatrol = GetComponent<SnakePatrol>();
      animator = GetComponent<Animator>();
   }

   private void Update()
   {

      if (timeBtwShots <= 1)
      {
         _snakePatrol.isShooting = true;
         animator.SetBool("isAttacking", true);
      }

      if (timeBtwShots >= 2.5f && timeBtwShots <= 3)
      {
         _snakePatrol.isShooting = false;
         animator.SetBool("isAttacking", false);
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
