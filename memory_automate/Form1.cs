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
using memory_automate.Model;
using memory_automate.Utils;
using memory_automate.Controller;
using memory_automate.Resources.Controller;

namespace memory_automate
{
    public partial class Form1 : MetroForm
    {
        public Form1()
        {
            InitializeComponent();
            fenetreSpash f = new fenetreSpash();
            this.Hide();
            f.ShowDialog();
            this.Show();
        }
        int x = 0;
        int y = 0;

        bool add = true;
        bool addCircle = false;
        bool addArc = false;

        bool addInit = false;
        bool addFin = false;

        Point ptA = new Point();
        Point ptB = new Point();
        List<Link> liens = new List<Link>();

        List<string> listPaquet = new List<string>();
        List<string> listHoteInit = new List<string>();
        List<string> listHoteFin = new List<string>();

        List<Position> pts = new List<Position>();
        string s1 = "";
        string s2 = "";

        //Liste des sommets
        List<object> listeGen = new List<object>();

        private bool isBefore(Point Pa, Point Pb)
        {
            if (Pa.X - Pb.X > 0)
            {
                return true;
            }
            else
                return false;
        }
        Dessin ds;
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
        private void YesVisibleElementHote()
        {
            picInitial.Visible = true;
            picFinal.Visible = true;
            linit.Visible = true;
            lfinal.Visible = true;
        }
        private void verifyBtnClicked(Button btn){
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
        private void returnPictureInit(Button btn){
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
        
        private void Form1_Load(object sender, EventArgs e)
        {
            initPicture();
            ds = new Dessin(pictureDesign);
            ds.color_line = Brushes.White;
            ds.color_circle = Color.White;
            BtnHote.PerformClick();

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
           
            bt.BackColor = Color.FromArgb(139,69,30);
            bt.ForeColor = Color.White;
            //panelContenu.Controls.Clear();

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void BtnHote_Click(object sender, EventArgs e)
        {
            ColorButton(BtnHote);
            verifyBtnClicked(BtnHote);
            YesVisibleElementHote();
            addArc = false;
            addCircle = true;

            if (addInit)
            {
               DialogResult rp = MessageBox.Show("Voulez-vous saisir un état initial", "CHANGER L'ETAT DE L'HOTE", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
               if (rp == DialogResult.No)
               {
                   addInit = false;
                   addFin = false;
               }
            }
            if (addFin)
            {
                DialogResult rp = MessageBox.Show("Voulez-vous saisir un état Final", "CHANGER L'ETAT DE L'HOTE", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (rp == DialogResult.No)
                {
                    addInit = false;
                    addFin = false;
                }
            }
 
        }

        private void BtnTransition_Click(object sender, EventArgs e)
        {
            ColorButton(BtnTransition);
            verifyBtnClicked(BtnTransition);
            NoVisibleElementHote();

            addArc = true;
            addCircle = false;
        }
        bool voir = false;

        private DataTable dataTable(List<string> listPaquet)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Hôte");
            foreach(var item in listPaquet)
            {
                DataColumn dc = new DataColumn("Paquet " + item);
                dt.Columns.Add(dc);
            }
            return dt;
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
       
        private void viewTableTransition(DataTable dt, DataGridView dgv)
        {
            List<object> copyListGen = new List<object>();
            copyListGen = listeGen;
            int L = copyListGen.Count;
            //Génération des lignes dans le DataGridView
            for (int i = 0; i < L; i++)
            {
                dt.Rows.Add(new object[] { "" });
            }
            //
            dgv.DataSource = dt;

            //Gestion de la taille de colonne DataGridView
            int C = dgv.Columns.Count;
            for (int j = 0; j < C; j++)
            {
                dgv.Columns[j].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            //
            //Suppression de la ligne header de DataGridView
            dgv.RowHeadersVisible = false;
            copyListGen.Sort();//trie selon ordre alphabétique

                for (int i = 0; i < L; i++)
                {
                    for (int j = 1; j < C; j++)
                    {
                        string ColumnName = dgv.Columns[j].Name.ToString();
                        string voisinHote = "";
                        int counter = 0;
                        foreach (var p in liens)
                        {
                            if (p.sommet1.Equals(copyListGen[i].ToString()))
                            {
                                if (ColumnName.Equals("Paquet " + p.paquet))
                                {

                                    voisinHote += p.sommet2;
                                    voisinHote += ", ";
                                    counter++;
                                } 
                            }
                        }
                        if (counter==0)
                        {
                            voisinHote += " - ";
                        }
                        dgv.Rows[i].Cells[j].Value = voisinHote;
                    }
                        dgv.Rows[i].Cells[0].Value = copyListGen[i].ToString();
                   
                }
   
        }
        
        private void BtnTable_Click(object sender, EventArgs e)
        {
            ColorButton(BtnTable);
            verifyBtnClicked(BtnTable);
            AfficheSV af = new AfficheSV();
            viewTableTransition(dataTable(listPaquet), af.tableTransit);

            af.listHoteInit = listHoteInit;
            af.listHoteFin = listHoteFin;
            af.listeGen = listeGen;
            af.listPaquet = listPaquet;

            af.lblTitre.Text = "TABLE DE TRANSITION";
            af.forColorCelluleDGV(af.tableTransit, af.listHoteInit, af.listHoteFin);
            af.tableTransit.Visible = true;

            viewTableDetermin(ReturnViewTableTransition(dataTable(listPaquet), af.tabledetermin));
            af.forColorCelluleDGV(af.tabledetermin, af.listHoteInit, af.listHoteFin);
            af.tabledetermin.Visible =false;

            viewTableDetermin(ReturnViewTableTransition(dataTable(listPaquet), af.cpytabledetermin));
            //af.forColorCelluleDGV(af.cpytabledetermin, af.listHoteInit, af.listHoteFin);
            af.tabledetermin.Visible = false;
            af.cpytabledetermin.Visible = false;

            af.ShowDialog();
            BtnTransition.PerformClick();
                  
        }
        
        private void BtnView_Click(object sender, EventArgs e)
        {
            /*
            ColorButton(BtnView);
            verifyBtnClicked(BtnView);
            NoVisibleElementHote();*/
           
        }
       
        private DataGridView ReturnViewTableTransition(DataTable dt, DataGridView dgv)
        {
            List<object> copyListGen = new List<object>();
            copyListGen = listeGen;
            int L = copyListGen.Count;
            //Génération des lignes dans le DataGridView
            for (int i = 0; i < L; i++)
            {
                dt.Rows.Add(new object[] { "" });
            }
            //
            dgv.DataSource = dt;

            //Gestion de la taille de colonne DataGridView
            int C = dgv.Columns.Count;
            for (int j = 0; j < C; j++)
            {
                dgv.Columns[j].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            //
            //Suppression de la ligne header de DataGridView
            dgv.RowHeadersVisible = false;
            copyListGen.Sort();//trie selon ordre alphabétique

            for (int i = 0; i < L; i++)
            {
                for (int j = 1; j < C; j++)
                {
                    string ColumnName = dgv.Columns[j].Name.ToString();
                    string voisinHote = "";
                    int counter = 0;
                    foreach (var p in liens)
                    {
                        if (p.sommet1.Equals(copyListGen[i].ToString().Trim()))
                        {
                            if (ColumnName.Equals("Paquet " + p.paquet))
                            {

                                voisinHote += p.sommet2;
                                voisinHote += ",";
                                counter++;
                            }
                        }
                    }
                    if (counter == 0)
                    {
                        voisinHote += " - ";
                    }
                    dgv.Rows[i].Cells[j].Value = voisinHote;
                }
                dgv.Rows[i].Cells[0].Value = copyListGen[i].ToString();

            }
            return dgv;
        }
        private void viewTableDetermin(DataGridView dgv)
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("Hôte");
                foreach (var item in listPaquet)
                {
                    DataColumn dc = new DataColumn("Paquet " + item);
                    dt.Columns.Add(dc);
                }

                int L = dgv.Rows.Count - 1;
                int C = dgv.Columns.Count;

                string[] tab = new string[C];

                for (int i = 0; i < 1; i++)
                {
                    for (int j = 0; j < C; j++)
                    {
                        tab[j] = dgv.Rows[i].Cells[j].Value.ToString().Trim();
                    }
                    //Cette liste nous permet de detecter par la suite lors de la déterminisation de l'auto si le sommet est déjà utilisé
                    List<string> usingHoteByDetermin = new List<string>();
                    //ajout d'une ligne de données  
                    if (i == 0)
                    {
                        dt.Rows.Add(tab);
                        //parcours de voisin pour deduire d'autres voisins
                        for (int j = 0; j < tab.Length; j++)
                        {
                            string[] cpyTab = new string[tab.Length];
                            if (j < tab.Length - 1)
                            {
                                if (!tab[j + 1].Trim().Equals("-") && !tab[j + 1].Trim().Equals(""))
                                {
                                    if (j == 0)
                                    {
                                        cpyTab[0] = tab[j + 1];
                                        if (!cpyTab[0].Trim().Equals("-") && !cpyTab[0].Trim().Equals(""))
                                        {
                                            if (!usingHoteByDetermin.Contains(cpyTab[0].Trim()) || !usingHoteByDetermin.Contains(renverseChar1(cpyTab[0].Trim())))
                                            {
                                                //ajout de l'hote
                                                usingHoteByDetermin.Add(cpyTab[0].Trim());
                                                //

                                                for (int k = 1; k < dgv.Columns.Count; k++)
                                                {
                                                    cpyTab[k] = seachVoisinByHote(cpyTab[0], dgv.Columns[k].Name.ToString());
                                                }
                                                dt.Rows.Add(cpyTab);

                                                //parcours de l'autre ligne pour trouver de nouveaux voisins
                                                string[] cpytab1 = new string[cpyTab.Length];
                                                for (int d = 0; d < cpyTab.Length; d++)
                                                {
                                                    if (d == 0)
                                                    {
                                                        cpytab1[0] = cpyTab[d + 1];
                                                        if (!cpytab1[0].Trim().Equals("-") && !cpytab1[0].Trim().Equals(""))
                                                        {
                                                            if (!usingHoteByDetermin.Contains(cpytab1[0].Trim()) || !usingHoteByDetermin.Contains(renverseChar1(cpytab1[0].Trim())))
                                                            {
                                                                //ajoute de l'hôte
                                                                usingHoteByDetermin.Add(cpytab1[0].Trim());

                                                                for (int k = 1; k < dgv.Columns.Count; k++)
                                                                {
                                                                    cpytab1[k] = seachVoisinByHote(cpytab1[0], dgv.Columns[k].Name.ToString());
                                                                }
                                                                dt.Rows.Add(cpytab1);
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        string[] cpytab2 = new string[cpyTab.Length];
                                                        if (d < cpyTab.Length - 1)
                                                        {
                                                            cpytab2[0] = cpyTab[d + 1];
                                                            if (!cpytab2[0].Trim().Equals("-") && !cpytab2[0].Trim().Equals(""))
                                                            {
                                                                if (!usingHoteByDetermin.Contains(cpytab2[0].Trim()) || !usingHoteByDetermin.Contains(renverseChar1(cpytab2[0].Trim())))
                                                                {
                                                                    //Ajout de l'hôte
                                                                    usingHoteByDetermin.Add(cpytab2[0].Trim());

                                                                    for (int k = 1; k < dgv.Columns.Count; k++)
                                                                    {
                                                                        cpytab2[k] = seachVoisinByHote(cpytab2[0], dgv.Columns[k].Name.ToString());
                                                                    }
                                                                    dt.Rows.Add(cpytab2);

                                                                    //parcours d'une autre ligne[cpytab1]
                                                                    string[] cpytab3 = new string[cpytab1.Length];
                                                                    for (int e = 0; e < cpytab1.Length; e++)
                                                                    {
                                                                        if (e == 0)
                                                                        {
                                                                            cpytab3[0] = cpytab1[e + 1];
                                                                            if (!cpytab3[0].Trim().Equals("-") && !cpytab3[0].Trim().Equals(""))
                                                                            {
                                                                                if (!usingHoteByDetermin.Contains(cpytab3[0].Trim()) || !usingHoteByDetermin.Contains(renverseChar1(cpytab3[0].Trim())))
                                                                                {
                                                                                    //ajoute de l'hôte
                                                                                    usingHoteByDetermin.Add(cpytab3[0].Trim());

                                                                                    for (int k = 1; k < dgv.Columns.Count; k++)
                                                                                    {
                                                                                        cpytab3[k] = seachVoisinByHote(cpytab3[0], dgv.Columns[k].Name.ToString());
                                                                                    }
                                                                                    dt.Rows.Add(cpytab3);
                                                                                }
                                                                            }
                                                                        }
                                                                        else
                                                                        {
                                                                            string[] cpytab4 = new string[cpytab1.Length];
                                                                            if (e < cpytab1.Length - 1)
                                                                            {
                                                                                cpytab4[0] = cpytab1[e + 1];
                                                                                if (!cpytab4[0].Trim().Equals("-") && !cpytab4[0].Trim().Equals(""))
                                                                                {
                                                                                    if (!usingHoteByDetermin.Contains(cpytab4[0].Trim()) || !usingHoteByDetermin.Contains(renverseChar1(cpytab4[0].Trim())))
                                                                                    {
                                                                                        //ajoute de l'hôte
                                                                                        usingHoteByDetermin.Add(cpytab4[0].Trim());

                                                                                        for (int k = 1; k < dgv.Columns.Count; k++)
                                                                                        {
                                                                                            cpytab4[k] = seachVoisinByHote(cpytab4[0], dgv.Columns[k].Name.ToString());
                                                                                        }
                                                                                        dt.Rows.Add(cpytab4);

                                                                                        //parcours d'une autre ligne[cpytab2]
                                                                                        string[] cpytab5 = new string[cpytab2.Length];
                                                                                        for (int f = 0; f < cpytab2.Length; f++)
                                                                                        {
                                                                                            if (f == 0)
                                                                                            {
                                                                                                cpytab5[0] = cpytab2[f + 1];
                                                                                                if (!cpytab5[0].Trim().Equals("-") && !cpytab5[0].Trim().Equals(""))
                                                                                                {
                                                                                                    if (!usingHoteByDetermin.Contains(cpytab5[0].Trim()) || !usingHoteByDetermin.Contains(renverseChar1(cpytab5[0].Trim())))
                                                                                                    {
                                                                                                        //ajoute de l'hôte
                                                                                                        usingHoteByDetermin.Add(cpytab5[0].Trim());

                                                                                                        for (int k = 1; k < dgv.Columns.Count; k++)
                                                                                                        {
                                                                                                            cpytab5[k] = seachVoisinByHote(cpytab5[0], dgv.Columns[k].Name.ToString());
                                                                                                        }
                                                                                                        dt.Rows.Add(cpytab5);
                                                                                                    }
                                                                                                }
                                                                                            }
                                                                                            else
                                                                                            {
                                                                                                string[] cpytab6 = new string[cpytab2.Length];
                                                                                                if (f < cpytab2.Length - 1)
                                                                                                {
                                                                                                    cpytab6[0] = cpytab2[f + 1];
                                                                                                    if (!cpytab6[0].Trim().Equals("-") && !cpytab6[0].Trim().Equals(""))
                                                                                                    {
                                                                                                        if (!usingHoteByDetermin.Contains(cpytab6[0].Trim()) || !usingHoteByDetermin.Contains(renverseChar1(cpytab6[0].Trim())))
                                                                                                        {
                                                                                                            //Ajout de l'hôte
                                                                                                            usingHoteByDetermin.Add(cpytab6[0].Trim());

                                                                                                            for (int k = 1; k < dgv.Columns.Count; k++)
                                                                                                            {
                                                                                                                cpytab6[k] = seachVoisinByHote(cpytab6[0], dgv.Columns[k].Name.ToString());
                                                                                                            }
                                                                                                            dt.Rows.Add(cpytab6);

                                                                                                            //Parcours de la dernière ligne[cpytab3] par rapport à notre base exemple
                                                                                                            string[] cpytab7 = new string[cpytab3.Length];
                                                                                                            //
                                                                                                            for (int g = 0; g < cpytab3.Length; g++)
                                                                                                            {
                                                                                                                if (g == 0)
                                                                                                                {
                                                                                                                    cpytab7[0] = cpytab3[g + 1];
                                                                                                                    if (!cpytab7[0].Trim().Equals("-") && !cpytab7[0].Trim().Equals(""))
                                                                                                                    {
                                                                                                                        if (!usingHoteByDetermin.Contains(cpytab7[0].Trim()) || !usingHoteByDetermin.Contains(renverseChar1(cpytab7[0].Trim())))
                                                                                                                        {
                                                                                                                            //ajoute de l'hôte
                                                                                                                            usingHoteByDetermin.Add(cpytab7[0].Trim());

                                                                                                                            for (int k = 1; k < dgv.Columns.Count; k++)
                                                                                                                            {
                                                                                                                                cpytab7[k] = seachVoisinByHote(cpytab7[0], dgv.Columns[k].Name.ToString());
                                                                                                                            }
                                                                                                                            dt.Rows.Add(cpytab7);
                                                                                                                        }
                                                                                                                    }
                                                                                                                }
                                                                                                                else
                                                                                                                {
                                                                                                                    /* je vais y revenir plutard
                                                                                                                    string[] cpytab8 = new string[cpytab3.Length];
                                                                                                                    if (g < cpytab3.Length - 1)
                                                                                                                    {
                                                                                                                        cpytab8[0] = cpytab3[g + 1];
                                                                                                                        if (!cpytab8[0].Trim().Equals("-") && !cpytab8[0].Trim().Equals(""))
                                                                                                                        {
                                                                                                                            if (!usingHoteByDetermin.Contains(cpytab8[0].Trim()) || !usingHoteByDetermin.Contains(renverseChar(cpytab8[0].Trim())))
                                                                                                                            {
                                                                                                                                //Ajout de l'hôte
                                                                                                                                usingHoteByDetermin.Add(cpytab8[0].Trim());
                                                                                                                                for (int k = 1; k < dgv.Columns.Count; k++)
                                                                                                                                {
                                                                                                                                    cpytab8[k] = seachVoisinByHote(cpytab8[0], dgv.Columns[k].Name.ToString());
                                                                                                                                }
                                                                                                                                dt.Rows.Add(cpytab8);
                                                                                                                            }
                                                                                                                        }
                                                                                                                    }*/
                                                                                                                }
                                                                                                            }
                                                                                                            //end
                                                                                                        }
                                                                                                    }
                                                                                                }
                                                                                            }
                                                                                        }

                                                                                    }
                                                                                }
                                                                            }
                                                                        }
                                                                    }

                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        //par rapport à l'exemple de base, la raison pour laquelle j'ai laissé un vide ici, mais toute fois nous allons y revenir
                                    }
                                }
                            }
                        }
                    }
                }
                //affiche la table de déterminisation
                dgv.DataSource = dt;
            }
            catch (Exception)
            {
            }

        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureDesign_MouseClick(object sender, MouseEventArgs e)
        {

            bool creer = false;
            x = e.Location.X;
            y = e.Location.Y;

            if (addCircle)
            {
                TextCircle cr = new TextCircle();
                cr.Location = new Point(x, y);
                if (cr.ShowDialog() == DialogResult.OK)
                {
                    if (addInit == false && addFin == false)
                    {
                        ds.color_circle = Color.White;
                    }
                    ds.drawCircle(new Point(x, y), cr.txtAddHote.Text);
                    if (addInit)
                    {
                        if (!listHoteInit.Contains(cr.txtAddHote.Text))
                        {
                            listHoteInit.Add(cr.txtAddHote.Text);
                        }
                    }
                    if (addFin)
                    {
                        if (!listHoteFin.Contains(cr.txtAddHote.Text))
                        {
                            listHoteFin.Add(cr.txtAddHote.Text);
                        }
                    }
                    pts.Add(new Position { depart = new Point(x, y), arrive = new Point(x + ds.ray, y + ds.ray), valeur = cr.txtAddHote.Text.ToString() });

                    if (!listeGen.Contains(cr.txtAddHote.Text))
                    {
                        listeGen.Add(cr.txtAddHote.Text);
                    }
                }
            }
            else if (addArc)
            {
                bool accepter = false;
                foreach (var s in pts)
                {
                    if ((x >= s.depart.X && x <= s.arrive.X) && (y >= s.depart.Y && y <= s.arrive.Y))
                    {
                        accepter = true;
                        if (add == true)
                        {
                            ptA = new Point(s.arrive.X, s.arrive.Y);
                            add = false;
                            s1 = s.valeur;
                            break;
                        }
                        else
                        {
                            ptB = new Point(s.depart.X, s.depart.Y);
                            s2 = s.valeur;
                            TextCircle cr = new TextCircle();
                            cr.Location = new Point(x, y);
                            //int pond = 0;
                            string paquet = "£";
                            string paquetVirgule = "";
                            string[] split ;
                            if (cr.ShowDialog() == DialogResult.OK)
                            {
                                //int.TryParse(cr.txtAddHote.Text, out pond);
                                paquet = cr.txtAddHote.Text;
                                paquetVirgule = paquet;
                                
                            }

                            int verify = verifyPaquetVirgule(paquetVirgule);

                            if (verify > 0)
                            {
                                split = paquetVirgule.Split(new char[] { ',' });
                                
                                for (int i = 0; i < split.Length; i++)
                                {
                                    paquetVirgule = split[i];
                                    liens.Add(new Link { sommet1 = s1, sommet2 = s2, paquet = paquetVirgule });
                                    if (!listPaquet.Contains(paquetVirgule))
                                    {
                                        listPaquet.Add(paquetVirgule);
                                    }
                                }
                            }
                            else
                            {
                                liens.Add(new Link { sommet1 = s1, sommet2 = s2, paquet = paquetVirgule });
                                if (!listPaquet.Contains(paquetVirgule))
                                {
                                    listPaquet.Add(paquetVirgule);
                                }
                            
                            }

                            if (isBefore(ptA, ptB))
                            {
                                Point P1 = new Point(ptA.X + ds.ray / 2 - 2, ptA.Y);
                                Point P2 = new Point(ptB.X, ptB.Y);
                                ds.createLine(P1, P2);
                                int Xm = (P1.X + P2.X) / 2;
                                int Ym = (P1.Y + P2.Y) / 2;
                                ds.writeText(new Point(Xm, Ym + 10), paquet.ToString());
                                //ds.writeText(new Point(Xm, Ym-5), "<");
                                //ds.createTriangle(new Point(Xm, Ym), new Point(Xm+10, Ym-4), new Point(Xm-10, Ym+4));
                            }
                            else
                            {
                                Point P1 = new Point(ptB.X, ptB.Y);
                                Point P2 = new Point(ptA.X, ptA.Y);
                                ds.createLine(P1, P2);
                                int Xm = (P1.X + P2.X) / 2;
                                int Ym = (P1.Y + P2.Y) / 2;
                                ds.writeText(new Point(Xm, Ym +10), paquet.ToString());
                                //ds.writeText(new Point(Xm+10, Ym-5), ">");
                                //ds.createTriangle(new Point(Xm, Ym), new Point(Xm + 10, Ym - 4), new Point(Xm + 10, Ym + 4));
                            }
                            ptA = new Point();
                            add = true;
                            break;
                        }
                    }
                }
            }
           
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

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void picInitial_Click(object sender, EventArgs e)
        {
           DialogResult rp = MessageBox.Show("Voulez-vous définir un Hôte comme initial?","Personalisez un Hôte",MessageBoxButtons.YesNo, MessageBoxIcon.Question);
           if (rp == DialogResult.Yes)
           {
               addInit = true;
               addFin = false;

               addArc = false;
               addCircle = false;
               ds.color_circle = Color.Red;

           }
           else
           {
               addInit = false;
               addFin = false;
               ds.color_circle = Color.White;
           }
        }

        private void picFinal_Click(object sender, EventArgs e)
        {
            DialogResult rp = MessageBox.Show("Voulez-vous définir un Hôte comme Fin?", "Personalisez un Hôte", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (rp == DialogResult.Yes)
            {
                addInit = false;
                addFin = true;

                addArc = false;
                addCircle = false;
                ds.color_circle = Color.Blue;
            }
            else
            {
                addInit = false;
                addFin = false;
                ds.color_circle = Color.White;
            }
        }

        private void BtnHote_MouseHover(object sender, EventArgs e)
        {
           
        }
    }
}
