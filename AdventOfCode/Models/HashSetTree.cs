namespace AdventOfCode.Models;

/// <summary>
/// Simple un-keyed tree implementation
/// </summary>
/// <remarks>
/// Initial inspiration: https://stackoverflow.com/a/942088/310001
/// Implementation guided by Jetbrains AI Assistant
/// </remarks>
[DebuggerDisplay("{Value} (children: {Count})")]
public class HashSetTree<TValue> : HashSet<HashSetTree<TValue>>
{
    public HashSetTree(TValue value)
    {
        Value = value;
    }
    
    public TValue Value { get; }
    
    public HashSetTree<TValue>? Parent { get; private set; }

    /// <summary>
    /// Add a node as a child to this node.
    /// </summary>
    /// <remarks>
    /// The added node will have this node set as its parent.
    /// </remarks>
    public new void Add(HashSetTree<TValue> childNode)
    {
        childNode.Parent = this; // Set the parent of the added child node
        base.Add(childNode); // Add the child node to the dictionary
    }
    
    /// <summary>
    /// Get a list of all the node's parent nodes, all the way to the topmost root node.
    /// </summary>
    public IEnumerable<TValue> GetParents(bool includeSelf = true)
    {
        if (includeSelf)
        {
            yield return this.Value;
        }
        
        var currentNode = this.Parent;

        while (currentNode != null)
        {
            yield return currentNode.Value;
            currentNode = currentNode.Parent;
        }
    }
}
