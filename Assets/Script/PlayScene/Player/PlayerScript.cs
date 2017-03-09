using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerScript : MonoBehaviour {

    // 이동을 위해서는 스피드
    // 어떤 이동속도로 가겠느냐?
    public float m_fSpeed = 10.0f;
    public float m_fJumpPower = 20.0f;
    public int MaxJumpCount = 3;
    //현재 나의 점프가 몇번이나 남았나
    public int CurJumpCount = 3;


    public Rigidbody m_Rigi = null;

    // 리터럴 초기화가 가장 먼저
    // CurJumpCount = 3;
    // 엔진 초기화
    // CurJumpCount = 5;
    // 시점 함수 초기화
    // CurJumpCount = 3;

    void Awake()
    {

        //MaxJumpcount = 3;
        JumpCountReset();
        //이 스크립트가 들어가 있는 게임 오브젝트의 레퍼런스를 의미
        //gameObject
        //transform

        m_Rigi = gameObject.GetComponent<Rigidbody>();

    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        // 델타타임
        // 1초/ 업데이트 실행횟수
        // 
        //게임은 거의 대부분 운영체제의 한계수준의 처리속력을 요구한다.
        //1초에 대략 3000~5000번을 실행한다.(컴퓨터의 성능에 따라 다르다)
        // update 1번 실행되면 이녀석은 10을 움직이게 된다.
        // 1초에 10을 이동하는 이동속력을 만들고 싶다.
        // 대략 맞출 수 없는 이유 오브젝트가 하나 더 생기면 update의 연산수가 떨어지게 된다.
        // 현실의 시간에 맞춰서 가야한다.
        // 내가 어떤 키를 눌렀다. 조건
        // 어떤 언어든 어떤 구문을 사용하나요 ?

        
        Input.GetKey(KeyCode.LeftArrow);
        //Input.GetKeyDown 눌렀을때
        //Input.GetKeyUp 땟을 때

        //n단 점프

        if ( 0 != CurJumpCount)
        {
            if (true == Input.GetKeyDown(KeyCode.Space))
            {
                // 의심이 더 중요하다
                if ( null != m_Rigi)
                {
                    // addForce 힘을 가한다.
                    // 왜 델타타임을 안 곱해줄까
                    // 중력만 작용하는게 아니라.
                    // 공기저항력
                    // 마찰력
                    // 나의 질량
                    // addForce에는 이러한 요인들을 무시하는 옵션이 존재한다. 함수 오버로딩

                    // 현재 가해지는 힘을 수정할 수 있다.

                    m_Rigi.velocity = Vector3.zero;
                    //new Vector3(0f, 0f, 0f);

                    // 유니티의 물리는 모두 다 기본적으로 
                    // 델타타암의 영향을 받는다.
                    // 이미 시간에 영향을 받고 있다.
                    m_Rigi.AddForce(new Vector3(0f, m_fJumpPower, 0f)
                        , ForceMode.Impulse);
                }
                //기본 로직을 굳혀놓자.
                CurJumpCount = CurJumpCount - 1;
                
            }

            //transform.position += new Vector3(m_fSpeed * Time.deltaTime, 0, 0);
        }

        //if ( true == Input.GetKey(KeyCode.D))
        
            transform.position += new Vector3(m_fSpeed * Time.deltaTime, 0, 0);
        
        //if (true == Input.GetKey(KeyCode.A))
        //{
        //    transform.position += new Vector3(-m_fSpeed * Time.deltaTime, 0, 0);
        //}
        //if (true == Input.GetKey(KeyCode.W))
        //{
        //    transform.position += new Vector3(0, m_fSpeed * Time.deltaTime, 0);
        //}
        //if (true == Input.GetKey(KeyCode.S))
        //{
        //    transform.position += new Vector3(0, -m_fSpeed * Time.deltaTime, 0);
        //}

    }

    void JumpCountReset()
    {
        CurJumpCount = MaxJumpCount;
    }


    // IsTrigger이 세팅되어 있을때와
    // 안되어 있을대의 충돌용 함수가 다르다.
    // 물리적용이 되지 않은 충돌
    void OnTriggerEnter(Collider _col)
    {
        _col.gameObject.SetActive(false);
    }
    //주의사항
    // 이 함수는 리지드 바디를 가진 오브젝트 안에 있는 스크립트에서만 실행된다.
    // 두번째, 컬라이더도 같이 가지고 있어야 실행된다.
    // 세번째, collision을 인자값으로 받아야 한다.
    // Collision _Col 은 나와 충돌한 충돌체에서 세팅한 값이 들어오게 됩니다.
    void OnCollisionEnter(Collision _Col)
    {
        if (9 == _Col.gameObject.layer) {

            //어떤 경우에만 리셋해줘야 한다.
            //내가 부딪힌 곳이 바닥인지 아니면 윗면인지 2가지 방식으로 체크가 가능하다.
            //평면의 방정식(유니티 지원), 
            //직선의 방정식

            // 가장 기본적인 것으로 2개의 벡터가 필요하다. 평면을 구성하기 위해서는
            //
            Plane ChechPlane = new Plane(_Col.gameObject.transform.up, _Col.gameObject.transform.position);
            float Dis = ChechPlane.GetDistanceToPoint(this.transform.position);

            if (Dis > 0f) {
                Debug.Log("윗면");
                JumpCountReset();
            }else
            {
                Debug.Log("아랫면");
            }
            
        }
            
        
        // 게임오브젝트의 이름을 의미한다. (나와 충돌한)
    }

    //처음 충돌했을 때
    //OnCollisionEnter;
    //OntriggerEnter;
    //계속 충돌 중일 때
    //OntriggerStay;
    //충돌에서 최초로 빠져 나갔을 때.
    //OntriggerExit
}
