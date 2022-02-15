using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSharp2Assessment1
{
    // Test
    // Test 2
    public partial class DataStructureWiki : Form
    {
        // Q8.1 Global 2D Array of type String using static variables for dimensions
        static int rowSize = 12; 
        static int colSize = 4;
        static String[,] myArray = new String[rowSize, colSize];


        public DataStructureWiki()
        {
            InitializeComponent();
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

        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {

        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {

        }
        #endregion ADD EDIT DELETE


        // Q8.3	Create a CLEAR method to clear the four text boxes so a new definition can be added
        private void textBoxName_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            textBoxName.Clear();
            textBoxName.ReadOnly = false;
            textBoxCategory.Clear();
            textBoxCategory.ReadOnly = false;
            textBoxStructure.Clear();
            textBoxStructure.ReadOnly = false;
            textBoxDefinition.Clear();
            textBoxDefinition.ReadOnly = false;
        }


        // Q8.4 A Bubble Sort method to sort the 2D array by Name ascending
        public void bubbleSort()
        {
            for (int x = 0; x < rowSize; x++)
            {
                for (int y = 0; y < colSize; y++)
                {
                    for (int i = 0; i < rowSize; i++)
                    {
                        for (int j = 0; j < colSize; j++)
                        {
                            if (string.Compare(myArray[i, j], myArray[x, y]) > 0)
                            {
                                swap(myArray[x, y], myArray[i, j]);
                            }
                        }
                    }
                }
            }
            displayArray();
        }
        public void swap(string a, string b)
        {
            string temp = a;
            a = b;
            b = temp;
        }


        // Q8.5 A Binary Search method for the Name in the 2D array, displaying information
        // in textboxes when found, with appropriate user feedback
        private void buttonSearch_Click(object sender, EventArgs e)
        {

        }


        // Q8.6 Create a display method that will show the following information
        // in a List box: Name and Category,
        public void DisplayArray()
        {
            listBoxArray.Items.Clear();
            listBoxArray.Items.Add("  Name      Category");
            for (int x = 0; x < rowSize; x++)
            {
                //listBoxArray.Items.Add(" ");
                string nameCategory = "";
                for (int y = 0; y < 2; y++)
                {
                    nameCategory = nameCategory + "   " + myArray[x, y];
                }
                listBoxArray.Items.Add(nameCategory);
            }
        }

        // Q8.7	Create a method so the user can select a definition(Name) from the Listbox
        // and all the information is displayed in the appropriate Textboxes,
        private void listBoxArray_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBoxName.Text = listBoxArray.SelectedItem.ToString();
        }


        // Q8.8 Create a SAVE button so the information from the 2D array can be written into
        // a binary file called definitions.dat which is sorted by Name
        private void buttonSave_Click(object sender, EventArgs e)
        {

        }

        // Q8.9 Create an OPEN button that will read the information from a binary file called
        // definitions.dat into the 2D array
        private void buttonOpen_Click(object sender, EventArgs e)
        {

        }

 
    }
}
