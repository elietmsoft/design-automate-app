using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using memory_automate.Model;
using memory_automate.Utils;
using memory_automate.Controller;

namespace memory_automate.View
{
    public partial class ViewHote : UserControl
    {
        public ViewHote()
        {
            InitializeComponent();
        }

        int x = 0;
        int y = 0;

        public static bool add = true;
        public static bool addCircle = false;
        public static bool addArc = false;

        public static bool addInit = false;
        public static bool addFin = false;

        public Point ptA = new Point();
        public Point ptB = new Point();
        public List<Link> liens = new List<Link>();

        public List<string> listPaquet = new List<string>();

        public List<Position> pts = new List<Position>();
        string s1 = "";
        string s2 = "";

        //Liste des sommets
        public List<object> listeGen = new List<object>();

        private bool isBefore(Point Pa, Point Pb)
        {
            if (Pa.X - Pb.X > 0)
            {
                return true;
            }
            else
                return false;
        }
        public Dessin ds;

        public DataTable dataTable(List<string> listPaquet)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Hôte");
            foreach (var item in listPaquet)
            {
                DataColumn dc = new DataColumn("Paquet " + item);
                dt.Columns.Add(dc);
            }
            return dt;
        }
        public void viewTableTransition(DataTable dt, DataGridView dgv)
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

            for (int i = 0; i < L; i++)
            {
                for (int j = 1; j < C; j++)
                {
                    string ColumnName = dgv.Columns[j].Name.ToString();
                    string voisinHote = "";
                    foreach (var p in liens)
                    {
                        if (p.sommet1.Equals(copyListGen[i].ToString()))
                        {
                            if (ColumnName.Equals("Paquet " + p.paquet))
                            {

                                voisinHote += p.sommet2;
                                voisinHote += ", ";
                            }
                        }
                    }
                    dgv.Rows[i].Cells[j].Value = voisinHote;
                }
                dgv.Rows[i].Cells[0].Value = copyListGen[i].ToString();
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

        public void pictureDesign_MouseClick(object sender, MouseEventArgs e)
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
                            string[] split;
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
                                //ds.createTriangle(new Point(Xm, Ym), new Point(Xm+10, Ym-4), new Point(Xm-10, Ym+4));
                            }
                            else
                            {
                                Point P1 = new Point(ptB.X, ptB.Y);
                                Point P2 = new Point(ptA.X, ptA.Y);
                                ds.createLine(P1, P2);
                                int Xm = (P1.X + P2.X) / 2;
                                int Ym = (P1.Y + P2.Y) / 2;
                                ds.writeText(new Point(Xm, Ym + 10), paquet.ToString());
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
    }
}
