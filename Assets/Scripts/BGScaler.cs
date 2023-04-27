using UnityEngine;

public class BGScaler : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        Vector3 tempScale = transform.localScale;   //1,1,1

        float height = sr.bounds.size.y;    //10.24
        float width = sr.bounds.size.x;     //10.24

        float worldHeight = Camera.main.orthographicSize * 2f;      //5*2=10
        float worldWidth = worldHeight * Screen.width / Screen.height;      //10*1440/2960=4.8648

        tempScale.y = worldHeight / height;     //10/10.24=0.976
        tempScale.x = worldWidth / width;       //4.8648/10.24=0.475

        transform.localScale = tempScale;
    }
}
