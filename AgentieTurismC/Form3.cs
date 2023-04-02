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
    public partial class Form3 : Form
    {
        public ServiceRezervare serviceRezervare { get; set; }
        public Form3()
        {
            InitializeComponent();
            
        }

        private void ReservationButton_Click(object sender, EventArgs e)
        {
            String name = textBox1.Text;
            String phoneNo = textBox2.Text;
            String numberOfTickets = textBox3.Text;
            this.serviceRezervare.Save(name, phoneNo, int.Parse(numberOfTickets));
            this.Close();
        }
    }
}
