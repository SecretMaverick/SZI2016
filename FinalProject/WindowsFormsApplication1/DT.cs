using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.IO;

namespace DecisionTree1
{

    
    /// <summary>
    /// Klasa, która implementuje drzewa decyzyjnego przy wykorzystaniu algorytmu ID3
    /// </summary>
    public class DecisionTree
    {
        private DataTable _sampleData;
        private int mTotalPositives = 0;
        private int mTotal = 0;
        private string mTargetAttribute = "result";
        private double mEntropySet = 0.0;

        /// <summary>
        /// Ca³kowita liczba dodatnia w danych Ÿród³owych
        /// </summary>
        /// <param name="samples">DataTable z próbkami</param>
        /// <returns>Ca³kowita NRO pozytywnych próbek</returns>
        private int countTotalPositives(DataTable samples)
        {
            int result = 0;

            foreach (DataRow aRow in samples.Rows)
            {
                if (aRow[mTargetAttribute].ToString().ToUpper().Trim() == "TRUE")
                    result++;
            }

            return result;
        }

        /// <summary>
        /// Oblicza entropiê podana nastêpuj¹cym wzorem
        /// -p+log2p+ - p-log2p-
        /// 
        /// onde: p+ Jest to proporcja wartoœci dodatnich
        ///		  p- Jest to proporcja wartoœci negatywnych
        /// </summary>
        /// <param name="positives">Liczba wartoœci dodatnich</param>
        /// <param name="negatives">Liczba wartoœci negatywnych</param>
        /// <returns>Retorna o valor da Entropia</returns>
        private double getCalculatedEntropy(int positives, int negatives)
        {
            int total = positives + negatives;
            double ratioPositive = (double)positives / total;
            double ratioNegative = (double)negatives / total;

            if (ratioPositive != 0)
                ratioPositive = -(ratioPositive) * System.Math.Log(ratioPositive, 2);
            if (ratioNegative != 0)
                ratioNegative = -(ratioNegative) * System.Math.Log(ratioNegative, 2);

            double result = ratioPositive + ratioNegative;

            return result;
        }

        /// <summary>
        /// Metoda usuwajaca przyk³adow¹ tabelê sprawdzaj¹c atrybut, a jeœli wynik jest dodatni lub ujemny
        /// </summary>
        /// <param name="samples">DataTable z próbkami</param>
        /// <param name="attribute">Atrybut wyszukaiwania</param>
        /// <param name="value">wartoœæ artybutu</param>
        /// <param name="positives">NRO bêdzie zawiera³ wszystkie atrybuty z wartoœci okreœlonej jako dodatnie</param>
        /// <param name="negatives">NRO bêdzie zawiera³ wszystkie atrybuty z wartoœci okreœlonej jako ujemne</param>
        private void getValuesToAttribute(DataTable samples, TreeAttribute attribute, string value, out int positives, out int negatives)
        {
            positives = 0;
            negatives = 0;

            foreach (DataRow aRow in samples.Rows)
            {
                ///Do zrobienia: dowiedzieæ siê, czy jest to ok - bo wygl¹da Ÿle
                if (((string)aRow[attribute.AttributeName] == value))
                    if (aRow[mTargetAttribute].ToString().Trim().ToUpper() == "TRUE")
                        positives++;
                    else
                        negatives++;
                
            }
        }

        /// <summary>
        /// Oblicza entropie atrybutu
        /// </summary>
        /// <param name="attribute"> Obliczany atrybut</param>
        /// <returns> entropia atrybutu </returns>
        private double gain(DataTable samples, TreeAttribute attribute)
        {
            PossibleValueCollection values = attribute.PossibleValues;
            double sum = 0.0;

            for (int i = 0; i < values.Count; i++)
            {
                int positives, negatives;

                positives = negatives = 0;

                getValuesToAttribute(samples, attribute, values[i], out positives, out negatives);

                double entropy = getCalculatedEntropy(positives, negatives);
                sum += -(double)(positives + negatives) / mTotal * entropy;
            }
            return mEntropySet + sum;
        }

        /// <summary>
        /// Zwraca najlepszy atrybut.
        /// </summary>
        /// <param name="attributes">tablica atrybutow</param>
        /// <returns>Zwraca te, które maj¹ najwy¿sza entropie</returns>
        private TreeAttribute getBestAttribute(DataTable samples, TreeAttributeCollection attributes)
        {
            double maxGain = 0.0;
            TreeAttribute result = null;

            foreach (TreeAttribute attribute in attributes)
            {
                double aux = gain(samples, attribute);
                if (aux > maxGain)
                {
                    maxGain = aux;
                    result = attribute;
                }
            }
            return result;
        }

        /// <summary>
        /// Zwraca true, jeœli wszystkie przyk³ady próbek s¹ pozytywne
        /// </summary>
        /// <param name="samples">DataTable z próbkami</param>
        /// <param name="targetAttribute">Cecha (kolumna) stosu, który sprawdza</param>
        /// <returns>True, jeœli wszystkie przyk³ady próbek s¹ pozytywne</returns>
        private bool allSamplesArePositive(DataTable samples, string targetAttribute)
        {
            foreach (DataRow row in samples.Rows)
            {
                if (row[targetAttribute].ToString().ToUpper().Trim() == "FALSE")
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Zwraca true, jeœli wszystkie przyk³ady próbek s¹ negatywne
        /// </summary>
        /// <param name="samples">DataTable z próbkami</param>
        /// <param name="targetAttribute">Cecha (kolumna) stosu, który sprawdza</param>
        /// <returns>True, jeœli wszystkie przyk³ady próbek s¹ negatywne</returns>
        private bool allSamplesAreNegative(DataTable samples, string targetAttribute)
        {
            foreach (DataRow row in samples.Rows)
            {
                if (row[targetAttribute].ToString().ToUpper().Trim() == "TRUE")
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Zwraca listê wszystkich ró¿nych wartoœci z przyk³adowej tabeli
        /// </summary>
        /// <param name="samples">DataTable z próbkami</param>
        /// <param name="targetAttribute">Cecha (kolumna) stosu, który sprawdza</param>
        /// <returns>ArrayList z ró¿nymi wartoœciami</returns>
        private ArrayList getDistinctValues(DataTable samples, string targetAttribute)
        {
            ArrayList distinctValues = new ArrayList(samples.Rows.Count);

            foreach (DataRow row in samples.Rows)
            {
                if (distinctValues.IndexOf(row[targetAttribute]) == -1)
                    distinctValues.Add(row[targetAttribute]);
            }

            return distinctValues;
        }

        /// <summary>
        /// Zwraca wartoœæ najczêœciej wystepujac¹ z pobranych próbek
        /// </summary>
        /// <param name="samples">DataTable z próbkami</param>
        /// <param name="targetAttribute">Cecha (kolumna) stosu, który sprawdza</param>
        /// <returns>Zwraca obiekt o najwy¿szej powtarzalnosci z tabeli przyk³adowej</returns>
        private object getMostCommonValue(DataTable samples, string targetAttribute)
        {
            ArrayList distinctValues = getDistinctValues(samples, targetAttribute);
            int[] count = new int[distinctValues.Count];

            foreach (DataRow row in samples.Rows)
            {
                int index = distinctValues.IndexOf(row[targetAttribute]);
                count[index]++;
            }

            int MaxIndex = 0;
            int MaxCount = 0;

            for (int i = 0; i < count.Length; i++)
            {
                if (count[i] > MaxCount)
                {
                    MaxCount = count[i];
                    MaxIndex = i;
                }
            }

            return distinctValues[MaxIndex];
        }

        /// <summary>
        /// Przechodzenie przez drzewo decyzyjne w oparciu o wy¿ej przedstawione próbki
        /// </summary>
        /// <param name="samples">Tabela z próbkami nale¿y przeniesc do drzewa</param>
        /// <param name="targetAttribute"> Nazwa kolumny tabeli, która ma wartoœæ true lub false, aby zweryfikowaæ dana próbke</param>
        private TreeNode internalMountTree(DataTable samples, string targetAttribute, TreeAttributeCollection attributes)
        {
            if (allSamplesArePositive(samples, targetAttribute) == true)
                return new TreeNode(new OutcomeTreeAttribute(true));

            if (allSamplesAreNegative(samples, targetAttribute) == true)
                return new TreeNode(new OutcomeTreeAttribute(false));

            if (attributes.Count == 0)
                return new TreeNode(new OutcomeTreeAttribute(getMostCommonValue(samples, targetAttribute)));

            mTotal = samples.Rows.Count;
            mTargetAttribute = targetAttribute;
            mTotalPositives = countTotalPositives(samples);

            mEntropySet = getCalculatedEntropy(mTotalPositives, mTotal - mTotalPositives);

            TreeAttribute bestAttribute = getBestAttribute(samples, attributes);

            TreeNode root = new TreeNode(bestAttribute);

            if (bestAttribute == null)
                return root;

            DataTable aSample = samples.Clone();

            foreach (string value in bestAttribute.PossibleValues)
            {
                // Zaznacz wszystkie elementy z wartoœci¹ tego atrybutu			
                aSample.Rows.Clear();

                DataRow[] rows = samples.Select(bestAttribute.AttributeName + " = " + "'" + value + "'");

                foreach (DataRow row in rows)
                {
                    aSample.Rows.Add(row.ItemArray);
                }
                // Zaznacz wszystkie elementy z wartoœci¹ tego atrybutu				

                // Utwórz now¹ listê atrybutów, najmniej wystepujacy atrybut jest najlepszym atrybutem		
                TreeAttributeCollection aAttributes = new TreeAttributeCollection();
                for (int i = 0; i < attributes.Count; i++)
                {
                    if (attributes[i].AttributeName != bestAttribute.AttributeName)
                        aAttributes.Add(attributes[i]);
                }	

                if (aSample.Rows.Count == 0)
                {
                    return new TreeNode(new OutcomeTreeAttribute(getMostCommonValue(aSample, targetAttribute)));
                }
                else
                {
                    DecisionTree dc3 = new DecisionTree();
                    TreeNode ChildNode = dc3.mountTree(aSample, targetAttribute, aAttributes);
                    root.AddTreeNode(ChildNode, value);
                }
            }

            return root;
        }


        /// <summary>
        /// Montowanie drzewa
        /// </summary>
        /// <param name="samples">Tabela z próbkami nale¿y przeniesc do drzewa</param>
        /// <param name="targetAttribute"> Nazwa kolumny tabeli, która ma wartoœæ true lub false, aby zweryfikowaæ dana próbke</param>
        /// <returns>Korzen drzewa decyzyjnego</returns></returns?>
        public TreeNode mountTree(DataTable samples, string targetAttribute, TreeAttributeCollection attributes)
        {
            _sampleData = samples;
            return internalMountTree(_sampleData, targetAttribute, attributes);
        }
    }

    /// <summary>
    /// Klasa, która jest przyk³adem wykorzystania algorytmu ID3
    /// </summary>
    public class DecisionTreeImplementation
    {

        string _sourceFile;

        public string GetTree(string sourceFile)
        {
            _sourceFile = sourceFile;
            RawDataSource samples = new RawDataSource(_sourceFile);

            TreeAttributeCollection attributes = samples.GetValidAttributeCollection();

            DecisionTree id3 = new DecisionTree();
            TreeNode root = id3.mountTree(samples, "result", attributes);

            return PrintNode(root, "");

        }
        public string PrintNode(TreeNode root, string tabs)
        {
            string returnString = String.Empty;
            string prefix = "Najlepszy atrybut: ";
            if (tabs != String.Empty)
                prefix = " -> Prawdopodobne wyjœcie: ";
            returnString += (tabs + prefix + root.Attribute ) + Environment.NewLine;

            if (root != null && root.Attribute!= null && root.Attribute.PossibleValues != null)
            {
                for (int i = 0; i < root.Attribute.PossibleValues.Count; i++)
                {

                    returnString += (Environment.NewLine + tabs + "\t" + "Wejœcie:  " + root.Attribute.PossibleValues[i]) + Environment.NewLine;
                    TreeNode childNode = root.GetChildByBranchName(root.Attribute.PossibleValues[i]);
                    returnString += PrintNode(childNode, "\t" + tabs);
                }
            }

            return returnString;
        }

    }
}
