using System.Collections.Generic;
using System.Linq;

namespace Spike.Repositories.GenericDecorator
{
    public interface IContextPopulator<out TOutput>
        where TOutput : Relationship
    {
        IEnumerable<TOutput> Populate(IEnumerable<Relationship> source);
    }

    /// <summary>
    /// The base class for all context populators. Provides the mechanism for preserving the type information while chaining the contexts.
    /// </summary>
    public abstract class ContextPopulator<TIn, TOut> : IContextPopulator<TOut>
        where TIn : Relationship
        where TOut : Relationship
    {
        private IContextPopulator<TIn> previous;

        public IEnumerable<TOut> Populate(IEnumerable<Relationship> source)
        {
            if (this.previous != null)
            {
                return this.Populate(this.previous.Populate(source));
            }

            return this.Populate(source.OfType<TIn>());
        }

        public abstract IEnumerable<TOut> Populate(IEnumerable<TIn> source);

        public IContextPopulator<TNextOut> Then<TNextOut>(ContextPopulator<TOut, TNextOut> next)
            where TNextOut : Relationship
        {
            next.previous = this;
            return next;
        }
    }
}