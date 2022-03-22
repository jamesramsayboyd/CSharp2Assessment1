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
     * A Forms app that stores information about various data structures
     * in a 2D Array, allowing the user to Add, Edit or Delete entries, 
     * search for an item, save the data to or load from a .dat file.
     */
    public partial class DataStructureWiki : Form
    {
        // Q8.1 Global 2D Array of type String using static variables for dimensions
        static int rowSize = 12;
        static int colSize = 4; // Name, Category, Structure, Definition
        static string[,] myArray = new string[rowSize, colSize];
        string defaultFileName = "definitions.dat";
        int nextEmptyRow = 0; // Pointer used to keep track of filled array elements

        public DataStructureWiki()
        {
            InitializeComponent();
            SetToolTips();
        }

        // Q8.2 Create ADD, EDIT and DELETE buttons that will store the information
        // from the four text boxes into the 2D Array, or allow users to change or 
        // delete this information
        #region ADD EDIT DELETE
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            // All four textboxes must contain information to add a new entry
            if (!string.IsNullOrEmpty(textBoxName.Text) && 
                !string.IsNullOrEmpty(textBoxCategory.Text) &&
                !string.IsNullOrEmpty(textBoxStructure.Text) && 
                !string.IsNullOrEmpty(textBoxDefinition.Text))
            {
                // Checks to see whether array is already full
                if (nextEmptyRow < rowSize)
                {
                    myArray[nextEmptyRow, 0] = textBoxName.Text;
                    myArray[nextEmptyRow, 1] = textBoxCategory.Text;
                    myArray[nextEmptyRow, 2] = textBoxStructure.Text;
                    myArray[nextEmptyRow, 3] = textBoxDefinition.Text;
                    nextEmptyRow++;
                    DisplayArray();
                    toolStripStatusLabel.Text = "Entry added";
                }
                else
                    toolStripStatusLabel.Text = "Array is full, cannot add new entry";
            }
            else
                toolStripStatusLabel.Text = "Please enter text in all textboxes";
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if (nextEmptyRow > 0)
            {
                if (listViewArray.SelectedItems.Count > 0)
                {
                    DialogResult editChoice = MessageBox.Show("Do you wish to edit this entry?",
                        "Edit Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (editChoice == DialogResult.Yes)
                    {
                        int index = listViewArray.SelectedIndices[0];
                        myArray[index, 0] = textBoxName.Text;
                        myArray[index, 1] = textBoxCategory.Text;
                        myArray[index, 2] = textBoxStructure.Text;
                        myArray[index, 3] = textBoxDefinition.Text;
                        toolStripStatusLabel.Text = "Showing all data for " + myArray[index, 0].ToString();
                        DisplayArray();
                        toolStripStatusLabel.Text = "Entry edited";
                    }
                    else
                        toolStripStatusLabel.Text = "Entry was not edited";
                }
                else
                    toolStripStatusLabel.Text = "Please select an entry to edit";
            }
            else
                toolStripStatusLabel.Text = "Cannot edit, array is empty";
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (nextEmptyRow > 0)
            {
                if (listViewArray.SelectedItems.Count > 0)
                {
                    DialogResult delChoice = MessageBox.Show("Do you wish to delete this Name?",
                 "Delete Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (delChoice == DialogResult.Yes)
                    {
                        int index = listViewArray.SelectedIndices[0];
                        myArray[index, 0] = "";
                        myArray[index, 1] = "";
                        myArray[index, 2] = "";
                        myArray[index, 3] = "";

                        // Using swap function to move selected item to end of array then
                        // decrementing nextEmptyRow and redisplaying list to 'delete' element
                        while (index < nextEmptyRow - 1)
                        {   
                            Swap(index + 1, index);
                            index++;
                        }
                        nextEmptyRow--;
                        DisplayArray();
                        ClearTextBoxes();
                        toolStripStatusLabel.Text = "Entry deleted";
                    }
                    else
                        toolStripStatusLabel.Text = "Entry was not deleted";
                }
                else
                    toolStripStatusLabel.Text = "Please select an entry to delete";
            }
            else
                toolStripStatusLabel.Text = "Cannot delete, array is empty";
        }
        #endregion ADD EDIT DELETE

        // Q8.3	Create a CLEAR method to clear the four text boxes so a new definition can be added
        #region CLEAR
        private void textBoxName_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ClearTextBoxes();
            ResetColours();
        }
        private void textBoxCategory_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ClearTextBoxes();
            ResetColours();
        }
        private void textBoxStructure_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ClearTextBoxes();
            ResetColours();
        }
        private void textBoxDefinition_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ClearTextBoxes();
            ResetColours();
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
            ResetColours();
        }
        #endregion CLEAR

        // Q8.4 A Bubble Sort method to sort the 2D array by Name ascending
        #region SORT
        private void buttonSort_Click(object sender, EventArgs e)
        {
            if (nextEmptyRow > 0)
                BubbleSort();
            else
                toolStripStatusLabel.Text = "Cannot sort, array is empty";
        }

        public void BubbleSort()
        {
            for (int i = 0; i < nextEmptyRow - 1; i++)
            {
                for (int j = i + 1; j < nextEmptyRow; j++)
                {
                    if (string.Compare(myArray[i, 0], myArray[j, 0]) > 0)
                    {
                        Swap(i, j);
                    }
                }
            }
            DisplayArray();
            toolStripStatusLabel.Text = "Data sorted by Name ascending";
        }

        // Swap function creates a temp array, for loop iterates through all four columns
        // of data swapping each element
        public void Swap(int a, int b)
        {
            string[] temp = new string[colSize];
            for (int i = 0; i < colSize; i++)
            {
                temp[i] = myArray[a, i];
                myArray[a, i] = myArray[b, i];
                myArray[b, i] = temp[i];
            }
        }

        #endregion SORT

        // Q8.5 A Binary Search method for the Name in the 2D array, displaying information
        // in textboxes when found, with appropriate user feedback
        #region SEARCH
        private void buttonSearch_Click(object sender, EventArgs e)
        {
            if (nextEmptyRow > 0)
            {
                if (!string.IsNullOrEmpty(textBoxSearch.Text))
                {
                    BubbleSort(); // Data is sorted before Binary Search
                    string target = textBoxSearch.Text;
                    int upperBound = nextEmptyRow - 1; ;
                    int lowerBound = 0;
                    int mid = 0;
                    bool found = false;

                    while (lowerBound <= upperBound)
                    {
                        mid = (upperBound + lowerBound) / 2;
                        if (string.Compare(target, myArray[mid, 0], ignoreCase: true) == 0)
                        {
                            found = true;
                            break;
                        }
                        else if (string.Compare(target, myArray[mid, 0], ignoreCase: true) < 0)
                        {
                            upperBound = mid - 1;
                        }
                        else if (string.Compare(target, myArray[mid, 0], ignoreCase: true) > 0)
                        {
                            lowerBound = mid + 1;
                        }
                    }
                    if (found)
                    {
                        toolStripStatusLabel.Text = "Search target \"" + target + "\" was found";
                        listViewArray.Items[mid].BackColor = Color.Blue;
                        listViewArray.Items[mid].ForeColor = Color.White;

                        textBoxName.Text = myArray[mid, 0];
                        textBoxCategory.Text = myArray[mid, 1];
                        textBoxStructure.Text = myArray[mid, 2];
                        textBoxDefinition.Text = myArray[mid, 3];
                    }
                    else
                        toolStripStatusLabel.Text = "Search target \"" + target + "\" was not found";
                }
                else
                    toolStripStatusLabel.Text = "Please enter a search query";
            }
            else
                toolStripStatusLabel.Text = "Cannot search, array is empty";
            
        }

        #endregion SEARCH

        // Q8.6 Create a display method that will show the following information
        // in a List box: Name and Category,
        #region DISPLAY
        public void DisplayArray()
        {
            listViewArray.Items.Clear();
            for (int x = 0; x < nextEmptyRow; x++)
            {
                ListViewItem lvi = new ListViewItem(myArray[x, 0]);
                lvi.SubItems.Add(myArray[x, 1]);
                listViewArray.Items.Add(lvi);
            }
        }
        // Method to reset colours of Listview after Binary Search method highlights a row
        public void ResetColours()
        {
            for (int i = 0; i < nextEmptyRow; i++)
            {
                listViewArray.Items[i].BackColor = Color.White;
                listViewArray.Items[i].ForeColor = Color.Black;
            }
        }
        
        public void SetToolTips()
        {
            toolTip1.SetToolTip(buttonAdd, "Enter data in all four textboxes to add it to the array");
            toolTip1.SetToolTip(buttonEdit, "Select an item from the array to edit its data");
            toolTip1.SetToolTip(buttonDelete, "Select an item from the array to delete it");
            toolTip1.SetToolTip(buttonSort, "Sort the data by Name ascending");
            toolTip1.SetToolTip(buttonOpen, "Open a .dat file");
            toolTip1.SetToolTip(buttonSave, "Save data to a .dat file");
            toolTip1.SetToolTip(buttonSearch, "Enter a data structure name in the textbox to search");
            toolTip1.SetToolTip(textBoxName, "Double-click to clear");
            toolTip1.SetToolTip(textBoxCategory, "Double-click to clear");
            toolTip1.SetToolTip(textBoxStructure, "Double-click to clear");
            toolTip1.SetToolTip(textBoxDefinition, "Double-click to clear");
            toolTip1.SetToolTip(textBoxSearch, "Double-click to clear");
        }
        #endregion DISPLAY

        // Q8.7	Create a method so the user can select a definition (Name) from the Listbox
        // and all the information is displayed in the appropriate Textboxes,
        #region SELECT
        private void listViewArray_Click(object sender, EventArgs e)
        {
            ResetColours();
            int index = listViewArray.SelectedIndices[0];
            textBoxName.Text = myArray[index, 0].ToString();
            textBoxCategory.Text = myArray[index, 1].ToString();
            textBoxStructure.Text = myArray[index, 2].ToString();
            textBoxDefinition.Text = myArray[index, 3].ToString();
            toolStripStatusLabel.Text = "Showing all data for " + myArray[index, 0].ToString();
        }
        #endregion SELECT

        // Q8.8 Create a SAVE button so the information from the 2D array can be written into
        // a binary file called definitions.dat which is sorted by Name
        #region SAVE
        private void buttonSave_Click(object sender, EventArgs e)
        {
            BubbleSort(); // File must be sorted by Name when saving
            SaveFileDialog saveFileDialogVG = new SaveFileDialog();
            saveFileDialogVG.InitialDirectory = Application.StartupPath;
            saveFileDialogVG.Filter = "DAT file|*.dat";
            saveFileDialogVG.Title = "Save a DAT File";
            saveFileDialogVG.FileName = defaultFileName;
            saveFileDialogVG.DefaultExt = "dat";
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
            toolStripStatusLabel.Text = "Saved data to .dat file";
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

        // User is prompted to open a .dat file on loading the form. If they choose not to, 
        // the array is initialised with empty string values
        private void DataStructureWiki_Load(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialogVG = new OpenFileDialog();
            openFileDialogVG.InitialDirectory = Application.StartupPath;
            openFileDialogVG.Filter = "DAT Files|*.dat";
            openFileDialogVG.Title = "Select a DAT File";
            if (openFileDialogVG.ShowDialog() == DialogResult.OK)
            {
                openRecord(openFileDialogVG.FileName);
                toolStripStatusLabel.Text = "Opened .dat file";
            }
            else
            {
                for (int x = 0; x < rowSize; x++)
                {
                    for (int y = 0; y < colSize; y++)
                    {
                        myArray[x, y] = "";
                    }
                }
            }
            DisplayArray();
        }

        // When the user manually loads a .dat file, the nextEmptyRow pointer is reset to 0
        private void buttonOpen_Click(object sender, EventArgs e)
        {
            nextEmptyRow = 0;
            OpenFileDialog openFileDialogVG = new OpenFileDialog();
            openFileDialogVG.InitialDirectory = Application.StartupPath;
            openFileDialogVG.Filter = "DAT Files|*.dat";
            openFileDialogVG.Title = "Select a DAT File";
            if (openFileDialogVG.ShowDialog() == DialogResult.OK)
            {
                openRecord(openFileDialogVG.FileName);
            }
            toolStripStatusLabel.Text = "Opened .dat file";
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
