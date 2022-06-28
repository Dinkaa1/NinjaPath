using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Numerics;
using DG.Tweening;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SocialPlatforms.GameCenter;
using UnityEngine.UI;
using Quaternion = UnityEngine.Quaternion;
using Random = UnityEngine.Random;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;
using UnityEngine.SceneManagement;

public class GameLogic : MonoBehaviour
{
    [SerializeField] private int _width = 5;
    [SerializeField] private int _height = 4;
    [SerializeField] private Node _node;
    [SerializeField] private Block _block;
    [SerializeField] private SpriteRenderer _board;
    [SerializeField] private List<Type> _types;
    [SerializeField] private float _time = 0.02f;
    [SerializeField] private int _win = 2048;
    [SerializeField] private GameObject _gameOverMenu;
    [SerializeField] private GameObject _button;
    [SerializeField] private GameObject _winMenu;
    [SerializeField] private GameObject _next;
    private List<Node> _nodes;
    private List<Block> _blocks;
    private GameState _state;
    private int _round;
    private Type GetTypeByValue(int value) => _types.First(x => x.Value == value);
    void Start()
    {
        ChangeState(GameState.GenerateLevel);
    }

    void Update()
    {
        if(_state != GameState.WaitForInput) return;
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Shift(Vector2.left);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Shift(Vector2.right);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Shift(Vector2.up);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Shift(Vector2.down);
        }
    }
    
    private void ChangeState(GameState state)
    {
        _state = state;
        switch (state)
        {
            case GameState.GenerateLevel:
                CreateGrid();
                break;
            case GameState.GenerateBlocks:
                GetBlocks(_round++ == 0 ? 2 : 1); // при запуске генерим два блока, дальше +1
                break;
            case GameState.WaitForInput:
                break;
            case GameState.Move:
                _button.SetActive(false);
                _next.SetActive(true);
                break;
            case GameState.Win:
                _winMenu.SetActive(true);
                break;
            case GameState.Lose:
                _gameOverMenu.SetActive(true);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(state), state, null);
        }
    }

    public void CompleteLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void BackToMain()
    {
        PlayerPrefs.SetInt("SavedScene", SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene(0);
    }
    
    void CreateGrid()
    {
        _round = 0;
        _nodes = new List<Node>();
        _blocks = new List<Block>();
        for (int i = 0; i < _width; i++)
        {
            for (int k = 0; k < _height; k++)
            {
                var node = Instantiate(_node, new Vector2(i, k), Quaternion.identity);
                _nodes.Add(node);
            }
        }
        var centre = new Vector2((float)_width / 2 - 0.5f, (float)_height / 2 - 0.5f);
        var board = Instantiate(_board, centre, Quaternion.identity);
        board.size = new Vector2(_width, _height);

        Camera.main.transform.position = new Vector3(centre.x, centre.y, -10);

        ChangeState(GameState.GenerateBlocks);

    }

    void GetBlocks(int amount)
    {
        // берем свободные ячейки
        var freeNodes = _nodes.Where(x => x.UsedBlock == null).OrderBy(y => Random.value).ToList();
        // генерим блоки для каждой
        foreach (var n in freeNodes.Take(amount))
        {
            SpawnOne(n, Random.value > 0.8f ? 4 : 2);
        }
        
        if (freeNodes.Count() == 0) 
        {
            ChangeState(GameState.Lose);
            return;
        }

        ChangeState(_blocks.Any(x=>x.Value == _win) ? GameState.Win : GameState.WaitForInput);
    }

    void SpawnOne(Node node, int value)
    {
        var block = Instantiate(_block, node.Pos, Quaternion.identity);
        block.Init(GetTypeByValue(value));
        block.SetBlock(node);
        _blocks.Add(block);
    }
    void Shift(Vector2 direction)
    {
        ChangeState(GameState.Move);
        var sortedBlocks = _blocks.OrderBy(b => b.Pos.x).ThenBy(b => b.Pos.y).ToList();
        if (direction == Vector2.right || direction == Vector2.up)
        {
            sortedBlocks.Reverse();
        }

        foreach (var b in sortedBlocks) // нам нужно пройтись для каждого хотя бы один раз
        {
            var nextNode = b.Node;
            do
            {
                b.SetBlock(nextNode);
                var potentialNode = GetPosition(nextNode.Pos + direction); // есть ли ячейка рядом
                if (potentialNode != null)
                {
                    if (potentialNode.UsedBlock != null && potentialNode.UsedBlock.AbleToMerge(b.Value))
                    {
                        b.MergeBlock(potentialNode.UsedBlock);
                        
                    }
                    else if (potentialNode.UsedBlock == null) nextNode = potentialNode;
                }
            } 
            while (nextNode != b.Node);

        }

        var seq = DOTween.Sequence();
        foreach (var b in sortedBlocks)
        {
            var movePoint = b.MergingBlock != null ? b.MergingBlock.Node.Pos : b.Node.Pos;
            seq.Insert(0, b.transform.DOMove(movePoint, _time));
        }

        seq.OnComplete(() =>
        {
            foreach (var bl in sortedBlocks.Where(x=>x.MergingBlock != null))
            {
                UniteBlocks(bl.MergingBlock, bl);
            }
            
            ChangeState(GameState.GenerateBlocks);
        });
        
    }

    void UniteBlocks(Block baseBlock, Block mergingOne)
    {
        SpawnOne(baseBlock.Node, baseBlock.Value * 2);
        DestroyBlocks(baseBlock);
        DestroyBlocks(mergingOne);
    }

    void DestroyBlocks(Block block)
    {
        _blocks.Remove(block);
        Destroy(block.gameObject);
    }
    Node GetPosition(Vector2 position)
    {
        return _nodes.FirstOrDefault(n => n.Pos == position);
    }
}

[Serializable]
public struct Type
{
    public Color Colour;
    public int Value;
}

public enum GameState
{
    GenerateLevel,
    GenerateBlocks,
    WaitForInput,
    Move,
    Win,
    Lose
}