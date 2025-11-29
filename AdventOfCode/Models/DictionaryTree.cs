namespace AdventOfCode.Models;

/// <summary>
/// Simple keyed tree implementation
/// </summary>
/// <remarks>
/// Initial inspiration: https://stackoverflow.com/a/942088/310001
/// Implementation guided by Jetbrains AI Assistant
/// </remarks>
[DebuggerDisplay("{Value} (chld: {Count})")]
public class DictionaryTree<TKey, TValue> : Dictionary<TKey, DictionaryTree<TKey, TValue>> where TKey : notnull
{
    public DictionaryTree(TValue value)
    {
        Value = value;
    }
    
    public TValue Value { get; set; }
    
    public DictionaryTree<TKey, TValue>? Parent { get; private set; }

    /// <summary>
    /// Add a node as a child to this node.
    /// </summary>
    /// <remarks>
    /// The added node will have this node set as its parent.
    /// </remarks>
    public new void Add(TKey key, DictionaryTree<TKey, TValue> childNode)
    {
        childNode.Parent = this; // Set the parent of the added child node
        base.Add(key, childNode); // Add the child node to the dictionary
    }
    
    /// <summary>
    /// Get a list of all the node's parent nodes, all the way to the topmost root node.
    /// </summary>
    public IEnumerable<DictionaryTree<TKey, TValue>> GetParents(bool includeSelf = true)
    {
        if (includeSelf)
        {
            yield return this;
        }
        
        var currentNode = this.Parent;

        while (currentNode != null)
        {
            yield return currentNode;
            currentNode = currentNode.Parent;
        }
    }
}
