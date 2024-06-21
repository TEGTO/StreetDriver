using UnityEngine;

namespace MovableObjecStNS
{
    [RequireComponent(typeof(Rigidbody))]
    public class Movable : MonoBehaviour
    {
        [SerializeField]
        private bool changeKinematicState = false;
        [SerializeField]
        private LayerMask proceedLayer;

        private Rigidbody rb;
        private Vector3 startLocalPosition;
        private Quaternion startLocalRotation;
        private bool isActive = true;

        private void OnEnable()
        {
            SetMovableOnStart();
        }
        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            CopyStartTransform();
            SetMovableOnStart();
        }
        private void OnTriggerEnter(Collider other)
        {
            if (changeKinematicState && isActive)
            {
                if ((proceedLayer & (1 << other.gameObject.layer)) != 0)
                {
                    rb.isKinematic = false;
                    isActive = false;
                }
            }
        }
        private void SetMovableOnStart()
        {
            transform.SetLocalPositionAndRotation(startLocalPosition, startLocalRotation);
            if (changeKinematicState)
                rb.isKinematic = true;
            isActive = true;
        }
        private void CopyStartTransform()
        {
            startLocalPosition = transform.localPosition;
            startLocalRotation = transform.localRotation;
        }
    }
}