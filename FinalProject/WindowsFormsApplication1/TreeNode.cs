using System;
using System.Collections.Generic;
using System.Text;

namespace DecisionTree1
{
    /// <summary>
    /// Klasa drzewa decyzyjnego;
    /// </summary>
    public class TreeNode
    {
        TreeNodeCollection _children;
        private TreeAttribute _attribute;

        /// <summary>
        /// Inicjuje nowe wyst¹pienie TreeNode
        /// </summary>
        /// <param name="attribute">Atrybut klasy decyzji</param>
        public TreeNode(TreeAttribute attribute)
        {
            if (attribute != null && attribute.PossibleValues != null)
            {
                _children = new TreeNodeCollection();                
                for (int i = 0; i < attribute.PossibleValues.Count; i++)
                    _children.Add(null);
            }
            else
            {
                _children = new TreeNodeCollection();
                _children.Add(null);
            }
            _attribute = attribute;
        }

        /// <summary>
        /// Dodawanie TreeNode jako dziecko
        /// </summary>
        /// <param name="treeNode">TreeNode</param>
        /// <param name="ValueName">Nazwa ga³êzi gdzie tworzoy jest TreeNode</param>
        public void AddTreeNode(TreeNode treeNode, string ValueName)
        {
            int index = _attribute.indexValue(ValueName);
            _children[index] = treeNode;
        }

        /// <summary>
        /// Zwraca ca³kowit¹ liczbe dzieci wêz³a
        /// </summary>
        public int ChildrenCount
        {
            get
            {
                return _children.Count;
            }
        }

        /// <summary>
        /// Zwraca wêze³ podrzêdny wêz³a
        /// </summary>
        /// <param name="index">Indeks wêz³a potomnego</param>
        /// <returns>Przedmiot klasy TreeNode reprezentowanego wêz³a</returns>
        public TreeNode GetChildAt(int index)
        {
            return _children[index];
        }

        /// <summary>
        /// Atrybut, który jest pod³¹czony do wêz³a
        /// </summary>
        public TreeAttribute Attribute
        {
            get
            {
                return _attribute;
            }
        }

        /// <summary>
        /// Zwraca dziecko wêz³a z nazw¹ ga³êzi, który prowadzi do niego
        /// </summary>
        /// <param name="branchName">Nazwa ga³êzi</param>
        /// <returns>wêze³</returns>
        public TreeNode GetChildByBranchName(string branchName)
        {
            int index = _attribute.indexValue(branchName);
            return _children[index];
        }
    }
}
