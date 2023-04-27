using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCollide : MonoBehaviour
{
    [SerializeField] Transform dragonPos;
    [SerializeField] Vector2 xLimit;        // Phạm vi giới hạn trục x giữa wall và dragon
    [SerializeField] Vector2 yLimit;        // Phạm vi giới hạn trục y giữa wall và dragon
    [SerializeField] float x,y;             /* Tính phạm vi hiện tại giữa wall và dragon
                                               (x = xWall - xDragon, y = yWall - yDragon)
                                            */

    [SerializeField] bool isScore = true;
    public bool IsScore { get => isScore; set => isScore = value; }

    private void Start()
    {
        this.dragonPos = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.IsCollide()) this.OnCollide();
    }

    // Kiểm tra Wall có va chạm với Dragon không
    private bool IsCollide()
    {
        x = transform.position.x - this.dragonPos.position.x;
        y = transform.position.y - this.dragonPos.position.y;

        if(x > xLimit.x && x < xLimit.y)
        {
            if (y < yLimit.x || y > yLimit.y) return true;
        }
        else if(x < xLimit.x)
        {
            // Tăng điểm 1 lần
            if (this.isScore)
            {
                GamePlayController.Instance.GetScore();
                this.isScore = false;
            }
        }
        return false;
    }

    // Nếu va chạm thì kết thúc game
    private void OnCollide()
    {
        GamePlayController.Instance.EndGame();
        GameObject.Find("Wall").SetActive(false);
        this.dragonPos.gameObject.SetActive(false);
    }
}
