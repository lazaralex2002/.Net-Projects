using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Interface
{
    public partial class Form1 : Form
    {
        Dictionary<int, Interface.Task> rowsAndTasks;
        DateTimePicker dateTimePicker1 = new DateTimePicker();
        Project p;

        public Form1()
        {
            p = new Project();
            InitializeComponent();
            ResizeDataGridView();
        }
        private void Form1_Resize(object sender, EventArgs e)
        {
            Control control = (Control)sender;

            // Ensure the Form remains square (Height = Width).
            if (control.Size.Height >= Screen.FromControl(this).Bounds.Height / 2 && control.Size.Width >= Screen.FromControl(this).Bounds.Width / 2)
            {
                control.Size = new Size(control.Size.Width, control.Size.Height);
            }
            else
            {
                control.Size = new Size(Screen.FromControl(this).Bounds.Width / 2, Screen.FromControl(this).Bounds.Height / 2);
            }
            ResizeDataGridView();
            ResizeDataGridViewColumns();
        }

        private void ResizeDataGridView()
        {
            dataGridView1.Width = this.Width / 10 * 9;
            dataGridView1.Height = this.Height / 10 * 8; ;
        }

        private void ResizeDataGridViewColumns()
        {
            if (dataGridView1.Columns.Count > 0)
            {
                this.dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                this.dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                this.dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                this.dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                this.dataGridView1.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                this.dataGridView1.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
        }

        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                if (e.ColumnIndex == 3 || e.ColumnIndex == 4)
                {
                    dateTimePicker1.Hide();
                    dateTimePicker1 = new DateTimePicker();
                    dataGridView1.Controls.Add(dateTimePicker1);
                    dateTimePicker1.Format = DateTimePickerFormat.Custom;
                    dateTimePicker1.CustomFormat = "M/dd/yyyy hh:mm:ss tt";
                    Rectangle oRectangle = dataGridView1.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);
                    dateTimePicker1.Size = new Size(oRectangle.Width, oRectangle.Height);
                    dateTimePicker1.Location = new Point(oRectangle.X, oRectangle.Y);
                    dateTimePicker1.TextChanged += new EventHandler(DateTimePickerChange);
                    dateTimePicker1.CloseUp += new EventHandler(DateTimePickerClose);
                }
                else
                {
                    dateTimePicker1.Hide();
                }
            }
        }

        private void DataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex < 3 && e.ColumnIndex != 0)
            {
                switch (e.ColumnIndex)
                {
                    case 1:
                        rowsAndTasks[int.Parse(dataGridView1[0, e.RowIndex].Value.ToString().Trim())].Name = dataGridView1[e.ColumnIndex, e.RowIndex].Value.ToString();
                        break;
                    case 2:
                        if (dataGridView1[e.ColumnIndex, e.RowIndex].Value.ToString().IsInteger())
                        {
                            rowsAndTasks[(int)dataGridView1[0, e.RowIndex].Value].Duration = int.Parse(dataGridView1[e.ColumnIndex, e.RowIndex].Value.ToString());
                        }
                        else
                        {
                            MessageBox.Show("The value must be an integer");
                            dataGridView1[e.ColumnIndex, e.RowIndex].Value = rowsAndTasks[(int)dataGridView1[0, e.RowIndex].Value].Duration;
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        private void DateTimePickerChange(object sender, EventArgs e)
        {
            dataGridView1.CurrentCell.Value = dateTimePicker1.Text.ToString();//to implement finish also
            if (dataGridView1.CurrentCell.ColumnIndex == 3)
            {
                rowsAndTasks[(int)dataGridView1[0, dataGridView1.CurrentCell.RowIndex].Value].Start =
                    new DateTime(dateTimePicker1.Value.Year, dateTimePicker1.Value.Month,
                    dateTimePicker1.Value.Day, dateTimePicker1.Value.Hour, dateTimePicker1.Value.Minute,
                    dateTimePicker1.Value.Second);
            }
            else
            {
                rowsAndTasks[(int)dataGridView1[0, dataGridView1.CurrentCell.RowIndex].Value].Finish =
                new DateTime(dateTimePicker1.Value.Year, dateTimePicker1.Value.Month,
                dateTimePicker1.Value.Day, dateTimePicker1.Value.Hour, dateTimePicker1.Value.Minute,
                dateTimePicker1.Value.Second);
            }
        }

        private void DateTimePickerClose(object sender, EventArgs e)
        {
            dateTimePicker1.Visible = false;
            dateTimePicker1.Hide();
        }

        private void LoadDataGrid()
        {
            dataGridView1.ColumnCount = 7;
            rowsAndTasks = new Dictionary<int, Interface.Task>();

            dataGridView1.Columns[0].Name = "Task ID";
            dataGridView1.Columns[1].Name = "Task Name";
            dataGridView1.Columns[2].Name = "Duration";
            dataGridView1.Columns[3].Name = "Start";
            dataGridView1.Columns[4].Name = "Finish";
            dataGridView1.Columns[5].Name = "Predecessors";
            dataGridView1.Columns[6].Name = "ResourceNames";

            dataGridView1.Rows.Clear();
            int i = 1;
            foreach (var task in p.tasks)
            {
                rowsAndTasks.Add(i, task);
                dataGridView1.Rows.Add(task.TaskID, task.Name, task.Duration, task.Start, task.Finish, task.Predecessors.ContentToString(),
                    task.ResourceNames.ContentToString());
                i++;
            }
            for (i = 0; i < dataGridView1.Rows.Count; i++)
            {
                dataGridView1[0, i].ReadOnly = true;
                dataGridView1[5, i].ReadOnly = true;
                dataGridView1[6, i].ReadOnly = true;
            }
            ResizeDataGridViewColumns();
        }

        private void QuitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to quit this application?",
                                "Exit", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                Application.Exit();
            }
        }

        private void LoadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFD.InitialDirectory = System.Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFD.Title = "Select XML file";
            openFD.Filter = "XML|*.xml|All Files|*.*";
            if (openFD.ShowDialog() != DialogResult.Cancel)
            {
                string chosenFile = openFD.FileName;
                p = p.Deserialize(chosenFile);
                if (p == null)
                {
                    MessageBox.Show("The provided XML files contains an empty object!");
                }
                else
                {
                    LoadDataGrid();
                }
            }
        }

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string savedFile;
            saveFD.InitialDirectory = System.Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFD.Title = "Save XML file";
            saveFD.Filter = "XML|*.xml|All Files|*.*";
            saveFD.FileName = "program";
            if (saveFD.ShowDialog() != DialogResult.Cancel)
            {
                savedFile = saveFD.FileName;
                p.Serialize(savedFile);
            }
        }

        private void InitializeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            p.Initialize("input.txt");
            LoadDataGrid();
        }
    }
}
