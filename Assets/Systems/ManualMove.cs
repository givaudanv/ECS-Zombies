using UnityEngine;
using FYFY;

public class ManualMove : FSystem
{
    private Family manualMoveGO = FamilyManager.getFamily(
        new AllOfComponents(typeof(Move), typeof(Shoot)),
        new NoneOfComponents(typeof(Target)));

    protected override void onProcess(int familiesUpdateCount)
    {
        foreach (GameObject go in manualMoveGO)
        {
            Vector3 movement = Vector3.zero;

            if (Input.GetKey(KeyCode.Q))
                movement += Vector3.left;
            if (Input.GetKey(KeyCode.D))
                movement += Vector3.right;
            if (Input.GetKey(KeyCode.Z))
                movement += Vector3.up;
            if (Input.GetKey(KeyCode.S))
                movement += Vector3.down;

            go.transform.position += movement.normalized * go.GetComponent<Move>().speed * Time.deltaTime;

            Vector2 direction = Input.mousePosition - Camera.main.WorldToScreenPoint(go.transform.position);
            float angle = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg) - 90f;
            go.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }
}