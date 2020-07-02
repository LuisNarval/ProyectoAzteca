using UnityEngine;
using Unity.Mathematics;

namespace RopeMinikit
{
    public class RopeMouseInteraction : MonoBehaviour
    {
        public Mesh indicatorMesh;
        public Material indicatorMaterial;

        public Rope[] ropes;
        
        protected Rope pulledRope;
        protected int pulledParticle;
        protected float pulledDistance;
        protected float3 currentPosition;
        protected float3 targetPosition;
        
        public void FixedUpdate()
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            
            if (Input.GetMouseButton(0))
            {
                // Mouse down
                if (pulledRope == null)
                {
                    // Not pulling a rope, find the closest one to the mouse
                    var closestRopeIndex = -1;
                    var closestParticleIndex = -1;
                    var closestDistance = 0.0f;
                    var closestDistanceAlongRay = 0.0f;

                    for (int i = 0; i < ropes.Length; i++)
                    {
                        ropes[i].GetClosestParticle(ray, out int particleIndex, out float distance, out float distanceAlongRay);

                        if (distance < closestDistance || i == 0)
                        {
                            closestRopeIndex = i;
                            closestParticleIndex = particleIndex;
                            closestDistance = distance;
                            closestDistanceAlongRay = distanceAlongRay;
                        }
                    }

                    if (closestRopeIndex != -1 && closestParticleIndex != -1 && ropes[closestRopeIndex].GetMassMultiplierAt(closestParticleIndex) > 0.0f)
                    {
                        // Found a rope and particle on the rope, start pulling that particle!
                        pulledRope = ropes[closestRopeIndex];
                        pulledParticle = closestParticleIndex;
                        pulledDistance = closestDistanceAlongRay;
                    }
                }
            }
            else
            {
                // Mouse up
                if (pulledRope != null)
                {
                    // Stop pulling the rope
                    pulledRope.SetMassMultiplierAt(pulledParticle, 1.0f);
                    pulledRope = null;
                }
            }

            if (pulledRope != null)
            {
                // We are pulling the rope

                // Adjust the grab plane
                pulledDistance += Input.mouseScrollDelta.y * 2.0f;

                // Move the rope particle to the mouse position on the grab-plane
                currentPosition = pulledRope.GetPositionAt(pulledParticle);
                targetPosition = ray.GetPoint(pulledDistance);

                pulledRope.SetPositionAt(pulledParticle, targetPosition);
                pulledRope.SetVelocityAt(pulledParticle, float3.zero);
                pulledRope.SetMassMultiplierAt(pulledParticle, 0.0f);
            }
        }

        public void Update()
        {
            if (indicatorMesh == null || indicatorMaterial == null)
            {
                return;
            }

            if (pulledRope != null)
            {
                Graphics.DrawMesh(indicatorMesh, Matrix4x4.TRS(currentPosition, Quaternion.identity, Vector3.one * 0.25f), indicatorMaterial, 0);
            }
        }
    }
}
