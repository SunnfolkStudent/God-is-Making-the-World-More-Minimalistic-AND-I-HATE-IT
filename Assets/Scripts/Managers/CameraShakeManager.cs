using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Random = UnityEngine.Random;

public class CameraShakeManager : MonoBehaviour
{
   public static CameraShakeManager instance;

   [SerializeField] private float globalShakeForce = 0.3f;

   private float velocityX, velocityY, velocityZ;
   
   private void Awake()
   {
      if (instance == null)
      {
         instance = this;
      }
   }

   public void CameraShake(CinemachineImpulseSource impulseSource)
   {
      globalShakeForce = Random.Range(0, 0.3f);
      velocityX = Random.Range(-1f, 1f); velocityY = Random.Range(-1f, 1f); velocityZ = Random.Range(-1f, 1f);

      impulseSource.m_DefaultVelocity = new Vector3(velocityX, velocityY, velocityZ);
      impulseSource.GenerateImpulseWithForce(globalShakeForce);
   }
}
