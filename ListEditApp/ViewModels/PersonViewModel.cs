using ListEditApp.Models;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System;
using System.ComponentModel.DataAnnotations;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace ListEditApp.ViewModels
{
    class PersonViewModel : IDisposable
    {
        private CompositeDisposable Disposable { get; } = new CompositeDisposable();

        public Person Model { get; }

        [Required]
        public ReactiveProperty<string> Name { get; }

        private ReactiveProperty<bool> hasErrors;
        public ReactiveProperty<bool> HasErrors => this.hasErrors ?? (this.hasErrors = this.Name.ObserveHasErrors.ToReactiveProperty());

        private Subject<Unit> CommitTrigger { get; } = new Subject<Unit>();

        private IObservable<Unit> CommitAsObservable => this.CommitTrigger
            .Select(_ => this.HasErrors.Value)
            .Where(x => !x)
            .ToUnit();

        public PersonViewModel(Person model)
        {
            this.Model = model;
            this.Name = this.Model
                .ObserveProperty(x => x.Name)
                .ToReactiveProperty()
                .SetValidateAttribute(() => this.Name)
                .AddTo(this.Disposable);
            this.CommitAsObservable
                .Select(_ => this.Name.Value)
                .Subscribe(x => this.Model.Name = x)
                .AddTo(this.Disposable);
        }

        public void Commit() => this.CommitTrigger.OnNext(Unit.Default);

        public void Dispose()
        {
            this.Disposable.Dispose();
        }
    }
}