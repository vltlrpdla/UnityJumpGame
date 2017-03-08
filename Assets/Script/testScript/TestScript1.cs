/** 유니티 버전에따라 C#을 지원하는 버전 확인이 필요함
 2015 1학기 윈도우 프로그래밍, C# 2016 1학기 컴퓨터 그래픽스 , opengl
 .netFramework 운영체제의주류 윈도우를 개발하는 miscrosoft에서 계속 개발 
 C#의 최신버전의 기능들이 안될 수 있다.
 **/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript1 : MonoBehaviour
{
    //접근 제한 지정자.
    //리터럴 초기화가 된다. C++ 11에서는 
    public int Hp = 500;

    //어트리뷰트라는 문법이 존재한다.
    //private 이면서도 엔진 공개가 가능해진다.
    //C#의 어트리뷰트라는 문법.
    [SerializeField]
    private int PrivateInt = 999;

    void Awake()
    {
        TestScript Number = null;

        if ( null != Number)
        {
            //함수가 아닌 것 처럼 보인다. 외부에서 볼때는 변수처럼 보인다.
            Number.TestSCNumber = 100;
        }
    }

}