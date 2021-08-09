using System.Collections.Generic;

public class TreeNode
{
    public TreeNode parent { get; private set; }
    public List<TreeNode> childrens { get; private set; }
    public string TitleText { get; private set; }
    public List<Answer> answers { get; private set; } 

    public TreeNode(TreeNode parent, List<TreeNode> childrens, string titleText, List<Answer> answers)
    {
        this.parent = parent;
        this.childrens = childrens;
        TitleText = titleText;
        this.answers = answers;
    }

    public void AddChild(TreeNode node) => childrens.Add(node);
}