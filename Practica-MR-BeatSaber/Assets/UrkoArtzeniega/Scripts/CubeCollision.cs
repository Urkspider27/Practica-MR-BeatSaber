using UnityEngine;

public class CubeCollision : MonoBehaviour
{
    public CubeSpawnerAndScore gameManager;

    void OnCollisionEnter(Collision collision)
    {
        // Si choca con la pared, se rompe sin dar puntos
        if (collision.gameObject.name == "Pared" || collision.gameObject.name == "Pared (1)")
        {
            Destroy(gameObject);
        }
        // Si choca da 1 punto
        else if (collision.gameObject.layer != LayerMask.NameToLayer("Cube"))
        {
            gameManager.AddScore(1);
            Destroy(gameObject);
        }
    }
}