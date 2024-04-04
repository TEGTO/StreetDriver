using UnityEngine;

namespace RaceNS
{
    public class RacePoint : MonoBehaviour
    {
        [SerializeField]
        private LayerMask processLayerMask;

        private LevelRaceManager levelRaceManager;
        private RCC_SceneManager rCC_SceneManager;

        private void Start()
        {
            levelRaceManager = LevelRaceManager.Instance;
            rCC_SceneManager = FindAnyObjectByType<RCC_SceneManager>();
        }
        private void OnTriggerEnter(Collider other)
        {
            if (processLayerMask == (processLayerMask | (1 << other.gameObject.layer)))
            {
                if (other.attachedRigidbody.GetComponent<RCC_CarControllerV3>() == rCC_SceneManager.activePlayerVehicle)
                    levelRaceManager.CrossedPoint(this);
            }
        }
        public void EnablePoint()
        {
            gameObject.SetActive(true);
        }
        public void DisablePoint()
        {
            gameObject.SetActive(false);
        }
    }
}