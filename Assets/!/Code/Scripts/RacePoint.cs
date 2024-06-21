using UnityEngine;

namespace RaceNS
{
    [RequireComponent(typeof(RCC_Waypoint))]
    public class RacePoint : MonoBehaviour
    {
        [SerializeField]
        private LayerMask processLayerMask;
        [SerializeField]
        private GameObject pointBody;
        [SerializeField]
        private bool addPointToManagerAutomatically;

        private LevelRaceManager levelRaceManager;
        private RCC_SceneManager rCC_SceneManager;
        private RCC_AIWaypointsContainer rCC_AIWaypointsContainer;
        private RCC_Waypoint rCC_Waypoint;
        private bool isInitialized;

        public bool IsActive { get; private set; } = true;

        private void OnEnable()
        {
            if (addPointToManagerAutomatically)
            {
                if (!isInitialized)
                {
                    InitializeVariables();
                    isInitialized = true;
                }
                rCC_AIWaypointsContainer.waypoints.Add(rCC_Waypoint);
                levelRaceManager.AddPoint(this);
            }
        }
        private void OnDisable()
        {
            rCC_AIWaypointsContainer.waypoints.Remove(rCC_Waypoint);
        }
        private void OnTriggerEnter(Collider other)
        {
            if (processLayerMask == (processLayerMask | (1 << other.gameObject.layer)))
            {
                if (other.attachedRigidbody.GetComponent<RCC_CarControllerV3>() == rCC_SceneManager.activePlayerVehicle)
                    levelRaceManager.CrossedPoint(this);
                else
                    levelRaceManager.CrossedPointNoNPlayer(this);
            }
        }
        public void EnablePoint()
        {
            pointBody.gameObject.SetActive(true);
            IsActive = true;
        }
        public void DisablePoint()
        {
            pointBody.gameObject.SetActive(false);
            IsActive = false;
        }
        private void InitializeVariables()
        {
            rCC_SceneManager = FindAnyObjectByType<RCC_SceneManager>();
            rCC_Waypoint = GetComponent<RCC_Waypoint>();
            rCC_AIWaypointsContainer = RCC_AIWaypointsContainer.Instance;
            levelRaceManager = LevelRaceManager.Instance;
        }
    }
}