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
    private int _currentPlayerIndex = 0;
    [SerializeField]
    private List<PCUnit> _playerUnits = new List<PCUnit>();
    [SerializeField]
    private List<NPCUnit> _enemyUnits = new List<NPCUnit>();

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < PlayerUnitSpecs.Count; i++)
        {
            GameObject player = Instantiate(PCUnitPrefab, PlayerPositions[i]);
            _playerUnits.Add(player.GetComponent<PCUnit>());
            _playerUnits[i].Initialize(PlayerUnitSpecs[i]);
        }

        //_playerUnits.Sort((x, y) => (y.GetSpeed() - x.GetSpeed()));

        for (int i = 0; i < EnemyUnitSpecs.Count; i++)
        {
            GameObject enemy = Instantiate(NPCUnitPrefab, EnemyPositions[i]);
            _enemyUnits.Add(enemy.GetComponent<NPCUnit>());
            _enemyUnits[i].Initialize(EnemyUnitSpecs[i]);
        }
        
        _enemyUnits.Sort((x, y) => (y.GetSpeed() - x.GetSpeed()));

        DisplayMoves();
    }

    void DisplayMoves()
    {
        _playerUnits[_currentPlayerIndex].Menu.Show();
        //Debug.Log("Displaying menu for character " + _currentPlayerIndex);
    }

    public void RecordMove(MoveSpec move)
    {
        //Debug.Log("Player chose " + move.Name);
        _playerUnits[_currentPlayerIndex].SetMove(move);
        _playerUnits[_currentPlayerIndex].Menu.Hide();
        _currentPlayerIndex++;
        if (_currentPlayerIndex < _playerUnits.Count) {
            //Debug.Log(_currentPlayerIndex);
            DisplayMoves();
        }
        else ChooseEnemyMoves();
    }
    void ChooseEnemyMoves() {
        foreach (NPCUnit u in _enemyUnits) {
            u.ChooseMove();
        }
        StartCoroutine(ResolveMoves());
    }

    IEnumerator ResolveMoves()
    {
        for (int i = 0; i < _playerUnits.Count; i++) {
            _playerUnits[i].UseMove(_playerUnits[i], _enemyUnits[i]);
            if (_enemyUnits.TrueForAll((u) => u.IsDead())) {
                Debug.Log("Victory!");
                //Call some end screen function
                yield break;
            }
            yield return new WaitForSeconds(2);
        }
        for (int i = 0; i < _enemyUnits.Count; i++) {
            _enemyUnits[i].UseMove(_enemyUnits[i], _playerUnits[i]);
            if (_playerUnits.TrueForAll((u) => u.IsDead())) {
                Debug.Log("Defeat!");
                //Call some end screen function
                yield break;
            }
            yield return new WaitForSeconds(2);
        }
        _currentPlayerIndex = 0;
        DisplayMoves();
    }
}
