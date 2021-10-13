using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSystem : MonoBehaviour
{
    private static BattleSystem _instance;
    public static BattleSystem Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public GameObject PCUnitPrefab, NPCUnitPrefab;
    public List<Transform> PlayerPositions, EnemyPositions;
    public List<UnitSpec> PlayerUnitSpecs, EnemyUnitSpecs;
    private List<PCUnit> _playerUnits = new List<PCUnit>();
    private List<NPCUnit> _enemyUnits = new List<NPCUnit>();
    private List<Unit> _turnOrder;
    private MoveSpec _playerMove, _enemyMove;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < PlayerUnitSpecs.Count; i++)
        {
            GameObject player = Instantiate(PCUnitPrefab, PlayerPositions[i]);
            _playerUnits.Add(player.GetComponent<PCUnit>());
            _playerUnits[i].Initialize(PlayerUnitSpecs[i]);
        }
        
        for (int i = 0; i < EnemyUnitSpecs.Count; i++)
        {
            GameObject enemy = Instantiate(NPCUnitPrefab, EnemyPositions[i]);
            _enemyUnits.Add(enemy.GetComponent<NPCUnit>());
            _enemyUnits[i].Initialize(EnemyUnitSpecs[i]);
        }

        DisplayMoves();
    }

    void DisplayMoves()
    {
        _playerUnits[0].Menu.Show();
    }

    public void RecordMove(MoveSpec move)
    {
        _playerMove = move;
        Debug.Log("Resolving Moves...");
        StartCoroutine(ResolveMoves());
    }

    IEnumerator ResolveMoves()
    {
        _playerUnits[0].Menu.Hide();
        ChooseEnemyMoves();
        _playerMove.UseMove(_playerUnits[0], _enemyUnits[0]);
        if (_enemyUnits[0].IsDead())
        {
            Debug.Log("Victory!");
            //Call some end screen function
            yield break;
        }
        yield return new WaitForSeconds(3);
        _enemyMove.UseMove(_enemyUnits[0], _playerUnits[0]);
        if (_playerUnits[0].IsDead())
        {
            Debug.Log("Defeat!");
            //Call some end screen function
            yield break;
        }
        yield return new WaitForSeconds(3);
        DisplayMoves();
    }

    void ChooseEnemyMoves()
    {
        _enemyMove = _enemyUnits[0].Movepool[(int)Mathf.Round(Random.value)];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
