using System;
using System.Collections.Generic;
using System.Linq;

namespace Spike.Repositories.GenericDecorator.Contexts.ContextA
{
    public interface IRelInContextAFactory<TIn>
        where TIn : Relationship
    {
        RelInContextA<TIn> Create(TIn relInput, RelInContextA contextInfo);
    }

    public class ContextAPopulator<TRelInput> : ContextPopulator<TRelInput, RelInContextA<TRelInput>>
        where TRelInput : Relationship
    {
        private readonly IRelInContextAFactory<TRelInput> factory;

        public ContextAPopulator(IRelInContextAFactory<TRelInput> factory)
        {
            this.factory = factory;
        }

        public override IEnumerable<RelInContextA<TRelInput>> Populate(IEnumerable<TRelInput> source)
        {
            return source.Select(x => this.factory.Create(x, new RelInContextAImp(x) { ModelledExtA = "in Context A", PlannedExtA = "in Context A" }));
        }
    }

}
