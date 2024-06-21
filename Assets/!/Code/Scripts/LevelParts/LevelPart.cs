using UnityEngine;

namespace LevelPartsNS
{
    public class LevelPart : MonoBehaviour
    {
        [SerializeField]
        private StartPartTrigger startPartTrigger;
        [SerializeField]
        private EndPartTrigger endPartTrigger;

        public StartPartTrigger StartPartTrigger { get => startPartTrigger; }
        public EndPartTrigger EndPartTrigger { get => endPartTrigger; }
    }
}