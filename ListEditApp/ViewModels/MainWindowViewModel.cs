using ListEditApp.Models;
using Prism.Interactivity.InteractionRequest;
using Reactive.Bindings;
using System;
using System.Linq;
using System.Reactive.Linq;

namespace ListEditApp.ViewModels
{
    class MainWindowViewModel
    {
        private PeopleManager Model { get; } = PeopleManager.Current;

        public ReadOnlyReactiveCollection<PersonViewModel> People { get; }

        public ReactiveProperty<PersonViewModel> SelectedPerson { get; } = new ReactiveProperty<PersonViewModel>();

        public InteractionRequest<Notification> EditRequest { get; } = new InteractionRequest<Notification>();

        public ReactiveCommand EditCommand { get; }

        public MainWindowViewModel()
        {
            this.People = this.Model
                .People
                .ToReadOnlyReactiveCollection(x => new PersonViewModel(x));

            this.EditCommand = this.SelectedPerson
                .Select(x => x != null)
                .ToReactiveCommand();
            this.EditCommand
                .Select(_ => this.SelectedPerson.Value.Model)
                .Subscribe(x => this.EditRequest.Raise(new Notification
                {
                    Title = "Edit",
                    Content = x,
                }));
        }
    }
}
