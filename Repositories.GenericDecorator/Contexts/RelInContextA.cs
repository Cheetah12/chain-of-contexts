using System;
using System.Linq;

namespace Spike.Repositories.GenericDecorator.Contexts
{
    public interface RelInContextA : Relationship
    {
        string ModelledExtA { get; }
        string PlannedExtA { get; }
    }

    public interface RelInContextA<out TRel> : RelInContextA, Relationship<TRel>
        where TRel : Relationship
    {
    }

}
