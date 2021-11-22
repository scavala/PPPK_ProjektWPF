using System.Collections.ObjectModel;
using System.Linq;
using Zadatak.Dal;
using Zadatak.Models;

namespace Zadatak.ViewModels
{
    public class PersonViewModel
    {
        public ObservableCollection<Person> People { get; }
        public PersonViewModel()
        {
            People = new ObservableCollection<Person>(RepositoryFactory.GetRepository<Person>().GetAll());
            People.CollectionChanged += People_CollectionChanged;
        }

        private void People_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                    RepositoryFactory.GetRepository<Person>().Add(People[e.NewStartingIndex]);
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
                    RepositoryFactory.GetRepository<Person>().Delete(e.OldItems.OfType<Person>().ToList()[0]);
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Replace:
                    RepositoryFactory.GetRepository<Person>().Update(e.NewItems.OfType<Person>().ToList()[0]);
                    break;
            }
        }

        internal void Update(Person person) => People[People.IndexOf(person)] = person;
    }
}
