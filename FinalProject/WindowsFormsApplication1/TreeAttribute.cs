using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace DecisionTree1
{
    /// <summary>
    /// Klasa dziala jako atrybut stosowany w klasie decyzji
    /// </summary>
    public class TreeAttribute
    {
        protected PossibleValueCollection _possibleValues;
        protected string _name;
        protected object _label;

        /// <summary>
        /// Inicjuje nowe wyst¹pienie klasy atrybutu
        /// </summary>
        /// <param name="name">Wskazuje nazwê atrybutu</param>
        /// <param name="values">Wskazuje mo¿liwe wartoœci dla atrybutu</param>
        public TreeAttribute(string name, PossibleValueCollection possibleValues)
        {
            _name = name;

            if (possibleValues == null)
                _possibleValues = null;
            else
            {
                _possibleValues = possibleValues;
                _possibleValues.Sort();
            }
        }



        /// <summary>
        /// Wskazuje nazwê atrybutu
        /// </summary>
        public string AttributeName
        {
            get
            {
                return _name;
            }
        }

        /// <summary>
        /// Zwraca tablicê z wartoœciami atrybutów
        /// </summary>
        public PossibleValueCollection PossibleValues
        {
            get
            {
                return _possibleValues;
            }
        }

        /// <summary>
        /// To wskazuje, czy wartoœæ nie jest dozwolone dla tego atrybutu
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool isValidValue(string value)
        {
            return indexValue(value) >= 0;
        }

        /// <summary>
        /// Zwraca indeks wartoœci
        /// </summary>
        /// <param name="value">Wartoœæ</param>
        /// <returns>Wartoœæ indeksu, na którym pozycja jest wartoœci¹</returns>
        public int indexValue(string value)
        {
            if (_possibleValues != null)
                return _possibleValues.BinarySearch(value);
            else
                return -1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (_name != string.Empty)
            {
                return _name;
            }
            else
            {
                return _label.ToString();
            }
        }
    }
}
