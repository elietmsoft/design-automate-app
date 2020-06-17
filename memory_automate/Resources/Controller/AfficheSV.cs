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
using memory_automate.Utils;
using memory_automate.Model;

namespace memory_automate.Resources.Controller
{
    public partial class AfficheSV : MetroForm
    {
        public AfficheSV()
        {
            InitializeComponent();
        }
        private void initPicture()
        {
            BtnHote.Image = Properties.Resources.hote1_;
            BtnTransition.Image = Properties.Resources.transition1_;
            BtnTable.Image = Properties.Resources.table1_;
            BtnView.Image = Properties.Resources.view1_;
            pro.Image = Properties.Resources.pro1;
            lpro.Text = "LearnPRO Dev.";
            NoVisibleElementHote();
        }

        private void NoVisibleElementHote()
        {
            picInitial.Visible = false;
            picFinal.Visible = false;
            linit.Visible = false;
            lfinal.Visible = false;
        }
        private void ColorButton(Button bt)
        {
            foreach (Control bou in panel3.Controls)
            {
                try
                {
                    ((Button)bou).BackColor = SystemColors.Control;
                    ((Button)bou).ForeColor = SystemColors.ControlText;
                    returnPictureInit(((Button)bou));
                }
                catch
                {
                    continue;
                }
            }

            bt.BackColor = Color.FromArgb(139, 69, 30);
            bt.ForeColor = Color.White;
            //panelContenu.Controls.Clear();

        }

        private void YesVisibleElementHote()
        {
            picInitial.Visible = true;
            picFinal.Visible = true;
            linit.Visible = true;
            lfinal.Visible = true;
        }
        private void verifyBtnClicked(Button btn)
        {
            switch (btn.Name)
            {
                case "BtnHote":
                    BtnHote.Image = Properties.Resources.hote2;
                    label3.Text = BtnHote.Text;
                    imgPicture.Image = Properties.Resources.computer;
                    imgPicture.Visible = true;
                    break;
                case "BtnTransition":
                    BtnTransition.Image = Properties.Resources.transition2;
                    label3.Text = BtnTransition.Text;
                    imgPicture.Image = Properties.Resources.transition1_;
                    imgPicture.Visible = true;
                    break;
                case "BtnTable":
                    BtnTable.Image = Properties.Resources.table2;
                    label3.Text = BtnTable.Text;
                    imgPicture.Image = Properties.Resources.table1_;
                    imgPicture.Visible = true;
                    break;
                case "BtnView":
                    BtnView.Image = Properties.Resources.view2;
                    label3.Text = BtnView.Text;
                    imgPicture.Image = Properties.Resources.view1_;
                    imgPicture.Visible = true;
                    break;
            }
        }
        Dessin ds;
        private void returnPictureInit(Button btn)
        {
            switch (btn.Name)
            {
                case "BtnHote":
                    BtnHote.Image = Properties.Resources.hote1_;
                    break;
                case "BtnTransition":
                    BtnTransition.Image = Properties.Resources.transition1_;
                    break;
                case "BtnTable":
                    BtnTable.Image = Properties.Resources.table1_;
                    break;
                case "BtnView":
                    BtnView.Image = Properties.Resources.view1_;
                    break;
            }
        }
        public List<string> listHoteInit = new List<string>();
        public List<string> listHoteFin = new List<string>();
        public List<string> listPaquet = new List<string>();
        public List<object> listeGen = new List<object>();
        public List<Link> liens = new List<Link>();

        public bool determin = false;
        public bool transition = false;
        private void AfficheSV_Load(object sender, EventArgs e)
        {
            if (lblTitre.Text == "TABLE DE DETERMINISATION")
            {
                initPicture();
                ColorButton(BtnView);
                verifyBtnClicked(BtnView);
                tabledetermin.Visible = true;
                //forColorCelluleDGV(tabledetermin, listHoteInit, listHoteFin);
                tableTransit.Visible = false;
                cpytabledetermin.Visible = false;
            
            }
            else
            {
                panelReplace.Visible = true;
                initPicture();
                ColorButton(BtnTable);
                verifyBtnClicked(BtnTable);
                tableTransit.Visible = true;
                forColorCelluleDGV(tableTransit, listHoteInit, listHoteFin);
                tabledetermin.Visible = false;
                cpytabledetermin.Visible = false;
            }
            //Colorier les cellules dataGridView
            
        }
        public void forColorCelluleDGV(DataGridView dgv, List<string> listHoteInit, List<string> listHoteFin)
        {
            int L = dgv.Rows.Count - 1;
            int C = dgv.Columns.Count;

            for (int i = 0; i < L; i++)
            {
                for (int j = 0; j < C; j++)
                {
                    if (listHoteInit.Contains(dgv.Rows[i].Cells[j].Value))
                    {
                        dgv.Rows[i].Cells[j].Style.ForeColor = Color.Red;
                        dgv.Rows[i].Cells[j].Style.SelectionForeColor = Color.Red;
                    }
                    else if (listHoteFin.Contains(dgv.Rows[i].Cells[j].Value))
                    {
                        dgv.Rows[i].Cells[j].Style.ForeColor = Color.Blue;
                        dgv.Rows[i].Cells[j].Style.SelectionForeColor = Color.Blue;
                    }
                    else
                    {
                        dgv.Rows[i].Cells[j].Style.ForeColor = Color.Black;
                        dgv.Rows[i].Cells[j].Style.SelectionForeColor = Color.Black;
                    }
                }
            }
        }

        private void BtnHote_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void picFinal_Click(object sender, EventArgs e)
        {

        }

        private void BtnView_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnTransition_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnTable_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void picInitial_Click(object sender, EventArgs e)
        {

        }

        List<string> listInitColorRed = new List<string>();
        List<string> listFinalColorBlue = new List<string>();

        private void pictureNommer_Click(object sender, EventArgs e)
        {
            if (verifyAutomateDeterministe(tabledetermin))
            {
                lblTitre.Text = "AUTOMATE FINI DETERMINISTE";
                try
                {
                    int L = cpytabledetermin.Rows.Count - 1;
                    int C = cpytabledetermin.Columns.Count;
                    ListBox listUsed = new ListBox();
                    int c = 0;
                    for (int j = 0; j < C; j++)
                    {
                        for (int i = 0; i < L; i++)
                        {
                            if (j == 0)
                            {
                                string hote = cpytabledetermin.Rows[i].Cells[j].Value.ToString();
                                if (!listUsed.Items.Contains(hote))
                                {
                                    if (!hote.Trim().Equals("-") && !hote.Trim().Equals(""))
                                    {
                                        c++;
                                        listUsed.Items.Add(hote);
                                        if (verifyInitEtat(hote) > 0)
                                        {
                                            cpytabledetermin.Rows[i].Cells[j].Value = "H" + c;
                                            //cpytabledetermin.Rows[i].Cells[j].Style.ForeColor = Color.Red;
                                            //cpytabledetermin.Rows[i].Cells[j].Style.SelectionForeColor = Color.Red;
                                            listInitColorRed.Add(cpytabledetermin.Rows[i].Cells[j].Value.ToString());
                                        }
                                        else if (verifyFinalEtat(hote) > 0)
                                        {
                                            cpytabledetermin.Rows[i].Cells[j].Value = "H" + c;
                                            //cpytabledetermin.Rows[i].Cells[j].Style.ForeColor = Color.Blue;
                                            //cpytabledetermin.Rows[i].Cells[j].Style.SelectionForeColor = Color.Blue;
                                            listFinalColorBlue.Add(cpytabledetermin.Rows[i].Cells[j].Value.ToString());
                                        }
                                        else
                                        {
                                            cpytabledetermin.Rows[i].Cells[j].Value = "H" + c;
                                        }
                                    }
                                }
                            }
                             
                            else
                            {
                                string hote = cpytabledetermin.Rows[i].Cells[j].Value.ToString();
                                if ((listUsed.Items.Contains(hote) || listUsed.Items.Contains(renverseChar1(hote)) && !hote.Equals("H1")))
                                {
                                    if (!hote.Trim().Equals("-") && !hote.Trim().Equals(""))
                                    {
                                        if (listUsed.Items.Contains(renverseChar1(hote)))
                                        {
                                            cpytabledetermin.Rows[i].Cells[j].Value = "H" + (listUsed.Items.IndexOf(renverseChar1(hote)) + 1);
                                            if (verifyFinalEtat(renverseChar(hote)) > 0)
                                            {
                                                /*cpytabledetermin.Rows[i].Cells[j].Style.ForeColor = Color.Blue;
                                                cpytabledetermin.Rows[i].Cells[j].Style.SelectionForeColor = Color.Blue;*/
                                            }
                                            else if (verifyInitEtat(renverseChar(hote)) > 0)
                                            {
                                                /*cpytabledetermin.Rows[i].Cells[j].Style.ForeColor = Color.Red;
                                                cpytabledetermin.Rows[i].Cells[j].Style.SelectionForeColor = Color.Red;*/
                                            }
                                        }
                                        else
                                        {
                                            cpytabledetermin.Rows[i].Cells[j].Value = "H" + (listUsed.Items.IndexOf(hote) + 1);
                                            if (verifyFinalEtat(hote) > 0)
                                            {
                                                /*cpytabledetermin.Rows[i].Cells[j].Style.ForeColor = Color.Blue;
                                                cpytabledetermin.Rows[i].Cells[j].Style.SelectionForeColor = Color.Blue;*/
                                            }
                                            else if (verifyInitEtat(hote) > 0)
                                            {
                                                /*cpytabledetermin.Rows[i].Cells[j].Style.ForeColor = Color.Red;
                                                cpytabledetermin.Rows[i].Cells[j].Style.SelectionForeColor = Color.Red;*/
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception) { }
                cpytabledetermin.Visible = true;
                tabledetermin.Visible = false;
                tableTransit.Visible = false;
                forColorCelluleDGV1(cpytabledetermin, listInitColorRed, listFinalColorBlue);
            }
            else
            {
                MessageBox.Show("Automate est déjà deterministe", "Test de determinisation");
            }
        }

        private void forColorCelluleDGV1(DataGridView dgv, List<string> listInitColorRed, List<string> listFinalColorBlue)
        {
            int L = dgv.Rows.Count - 1;
            int C = dgv.Columns.Count;

            for (int j = 0; j < C; j++)
            {
                for (int i = 0; i < L; i++)
                {
                    if (j == 0)
                    {
                        if (listInitColorRed.Contains(dgv.Rows[i].Cells[j].Value.ToString()))
                        {
                            dgv.Rows[i].Cells[j].Style.ForeColor = Color.Red;
                            dgv.Rows[i].Cells[j].Style.SelectionForeColor = Color.Red;
                        }
                        else if (listFinalColorBlue.Contains(dgv.Rows[i].Cells[j].Value.ToString()))
                        {
                            dgv.Rows[i].Cells[j].Style.ForeColor = Color.Blue;
                            dgv.Rows[i].Cells[j].Style.SelectionForeColor = Color.Blue;
                        }
                        else
                        {
                            dgv.Rows[i].Cells[j].Style.ForeColor = Color.Black;
                            dgv.Rows[i].Cells[j].Style.SelectionForeColor = Color.Black;
                        }
                    }
                   
                }
                break; //on a besoin juste de colorier les éléments de la première colonne
            }
        }
        private int verifyInitEtat(string hote)
        {
            int counter = 0;
            int v = verifyPaquetVirgule(hote.Trim());
            if (v > 0)
            {
                string[] split = hote.Trim().Split(new char[] { ',' });
                int C = split.Length - 2;
                for (int i = C; i >= 0; i--)
                {
                    if (!split[i].Equals(""))
                    {
                        if (listHoteInit.Contains(split[i].Trim()))
                        {
                            counter++;
                        }
                    }
                }
            }
            else
            {
                if (listHoteInit.Contains(hote.Trim()))
                {
                    counter++;
                } 
            }

            return counter;
        }
        private int verifyFinalEtat(string hote)
        {
            int counter = 0;
            int v = verifyPaquetVirgule(hote.Trim());
            if (v > 0)
            {
                string[] split = hote.Trim().Split(new char[] { ',' });
                int C = split.Length - 1;
                for (int i = 0; i<C; i++)
                {
                    if (!split[i].Equals(""))
                    {
                        if (listHoteFin.Contains(split[i].Trim()))
                        {
                            counter++;
                        }
                    }
                }
            }
            else
            {
                if (listHoteFin.Contains(hote.Trim()))
                {
                    counter++;
                }
            }

            return counter;
        }
        private int verifyPaquetVirgule(string paquet)
        {
            int counter = 0;
            if (paquet.Contains(","))
            {
                int nbre = paquet.Length;
                for (int i = 0; i < nbre; i++)
                {
                    if (paquet[i] == ',')
                    {
                        counter++;
                    }
                }
            }
            return counter;
        }
        private string renverseChar1(string hote)
        {
            string chr = "";
            if (verifyPaquetVirgule(hote) > 0)
            {
                string[] split = hote.Trim().Split(new char[] { ',' });
                int C = split.Length - 1;
                for (int i = C; i >= 0; i--)
                {
                    if (!split[i].Equals(""))
                    {
                        chr += split[i];
                        chr += ",";
                    }
                }
            }
            else
            {
                chr = hote.Trim();
            }
            return chr;
        }
        private bool verifyAutomateDeterministe(DataGridView dgv)
        {
            bool test = false;
            int L = dgv.Rows.Count - 1;
            int C = dgv.Columns.Count;
            for (int i = 0; i < L; i++)
            {
                for (int j = 0; j < C; j++)
                {
                    try
                    {
                      string hote = dgv.Rows[i].Cells[j].Value.ToString();
                      if (verifyPaquetVirgule(hote) > 0)
                      {
                          string[] split = hote.Trim().Split(new char[] { ',' });
                          int nbre = split.Length;
                          if (nbre <= 2)
                          {
                              test = true;
                              break;
                          }

                      }
                      
                    }
                    catch (Exception)
                    {
                    }
                }
            }


                return test;
        }
        private string renverseChar(string hote)
        {
            string chr = "";
            if (verifyPaquetVirgule(hote) > 0)
            {
                string[] split = hote.Trim().Split(new char[] { ',' });
                int C = split.Length - 2;
                for (int i = C; i >= 0; i--)
                {
                    if (!split[i].Equals(""))
                    {
                        chr += split[i];
                        chr += ",";
                    }
                }
            }
            else
            {
                chr = hote.Trim();
            }
            return chr;
        }

        private void picDeterminAutomate_Click(object sender, EventArgs e)
        {

            lblTitre.Text = "TABLE DE DETERMINISATION";
            try
            {
                if (verifyAutomateDeterministe(tabledetermin))
                {
                    forColorCelluleDGV(tabledetermin, listHoteInit, listHoteFin);
                    tabledetermin.Visible = true;
                    tableTransit.Visible = false;
                    cpytabledetermin.Visible = false;

                }
                else
                {
                    MessageBox.Show("Automate est déjà deterministe", "Test de déterminisation");
                }
               
            }
            catch (Exception)
            {
            }
           
        }
        private string seachVoisinByHote(string hote, string paquet)
        {

            string voisin = "";

            if (verifyPaquetVirgule(hote) > 0)
            {
                string[] split = hote.Split(new char[] { ',' });
                int lengh = split.Length;
                for (int i = 0; i < lengh; i++)
                {
                    string h = split[i];
                    foreach (var item in liens)
                    {
                        if (item.sommet1.Equals(h.Trim()))
                        {
                            if (paquet.Equals("Paquet " + item.paquet))
                            {
                                voisin += item.sommet2;
                                voisin += ",";
                            }
                        }
                    }
                }
            }
            else
            {
                foreach (var item in liens)
                {
                    if (item.sommet1.Equals(hote.Trim()))
                    {
                        if (paquet.Equals("Paquet " + item.paquet))
                        {
                            voisin += item.sommet2;
                            voisin += ",";
                        }
                    }
                }
            }
            return voisin;
        }
        private void picViewTransition_Click(object sender, EventArgs e)
        {

            lblTitre.Text = "TABLE DE TRANSITION";
            try
            {
                tableTransit.Visible = true;
                tabledetermin.Visible = false;
                cpytabledetermin.Visible = false;
                forColorCelluleDGV(tableTransit, listHoteInit, listHoteFin);
                
            }
            catch (Exception)
            {
            }
           
        }
      
    }
}
