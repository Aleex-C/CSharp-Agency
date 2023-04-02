using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AgentieTurismC.Repository;

namespace AgentieTurismC
{
    public partial class Form2 : Form
    {
         ServiceExcursie serviceExcursie { get; set; }
         ServiceRezervare serviceRezervare { get; set; }
        
        public Form2()
        {
            InitializeComponent();
            ExcursieRepository excursieRepository = new ExcursieRepository();
            RezervareRepository rezervareRepository = new RezervareRepository();
            this.serviceExcursie = new ServiceExcursie(excursieRepository);
            this.serviceRezervare = new ServiceRezervare(rezervareRepository);
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            
            var trips = serviceExcursie.getAll();
            dataGridView1.DataSource = trips;
            dataGridView1.Columns["Id"].Visible = false;
            foreach (var trip in trips)
            {
                comboBox1.Items.Add(trip.landmark);
            }
            
        }

        private void FindButton_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null && startTimeBox.Text.Length == 5 && endTimeBox.Text.Length == 5 && startTimeBox.Text.Contains(":") && endTimeBox.Text.Contains(":"))
            {

                String selectedLandmark = comboBox1.SelectedItem.ToString();
                var partsStart = startTimeBox.Text.Split(':');
                var partsEnd = endTimeBox.Text.Split(':');
                if (partsStart.Length == 2 && partsEnd.Length == 2)
                {
                    TimeOnly startTime = new TimeOnly(int.Parse(partsStart[0]), int.Parse(partsStart[1]));
                    TimeOnly endTime = new TimeOnly(int.Parse(partsEnd[0]), int.Parse(partsEnd[1]));

                    var trips = serviceExcursie.getAllByLandmarkAndInterval(selectedLandmark, startTime, endTime);

                    dataGridView2.DataSource = trips;
                    DataGridViewButtonColumn reserveButton = new DataGridViewButtonColumn();
                    reserveButton.UseColumnTextForButtonValue = true;
                    reserveButton.Name = "ReserveColumn";
                    reserveButton.HeaderText = "Reserve";
                    reserveButton.Text = "Reserve";
                    if (dataGridView2.Columns["ReserveColumn"] == null)
                    {
                        dataGridView2.Columns.Add(reserveButton);
                    }
                    
                    dataGridView2.Columns["Id"].Visible = false;
                    dataGridView2.Columns["landmark"].Visible = false;
                    
                }
            }

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView) sender;
            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
        e.RowIndex >= 0)
            {
                Form3 reservationForm = new Form3();
                reservationForm.serviceRezervare = this.serviceRezervare;
                reservationForm.Show();
            }
        }
    }
}
