﻿using System;
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

namespace CSharp2Assessment1
{
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

        private void buttonInitialise_Click(object sender, EventArgs e)
        {
            InitializeArray();
        }

        public void InitializeArray()
        {
            for (int i = 0; i < rowSize; i++)
            {
                for (int j = 0; j < colSize; j++)
                {
                    myArray[i, j] = "x";
                }
            }
            DisplayArray();
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

        private void ClearTextBoxes()
        {
            textBoxName.Clear();
            textBoxCategory.Clear();
            textBoxStructure.Clear();
            textBoxDefinition.Clear();
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
            for (int i = 0; i < nextEmptyRow; i++)
            {
                for (int j = i + 1; j < nextEmptyRow; j++)
                {
                    if (string.Compare(myArray[i, 0], myArray[j, 0]) > 0)
                    {
                        Swap(myArray[i, 0], myArray[j, 0]);
                    }
                }
            }
            DisplayArray();
            toolStripStatusLabel.Text = "Data sorted by Name ascending";
        }
        public void Swap(string a, string b)
        {
            string temp = a;
            a = b;
            b = temp;
        }
        #endregion SORT


        // Q8.5 A Binary Search method for the Name in the 2D array, displaying information
        // in textboxes when found, with appropriate user feedback
        private void buttonSearch_Click(object sender, EventArgs e)
        {
            BubbleSort();
            //int target = int.Parse(textBoxSearch.Text);
            //int row1 = 0;
            //int col1 = colSize - 1;
            //while ((row1 <= rowSize - 1) && (col1 >= 0))
            //{
            //    if (myArray[row1, 0] < target)
            //    {
            //        Console.WriteLine("Row " + row1 + " matrix[" + row1 + "," + col1 + "] " + myArray[row1, col1]);
            //        // listBoxDisplay.Items.Add(^) also possible
            //        row1++;
            //    }
            //    else if (myArray[row1, col1] > target)
            //    {
            //        Console.WriteLine("col " + col1 + " matrix[" + row1 + "," + col1 + "] " + myArray[row1, col1]);
            //        col1--;
            //    }
            //    else
            //    {
            //        Console.WriteLine("Found");
            //        return;
            //    }
            //}
        }


        // Q8.6 Create a display method that will show the following information
        // in a List box: Name and Category,
        public void DisplayArray()
        {
            listBoxArray.Items.Clear();
            for (int x = 0; x < rowSize; x++)
            {
                string nameCategory = "";
                for (int y = 0; y < 2; y++)
                {
                    nameCategory = nameCategory + "   " + myArray[x, y];
                }
                listBoxArray.Items.Add(nameCategory);
            }

            //listBoxArray.Items.Clear();
            //for (int x = 0; x < rowSize; x++)
            //{
            //    ListViewItem lvi = new ListViewItem(myArray[x, 0]);
            //    lvi.SubItems.Add(myArray[x, 1]);
            //    listBoxArray.Items.Add(lvi);
            //}
        }


        // Q8.7	Create a method so the user can select a definition(Name) from the Listbox
        // and all the information is displayed in the appropriate Textboxes,
        private void listBoxArray_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBoxName.Text = myArray[listBoxArray.SelectedIndex, 0].ToString();
            textBoxCategory.Text = myArray[listBoxArray.SelectedIndex, 1].ToString();
            textBoxStructure.Text = myArray[listBoxArray.SelectedIndex, 2].ToString();
            textBoxDefinition.Text = myArray[listBoxArray.SelectedIndex, 3].ToString();
        }



        // Q8.8 Create a SAVE button so the information from the 2D array can be written into
        // a binary file called definitions.dat which is sorted by Name
        #region SAVE
        private void buttonSave_Click(object sender, EventArgs e)
        {
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
                    for (int y = 0; y < colSize; y++)
                    {
                        for (int x = 0; x < rowSize; x++)
                        {
                            bin.Serialize(stream, myArray[x, y]);
                        }
                    }
                }
            }
            catch (IOException ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        #endregion SAVE

        // Q8.9 Create an OPEN button that will read the information from a binary file called
        // definitions.dat into the 2D array
        #region OPEN
        private void buttonOpen_Click(object sender, EventArgs e)
        {
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
                    for (int y = 0; y < colSize; y++)
                    {
                        for (int x = 0; x < rowSize; x++)
                        {
                            myArray[x, y] = (string)bin.Deserialize(stream);
                        }
                    }
                }
            }
            catch (IOException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            DisplayArray();
        }
        #endregion OPEN
    }
}
