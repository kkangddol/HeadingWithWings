using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingController : MonoBehaviour
{

    PlayerInfo playerInfo;

    public float FireDelay = 1.0f;             // 미사일 발사 딜레이?(미사일이 날라가는 속도x)
    private bool FireState = true;


    //public GameObject bullet;   //Prefabs을 GameObject로 받는다 아니면 transformComponent로 받아도 된다
    public BulletController bulletController;
    public Transform firePos;
    public AudioClip fireSfx;
    public MeshRenderer muzzleFlash;
    private AudioSource _audio; //언더바를 붙인이유는 audio라는 메소드가 존재했었어서 헷갈리지말라고 ㅇㅇ
    // Start is called before the first frame update
    //Ray에 닿은 객체의 여러가지 충돌 정보를 저장할 변수
    private RaycastHit hit;

    private Light fireLight;

    void Start()
    {
        playerInfo = GetComponent<PlayerInfo>();

        _audio = GetComponent<AudioSource>();
        muzzleFlash.enabled = false;

        fireLight = firePos.Find("PointLight").GetComponent<Light>(); //GameObject.Find는 전체를 탐색 firePos.Find는 firePos의 차일드들에서 탐색
        fireLight.intensity = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(FireState){
            StartCoroutine(FireCycleControl());
            Fire();
        }
    }
    IEnumerator FireCycleControl()
    {
        // 처음에 FireState를 false로 만들고
        FireState = false;
        // FireDelay초 후에
        yield return new WaitForSeconds(FireDelay);
        // FireState를 true로 만든다.
        FireState = true;
    }

    void Fire(){
        //Instantiate 방식에 따라 BulletController에서 AddRelativeForce도 약간 바뀜

        //총알 생성
        //GameObject nowBullet = Instantiate(bullet, firePos.position,firePos.rotation); //불릿을 이위치 이각도에 만들어라
        //nowBullet.GetComponent<BulletController>().damage = playerInfo.damage;
        
        // 총알 생성 컴포넌트 방식
        //BulletController firedBullet = (BulletController)Instantiate(bulletController, firePos.position, firePos.rotation);
        //firedBullet.damage = playerInfo.damage;


        //총알 생성 제너릭 방식 연습
        //BulletController firedBullet = Instantiate<BulletController>(bulletController);
        //firedBullet.damage = playerInfo.damage;
        //firedBullet.transform.SetPositionAndRotation(firePos.position,firePos.rotation);

        //오브젝트 풀 방식
        BulletController firedBullet = BulletPool.GetObject();
        firedBullet.transform.SetPositionAndRotation(firePos.position, firePos.rotation);
        //firedBullet.damage = playerInfo.damage;
        firedBullet.damage = 4;
        firedBullet.GetComponent<Rigidbody>().AddForce(firedBullet.transform.forward * 15f, ForceMode.Impulse);
        firedBullet.ActivateBullet();
        

        //총 소리 발생
        
        //이방식은 소리가 끊어진다
        //_audio.clip = fireSfx;
        //_audio.Play();

        //소리를 중첩해서내게 한다.
        _audio.PlayOneShot(fireSfx,0.5f);   //오디오파일,볼륨

        StartCoroutine(ShowMuzzleFlash());  //코루틴 실행
    }

    IEnumerator ShowMuzzleFlash(){  //이뉴머레이터는 데이터를 순차적으로갖고올때 사용?

        //조명을 활성화
        fireLight.intensity = Random.Range(1.0f,5.0f);

        //offset 값 변경
        Vector2 offset = new Vector2(Random.Range(0,2),Random.Range(0,2)) * 0.5f;
        muzzleFlash.material.SetTextureOffset("_MainTex",offset);   //주요한 텍스쳐를 바로 접근하기위해? _MainTex라고 미리 저장이 되어있대 변수명이 흠..

        //Scale 변경 (컴포넌트).transform
        muzzleFlash.transform.localScale = Vector3.one * Random.Range(0.5f,2.0f);   //localScale local또뭐있었지

        //회전 : quaternion.Euler -> 오일러각을 쿼터니언 타입으로 변환
        Quaternion rot = Quaternion.Euler(0,0,Random.Range(0,360));
        muzzleFlash.transform.localRotation = rot;

        muzzleFlash.enabled = true;
        yield return new WaitForSeconds(0.01f); //yield 양보할게
        muzzleFlash.enabled = false;
        //조명 비활성
        fireLight.intensity = 0.0f;
    }
}