using System.Collections.Generic;

class GameConfig
{
    private List<TreeNode> nodes = new List<TreeNode>();

    public GameConfig(List<TreeNode> nodes)
    {
        this.nodes = nodes;
    }

    public TreeNode GetRootNode() => nodes.Find(node => node.parent == null);

    public void AddNode(TreeNode node) => nodes.Add(node);
}
