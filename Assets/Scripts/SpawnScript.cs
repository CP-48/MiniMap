using System.Collections;
using UnityEngine;
using Fusion;

public class SpawnScript : SimulationBehaviour, IPlayerJoined
{
    public static SpawnScript instance;

    public Transform environment;
    public GameObject playerPrefab;
    public float spawnAreaRadius = 10f;

    public GameObject powerUpObject;
    public float powerUpSpeed = 10f;

    public NetworkObject myPlayerObject;
    public Camera mainCamera;

    void Awake()
    {
        instance = this;
    }

    public void PlayerJoined(PlayerRef player)
    {
        if (Runner.LocalPlayer == player)
        {
            Vector3 spawnPosition = GetRandomSpawnPosition();
            StartCoroutine(SpawnPlayerWithDelay(player, spawnPosition));
        }
    }

    IEnumerator SpawnPlayerWithDelay(PlayerRef player, Vector3 spawnPosition)
    {
        yield return new WaitForSeconds(0.1f);

        var gm = Runner.Spawn(playerPrefab, spawnPosition, Quaternion.identity, player);
        gm.transform.SetParent(environment, false);

        gm.GetComponent<PlayerMovement>().Initialize(this);
        if (MiniMapClickHandler.instance) MiniMapClickHandler.instance.isPlayerSpawned = true;

        myPlayerObject = gm;
    }

    public void SpawnPowerUp(Vector3 position, GameObject gameObject)
    {
        var gm = Runner.Spawn(powerUpObject, position);
        gm.transform.parent = environment;
        gm.transform.GetComponent<Rigidbody>().velocity = gameObject.transform.forward * powerUpSpeed;

        //Optional - Use this if you want power up to stay for limited time.
        StartCoroutine(DestroyPowerUp(gm));
    }

    IEnumerator DestroyPowerUp(NetworkObject gm)
    {
        yield return new WaitForSeconds(5f);
        Runner.Despawn(gm);
    }

    Vector3 GetRandomSpawnPosition()
    {
        Vector3 randomPosition = Random.insideUnitSphere * spawnAreaRadius;
        randomPosition.y = 1.16f;
        return randomPosition;
    }
}