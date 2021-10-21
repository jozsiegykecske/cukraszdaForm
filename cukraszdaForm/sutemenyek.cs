using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cukraszdaForm
{
  class sutemenyek
  {
    private string nev;

    public string Nev
    {
      get { return nev; }
      set { nev = value; }
    }
    private string tipus;

    public string Tipus
    {
      get { return tipus; }
      set { tipus = value; }
    }
    private bool dij;

    public bool Dij
    {
      get { return dij; }
      set { dij = value; }
    }
    private int ar;

    public int Ar
    {
      get { return ar; }
      set { ar = value; }
    }
    private string egyseg;

    public string Egyseg
    {
      get { return egyseg; }
      set { egyseg = value; }
    }
    public sutemenyek(string nev, string tipus, string egyseg, bool dij = false, int ar = 100)
    {
      this.nev = nev;
      this.tipus = tipus;
      this.egyseg = egyseg;
      this.dij = dij;
      this.ar = ar;
    }
    public sutemenyek(string nev, string tipus, bool dij, int ar, string egyseg)
    {
      this.nev = nev;
      this.tipus = tipus;
      this.dij = dij;
      this.ar = ar;
      this.egyseg = egyseg;
    }
    public override string ToString()
    {
      return $"{nev};{tipus};{ar};{egyseg},{dij}";
    }
  }
}
