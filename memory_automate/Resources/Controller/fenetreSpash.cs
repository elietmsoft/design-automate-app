using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Forms;

namespace memory_automate.Resources.Controller
{
    public partial class fenetreSpash : Form
    {
        public fenetreSpash()
        {
            InitializeComponent();
        }

        private void FormTableTransition_Load(object sender, EventArgs e)
        {
            timer1.Start();
            this.numero = 1;
            init1();
        }
       
        private void BtnViewTransition_Click(object sender, EventArgs e)
        {
            //chargeDataGridView(dgvTableTransition);
        }
        private void init1()
        {
            picImgChange.Image = Properties.Resources.img1;
            lblTitre.Text = "processus de partage d’information";
           this. numero = 2;
        }
        private void init2()
        {
            picImgChange.Image = Properties.Resources.img2;
            lblTitre.Text = "interaction des protocoles";
            this.numero = 3;
        }
        private void init3()
        {
            picImgChange.Image = Properties.Resources.img3;
            lblTitre.Text = "processus de communication complet";
            this.numero = 4;
        }
        private void init4()
        {
            picImgChange.Image = Properties.Resources.img4;
            lblTitre.Text = "le modèle de référence (OSI et TCP/IP)";
            this.numero = 5;
        }
        private void init5()
        {
            picImgChange.Image = Properties.Resources.img5;
            lblTitre.Text = "processus d’encapsulation et désencapsulassions";
            //this.numero = 1;
        }
        public int numero ;
        private void timer1_Tick(object sender, EventArgs e)
        {
            
            lblpourc.Text=(progressBar1.Value ++).ToString()+"%";
           
            switch (this.numero)
            {
                case 1:
                    init1();
                    break;
                case 2:
                    if(progressBar1.Value==20)
                    init2();
                    break;
                case 3:
                    if(progressBar1.Value==40)
                    init3();
                    break;
                case 4:
                    if(progressBar1.Value==60)
                    init4();
                    break;
                case 5:
                    if(progressBar1.Value==80)
                    init5();
                    break;
                default :
                    break;
            }

            if (progressBar1.Value == progressBar1.Maximum)
            {
                this.Close();
                timer1.Stop();
            }
             
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
