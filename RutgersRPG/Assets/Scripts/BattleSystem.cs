using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BattleSystem : MonoBehaviour {
    class Turn {
        public MoveSpec move; 
        public List<Unit> targets;
        public Turn (MoveSpec move, List<Unit> targets) {
            this.move = move;
            this.targets = targets;
        }
    }

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
    private MoveSpec _move;
    private Dictionary<Unit, Turn> _turnOrder;
    private List<Unit> _playerUnits = new List<Unit>();
    private List<Unit> _enemyUnits = new List<Unit>();
    private List<Unit> _targets = new List<Unit>();
    private List<Unit> _potentialTargets = new List<Unit>();


    // Start is called before the first frame update
    void Start()
    {
        _turnOrder = new Dictionary<Unit, Turn>();

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
        if (_playerUnits[_currentPlayerIndex].IsDead()) {
            NextChooseMove();
        }
        else ((PCUnit)_playerUnits[_currentPlayerIndex]).Menu.Show();
    }

    public void RecordMove(MoveSpec move)
    {
        _move = move;
        ((PCUnit)_playerUnits[_currentPlayerIndex]).Menu.Hide();
        DisplayTargets();
        
    }

    void DisplayTargets() 
    {
        Predicate<Unit> p = _move.Targets.ConstructPredicate(_playerUnits[_currentPlayerIndex].Type);
        foreach (Unit u in _playerUnits.Concat(_enemyUnits)) {
            if (p(u)) {
                _potentialTargets.Add(u);
            }
        }
        if (_move.Targets.ChooseTargets) {
            foreach (Unit u in _potentialTargets) {
                u.DisplaySelection();
            }
        }
        else {
            _targets = new List<Unit>(_potentialTargets);
            RegisterMove();
        }
    }

    public void RecordTarget(Unit unit) {
        if (_targets.Contains(unit)) {
            _targets.Remove(unit);
        }
        else _targets.Add(unit);
        if (_targets.Count >= _move.Targets.NumberOfTargets) {
            RegisterMove();
        }
    }

    void RegisterMove() {
        _turnOrder.Add(_playerUnits[_currentPlayerIndex], new Turn(_move, new List<Unit>(_targets)));
        foreach (Unit t in _potentialTargets) {
            t.HideSelection();
        }
        _potentialTargets.Clear();
        _targets.Clear();
        NextChooseMove();
    }

    void NextChooseMove() {
        _currentPlayerIndex++;
        if (_currentPlayerIndex < _playerUnits.Count) {
            DisplayMoves();
        }
        else ChooseEnemyMoves();
    }

    void ChooseEnemyMoves() {
        foreach (NPCUnit u in _enemyUnits) {
            if (u.IsDead()) continue;
            _move = u.ChooseMove();
            Predicate<Unit> p = _move.Targets.ConstructPredicate(u.Type);
            foreach (Unit t in _playerUnits.Concat(_enemyUnits)) {
                if (p(t)) {
                    _potentialTargets.Add(t);
                }
            }
            if (_move.Targets.ChooseTargets) {
                _potentialTargets.Sort((x, y) => y.Threat - x.Threat);
                for (int i = 0; i < _move.Targets.NumberOfTargets; i++) {
                    _targets.Add(_potentialTargets[i]);
                }
            }
            else {
                _targets = new List<Unit>(_potentialTargets);
            }
            _turnOrder.Add(u, new Turn(_move, new List<Unit>(_targets)));
            _potentialTargets.Clear();
            _targets.Clear();
        }
        StartCoroutine(ResolveMoves());
    }

    IEnumerator ResolveMoves()
    {
        foreach (KeyValuePair<Unit, Turn> k in _turnOrder) {
            if (k.Key.IsDead()) continue;
            k.Value.move.UseMove(k.Key, k.Value.targets);
            if (k.Key is PCUnit) {
                PCUnit pcUnit = (PCUnit)k.Key;
                pcUnit.SetMana(-k.Value.move.GetCost());
            }
            if (_enemyUnits.TrueForAll((u) => u.IsDead())) {
                Debug.Log("Victory!");
                //Call some end screen function
                yield break;
            }
            if (_playerUnits.TrueForAll((u) => u.IsDead())) {
                Debug.Log("Defeat!");
                //Call some end screen function
                yield break;
            }
            yield return new WaitForSeconds(2);
        }
        _turnOrder.Clear();
        _currentPlayerIndex = 0;
        DisplayMoves();
    }
}
