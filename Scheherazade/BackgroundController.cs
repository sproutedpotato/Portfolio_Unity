using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    private float spriteWidth;
    private Camera mainCam;

    void Start()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        spriteWidth = sr.bounds.size.x; // 스프라이트의 실제 길이
        mainCam = Camera.main;
    }

    void Update()
    {
        // 카메라의 왼쪽 경계
        float leftEdge = mainCam.transform.position.x - mainCam.orthographicSize * mainCam.aspect;

        // 배경이 왼쪽 화면 바깥으로 완전히 나갔다면
        if (transform.position.x + spriteWidth < leftEdge)
        {
            // 배경을 오른쪽으로 spriteWidth * 2 만큼 이동 (배경 2개 있을 때 기준)
            transform.position += new Vector3(spriteWidth * 2f, 0f, 0f);
        }
    }
}