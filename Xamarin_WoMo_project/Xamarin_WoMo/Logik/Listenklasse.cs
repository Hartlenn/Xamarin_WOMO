using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WoMo.Logik
{
    class Listenklasse : IListeneintrag
    {
        private List<IListeneintrag> liste;
        private IListeneintrag cursor;
        private Type akzeptiert;

        private string text;
        private int id;

        public string Text
        {
            get
            {
                return this.text;
            }

            set
            {
                this.text = value;
            }
        }

        public int Id
        {
            get
            {
                return this.id;
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public Listenklasse(string text, int id)
        {

        }
            
        public void sortiere()
        {
            throw new NotImplementedException();
        }

        public void xmlExport()
        {
            throw new NotImplementedException();
        }
    }
}
