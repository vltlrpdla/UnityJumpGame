/** 유니티 버전에따라 C#을 지원하는 버전 확인이 필요함
 2015 1학기 윈도우 프로그래밍, C# 2016 1학기 컴퓨터 그래픽스 , opengl
 .netFramework 운영체제의주류 윈도우를 개발하는 miscrosoft에서 계속 개발 
 C#의 최신버전의 기능들이 안될 수 있다.
 **/

 /* created by May Lee 030917 */

// namespace를 using , java의 import와 비슷
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

struct Vec3
{
    float x;
    float y;
    float z;
}

/* 게임 프레임워크의 기초이론
 * 게임에서는 기본적으로 루프를 사용한다.
 * 반복 실행
 * while(true){
 *  if( false == GameObject.bStart){
 *  GameObject.Start();
 *  }
 *  내가 어떤 특정한 기능을 사용하지 않으면 
 *  일시 정지 메뉴창을 열거나.
 *  게임 전체 코드
 * }
 * 
 * why? 내가 조작을 안해도 몬스터는 나를 때리니까
*/


// : 상속 C++은 무조건 public이지만 C#이나 java는 상속에 대한 접근지정자가 정해져 있지 않다.
//이 때 유니티는 이녀석의 메모리를 만들때 클래스의 이름과 파일명을 매칭시켜서 만든다.
//클래스명과 이 클래스를 담고 있는 파일명이 다르다면 
//실행이 안되거나 오작동 할수도 있고,
//기본적으로 오브젝트에 스크립트를 넣어 줄 수 없다.
public class TestScript : MonoBehaviour {

    public TestScript1 m_NewTestScript1 = null;
    public Transform GetTrans = null;
    //리터럴 초기화보다 = null
    //엔진에서의 세팅이 우선된다.
    public GameObject FindObj = null;

    //C# 에서는 프로퍼티라는 문법이 존재합니다.
    //adv ? 
    private int m_TestSCNumber = 10;

    public int TestSCNumber
    {
        get
        {
            return m_TestSCNumber;
        }

        set
        {
            if( 0 > value)
            {
                return;
            }
            m_TestSCNumber = value;
        }
    }

    //메모리에 올라왔을 때의 한번 실행하는 코드
    //유니티의 시점 함수 ; 특정 시점에서 특정 이름의 함수를 유니티 엔진이 스크립트에 있는 함수를 자동으로 실행시켜 준다.
    //이런 시점함수는 오브젝트안에 넣어줘야만 동작한다.
    void Awake()
    {
        //Debug.Log("정말 한번만 메모리에 올라왔을때 글자");

        //아무것도 없는 빈 오브젝트 만드는 법
        //유니티는 씬파일을 통한 씬의 마지막 구성요소를 저장하고 있다. 

        //실행 됐을 때 메모리에 올라온 씬과
        //파일로서의 씬은 다른 것
        //C#의 new라는 것은 레퍼런스가 생긴다.
        //C++에서는 포인터가 생긴다. new 포인터가 기본적인 개념
        // 힙에 올라간 메모리를 직접 안지워도 된다.
        // 가비지 컬렉터라는 것이 레퍼런스 카운팅 방식으로 호출됨
        // 레퍼런스라는 자바는
        // int도 기본 자료형이라고 생각할수 있지만 클래스 개념으로 잡혀 있다. 
        //int Number = 10;
        //Number.ToString();

        //새로운 오브젝트를 만들었다.
        GameObject NewObject = new GameObject();

        //C#은 . 밖에 없다.
        //.으로만 접근이 가능하다.
        //새로운 컴포넌트를 넣어보자
        //자바로치면 제네릭 함수고
        //C++치면 템플릿 함수
        
        Light tempLight = NewObject.AddComponent<Light>();

        if(null != tempLight) { tempLight.type = LightType.Directional; }


        //내가 만든 클래스도 언제든지 넣어줄 수 있다.
        //addComponent가 조금 느리다.
        // 그래서 멤버변수로 해서 시작할때 한번 받아놓고 
        m_NewTestScript1 = NewObject.AddComponent<TestScript1>();
        

        //static 함수 입니다.
        GameObject Obj = GameObject.CreatePrimitive(PrimitiveType.Cube);

        //컴포넌트에 접근하는 법.
        //add가 아니고 get
        //안에 이미 들어와 있는 녀석들 중에서 
        //Transform이라는 녀석이 존재한다면 
        //Transform을 리턴해 달라.
        GetTrans = Obj.GetComponent<Transform>();

        if( null != GetTrans)
        {
            GetTrans.position = new Vector3(2, 2, 2);
        }

        // 트랜스폼은 어떤 오브젝트에 들어가고 , 예를 들면 위치 크기 회전
        // 항상 정말 많이 사용하는 컴포넌트이다.
        // C++ Java component를 
        // 외부에 public 으로 공개해놓은 것 아니냐 ?
        // 멤버변수로 받지 않아도 될정도면 그냥 사용하고
        // 트랜스를 많이 사용할 것 같다. 그러면 선언 해서 사용한다.
        // Getcomponenet<> 느리다.
        // 함수기 때문이다. 스택 사용하지 않는 시점 함수 같은것은 주석처리 하는 게 낫다.
        Obj.transform.position = new Vector3(-2, -2, -2);

        // 분기가 걸리면서 카메라가 물체를 확인한번씩 한다. 잘쓰지 않는다.
        //FindObj = GameObject.Find("Capsule");

        if ( null != FindObj)
        {
            //1.0f opengl에서사용하는 형식 무시자형변환
            FindObj.transform.position = new Vector3(-1, 2 ,3);
        }
    }

    //android의 생명주기와 비슷한 onCreate onStart 이런 개념인 것 같다.
	// Use this for initialization , 이 스크립트가 처음 만들어져서 실행 될 때 한번
	void Start () {
        //눈에 보이는 컴포넌트 대부분 클래스일 경우가 많다.
        //스타트는 한번만 된다.
        //이 오브젝트가 만들어지고 게임에서 메모리상에 올라가게 되면
        //조건에 따라서 한번 실행되게 된다.
        //일반적으로 한번만 실행된다.
        Debug.Log("글자");
	}
	
	// Update is called once per frame
	void Update () {
        // debug 느리다.
        // 충돌보다 더 느리다.
        // 렌더링 보다도 더 느리다.
        // 특정 경우일때만 써야한다.
        //Debug.Log("계속 나오는 글자");

        GetTrans.position += new Vector3(1, 0, 0);

        if (null != m_NewTestScript1)
        {
            m_NewTestScript1.Hp = m_NewTestScript1.Hp - 1;
            Debug.Log(m_NewTestScript1.Hp);

            if (0 == m_NewTestScript1.Hp)
            {
                //모든 스크립트들은 자신의 내부에 
                //자기를 가진 게임오브젝트의 레퍼런스를 가지고 있다.
                //오브젝트를 삭제하는 방법
                //컴포넌트에서 오브젝트에 접근하는 방법

                //GameObject에서 기본적으로 지원해주는 파괴 함수.
                //GameObject.Destroy(m_NewTestScript1.gameObject);
                //넣는 것을 삭제 해준다 컴포넌트 컴포넌트를 통한 오브젝트
                GameObject.Destroy(m_NewTestScript1.gameObject);

                m_NewTestScript1 = null;
            }
        }

    }
}
