using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;

namespace RopeMinikit
{
    public class RopeRigidbodyConnection : MonoBehaviour
    {
        public Rope rope;
        [DisableInPlayMode, Range(0.0f, 1.0f)] public float ropeLocation = 0.0f;
        public bool automaticallyFindRopeLocation = false;
        public new Rigidbody rigidbody;
        public Vector3 localPointOnBody;

        [Tooltip("The amount of the rigidbody velocity to remove when the impulse is from the rope is applied to the rigidbody")]
        [Range(0.0f, 1.0f)] public float rigidbodyDamping = 0.1f;

        [Tooltip("A measure of the stiffness of the connection. Lower values are usually more stable.")]
        [Range(0.0f, 1.0f)] public float stiffness = 1.0f;

        protected int particleIndex;

        public void OnEnable()
        {
            if (rope == null || rigidbody == null)
            {
                return;
            }

            if (automaticallyFindRopeLocation)
            {
                var pointOnBody = rigidbody.transform.TransformPoint(localPointOnBody);
                rope.GetClosestParticle(pointOnBody, out particleIndex, out float distance);
                ropeLocation = rope.GetScalarDistanceAt(particleIndex);
            }
            else
            {
                var ropeDistance = ropeLocation * rope.measurements.realCurveLength;
                particleIndex = rope.GetParticleIndexAt(ropeDistance);
            }
        }

        public void FixedUpdate()
        {
            if (rope == null || rigidbody == null)
            {
                return;
            }

            var pointOnBody = rigidbody.transform.TransformPoint(localPointOnBody);
            rope.RegisterRigidbodyConnection(particleIndex, rigidbody, rigidbodyDamping, pointOnBody, stiffness);
        }

#if UNITY_EDITOR
        public void OnDrawGizmos()
        {
            if (Application.isPlaying)
            {
                return;
            }
            if (rope == null || rigidbody == null || rope.spawnPoints.Count < 2)
            {
                return;
            }

            var pointOnBody = rigidbody.transform.TransformPoint(localPointOnBody);
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(pointOnBody, Vector3.one * 0.05f);

            if (!automaticallyFindRopeLocation)
            {
                var localToWorld = (float4x4)rope.transform.localToWorldMatrix;
                var ropeLength = rope.spawnPoints.GetLengthOfCurve(ref localToWorld);
                rope.spawnPoints.GetPointAlongCurve(ref localToWorld, ropeLength * ropeLocation, out float3 ropePoint);

                Gizmos.DrawWireCube(ropePoint, Vector3.one * 0.05f);
                Gizmos.DrawLine(ropePoint, pointOnBody);
            }
        }
#endif
    }
}
