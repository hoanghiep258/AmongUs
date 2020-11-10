using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionControl : MonoBehaviour
{
    public static MissionControl instance;
    private MissionData dataModel;

    private const int valueAppearBoss = 100;
    private const float timerCreateEnemy = 1;

    public PlayerControl player;

    public List<EnemyControl> lsEnemyControls = new List<EnemyControl>();

    public KdTree<Transform> enemyKdTree = new KdTree<Transform>();
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        dataModel = GetComponent<MissionData>();
        player.gameObject.SetActive(false);
    }

    public void InitMission(bool isColor = false)
    {
        lsEnemyControls.Clear();
        enemyKdTree.Clear();
        Debug.LogError("isColor: " + isColor);
        player.OnSetup(isColor);
        player.gameObject.SetActive(true);
        GameObject goPlayer = player.gameObject;
        goPlayer.transform.position = ConfigScene.instance.posPlayer.position;
        goPlayer.transform.rotation = Quaternion.identity;
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
        goenemy.name = dataModel.currentEnemy.ToString();
    }

    private void OnEnemyDeadCallback(EnemyControl enemy)
    {
        // check 
        dataModel.currentEnemy--;
        dataModel.totalEnemyDie++;

        OnUpdateEnemy(enemy);
        if (dataModel.totalEnemyDie >= valueAppearBoss)
        {
            // Show Boss
        }
    }

    public void OnUpdateEnemy(EnemyControl enemyControl)
    {
        int index = lsEnemyControls.IndexOf(enemyControl);
        lsEnemyControls.RemoveAt(index);
        enemyKdTree.RemoveAt(index);
    }
}
