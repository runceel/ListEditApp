using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListEditApp.Models
{
    class PeopleManager
    {
        public static PeopleManager Current { get; } = new PeopleManager();

        private ObservableCollection<Person> PeopleSource { get; } = new ObservableCollection<Person>();

        public ReadOnlyObservableCollection<Person> People { get; }

        public PeopleManager()
        {
            var source = Enumerable.Range(1, 100).Select(x => new Person { Name = $"tanaka {x}" });
            foreach (var item in source)
            {
                this.PeopleSource.Add(item);
            }

            this.People = new ReadOnlyObservableCollection<Person>(this.PeopleSource);
        }
    }
}
