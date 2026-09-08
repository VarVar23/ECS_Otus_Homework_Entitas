using Infrastructure;
using UnityEngine;

namespace Gameplay
{
    public class BulletView : EntityMonoView
    {
        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.layer != gameObject.layer)
            {
                var otherEntityMono = other.GetComponent<EntityMonoView>();

                if (otherEntityMono != null)
                {
                    otherEntityMono.Entity.isDestroy = true;
                }    
            }
        }
    }
}