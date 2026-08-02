using System;
using System.Collections.Generic;

public class TreeNode
{
    public string Value { get; set; }
    public List<TreeNode> Children { get; set; } = new();
}

public static class TreeUtility
{
    public static List<string> FlattenTree(params TreeNode[] roots)
    {
        List<string> result = new();

        void Traverse(TreeNode node, ref int depth)
        {
            result.Add(node.Value);
            Console.WriteLine($"{node.Value} : Depth {depth}");

            depth++;

            foreach (TreeNode child in node.Children)
            {
                Traverse(child, ref depth);
            }

            depth--;
        }

        foreach (TreeNode root in roots)
        {
            int depth = 0;
            Traverse(root, ref depth);
        }

        return result;
    }
}

class H2
{
    public static void main()
    {
        TreeNode A = new TreeNode { Value = "A" };
        A.Children.Add(new TreeNode { Value = "A1" });
        A.Children.Add(new TreeNode { Value = "A2" });

        TreeNode B = new TreeNode { Value = "B" };
        TreeNode B1 = new TreeNode { Value = "B1" };
        B1.Children.Add(new TreeNode { Value = "B1a" });
        B1.Children.Add(new TreeNode { Value = "B1b" });
        B.Children.Add(B1);

        TreeNode C = new TreeNode { Value = "C" };

        List<string> list = TreeUtility.FlattenTree(A, B, C);

        Console.WriteLine(string.Join(", ", list));
    }
}