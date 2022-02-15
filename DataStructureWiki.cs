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
            initializeArray();
        }

        public void initializeArray()
        {
            for (int i = 0; i < rowSize; i++)
            {
                for (int j = 0; j < colSize; j++)
                {
                    myArray[i, j] = "x";
                }
            }
            displayArray();
        }

        // Q8.6 Create a display method that will show the following information
        // in a List box: Name and Category,
        public void displayArray()
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
                            if (string.Compare(myArray[i, j], myArray[x, y]) > 0))
                            {
                                swap(myArray, x, y, i, j);
                            }
                        }
                    }
                }
            }
            displayArray();
        }

        public void swap(String[] a, int x, int y, int i, int j)
        {
            int temp = a[x, y];
            a[x, y] = a[i, j];
            a[i, j] = temp;
        }

        // Q8.5 A Binary Search method for the Name in the 2D array, displaying information
        // in textboxes when found, with appropriate user feedback
        private void buttonSearch_Click(object sender, EventArgs e)
        {

        }


        #region ADD EDIT DELETE
        // Q8.2 Create ADD, EDIT and DELETE buttons that will store the information
        // from the four text boxes into the 2D Array, or allow users to change or 
        // delete this information
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
