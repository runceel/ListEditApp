using ListEditApp.Models;
using Prism.Interactivity.InteractionRequest;
using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListEditApp.ViewModels
{
    class PersonEditViewModel : IInteractionRequestAware
    {
        public Action FinishInteraction { get; set; }

        private INotification notification;
        public INotification Notification
        {
            get
            {
                return this.notification;
            }

            set
            {
                this.notification = value;
                this.EditTarget.Value?.Dispose();
                this.EditTarget.Value = new PersonViewModel(value.Content as Person);
            }
        }

        public ReactiveProperty<PersonViewModel> EditTarget { get; } = new ReactiveProperty<PersonViewModel>();

        public ReactiveCommand CommitCommand { get; }

        public PersonEditViewModel()
        {
            this.CommitCommand = this.EditTarget
                .Where(x => x != null)
                .SelectMany(x => x.HasErrors)
                .Select(x => !x)
                .ToReactiveCommand(false);
            this.CommitCommand.Subscribe(_ =>
            {
                this.EditTarget.Value.Commit();
                this.FinishInteraction();
            });
        }
    }
}
