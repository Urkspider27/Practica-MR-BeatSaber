using UnityEngine;

public class CubeCollision : MonoBehaviour
{
    public CubeSpawnerAndScore gameManager;

    void OnCollisionEnter(Collision collision)
    {
        // Comprobamos si choca
        string objName = collision.gameObject.name.ToLower();

        if (objName.Contains("hand") || objName.Contains("finger") || objName.Contains("bone") || collision.gameObject.CompareTag("Stick"))
        {
            gameManager.AddScore(1);
            Destroy(gameObject);
        }
    }
}