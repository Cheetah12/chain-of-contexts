using System;
using System.Linq;

namespace Spike.Repositories.GenericDecorator.Contexts
{
    public interface RelInContextB : Relationship
    {
        string ModelledExtB { get; }
        string PlannedExtB { get; }
    }

    public interface RelInContextB<out TRel> : RelInContextB, Relationship<TRel>
        where TRel : Relationship
    {
    }
}
