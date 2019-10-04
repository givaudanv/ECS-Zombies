using UnityEngine;
using FYFY;

public class ShootSystem : FSystem
{
    private Family playerGO = FamilyManager.getFamily(
        new AllOfComponents(typeof(Move), typeof(Shoot)),
        new NoneOfComponents(typeof(Target)));

    private Shoot shoot;

    public ShootSystem()
    {
        shoot = playerGO.First().GetComponent<Shoot>();
    }

    protected override void onProcess(int familiesUpdateCount)
    {
        foreach (GameObject go in playerGO)
        {
            shoot.reloadProgress += Time.deltaTime;

            if (shoot.reloadProgress >= shoot.reloadTime)
            {
                if (Input.GetMouseButton(0))
                {
                    GameObject shot = Object.Instantiate(shoot.bulletPrefab, go.transform.position, go.transform.rotation);
                    GameObjectManager.bind(shot);
                    shoot.reloadProgress = 0;
                }
            }
        }
    }
}