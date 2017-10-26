using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class sbAI : MonoBehaviour
{
    public Transform solider1;
    public Transform solider2;
    float m_soliderRate = 0;
    public float m_life = 10;
    //士兵能量点
    public int SoliderPoint, sp;
    //创建士兵标识符
    public Transform SoliderClass;
    // Use this for initialization



    //HP条部分
    private GameObject hp_bar;
    private Slider HPP;
    void NewHP()
    {

        hp_bar = Instantiate((GameObject)Resources.Load("Prefabs/NeutralTower_HP"));
        hp_bar.transform.rotation = Camera.main.transform.rotation;
        hp_bar.transform.SetParent(gameObject.transform);
        hp_bar.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 90, gameObject.transform.position.z+20);
        HPP = hp_bar.GetComponentInChildren<Slider>();
        HPP.maxValue = m_life;
        HPP.value = m_life;

    }
    void SetHP()
    {
        HPP.value = m_life;
        SetHPColor(Color.green);
    }

    void SetHPColor(Color color)
    {
        Image[] image = HPP.GetComponentsInChildren<Image>();
        foreach (var i in image)
        {
            if (i.name == "Fill")
            {
                i.color = color;
            }
        }
    }







    void Start()
    {
        NewHP();
        SoliderPoint = 20;
        SoliderClass = solider1;
        sp = 5;
        InvokeRepeating("AddSoliderPoint", 2, 1.5F);
    }

    // Update is called once per frame
    //
    //
    //
    //
    //
    //
    //
    //
    //Update修改了自动生成士兵的范围
    void Update()
    {
        SetHP();
        if (SoliderPoint >= 5)
        {
            int n = Random.Range(0, 1);
            if (n == 1)
            {
                Vector3 RanPos;
                RanPos.x = Random.Range(60, 240);
                RanPos.y = 66;
                RanPos.z = Random.Range(40, 190);
                CreateSolider(RanPos, solider1, 5);
            }
            if (n == 0 && SoliderPoint >= 10)
            {
                Vector3 RanPos;
                RanPos.x = Random.Range(10, 260);
                RanPos.y = 46;
                RanPos.z = Random.Range(20, 220);
                CreateSolider(RanPos, solider2, 10);
            }
        }
    }

    void CreateSolider(Vector3 targetPos, Transform soliderClass, int sp)
    {
        if (SoliderPoint >= sp)
        {
            SoliderPoint -= sp;
            m_soliderRate -= Time.deltaTime;
            if (m_soliderRate <= 0)
            {
                m_soliderRate = 0.1f;
            }
            targetPos.y -= 10;
            Instantiate(soliderClass, targetPos, this.transform.rotation);

        }
    }

    void OnTriggerEnter(Collider other)
    {
        //.Log(m_life + " TEST ");
        if (other.tag.CompareTo("bullet") == 0)
        {
            zidang bullet = other.GetComponent<zidang>();
            aoe boomb = other.GetComponent<aoe>();
            if (bullet != null)
            {
                m_life -= bullet.zidang_power;

            }
            if (boomb != null)
            {
                m_life -= boomb.zidang_power;
            }
        }
        if (m_life <= 0)
        {
            Debug.Log(m_life + " destory ");
            Destroy(this.gameObject);
        }
        //Debug.Log(m_life);
       // Debug.Log(SoliderPoint + " SP ");
    }
    void OnTriggerStay(Collider other)
    {
        if (other.tag.CompareTo("aoe") == 0)
        {
            Debug.Log("着弹确认");
            aoe bomb = other.GetComponent<aoe>();
            if (bomb != null)
            {
                m_life -= bomb.zidang_power;
            }
            if (m_life <= 0)
            {
                Debug.Log(m_life + " destory ");
                Destroy(this.gameObject);
            }
        }
    }
    void AddSoliderPoint()
    {
        SoliderPoint += 1;
    }
}
