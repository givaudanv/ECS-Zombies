using UnityEngine;
using FYFY;
using FYFY_plugins.TriggerManager;

public class ZombieHit : FSystem
{
    private Family triggeredGO = FamilyManager.getFamily(
        new AllOfComponents(typeof(Triggered2D), typeof(Target)));

    protected override void onProcess(int familiesUpdateCount)
    {
        foreach (GameObject go in triggeredGO)
        {
            Triggered2D t2d = go.GetComponent<Triggered2D>();

            foreach (GameObject target in t2d.Targets)
            {
                target.GetComponent<Health>().hp -= go.GetComponent<Damage>().dmg;
                if (target.GetComponent<Health>().hp <= 0)
                {
                    GameObjectManager.unbind(target);
                    Object.Destroy(target);
                }

                target.transform.position += go.transform.up.normalized * 0.6f;
            }
        }
    }
}