using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{

    public Transform target;
    public float distance = 5.0f;   //타겟과의 거리
    public float height = 5.0f; //타겟과의 높이차

    public float damping = 3.0f;    //추적할때의 민감도
    private Transform tr;


    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<Transform>();
    }

    //이게 무사에 있는 업데이트보다 먼저 실행되게되면 떨림현상이 발생하므로 그것을 방지하기 위해 LateUpdate를 사용한다
    void LateUpdate()
    {
        Vector3 pos = target.position //- tr.position   //주인공의 위치
                            + (Vector3.back * distance)    //카메라의 위치를 X축 이동
                            + (Vector3.up * height);        //카메라의 위치를 Y축 이동

        tr.position = Vector3.Lerp(tr.position,pos,Time.deltaTime * damping);
        //카메라가 주인공을 바라본다. 주인공을 향해서 바라보도록 회전
        //tr.LookAt(target.position);
    }
}
