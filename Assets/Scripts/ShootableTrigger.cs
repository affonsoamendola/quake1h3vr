using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

#if H3VR_IMPORTED
using FistVR;
#endif

public class ShootableTrigger : MonoBehaviour 
#if H3VR_IMPORTED
    , IFVRDamageable
#endif
{
	public UnityEvent trigger;

#if H3VR_IMPORTED
    // Event for when we are damaged by something in the game
    void IFVRDamageable.Damage(Damage dam)
    {
        // If we're not set or damage was non-projectile, ignore.
        if (dam.Class != Damage.DamageClass.Projectile) return;

        trigger.Invoke();
    }
#endif
}
