using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace cukraszdaForm
{
  public partial class Form1 : Form
  {
    List<sutemenyek> sutemenyek = new List<sutemenyek>();
    public Form1()
    {
      InitializeComponent();
    }

    private void Form1_Shown(object sender, EventArgs e)
    {
      Beolvasas();
      RandomKivalasztas();
      LegdragabbEsLegolcsobb();
      DijNyertesSutik();
      SutikKiirasaTxtbe();
      SutiTipusokSzamolasa();
    }


    private void SutiTipusokSzamolasa()
    {
      Dictionary<string, int> dic = new Dictionary<string, int>();
      StreamWriter ki = new StreamWriter("stat.txt");
      foreach (var s in sutemenyek)
      {
        if (!dic.ContainsKey(s.Tipus))
        {
          dic.Add(s.Tipus,1);
        }
        else
        {
          dic[s.Tipus]++;
        }
      }
      foreach (var d in dic)
      {
        ki.WriteLine(d.Key+";"+d.Value);
      }
      ki.Close();
    }

    private void SutikKiirasaTxtbe()
    {
      StreamWriter ki = new StreamWriter("lista.txt");
      List<string> lista = new List<string>();
      foreach (var s in sutemenyek)
      {
        if (!lista.Contains(s.Nev))
        {
          ki.WriteLine($"{s.Nev} {s.Tipus}");
          lista.Add(s.Nev);
        }
        
      }
      ki.Close();
    }

    private void DijNyertesSutik()
    {
      int szamlalo = 0;
      foreach (var s in sutemenyek)
      {
        if (s.Dij)
        {
          szamlalo++;
        }
      }
      txtboxDijnyertesSuti.Text = $"{szamlalo} féle díjnyertes édességből választhat!";
    }

    private void LegdragabbEsLegolcsobb()
    {
      string legdragabb = "";
      string legolcsobb = "";
      int dragaar = sutemenyek[0].Ar;
      int olcsoar = sutemenyek[0].Ar;
      string dragaegyseg = "";
      string olcsoegyseg = "";
      foreach (var s in sutemenyek)
      {
        if (s.Ar>dragaar)
        {
          legdragabb = s.Nev;
          dragaar = s.Ar;
          dragaegyseg = s.Egyseg;
        }
        else if (s.Ar<olcsoar)
        {
          legolcsobb = s.Nev;
          olcsoar = s.Ar;
          olcsoegyseg = s.Egyseg;
        }
      }
      txtboxLegdragabbsuti.Text = legdragabb;
      txtboxLegdragabbsutiAra.Text = Convert.ToString(dragaar)+ " Ft"+"/"+dragaegyseg;
      txtboxLegolcsobbsuti.Text = legolcsobb;
      txtboxLegolcsobbsutiAra.Text= Convert.ToString(olcsoar)+ " Ft" + "/" + olcsoegyseg;

    }

    private void RandomKivalasztas()
    {
      Random random = new Random();
      txtboxRndSuti.Text = $"Mai ajánlatunk:{sutemenyek[random.Next(1, sutemenyek.Count)].Nev}";
    }

    private void Beolvasas()
    {
      StreamReader be = new StreamReader("cuki.txt");
      while (!be.EndOfStream)
      {
        string[] a = be.ReadLine().Split(';');
        sutemenyek.Add(new sutemenyek(a[0],a[1],Convert.ToBoolean(a[2]),Convert.ToInt32(a[3]),a[4]));
      }
      be.Close();
    }

    private void button1_Click(object sender, EventArgs e)
    {
      if (Ellenoriz(txtBoxSutiTipusa.Text))
      {
        bool volt = false;
        int sz = 0;
        int ar = 0;
        StreamWriter ki = new StreamWriter("ajanlat.txt");
        foreach (var s in sutemenyek)
        {
          if (s.Tipus.ToLower()==txtBoxSutiTipusa.Text.ToLower())
          {
            ki.WriteLine(s.Nev+" - "+s.Ar+" Ft "+" - "+s.Egyseg);
            volt = true;
            sz++;
            ar += s.Ar;
          }
        }
        if (volt)
        {
          MessageBox.Show($"{sz} db sütit írtam a file-ba.\n Ezek átlagára:{ar/sz}");
          txtBoxSutiTipusa.Clear();
        }
        else
        {
          MessageBox.Show("Nincs megfelelő sütink. Kérjük válasszon mást!");
          txtBoxSutiTipusa.Clear();
        }
        ki.Close();
      }
    }

    private bool Ellenoriz(string text)
    {
      int valami = 0;
      if (string.IsNullOrEmpty(text))
      {
        MessageBox.Show("Nem adtál meg adatot!");
        return false;
      }
      if (int.TryParse(text,out valami))
      {
        MessageBox.Show("Rossz a bementi szöveg típusa!");
        return false;
      }
      else
      {
        return true;
      }
    }

    private void button2_Click(object sender, EventArgs e)
    {
      if (Ellenoriz(txtBoxUjSutiNeve.Text) && Ellenoriz(txtBoxUjSutiTipusa.Text) && Ellenoriz(txtBoxUjSutiEgysege.Text) && EllenorizSzam(txtBoxUjSutiAra.Text))
      {
        StreamWriter ki = File.AppendText("cuki.txt");
        if (cbDíj.Checked)
        {
          ki.WriteLine(txtBoxUjSutiNeve.Text + ";" + txtBoxUjSutiTipusa.Text + ";" + "true" + ";" + txtBoxUjSutiAra.Text + ";" + txtBoxUjSutiEgysege.Text);
        }
        else
        {
          ki.WriteLine(txtBoxUjSutiNeve.Text + ";" + txtBoxUjSutiTipusa.Text+";" +"false" + ";" + txtBoxUjSutiAra.Text + ";" + txtBoxUjSutiEgysege.Text);
        }
        ki.Close();
        MessageBox.Show("Az adatok mentése sikeres volt!");
        txtBoxUjSutiNeve.Clear();
        txtBoxUjSutiAra.Clear();
        txtBoxUjSutiEgysege.Clear();
        txtBoxUjSutiTipusa.Clear();
      }

    }
    private bool EllenorizSzam(string text)
    {
      int valami;
      if (int.TryParse(text,out valami))
      {
        return true;
      }
      else
      {
        MessageBox.Show("Az árhoz nem számot adtál meg!");
        return false;
      }
    }
  }
}
