using UnityEngine;
using FYFY;

public class AutoMove : FSystem
{
    private Family autoMoveTargetedGO = FamilyManager.getFamily(
        new AllOfComponents(typeof(Move), typeof(Target)));

    private Family autoMoveForwardGO = FamilyManager.getFamily(
        new AllOfComponents(typeof(Move)),
        new NoneOfComponents(typeof(Target), typeof(Shoot)));

    protected override void onProcess(int familiesUpdateCount)
    {
        foreach (GameObject go in autoMoveTargetedGO)
        {
            Vector3 posTarget = go.GetComponent<Target>().target.transform.position;
            go.transform.position = Vector3.MoveTowards(go.transform.position, posTarget, go.GetComponent<Move>().speed * Time.deltaTime);
            go.transform.rotation = Quaternion.LookRotation(Vector3.forward, posTarget - go.transform.position);
        }

        foreach (GameObject go in autoMoveForwardGO)
        {
            go.transform.position += go.transform.up * go.GetComponent<Move>().speed * Time.deltaTime;
        }
    }
}