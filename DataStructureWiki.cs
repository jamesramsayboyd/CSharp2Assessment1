using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.ListView;

namespace CSharp2Assessment1
{
    /* 
     * James Boyd 30041547
     * C Sharp 2 Assignment AT01
     * Data Structure Wiki
     */
    public partial class DataStructureWiki : Form
    {
        // Q8.1 Global 2D Array of type String using static variables for dimensions
        static int rowSize = 12;
        static int colSize = 4;
        static string[,] myArray = new string[rowSize, colSize];
        string defaultFileName = "definitions.dat";
        int nextEmptyRow = 0;

        public DataStructureWiki()
        {
            InitializeComponent();
        }


        // Q8.2 Create ADD, EDIT and DELETE buttons that will store the information
        // from the four text boxes into the 2D Array, or allow users to change or 
        // delete this information
        #region ADD EDIT DELETE
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            myArray[nextEmptyRow, 0] = textBoxName.Text;
            myArray[nextEmptyRow, 1] = textBoxCategory.Text;
            myArray[nextEmptyRow, 2] = textBoxStructure.Text;
            myArray[nextEmptyRow, 3] = textBoxDefinition.Text;
            nextEmptyRow++;
            DisplayArray();
            toolStripStatusLabel.Text = "Entry added";
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            myArray[listBoxArray.SelectedIndex, 0] = textBoxName.Text;
            myArray[listBoxArray.SelectedIndex, 1] = textBoxCategory.Text;
            myArray[listBoxArray.SelectedIndex, 2] = textBoxStructure.Text;
            myArray[listBoxArray.SelectedIndex, 3] = textBoxDefinition.Text;
            DisplayArray();
            toolStripStatusLabel.Text = "Entry edited";
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            for (int j = 0; j < 4; j++)
            {
                myArray[listBoxArray.SelectedIndex, j] = "x";
            }
            nextEmptyRow--;
            DisplayArray();
            toolStripStatusLabel.Text = "Entry deleted";
        }
        #endregion ADD EDIT DELETE


        // Q8.3	Create a CLEAR method to clear the four text boxes so a new definition can be added
        #region CLEAR
        private void textBoxName_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ClearTextBoxes();
        }
        private void textBoxCategory_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ClearTextBoxes();
        }
        private void textBoxStructure_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ClearTextBoxes();
        }
        private void textBoxDefinition_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ClearTextBoxes();
        }
        private void ClearTextBoxes()
        {
            textBoxName.Clear();
            textBoxCategory.Clear();
            textBoxStructure.Clear();
            textBoxDefinition.Clear();
            toolStripStatusLabel.Text = "Textboxes cleared";
        }

        // A double mouse click in the search text box will clear the search input box
        private void textBoxSearch_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            textBoxSearch.Clear();
        }
        #endregion CLEAR


        // Q8.4 A Bubble Sort method to sort the 2D array by Name ascending
        #region SORT
        private void buttonSort_Click(object sender, EventArgs e)
        {
            BubbleSort();
        }

        public void BubbleSort()
        {
            for (int i = 0; i < nextEmptyRow - 1; i++)
            {
                for (int j = i + 1; j < nextEmptyRow; j++)
                {
                    if (string.Compare(myArray[i, 0], myArray[j, 0]) > 0)
                    {
                        for (int k = 0; k < colSize; k++)
                        {
                            Swap(myArray, i, j, k);
                        }
                    }
                }
            }
            DisplayArray();
            toolStripStatusLabel.Text = "Data sorted by Name ascending";
        }

        // Swap function creates a temp 2D array, for loop iterates through all four columns
        // of data swapping each element
        public void Swap(string[,] x, int a, int b, int k)
        {
            string[,] temp = new string[1, colSize];
            temp[0, k] = x[a, k];
            x[a, k] = x[b, k];
            x[b, k] = temp[0, k];
        }
        #endregion SORT


        // Q8.5 A Binary Search method for the Name in the 2D array, displaying information
        // in textboxes when found, with appropriate user feedback
        #region SEARCH
        private void buttonSearch_Click(object sender, EventArgs e)
        {
            BubbleSort();
            string target = textBoxSearch.Text.ToUpper();
            int upperBound = nextEmptyRow - 1; ;
            int lowerBound = 0;
            int mid = 0;
            bool found = false;

            while (lowerBound <= upperBound)
            {
                mid = (upperBound + lowerBound) / 2;
                if (string.Compare(target, myArray[mid, 0].ToUpper()) == 0)
                {
                    found = true;
                    break;
                }
                else if (string.Compare(target, myArray[mid, 0].ToUpper()) < 0)
                {
                    upperBound = mid - 1;
                }
                else if (string.Compare(target, myArray[mid, 0].ToUpper()) > 0)
                {
                    lowerBound = mid + 1;
                }
            }
            if (found)
            {
                toolStripStatusLabel.Text = "Found";
                listBoxArray.SelectedIndex = mid;
                listViewArray.SelectedIndices.Add(mid);

                textBoxName.Text = myArray[mid, 0];
                textBoxCategory.Text = myArray[mid, 1];
                textBoxStructure.Text = myArray[mid, 2];
                textBoxDefinition.Text = myArray[mid, 3];
            }
            else
            {
                toolStripStatusLabel.Text = "Not found";
            }
        }
        #endregion SEARCH

        // Q8.6 Create a display method that will show the following information
        // in a List box: Name and Category,
        #region DISPLAY
        public void DisplayArray()
        {
            listBoxArray.Items.Clear();

            //for (int x = 0; x < rowSize; x++)
            //{
            //    string nameCategory = "";
            //    for (int y = 0; y < 2; y++)
            //    {
            //        nameCategory = nameCategory + "   " + myArray[x, y];
            //    }
            //    listBoxArray.Items.Add(nameCategory);
            //}

            for (int i = 0; i < nextEmptyRow; i++)
            {
                string name = myArray[i, 0].ToString();
                string category = myArray[i, 1].ToString();
                int x = 28 - name.Length - category.Length;
                string space = new string(' ', x);
                string display = name + space + category;
                listBoxArray.Items.Add(display);
            }


            listViewArray.Items.Clear();
            for (int x = 0; x < rowSize; x++)
            {
                ListViewItem lvi = new ListViewItem(myArray[x, 0]);
                lvi.SubItems.Add(myArray[x, 1]);
                listViewArray.Items.Add(lvi);
            }

        }
        #endregion DISPLAY

        // Q8.7	Create a method so the user can select a definition(Name) from the Listbox
        // and all the information is displayed in the appropriate Textboxes,
        #region SELECT
        private void listBoxArray_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBoxName.Text = myArray[listBoxArray.SelectedIndex, 0].ToString();
            textBoxCategory.Text = myArray[listBoxArray.SelectedIndex, 1].ToString();
            textBoxStructure.Text = myArray[listBoxArray.SelectedIndex, 2].ToString();
            textBoxDefinition.Text = myArray[listBoxArray.SelectedIndex, 3].ToString();
            toolStripStatusLabel.Text = "Showing all data for " + myArray[listBoxArray.SelectedIndex, 0].ToString();
        }

        private void listViewArray_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedIndexCollection indices = new SelectedIndexCollection(listViewArray);
            foreach (int index in indices)
            {
                textBoxName.Text = myArray[index, 0].ToString();
                textBoxCategory.Text = myArray[index, 1].ToString();
                textBoxStructure.Text = myArray[index, 2].ToString();
                textBoxDefinition.Text = myArray[index, 3].ToString();
                toolStripStatusLabel.Text = "Showing all data for " + myArray[index, 0].ToString();
            }            
        }
        #endregion SELECT


        // Q8.8 Create a SAVE button so the information from the 2D array can be written into
        // a binary file called definitions.dat which is sorted by Name
        #region SAVE
        private void buttonSave_Click(object sender, EventArgs e)
        {
            BubbleSort();
            SaveFileDialog saveFileDialogVG = new SaveFileDialog();
            saveFileDialogVG.Filter = "DAT file|*.dat";
            saveFileDialogVG.Title = "Save a DAT File";
            saveFileDialogVG.DefaultExt = ".dat";
            //saveFileDialogVG.InitialDirectory = ToString();
            saveFileDialogVG.ShowDialog();
            string fileName = saveFileDialogVG.FileName;
            if (saveFileDialogVG.FileName != "")
            {
                saveRecord(fileName);
            }
            else
            {
                saveRecord(defaultFileName);
            }
        }
        private void saveRecord(string saveFileName)
        {
            try
            {
                using (Stream stream = File.Open(saveFileName, FileMode.Create))
                {
                    BinaryFormatter bin = new BinaryFormatter();
                    for (int x = 0; x < rowSize; x++)
                    {
                        for (int y = 0; y < colSize; y++)
                        {
                            bin.Serialize(stream, myArray[x, y]);
                        }
                    }
                }
            }
            catch (IOException ex)
            {
                toolStripStatusLabel.Text = "Could not save .dat file";
            }
        }
        #endregion SAVE

        // Q8.9 Create an OPEN button that will read the information from a binary file called
        // definitions.dat into the 2D array
        #region OPEN
        private void buttonOpen_Click(object sender, EventArgs e)
        {
            nextEmptyRow = 0;
            OpenFileDialog openFileDialogVG = new OpenFileDialog();
            openFileDialogVG.Filter = "DAT Files|*.dat";
            openFileDialogVG.Title = "Select a DAT File";
            if (openFileDialogVG.ShowDialog() == DialogResult.OK)
            {
                openRecord(openFileDialogVG.FileName);
            }
        }
        private void openRecord(string openFileName)
        {
            try
            {
                using (Stream stream = File.Open(openFileName, FileMode.Open))
                {
                    BinaryFormatter bin = new BinaryFormatter();
                    for (int x = 0; x < rowSize; x++)
                    {
                        for (int y = 0; y < colSize; y++)
                        {
                            myArray[x, y] = (string)bin.Deserialize(stream);
                        }
                        nextEmptyRow++;
                    }
                }
            }
            catch (IOException ex)
            {
                toolStripStatusLabel.Text = "Could not open .dat file";
            }
            DisplayArray();
        }
        #endregion OPEN

    }
}
