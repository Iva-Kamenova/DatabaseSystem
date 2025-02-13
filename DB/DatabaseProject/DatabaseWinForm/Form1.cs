using DatabaseProject;
using DatabaseProject.MyUtils;

namespace DatabaseWinForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void btnExecute_Click(object sender, EventArgs e)
        {
            listViewQuery.Items.Clear();
            listViewQuery.Columns.Clear(); 

            string command = txtBoxQuery.Text;
            string result = Database.ExecuteQuery(command);
            DisplayResultInListView(result);
        }

        private void DisplayResultInListView(string result)
        {
            var rows = MyString.Split(result, '\n');

            if (rows.Length == 0)
            {
                listViewQuery.Items.Add(new ListViewItem("No results."));
                return;
            }

            var firstRowValues = MyString.Split(rows[0], '\t');
            for (int i = 0; i < firstRowValues.Length; i++)
            {
                listViewQuery.Columns.Add("Column " + (i + 1), 100);
            }

            foreach (var row in rows)
            {
                var values = MyString.Split(row, '\t');
                var listItem = new ListViewItem(values[0]);
                for (int i = 1; i < values.Length; i++)
                {
                    listItem.SubItems.Add(values[i]);
                }
                listViewQuery.Items.Add(listItem);
            }

            foreach (ColumnHeader column in listViewQuery.Columns)
            {
                column.Width = -2;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            listViewQuery.Items.Clear();
            listViewQuery.Columns.Clear();

            listViewQuery.View = View.Details;
            listViewQuery.GridLines = true;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtBoxQuery.Clear();
            listViewQuery.Items.Clear();
            listViewQuery.Columns.Clear();
        }
    }
}
