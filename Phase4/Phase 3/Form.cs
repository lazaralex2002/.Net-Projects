using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace LazarAlexandruConstantin
{
    public partial class Form : System.Windows.Forms.Form
    {
        private DateTimePicker dateTimePicker = new DateTimePicker();
        private Project project;

        public Form()
        {
            project = new Project();
            InitializeComponent();
            SetDataGridProperties();
            ResizeDataGridView();
        }

        private Task GetTask(int taskID)
        {
            return project.Tasks.GetTask(taskID);
        }

        private void Form_Resize(object sender, EventArgs e)
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
            dataGridView.Width = this.Width / 10 * 9;
            dataGridView.Height = this.Height / 10 * 8; ;
        }

        private void ResizeDataGridViewColumns()
        {
            if (dataGridView.Columns.Count > 0)
            {
                this.dataGridView.Columns[Columns.TaskID].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                this.dataGridView.Columns[Columns.TaskName].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                this.dataGridView.Columns[Columns.Duration].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                this.dataGridView.Columns[Columns.Start].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                this.dataGridView.Columns[Columns.Finish].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                this.dataGridView.Columns[Columns.Predecessors].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                this.dataGridView.Columns[Columns.ResourceNames].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                this.dataGridView.Columns[Columns.TaskMode].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
        }

        private void DataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                dateTimePicker.Dispose();
                if (e.ColumnIndex == Columns.Start || e.ColumnIndex == Columns.Finish)
                {
                    dateTimePicker = new DateTimePicker();
                    SetDateTimePickerValue(e.RowIndex, e.ColumnIndex);
                    dataGridView.Controls.Add(dateTimePicker);
                    dateTimePicker.Format = DateTimePickerFormat.Custom;
                    dateTimePicker.CustomFormat = "M/dd/yyyy hh:mm:ss tt";
                    Rectangle oRectangle = dataGridView.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);
                    dateTimePicker.Size = new Size(oRectangle.Width, oRectangle.Height);
                    dateTimePicker.Location = new Point(oRectangle.X, oRectangle.Y);
                    dateTimePicker.TextChanged += new EventHandler(DateTimePickerChange);
                    dateTimePicker.CloseUp += new EventHandler(DateTimePickerClose);
                }
            }
        }

        private void SetDateTimePickerValue(int rowIndex, int columnIndex)
        {
            var currentCell = dataGridView[columnIndex, rowIndex];
            string date = currentCell.Value.ToString();
            char[] separators = { ' ', '/', ':' };
            string[] fields = date.Trim().Split(separators);
            dateTimePicker.Value = new DateTime(
                int.Parse(fields[2]), //year
                int.Parse(fields[0]), //month
                int.Parse(fields[1]), //day
                int.Parse(fields[3]), //hour
                int.Parse(fields[4]), //minute
                int.Parse(fields[5])  //second
                ) ;
        }

        private void DataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                int rowTaskID = int.Parse(dataGridView[0, e.RowIndex].Value.ToString().Trim());
                var currentCellValue = dataGridView[e.ColumnIndex, e.RowIndex].Value;
                switch (e.ColumnIndex)
                {
                    case Columns.TaskName:
                        GetTask(rowTaskID).Name = currentCellValue.ToString();
                        break;
                    case Columns.Duration:
                        if (currentCellValue.ToString().IsInteger())
                        {
                            GetTask(rowTaskID).Duration = int.Parse(currentCellValue.ToString());
                        }
                        else
                        {
                            MessageBox.Show("The value must be an integer");
                            dataGridView[e.ColumnIndex, e.RowIndex].Value = GetTask(rowTaskID).Duration;
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        private void DateTimePickerChange(object sender, EventArgs e)
        {
            dataGridView.CurrentCell.Value = dateTimePicker.Text.ToString();
            int taskID = (int)dataGridView[Columns.TaskID, dataGridView.CurrentCell.RowIndex].Value;

            var selectedDate = dateTimePicker.Value;

            if (dataGridView.CurrentCell.ColumnIndex == Columns.Start)
            {
                GetTask(taskID).Start = selectedDate;
            }
            else
            {
                GetTask(taskID).Finish = selectedDate;
            }
        }

        private void DateTimePickerClose(object sender, EventArgs e)
        {
            dateTimePicker.Visible = false;
            dateTimePicker.Hide();
        }

        private void SetDataGridProperties()
        {
            dataGridView.ColumnCount = 8;

            dataGridView.AllowUserToAddRows = false;
            dataGridView.AllowUserToDeleteRows = false;

            dataGridView.Columns[Columns.TaskID].Name = "Task ID";
            dataGridView.Columns[Columns.TaskName].Name = "Task Name";
            dataGridView.Columns[Columns.Duration].Name = "Duration";
            dataGridView.Columns[Columns.Start].Name = "Start";
            dataGridView.Columns[Columns.Finish].Name = "Finish";
            dataGridView.Columns[Columns.Predecessors].Name = "Predecessors";
            dataGridView.Columns[Columns.ResourceNames].Name = "ResourceNames";
            dataGridView.Columns[Columns.TaskMode].Name = "TaskMode";
            dataGridView.Columns[Columns.TaskID].ReadOnly = true;
            dataGridView.Columns[Columns.Start].ReadOnly = true;
            dataGridView.Columns[Columns.Finish].ReadOnly = true;
            dataGridView.Columns[Columns.ResourceNames].ReadOnly = true;
            dataGridView.Columns[Columns.Predecessors].ReadOnly = true;
            dataGridView.Columns[Columns.TaskMode].ReadOnly = true;
        }

        private void LoadDataGrid()
        {
            dataGridView.Rows.Clear();
            foreach (var task in project.Tasks)
            {
                dataGridView.Rows.Add(task.TaskID, task.Name, task.Duration, task.Start, task.Finish, task.Predecessors.ContentToString(),
                task.ResourceNames.ContentToString(), task.TaskMode);
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
                project = project.Deserialize(chosenFile);
                if (project == null)
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
                project.Serialize(savedFile);
            }
        }

        private void InitializeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            project.Initialize("input.txt");
            LoadDataGrid();
        }

        private static class Columns
        {
            public const int TaskID = 0;
            public const int TaskName = 1;
            public const int Duration = 2;
            public const int Start = 3;
            public const int Finish = 4;
            public const int Predecessors = 5;
            public const int ResourceNames = 6;
            public const int TaskMode = 7;
        }

    }
}
