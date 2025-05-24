using UnityEngine;
using UnityEngine.Serialization;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform lookAt;
    [SerializeField] private float boundaryX;
    [SerializeField] private float boundaryY;
    private void LateUpdate()
    {
        Vector3 position = lookAt.position - transform.position;
        float positionX = lookAt.position.x - transform.position.x;
        float positionY = lookAt.position.y - transform.position.y;
        
        if ( positionX > boundaryX || positionX < -boundaryX )
        {
            if (transform.position.x < lookAt.position.x)
                position.x = positionX - boundaryX;
            else
                position.x = positionX + boundaryX;
        }
        if ( positionY > boundaryY || positionY < -boundaryY )
        {
            if (transform.position.y < lookAt.position.y)
                position.y = positionY - boundaryY;
            else
                position.y = positionY + boundaryY;
        }
        
        transform.position += new Vector3(position.x, position.y, 0);
        
    }
}
