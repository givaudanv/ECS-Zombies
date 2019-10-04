using UnityEngine;
using FYFY;

public class FactorySystem : FSystem
{
    private Family factorySystemGO = FamilyManager.getFamily(
        new AllOfComponents(typeof(Factory)));

    private Family playerGO = FamilyManager.getFamily(
        new AllOfComponents(typeof(Move)),
        new NoneOfComponents(typeof(Target)));

    private Factory factory;
    private GameObject playerTargeted;

    public FactorySystem()
    {
        playerTargeted = playerGO.First();
        factory = factorySystemGO.First().GetComponent<Factory>();
    }

    protected override void onProcess(int familiesUpdateCount)
    {
        factory.reloadProgress += Time.deltaTime;

        if (factory.reloadProgress >= factory.reloadTime)
        {
            GameObject prefabToUse = factory.zombiePrefab;
            if (Random.value < 0.2f)
            {
                prefabToUse = factory.bigZombiePrefab;
            }
            GameObject go = Object.Instantiate(prefabToUse, new Vector3((Random.value - 0.5f) * 19, (Random.value - 0.5f) * 10), Quaternion.identity);
            go.GetComponent<Target>().target = playerTargeted;
            GameObjectManager.bind(go);
            factory.reloadProgress = 0;
        }
    }
}