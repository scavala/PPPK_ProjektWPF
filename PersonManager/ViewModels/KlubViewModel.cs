using System.Collections.ObjectModel;
using System.Linq;
using Zadatak.Dal;
using Zadatak.Models;


namespace Zadatak.ViewModels
{
    public class KlubViewModel
    {
        private Klub selectedKlub;

        public ObservableCollection<Klub> Klubs { get; }
        public Klub SelectedKlub
        {
            get => selectedKlub;
            set
            {
                selectedKlub = value ?? Klubs.FirstOrDefault();

            }
        }

        public KlubViewModel()
        {
            Klubs = new ObservableCollection<Klub>(RepositoryFactory.GetRepository<Klub>().GetAll());
            Klubs.CollectionChanged += Klubs_CollectionChanged;

        }

        private void Klubs_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                    RepositoryFactory.GetRepository<Klub>().Add(Klubs[e.NewStartingIndex]);
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
                    RepositoryFactory.GetRepository<Klub>().Delete(e.OldItems.OfType<Klub>().ToList()[0]);
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Replace:
                    RepositoryFactory.GetRepository<Klub>().Update(e.NewItems.OfType<Klub>().ToList()[0]);
                    break;
            }
        }

        internal void Update(Klub klub) => Klubs[Klubs.IndexOf(klub)] = klub;
    }
}
