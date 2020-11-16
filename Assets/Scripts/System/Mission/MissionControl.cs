using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionControl : MonoBehaviour
{
    public static MissionControl instance;
    public MissionData dataModel;

    private const int valueAppearBoss = 100;
    private const float timerCreateEnemy = 1;

    public PlayerControl player;

    public List<EnemyControl> lsEnemyControls = new List<EnemyControl>();

    public KdTree<Transform> enemyKdTree = new KdTree<Transform>();
    public int totalEnemyDead = 0;
    public int curCoin;
    public List<int> lsHPBoss = new List<int>();
    public bool isBossTime;
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        dataModel = GetComponent<MissionData>();
        player.gameObject.SetActive(false);
    }

    public void InitMission(bool isColor = false)
    {
        isBossTime = false;
        lsEnemyControls.Clear();
        enemyKdTree.Clear();        
        player.OnSetup(isColor);
        player.gameObject.SetActive(true);
        player.GetComponent<CharacterDataBinding>().Attack = -1;
        GameObject goPlayer = player.gameObject;
        goPlayer.transform.position = ConfigScene.instance.posPlayer.position;
        goPlayer.transform.rotation = Quaternion.identity;

        totalEnemyDead = 0;
        dataModel.currentEnemy = 0;
        dataModel.totalEnemyDie = 0;
        dataModel.totalEnemyCreated = 0;
        curCoin = 0;

        //goPlayer.GetComponent<WeaponControl>().Setup();
        //goPlayer.transform.localScale = Vector3.one;
        StartCoroutine("LoopCreateEnemy");
    }

    IEnumerator LoopCreateEnemy()
    {
        while (true)
        {
            yield return new WaitForSeconds(timerCreateEnemy);

            if (dataModel.totalEnemyDie < valueAppearBoss)
            {
                CreateEnemy();
            }
        }
    }

    void CreateEnemy()
    {
        //1. tao Enemy in list: 
        ConfigEnemyData cfEnemy = ConfigManager.configEnemy.GetConfigEnemyByID(1);
        GameObject goenemy = Instantiate(Resources.Load("Enemy/" + cfEnemy.namePrefab, typeof(GameObject))) as GameObject;

        Vector3 posEnemy = new Vector3(Random.RandomRange(-5f, 5f), Random.RandomRange(-5f, 5f), 0);
        while (Vector3.Distance(player.transform.position, posEnemy) < 3)
        {
            posEnemy = new Vector3(Random.RandomRange(-5f, 5f), Random.RandomRange(-5f, 5f), 0);
        }
        goenemy.transform.position = posEnemy;

        EnemyCreateData data = new EnemyCreateData
        {
            config = cfEnemy
        };
        EnemyControl enemyControl = goenemy.GetComponent<EnemyControl>();
        enemyControl.OnEnemyDead += OnEnemyDeadCallback;
        enemyControl.OnSetup(data);

        lsEnemyControls.Add(enemyControl);
        enemyKdTree.Add(enemyControl.transform);

        //3.
        dataModel.currentEnemy++;
        dataModel.totalEnemyCreated++;
        goenemy.name = dataModel.totalEnemyCreated.ToString();
        if (dataModel.totalEnemyCreated % valueAppearBoss == 0)
        {
            StopAllCoroutines();
        }
    }
    
    private void OnEnemyDeadCallback(EnemyControl enemy)
    {
        // check 
        dataModel.currentEnemy--;
        dataModel.totalEnemyDie++;
        totalEnemyDead++;
        OnUpdateEnemy(enemy);
        if (dataModel.totalEnemyDie >= valueAppearBoss)
        {
            dataModel.totalEnemyDie = 0;
            // Show warning
            GameplayView gameplayView = (GameplayView)ViewManager.Instance.currentView;
            gameplayView.ShowWarning();
            
        }
    }

    public void ShowBoss()
    {
        isBossTime = true;
        // Show Boss        
        int indexBoss = Random.Range(0, 3);        
        GameObject goBoss = Instantiate(Resources.Load("Enemy/Boss1", typeof(GameObject))) as GameObject;
        goBoss.transform.position = ConfigScene.instance.posBoss.position;
        EnemyControl enemyControl = goBoss.GetComponent<EnemyControl>();
        enemyControl.OnEnemyDead += OnBossDeadCallback;
        enemyControl.OnSetup(null, lsHPBoss[indexBoss]);
        enemyControl.GetComponent<Boss1Control>().OnSetupBoss(indexBoss);
        lsEnemyControls.Add(enemyControl);
        enemyKdTree.Add(enemyControl.transform);
    }

    private void OnBossDeadCallback(EnemyControl enemy)
    {
        //dataModel.currentEnemy--;
        //dataModel.totalEnemyDie++;
        isBossTime = false;
        totalEnemyDead++;
        OnUpdateEnemy(enemy);
        StartCoroutine("LoopCreateEnemy");
    }

    public void OnUpdateEnemy(EnemyControl enemyControl)
    {
        int index = lsEnemyControls.IndexOf(enemyControl);
        lsEnemyControls.RemoveAt(index);
        enemyKdTree.RemoveAt(index);
    }

    public void AddCoin()
    {
        curCoin++;
    }

    public void ClearAllEnemy()
    {
        StopAllCoroutines();
        //player.gameObject.SetActive(false);
        for(int i = 0; i < lsEnemyControls.Count; i++)
        {
            Destroy(lsEnemyControls[i].gameObject);
        }
        for(int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }
}
