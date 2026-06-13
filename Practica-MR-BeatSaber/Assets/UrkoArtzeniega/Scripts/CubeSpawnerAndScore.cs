using UnityEngine;
using System.Collections;
using TMPro;

public class CubeSpawnerAndScore : MonoBehaviour
{
    public float spawnInterval = 2f;
    public float spawnDistance = 10f;
    public float cubeSpeed = 5f;
    public int score = 0;
    public TMP_Text scoreText;
    public float destinationOffsetRange = 1f;

    public GameObject sableDoble;
    private float fixedHeadHeight;

    void Start()
    {
        if (PlayerPrefs.GetInt("sableDoble") == 0)
        {
            if (sableDoble != null) sableDoble.SetActive(false);
        }

        fixedHeadHeight = Camera.main.transform.position.y;

        int cubeLayer = LayerMask.NameToLayer("Cube");
        if (cubeLayer < 0)
        {
            Debug.LogError("Falta la capa Cube");
            return;
        }

        StartCoroutine(SpawnCubes());
    }

    IEnumerator SpawnCubes()
    {
        while (true)
        {
            SpawnCube();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnCube()
    {
        Vector3 spawnPos = Camera.main.transform.position + Camera.main.transform.forward * spawnDistance;
        spawnPos.y = fixedHeadHeight;

        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.position = spawnPos;
        cube.transform.localScale *= 0.1f;
        cube.layer = LayerMask.NameToLayer("Cube");

        Rigidbody rb = cube.AddComponent<Rigidbody>();
        rb.useGravity = false;

        // Evita que el cubo atraviese las manos en velocidades altas
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;

        float offset = Random.Range(-destinationOffsetRange, destinationOffsetRange);
        Vector3 direction = (new Vector3(Camera.main.transform.position.x + offset, Camera.main.transform.position.y, Camera.main.transform.position.z) - spawnPos).normalized;
        rb.linearVelocity = direction * cubeSpeed;

        CubeCollision collisionScript = cube.AddComponent<CubeCollision>();
        collisionScript.gameManager = this;
    }

    public void AddScore(int amount)
    {
        score += amount;
        scoreText.text = score.ToString();

        // Si se juega la escena suelta el objetivo sera 10 por defecto
        int target = PlayerPrefs.GetInt("puntObj");
        if (target <= 0) target = 10;

        if (score >= target)
        {
            scoreText.text = "FIN";
        }
    }
}