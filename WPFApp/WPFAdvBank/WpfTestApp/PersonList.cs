using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfTestApp
{
    class PersonList : ObservableCollection<Person>
    {
        public PersonList()
        {
            this.Add(new Person("Willa", "Cather"));
            this.Add(new Person("Isak", "Diena"));
            this.Add(new Person("Victor", "Hugo"));
        }
    }
}
