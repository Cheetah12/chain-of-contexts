using System;
using System.Collections.Generic;
using System.Linq;

namespace Spike.Repositories.GenericDecorator.Contexts.ContextB
{
    public class ContextBPopulator<TRelInput> : ContextPopulator<TRelInput, RelInContextB<TRelInput>>
        where TRelInput : Relationship
    {
        private readonly IRelInContextBFactory<TRelInput> factory;

        public ContextBPopulator(IRelInContextBFactory<TRelInput> factory)
        {
            this.factory = factory;
        }

        public override IEnumerable<RelInContextB<TRelInput>> Populate(IEnumerable<TRelInput> source)
        {
            return source.Select(x => this.factory.Create(x, new RelInContextBImp(x) { ModelledExtB = "in Context B", PlannedExtB = "in Context B" }));
        }
    }

    public interface IRelInContextBFactory<TIn>
        where TIn : Relationship
    {
        RelInContextB<TIn> Create(TIn relInput, RelInContextB contextInfo);
    }
}
