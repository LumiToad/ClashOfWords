
using UnityEngine;

namespace Clash_Of_Words.Destructable
{
    public class Destructable : MonoBehaviour
    {
        protected virtual void OnHit(HitBoxBase hitBox)
        {
            Destruction();
        }

        protected virtual void Destruction()
        {
            Destroy(gameObject);
        }
    }
}
