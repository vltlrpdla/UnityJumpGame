using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaffoldingParent : MonoBehaviour {


    //판이 몇개가 생겨야 할까?
    //C#에서 배열이란 무조건 동적 배열
    // 그 자체도 이미 클래스이다.

    public int ScaffoldingCount = 30;

    public GameObject ScaffoldingPrefab = null;

    public GameObject[] ArrScaffolding = null;

    public Vector3 LastScaffoldingPos = Vector3.zero;
    public GameObject Player = null;



    public int CoinCount = 0;
    public GameObject CoinPrefab = null;
    public GameObject[] ArrCoin = null;
     
    void Awake () {

        Player = GameObject.Find("Player");

        // 리소스라는 클래스는 이미 알고 있다
        // 어차피 할 수 있는게 리소스 폴더 아래있는 녀석들만 로드 할 수 있다.
        ScaffoldingPrefab = Resources.Load<GameObject>("Prefab/ActiveScaffolding");
        CoinPrefab = Resources.Load<GameObject>("Prefab/Coin");

        if ( null == ScaffoldingPrefab)
        {
            return;
        }

        if (null == CoinPrefab)
        {
            return;
        }

        if ( null == Player)
        {
            return;
        }

        LastScaffoldingPos = Player.transform.position;

        //GameObj를 30개 만들었다는 것이 아니다.
        //Gameobj를 담을 수 있는 30개의 칸을 만들어 놨다.
        ArrScaffolding = new GameObject[ScaffoldingCount];

        for ( int i = 0;  i < ArrScaffolding.Length ; i++)
        {
            ArrScaffolding[i] = GameObject.Instantiate<GameObject>(ScaffoldingPrefab);

            //부모자식관계는 오브젝트간의 관계라기 보다는
            //트랜스폼의 관계에 더 가깝다.
            ArrScaffolding[i].transform.SetParent(this.transform);
            //가시적인 기능, 물리적인 것도 꺼진다.
            ArrScaffolding[i].SetActive(false);
        }

        CoinCount = ScaffoldingCount * 9;

        ArrCoin = new GameObject[CoinCount];


        for (int i = 0; i < ArrCoin.Length; i++)
        {
            ArrCoin[i] = GameObject.Instantiate<GameObject>(CoinPrefab);

            //부모자식관계는 오브젝트간의 관계라기 보다는
            //트랜스폼의 관계에 더 가깝다.
            ArrCoin[i].transform.SetParent(this.transform);
            //가시적인 기능, 물리적인 것도 꺼진다.
            ArrCoin[i].SetActive(false);
        }

    }

    //플레이어의 위치를 계속 파악하면서 
    //판을 앞에 하나씩 놔줘야 한다.
    //마찬가지로 그때마다 뒤에 있는 판을 하나씩 앞으로 옮겨야 한다.

    int m_CurActiveScaffolding = 0;
    int m_CurActiveCoin = 0;

    void Update()
    {
        if (LastScaffoldingPos.x <= Player.transform.position.x + 20f)
        {
            ArrScaffolding[m_CurActiveScaffolding].SetActive(true);

            

            Vector3 RandomPos;

            RandomPos.x = LastScaffoldingPos.x + Random.Range(8f, 20f);
            RandomPos.y = Random.Range(-4f, 4f);
            RandomPos.z = 0f;

            Vector3 RandomRot;

            RandomRot.x = 0f;
            RandomRot.y = 0f;
            RandomRot.z = Random.Range(-40f, 40f);

            ArrScaffolding[m_CurActiveScaffolding].transform.position = RandomPos;
            //유니티의 회전값은 기본적으로 사원수인 쿼터니온을 사용한다.
            ArrScaffolding[m_CurActiveScaffolding].transform.eulerAngles = RandomRot;
            LastScaffoldingPos = RandomPos;

            Vector3 StartScaffoldingCoinPos = LastScaffoldingPos;

            StartScaffoldingCoinPos += ArrScaffolding[m_CurActiveScaffolding].transform.up;
            StartScaffoldingCoinPos -= ArrScaffolding[m_CurActiveScaffolding].transform.right * 4;

            for (int i = 0; i < 9; i++)
            {
                ArrCoin[m_CurActiveCoin].SetActive(true);
                ArrCoin[m_CurActiveCoin].transform.position = StartScaffoldingCoinPos;
                StartScaffoldingCoinPos += ArrScaffolding[m_CurActiveScaffolding].transform.right;
                m_CurActiveCoin += 1;
            }


            m_CurActiveCoin += 1;
            m_CurActiveScaffolding += 1;

            if (m_CurActiveScaffolding >= ScaffoldingCount)
            {
                m_CurActiveScaffolding = 0;
            }

            if ( m_CurActiveCoin >= CoinCount)
            {
                m_CurActiveCoin = 0;
            }
        }
    }
	
}
