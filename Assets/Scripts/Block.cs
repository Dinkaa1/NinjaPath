using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] private TextMeshPro _text;
    [SerializeField] private SpriteRenderer _renderer;
    public int Value;
    public Node Node;
    public Block MergingBlock;
    public bool Merging;
    public Vector2 Pos => transform.position;
    public void Init(Type type)
    {
        Value = type.Value;
        _renderer.color = type.Colour;
        _text.text = type.Value.ToString();
    }

    public void SetBlock(Node node)
    {
        if (Node != null)
        {
            Node.UsedBlock = null;
        }
        Node = node;
        Node.UsedBlock = this;
    }

    public void MergeBlock(Block blockToMerge)
    {
        MergingBlock = blockToMerge;
        Node.UsedBlock = null;
        blockToMerge.Merging = true;
    }

    public bool AbleToMerge(int value) => value == Value && !Merging && MergingBlock == null;

}
