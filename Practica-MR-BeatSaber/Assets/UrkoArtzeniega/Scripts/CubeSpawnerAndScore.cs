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
    // Almacena la altura inicial de la cabeza (posicion de la camara)
    private float fixedHeadHeight;

    void Start()
    {
        Debug.Log("sableDoble" + PlayerPrefs.GetInt("sableDoble"));
        if (PlayerPrefs.GetInt("sableDoble") == 0)
        {
            Debug.Log("sableDoble" + PlayerPrefs.GetInt("sableDoble"));
            if (sableDoble != null) sableDoble.SetActive(false);
        }
        // Al inicio se guarda la altura actual de la camara
        fixedHeadHeight = Camera.main.transform.position.y;

        int cubeLayer = LayerMask.NameToLayer("Cube");

        if (cubeLayer < 0)
        {
            Debug.LogError("La capa 'Cube' no esta definida. Asegurate de crearla en Project Settings > Tags and Layers.");
            return;
        }

        // Quitamos el bucle restrictivo de colisiones para que funcione con las manos
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
        // Calcula la posicion de spawn
        Vector3 spawnPos = Camera.main.transform.position + Camera.main.transform.forward * spawnDistance;
        spawnPos.y = fixedHeadHeight;

        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.position = spawnPos;

        // Reducir el tamano del cubo
        cube.transform.localScale *= 0.1f;

        cube.layer = LayerMask.NameToLayer("Cube");

        Rigidbody rb = cube.AddComponent<Rigidbody>();
        rb.useGravity = false;
        float offset = Random.Range(-destinationOffsetRange, destinationOffsetRange);

        Vector3 direction = (new Vector3(Camera.main.transform.position.x + offset, Camera.main.transform.position.y, Camera.main.transform.position.z) - spawnPos).normalized;
        rb.linearVelocity = direction * cubeSpeed;

        CubeCollision collisionScript = cube.AddComponent<CubeCollision>();
        collisionScript.gameManager = this;
    }

    public void AddScore(int amount)
    {
        if (score >= PlayerPrefs.GetInt("puntObj"))
        {
            scoreText.text = "FIN";
        }
        else
        {
            score += amount;
            Debug.Log("Score: " + score);
            scoreText.text = score.ToString();
        }
    }
}