using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public class ECSManager : MonoBehaviour
{
    public static EntityManager entityManager;
    public GameObject virusPrefab;
    public GameObject bloodPrefab;
    public GameObject bulletPrefab;
    public GameObject whitePrefab;
    public GameObject player;

    int numVirus = 5000;
    int numBlood = 50000;
    int numBullets = 10;
    BlobAssetStore store;

    Entity bullet;
    public static Entity whiteBlood;


    // Start is called before the first frame update
    void Start()
    {
        store = new BlobAssetStore();
        entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
        var settings = GameObjectConversionSettings.FromWorld(World.DefaultGameObjectInjectionWorld, store);
        Entity virus = GameObjectConversionUtility.ConvertGameObjectHierarchy(virusPrefab, settings);
        Entity blood = GameObjectConversionUtility.ConvertGameObjectHierarchy(bloodPrefab, settings);
        bullet = GameObjectConversionUtility.ConvertGameObjectHierarchy(bulletPrefab, settings);
        whiteBlood = GameObjectConversionUtility.ConvertGameObjectHierarchy(whitePrefab, settings);

        for (int i=0; i < numVirus; i++)
        {
            var instance = entityManager.Instantiate(virus);
            float x = UnityEngine.Random.Range(-100, 100);
            float y = UnityEngine.Random.Range(-100, 100);
            float z = UnityEngine.Random.Range(-100, 100);

            float3 position = new float3(x, y, z);
            entityManager.SetComponentData(instance, new Translation { Value = position });
            entityManager.SetComponentData(instance, new VirusData { alive = true });

            float rSpeed = UnityEngine.Random.Range(1, 10) / 10.0f;
            entityManager.SetComponentData(instance, new FloatData { speed = rSpeed });
        }

        for (int i = 0; i < numBlood; i++)
        {
            var instance = entityManager.Instantiate(blood);
            float x = UnityEngine.Random.Range(-100, 100);
            float y = UnityEngine.Random.Range(-100, 100);
            float z = UnityEngine.Random.Range(-100, 100);

            float3 position = new float3(x, y, z);
            entityManager.SetComponentData(instance, new Translation { Value = position });

            float rSpeed = UnityEngine.Random.Range(1, 10) / 20.0f;
            entityManager.SetComponentData(instance, new FloatData { speed = rSpeed });
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            for(int i = 0; i < numBullets; i++)
            {
                var instance = entityManager.Instantiate(bullet);
                var startPos = player.transform.position + UnityEngine.Random.insideUnitSphere * 2;
                entityManager.SetComponentData(instance, new Translation { Value = startPos });
                entityManager.SetComponentData(instance, new Rotation { Value = player.transform.rotation });
            }
        }
    }

    private void OnDestroy()
    {
        store.Dispose();
    }
}
