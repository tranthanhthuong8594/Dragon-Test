using UnityEngine;

public class WallMove : MonoBehaviour
{
    [SerializeField] float moveSpeed = 2.5f;
    [SerializeField] float minY = -2.2f;
    [SerializeField] float maxY = 2.8f;
    [SerializeField] float oldPosition;

    [SerializeField] CheckCollide checkCollide;

    // Start is called before the first frame update
    void Awake()
    {
        transform.position = new Vector3(transform.position.x, Random.Range(minY, maxY), 0);
        oldPosition = 9f;
        checkCollide = GetComponent<CheckCollide>();
    }

    // Update is called once per frame
    void Update()
    {
        this.Move();
        this.ResetWall();
    }

    private void Move()
    {
        transform.Translate(new Vector3(-1 * moveSpeed * Time.deltaTime, 0, 0));
    }

    private void ResetWall()
    {
        if(transform.position.x < -5f)
        {
            transform.position = new Vector3(oldPosition, Random.Range(minY, maxY), 0);
            checkCollide.IsScore = true;
        }
    }
}
