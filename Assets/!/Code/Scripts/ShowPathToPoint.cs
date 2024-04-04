using DG.Tweening;
using UnityEngine;

namespace RaceNS
{
    public class ShowPathToPoint : MonoBehaviour
    {
        [SerializeField]
        private GameObject pointer;
        [SerializeField]
        private AxisConstraint axisConstraint;
        [SerializeField]
        private float updateDuration;

        private LevelRaceManager levelRaceManager;

        private void Start()
        {
            levelRaceManager = LevelRaceManager.Instance;
        }
        private void LateUpdate()
        {
            RotateTowardsTarget();
        }
        private void RotateTowardsTarget()
        {
            RacePoint racePoint = levelRaceManager.CurrentPoint;
            if (racePoint != null)
            {
                Vector3 lookTo = racePoint.transform.position;
                pointer.transform.DODynamicLookAt(lookTo, updateDuration, axisConstraint)
                 .OnComplete(RotateTowardsTarget);
            }
        }
    }
}