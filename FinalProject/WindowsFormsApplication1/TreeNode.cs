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
        /// Inicjuje nowe wyst�pienie TreeNode
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
        /// <param name="ValueName">Nazwa ga��zi gdzie tworzoy jest TreeNode</param>
        public void AddTreeNode(TreeNode treeNode, string ValueName)
        {
            int index = _attribute.indexValue(ValueName);
            _children[index] = treeNode;
        }

        /// <summary>
        /// Zwraca ca�kowit� liczbe dzieci w�z�a
        /// </summary>
        public int ChildrenCount
        {
            get
            {
                return _children.Count;
            }
        }

        /// <summary>
        /// Zwraca w�ze� podrz�dny w�z�a
        /// </summary>
        /// <param name="index">Indeks w�z�a potomnego</param>
        /// <returns>Przedmiot klasy TreeNode reprezentowanego w�z�a</returns>
        public TreeNode GetChildAt(int index)
        {
            return _children[index];
        }

        /// <summary>
        /// Atrybut, kt�ry jest pod��czony do w�z�a
        /// </summary>
        public TreeAttribute Attribute
        {
            get
            {
                return _attribute;
            }
        }

        /// <summary>
        /// Zwraca dziecko w�z�a z nazw� ga��zi, kt�ry prowadzi do niego
        /// </summary>
        /// <param name="branchName">Nazwa ga��zi</param>
        /// <returns>w�ze�</returns>
        public TreeNode GetChildByBranchName(string branchName)
        {
            int index = _attribute.indexValue(branchName);
            return _children[index];
        }
    }
}
