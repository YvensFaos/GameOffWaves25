using UnityEngine;

namespace Actors.Cannon
{
    public class BaseCannon : MonoBehaviour
    {
        [SerializeField]
        private CannonSo cannonData;

        public CannonSo GetCannonSo => cannonData;
    }
}
